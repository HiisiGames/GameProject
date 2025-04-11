using Godot;
using System;
using System.IO;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class GameSave : Node2D
	{
		[Export] public int[] CurrentLevelComplete { get; set; }
		[Export] public int CurrentLevelOn { get; set; }
		[Export] public float AudioVolume { get; set; }
		[Export] public int FastestTime { get; set; }

		// [Export] public bool ReverseControls { get; set; }
		public override void _Ready()
		{
			Godot.Collections.Dictionary data = new Godot.Collections.Dictionary();
			data.Add("name","Tim");
			data.Add("job", "Programmer");

			string json = Json.Stringify(data);
			GD.Print(json);

			string path = ProjectSettings.GlobalizePath("user://");

			SaveTextToFile(path, "SaveGame1.json", json);
		}

		private void SaveTextToFile(string path, string fileName, string data)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Join(path, fileName);
			GD.Print(path);

			try
			{
				File.WriteAllText(path, data);
			}
			catch (System.Exception e)
			{
				GD.Print(e);
			}
		}

		private string LoadTextFromFile(string path, string fileName)
		{
			string data = null;

			path = Path.Join(path, fileName);

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
		// public GameSave()
		// {
		// 	CurrentLevelComplete = new int[3];
		// 	AudioVolume = 1.0f;
		// 	// ReverseControls = false;

		// }
	}
}


