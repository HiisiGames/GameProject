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
		[Export] private string _level2ScenePath = "res://Levels/Level2.tscn";
		[Export] private string _level3ScenePath = "res://Levels/Level3.tscn";
		[Export] private string _level1ScenePath = "res://Levels/Level1.tscn";
		private string _nextScenePath;
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		private TextureButton _selectResume;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Instantiate = this;

			GD.Print("LEVELCOMPLETE.CS STARTS");
			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ContinueButton");

			// LoadLevelData();
			// IsLevelComplete();
			CheckScene(); // Checks if current scene is main menu / used to pause the scene tree
			// AutoLoad();
			// AutoSave();

			_selectMainMenu.Pressed += OnMainMenuPressed;
			_selectRestart.Pressed += OnRestartPressed;
			_selectResume.Pressed += OnContinuePressed;

			GD.Print("LEVELCOMPLETE.CS ENDS");
		}

		private void OnMainMenuPressed() // Goes back to main menu
		{
			VictorySoundStop();

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
			VictorySoundStop();

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
			VictorySoundStop();

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
		private void VictorySoundStop()
		{
			AudioManager.Instantiate.victorySound.Stop();
		}

	}
}