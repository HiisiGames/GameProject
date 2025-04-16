using Godot;
using Godot.Collections;
using GodotPlugins.Game;
using System;
using System.Collections.Generic;
// FOR THIS TO WORK MAKE SURE THAT THE NODE WITH THE SCRIPT HAS "ALWAYS" ON IN THE PROCESS
// IT CAN BE CHECKED IN THE GODOT EDITOR FROM NODE ---->>> PROCESS
// MAKE SURE "ALWAYS" IS SELECTED
namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class LevelComplete : Node
	{
		public static LevelComplete Instantiate;
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _level2ScenePath = "res://Levels/Level_2.tscn";
		[Export] private string _level3ScenePath = "res://Levels/Level_3.tscn";
		[Export] private string _level1ScenePath = "res://Levels/Level_1.tscn";
		[Export] private string _gameSaveScenePath = "res://Scenes/GameSave.tscn";
		private string _nextScenePath;
		PackedScene _gameSave;
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		private TextureButton _selectResume;
		public bool _isLevelComplete1 = false;
		public bool _isLevelComplete2 = false;
		public bool _isLevelComplete3 = false;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Instantiate = this;

			_gameSave = ResourceLoader.Load<PackedScene>(_gameSaveScenePath);

			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ContinueButton");

			LoadLevelData();
			IsLevelComplete();
			CheckScene(); // Checks if current scene is main menu / used to pause the scene tree
			AutoLoad();
			AutoSave();

			_selectMainMenu.Pressed += OnMainMenuPressed;
			_selectRestart.Pressed += OnRestartPressed;
			_selectResume.Pressed += OnContinuePressed;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		private void OnMainMenuPressed() // Goes back to main menu
		{
			PackedScene selectMainMenuButton = ResourceLoader.Load<PackedScene>(_mainMenuScenePath);
			if (selectMainMenuButton != null)
			{
				GetTree().Paused = false;
				GetTree().ChangeSceneToPacked(selectMainMenuButton);
			}
			else
			{
				GD.Print("Main menu scene not found");
			}
		}
		private void OnRestartPressed()
		{
			Node currentScene = GetTree().CurrentScene;
			if (currentScene != null)
			{
				GetTree().Paused = false;
				GetTree().ReloadCurrentScene();
			}
			else
			{
				GD.Print("Current scene not found");
			}
		}

		private void CheckScene() // This pauses the scene tree
		{
			Node currentScene = GetTree().CurrentScene;

			if (currentScene.SceneFilePath == "res://GUI/MainMenu.tscn")
			{

			}
			else
			{
				GetTree().Paused = true;
			}
		}
		private void OnContinuePressed()
		{
			Node CurrentScene = GetTree().CurrentScene;
			if (CurrentScene.Name == "Level1")
			{
				_nextScenePath = _level2ScenePath;
			}
			else if (CurrentScene.Name == "Level2")
			{
				_nextScenePath = _level3ScenePath;
			}
			else
			{
				_nextScenePath = _mainMenuScenePath;
			}
			PackedScene selectLevelButton = ResourceLoader.Load<PackedScene>(_nextScenePath);
			if (selectLevelButton != null)
			{
				GetTree().Paused = false;
				GetTree().ChangeSceneToPacked(selectLevelButton);
			}
			else
			{
				GD.Print("Main menu scene not found");
			}

		}

		public void IsLevelComplete()
		{
			Node CurrentScene = GetTree().CurrentScene;

			if (CurrentScene.Name == "Level1" && _isLevelComplete1 == false)
			{
				_isLevelComplete1 = true;
				GD.Print("Level 1 is complete (IsLevelComplete method)");
				// TO DO
				// Add the way that this is saved, also see if you can check with if that it only does this if
			}
			else if (CurrentScene.Name == "Level2" && _isLevelComplete2 == false)
			{
				_isLevelComplete2 = true;
				GD.Print("Level 2 is complete (IsLevelComplete method)");
			}
			else if (CurrentScene.Name == "Level3" && _isLevelComplete3 == false)
			{
				_isLevelComplete3 = true;
				GD.Print("Level 3 is complete (IsLevelComplete method)");
			}
		}
		public void LoadLevelData()
		{
			GameSave.Instantiate.Load();
		}
		public void AutoLoad()
		{

			if (_isLevelComplete1 || _isLevelComplete2 || _isLevelComplete3)
			{
				GameSave.Instantiate.Load();
				GD.Print("AutoLoad succesfull, LevelComplete");
			}
			else
			{
				GD.Print("AutoLoad did not work");
			}
		}
		public void AutoSave()
		{
			if (_isLevelComplete1 || _isLevelComplete2 || _isLevelComplete3)
			{
				GameSave.Instantiate.Save();
				GD.Print("AutoSave succesfull, LevelComplete");
			}
			else
			{
				GD.Print("AutoSave did not work");
			}
		}

		public Dictionary LevelData()
		{
			Dictionary data = new Dictionary();
			data.Add("Level-1", _isLevelComplete1);
			data.Add("Level-2", _isLevelComplete2);
			data.Add("Level-3", _isLevelComplete3);
			return data;
		}
	}
}