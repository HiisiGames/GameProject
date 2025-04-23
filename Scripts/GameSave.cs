using Godot;
using Godot.Collections;
using System;
using System.IO;

using Dictionary = Godot.Collections.Dictionary;

namespace TruckGame
{
	/// <summary>
	/// This file makes sure that all the data for the user (time and audio) are saved
	/// </summary>
	public partial class GameSave : Node2D
	{
		public static GameSave Instantiate;
		public override void _Ready()
		{
			Instantiate = this;
		}

		/// <summary>
		/// Loads up the saved data from "autoSave.json"
		/// </summary>
		public void Load()
		{
			string savePath = ProjectSettings.GlobalizePath("user://");
			savePath = Path.Combine(savePath, Config.SaveFolder);

			string loadedData = LoadFromFile(savePath, Config.AutoSaveFile);

			Json jsonLoader = new Json();
			Error loadError = jsonLoader.Parse(loadedData);
			if (loadError != Error.Ok)
			{
				GD.PrintErr($"Virhe ladattaessa peliä: {loadError}");
				return;
			}

			Dictionary saveData = (Dictionary)jsonLoader.Data;

			GD.Print(saveData);

			if (LevelCompleteTime.Instantiate != null)
			{
				if (saveData.ContainsKey("TimeData"))
				{
					Dictionary timeData = (Dictionary)saveData["TimeData"];

					LevelCompleteTime.Instantiate._currentLevel1Time = (float)timeData["BestTimeLevel1"];
					LevelCompleteTime.Instantiate._currentLevel2Time = (float)timeData["BestTimeLevel2"];
					LevelCompleteTime.Instantiate._currentLevel3Time = (float)timeData["BestTimeLevel3"];

					GD.Print("Fastest time recovered, GameSave.cs");
				}
			}

			if (LevelSelect.Instantiate != null)
			{
				if (saveData.ContainsKey("TimeData"))
				{
					Dictionary timeData = (Dictionary)saveData["TimeData"];

					LevelSelect.Instantiate._level1Time = (float)timeData["BestTimeLevel1"];
					LevelSelect.Instantiate._level2Time = (float)timeData["BestTimeLevel2"];
					LevelSelect.Instantiate._level3Time = (float)timeData["BestTimeLevel3"];

					GD.Print("Labels updated, GameSave.cs");
				}
			}

			GD.Print("Load works, GameSave");
		}

		/// <summary>
		/// Loads up the saved data from "autoSave.json"
		/// </summary>
		public void LoadAudio()
		{
			string savePath = ProjectSettings.GlobalizePath("user://");
			savePath = Path.Combine(savePath, Config.SaveFolder);

			string loadedData = LoadFromFile(savePath, Config.AudioSaveFile);

			Json jsonLoader = new Json();
			Error loadError = jsonLoader.Parse(loadedData);
			if (loadError != Error.Ok)
			{
				GD.PrintErr($"Virhe ladattaessa peliä: {loadError}");
				return;
			}

			Dictionary saveData = (Dictionary)jsonLoader.Data;

			GD.Print(saveData);

			if (AudioManager.Instantiate != null)
			{
				if (saveData.ContainsKey("SFX"))
				{
					Dictionary sfxData = (Dictionary)saveData["SFX"];

					AudioManager.Instantiate.collideSound.VolumeDb = (float)sfxData["CollideSound"];
					AudioManager.Instantiate.engineSound.VolumeDb = (float)sfxData["EngineSound"];
					AudioManager.Instantiate.victorySound.VolumeDb = (float)sfxData["VictorySound"];
					AudioManager.Instantiate.clickButtonSound.VolumeDb = (float)sfxData["ClickSound"];

					GD.Print("SFX volumes restored");
				}
			}

			if (AudioManager.Instantiate != null)
			{
				if (saveData.ContainsKey("Music"))
				{
					Dictionary musicData = (Dictionary)saveData["Music"];

					AudioManager.Instantiate.bgMusic.VolumeDb = (float)musicData["bgMusic"];

					GD.Print("music volumes restored");
				}
			}

			GD.Print("Load works, GameSave");
		}

