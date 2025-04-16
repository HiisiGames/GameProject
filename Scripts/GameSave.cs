using Godot;
using Godot.Collections;
using System;
using System.IO;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class GameSave : Node2D
	{
		private LevelComplete levelComplete;
		public static GameSave Instantiate;
		// [Export] public int[] CurrentLevelComplete { get; set; }
		// [Export] public int CurrentLevelOn { get; set; }
		// [Export] public float AudioVolume { get; set; }
		// [Export] public int FastestTime { get; set; }

		// [Export] public bool ReverseControls { get; set; }
		public override void _Ready()
		{
			Instantiate = this;
		}

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

			if (saveData.ContainsKey("LevelComplete") && LevelComplete.Instantiate != null)
			{
				Dictionary levelData = (Dictionary)saveData["LevelComplete"];

				LevelComplete.Instantiate._isLevelComplete1 = (bool)levelData["Level-1"];
				LevelComplete.Instantiate._isLevelComplete2 = (bool)levelData["Level-2"];
				LevelComplete.Instantiate._isLevelComplete3 = (bool)levelData["Level-3"];

				GD.Print("Level data loaded");
			}
			if (saveData.ContainsKey("SFX"))
			{
				Dictionary sfxData = (Dictionary)saveData["SFX"];

				AudioManager.Instantiate.collideSound.VolumeDb = (float)sfxData["CollideSound"];
				AudioManager.Instantiate.engineSound.VolumeDb = (float)sfxData["EngineSound"];
				AudioManager.Instantiate.victorySound.VolumeDb = (float)sfxData["VictorySound"];
				AudioManager.Instantiate.clickButtonSound.VolumeDb = (float)sfxData["ClickSound"];

				GD.Print("SFX volumes restored");
			}
			if (saveData.ContainsKey("Music"))
			{
				Dictionary musicData = (Dictionary)saveData["Music"];

				AudioManager.Instantiate.bgMusic.VolumeDb = (float)musicData["bgMusic"];

				GD.Print("music volumes restored");
			}
			if (saveData.ContainsKey("TimeData"))
			{
				Dictionary timeData = (Dictionary)saveData["TimeData"];

				LevelSelect.Instantiate._level1TimeLabel.Text = (string)timeData["FastestTimeLevel1"];
				LevelSelect.Instantiate._level2TimeLabel.Text = (string)timeData["FastestTimeLevel2"];
				LevelSelect.Instantiate._level3TimeLabel.Text = (string)timeData["FastestTimeLevel3"];

				GD.Print("Fastest time recovered");
			}


			GD.Print("Load works, GameSave");
		}

		public string Save()
		{
			Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary();
			saveData.Add("LevelComplete", LevelComplete.Instantiate.LevelData());
			saveData.Add("SFX", AudioManager.Instantiate.SFXData());
			saveData.Add("Music", AudioManager.Instantiate.MusicData());
			saveData.Add("TimeData",LevelSelect.Instantiate.TimeData());

			string json = Json.Stringify(saveData);
			GD.Print(json);

			string savePath = ProjectSettings.GlobalizePath("user://");
			savePath = Path.Combine(savePath, Config.SaveFolder);

			if (SaveToFile(savePath, Config.AutoSaveFile, json))
			{
				GD.Print("Peli tallentui.");
			}
			else
			{
				GD.Print("Peli ei tallentunut.");
			}
			return savePath;
		}
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
	}
}


