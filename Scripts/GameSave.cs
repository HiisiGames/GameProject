// using Godot;
// using Godot.Collections;
// using System;
// using System.IO;

// namespace TruckGame
// {
// 	/// <summary>
// 	///
// 	/// </summary>
// 	public partial class GameSave : Node2D
// 	{
// 		// [Export] public int[] CurrentLevelComplete { get; set; }
// 		// [Export] public int CurrentLevelOn { get; set; }
// 		// [Export] public float AudioVolume { get; set; }
// 		// [Export] public int FastestTime { get; set; }

// 		// [Export] public bool ReverseControls { get; set; }
// 		public override void _Ready()
// 		{

// 		}

// 		public void Load()
// 		{
// 			string savePath = ProjectSettings.GlobalizePath("user://");
// 			savePath = Path.Combine(savePath, Config.SaveFolder);

// 			string loadedData = LoadFromFile(savePath, Config.AutoSaveFile);

// 			Json jsonLoader = new Json();
// 			Error loadError = jsonLoader.Parse(loadedData);
// 			if (loadError!= Error.Ok)
// 			{
// 				GD.PrintErr($"Virhe ladattaessa peliä: {loadError}");
// 			}

// 			Dictionary saveData = (Dictionary)jsonLoader.Data;

// 		}
// 		public void Save()
// 		{
// 			Godot.Collections.Dictionary saveData = new Godot.Collections.Dictionary();
// 			saveData.Add("IsLevelComplete", LevelSelect.IsLevelComplete);
// 			saveData.Add("Music", Music);
// 			saveData.Add("SFX", SFX);


// 			string json = Json.Stringify(saveData);
// 			GD.Print(json);

// 			string savePath = ProjectSettings.GlobalizePath("user://");
// 			savePath = Path.Combine(savePath, Config.SaveFolder);

// 			if (SaveToFile(savePath, Config.AutoSaveFile, json))
// 			{
// 				GD.Print("Peli tallentui.");
// 			}
// 			else
// 			{
// 				GD.Print("Peli ei tallentunut.");
// 			}
// 		}
// 		private bool SaveToFile(string path, string fileName, string json)
// 		{
// 			if (!Directory.Exists(path))
// 			{
// 				Directory.CreateDirectory(path);
// 			}
// 			path = Path.Combine(path, fileName);
// 			GD.Print(path);

// 			try
// 			{
// 				File.WriteAllText(path, json);
// 			}
// 			catch (System.Exception e)
// 			{
// 				GD.Print(e);
// 				return false;
// 			}
// 			return true;
// 		}

// 		private string LoadFromFile(string path, string fileName)
// 		{
// 			string data = null;

// 			path = Path.Combine(path, fileName);

// 			if (!File.Exists(path)) return null;

// 			try
// 			{
// 				data = File.ReadAllText(path);
// 			}
// 			catch (System.Exception e)
// 			{
// 				GD.Print(e);
// 			}
// 			return data;
// 		}
// 		// public GameSave()
// 		// {
// 		// 	IsLevelComplete = new int[3];
// 		// 	AudioVolume = -8.0f;
// 		// }
// 	}
// }