		/// <summary>
		/// Saves the data to folder "Save" and saves "autoSave.json" to there
		/// </summary>
		/// <returns>TimeData as savePath</returns>
		public string Save()
		{
			Dictionary saveData = new Dictionary(); // Creates a dictionary called saveData

			if (LevelCompleteTime.Instantiate != null) // checks that its not null
			{
				saveData.Add("TimeData", LevelCompleteTime.Instantiate.TimeData()); // Adds the data in TimeData method
			}

			string json = Json.Stringify(saveData);
			GD.Print(json);

			string savePath = ProjectSettings.GlobalizePath("user://");
			savePath = Path.Combine(savePath, Config.SaveFolder);

			if (SaveToFile(savePath, Config.AutoSaveFile, json))
			{
				GD.Print("Game saved, GameSave.cs");
			}
			else
			{
				GD.Print("Game did not save, GameSave.cs");
			}
			return savePath;
		}

		/// <summary>
		///  Saves the data to folder "Save" and saves "audioSave.json" to there
		/// </summary>
		/// <returns>AudioData as savePath</returns>
		public string SaveAudio()
		{
			Dictionary saveData = new Dictionary(); // creates new dictionary

			if (AudioManager.Instantiate != null)
			{
				saveData.Add("SFX", AudioManager.Instantiate.SFXData());
				saveData.Add("Music", AudioManager.Instantiate.MusicData());
			}

			string json = Json.Stringify(saveData);
			GD.Print(json);

			string savePath = ProjectSettings.GlobalizePath("user://");
			savePath = Path.Combine(savePath, Config.SaveFolder);

			if (SaveToFile(savePath, Config.AudioSaveFile, json))
			{
				GD.Print("Audio saved, GameSave.cs");
			}
			else
			{
				GD.Print("Audio did not save, GameSave.cs");
			}
			return savePath;
		}

		/// <summary>
		/// Writes the provided saved data to a file
		/// </summary>
		/// <param name="path">The Directory where the file will be saved</param>
		/// <param name="fileName">The name of the file to save data to</param>
		/// <param name="json">the content of the file</param>
		/// <returns></returns>
		private bool SaveToFile(string path, string fileName, string json)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, fileName);
			GD.Print(path);

			try
			{
				File.WriteAllText(path, json);
			}
			catch (System.Exception e)
			{
				GD.Print(e);
				return false;
			}
			return true;
		}

		/// <summary>
		/// The data loaded from specified file as string.
		/// </summary>
		/// <param name="path">The directory path where the file is located.</param>
		/// <param name="fileName">The name of the file to load.</param>
		/// <returns></returns>
		private string LoadFromFile(string path, string fileName)
		{
			string data = null;

			path = Path.Combine(path, fileName);

			if (!File.Exists(path)) return null;

			try
			{
				data = File.ReadAllText(path);
			}
			catch (System.Exception e)
			{
				GD.Print(e);
			}
			return data;
		}

		// This was for making defautvalues for the game, but it seems to work without it.

		// public Dictionary CreateDefaultSaveValues()
		// {
		// 	Dictionary defaultSave = new Dictionary();

		// 	Dictionary sfxData = new Dictionary
		// 	{
		// 		{ "CollideSound", 0f },
		// 		{ "EngineSound", 0f },
		// 		{ "VictorySound", 0f },
		// 		{ "ClickSound", 0f }
		// 	};

		// 	defaultSave.Add("SFX", sfxData);


		// 	Dictionary musicData = new Dictionary
		// 	{
		// 		{ "bgMusic", 0f }
		// 	};

		// 	defaultSave.Add("Music", musicData);


		// 	Dictionary timeData = new Dictionary
		// 	{
		// 		{ "FastestTimeLevel1", 0f },
		// 		{ "FastestTimeLevel2", 0f },
		// 		{ "FastestTimeLevel3", 0f }
		// 	};

		// 	defaultSave.Add("TimeData", timeData);

		// 	return defaultSave;
		// }
		// public string CreateDefaultSave()
		// {
		// 	Dictionary defaultSave = CreateDefaultSaveValues();

		// 	string json = Json.Stringify(defaultSave);
		// 	GD.Print(json);

		// 	string savePath = ProjectSettings.GlobalizePath("user://");
		// 	savePath = Path.Combine(savePath, Config.SaveFolder);

		// 	if (SaveToFile(savePath, Config.AutoSaveFile, json))
		// 	{
		// 		GD.Print("Default save created, GameSave.cs");
		// 	}
		// 	else
		// 	{
		// 		GD.Print("Default save not created, GameSave.cs");
		// 	}
		// 	return savePath;
		// }
	}
}


