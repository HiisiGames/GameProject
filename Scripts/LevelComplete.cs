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

		public bool IsLevelComplete1
		{
			get { return _isLevelComplete1; }
		}
		public bool IsLevelComplete2
		{
			get { return _isLevelComplete2; }
		}
		public bool IsLevelComplete3
		{
			get { return _isLevelComplete3; }
		}
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_gameSave = ResourceLoader.Load<PackedScene>(_gameSaveScenePath);

			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ContinueButton");

			IsLevelComplete();
			CheckScene(); // Checks if current scene is main menu / used to pause the scene tree
			GameSave();

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
		private void GameSave()
		{
			{
				if (_gameSave != null)
				{
					if (GetNode<Node>("GameSave") == null)
					{
						GD.Print("GameSave node found, LevelComplete Method");
						Node gameSave = _gameSave.Instantiate();
						gameSave.Name = "GameSave"; // THIS IS IMPORTANT DONT DELETE
						AddChild(gameSave);
					}
				}
				else
				{
					GD.Print("Gamesave node not found, LevelComplete Method");
				}
			}
		}
	}
}