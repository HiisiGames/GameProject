using Godot;
using GodotPlugins.Game;
using System;

namespace TruckGame
{
	/// <summary>
	/// GameOver contains the code side for GameOver.tscn <br/>
	/// Refer to CollisionDetector.cs for information, when this is brought up
	/// </summary>
	public partial class GameOver : Node
	{
		/// <summary>
		/// The path to where main menu is located in the project directory
		/// </summary>
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";

		/// <summary>
		/// The path to where the level selection is located in the project directory
		/// </summary>
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		private TextureButton _selectResume;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			//Get node gets the node with the exact name and type, won't work otherwise
			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ContinueButton");

			CheckScene(); // Checks if current scene is main menu

			//Listens to the methods listed
			_selectMainMenu.Pressed += OnMainMenuPressed;
			_selectRestart.Pressed += OnRestartPressed;
			_selectResume.Pressed += OnContinuePressed;

		}

		/// <summary>
		/// This method loads up the _mainMenuScenePath and changes the current scene to it. <br/>
		/// "GetTree().Paused = false, This method unfreezes the scene tree when the value is set to false
		/// </summary>
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

		/// <summary>
		/// Searches the current scene and reloads it, also unpauses the scene tree
		/// </summary>
		private void OnRestartPressed()
		{
			Node currentScene = GetTree().CurrentScene; // Takes the current scene from the scene tree

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

		/// <summary>
		/// This method checks if the current scene is main menu or not,
		/// if it's not main menu, the scene tree is paused
		/// </summary>
		private void CheckScene() // This will hide the specific buttons from the main menu
		{
			Node currentScene = GetTree().CurrentScene; // Takes the current scene from the scene tree

			if (currentScene.SceneFilePath == "res://GUI/MainMenu.tscn")
			{

			}
			else
			{
				GetTree().Paused = true;
			}
		}

		/// <summary>
		/// This method loads the _levelscenepath and changes the current scene to it.
		/// It also unpauses the scene tree.
		/// </summary>
		private void OnContinuePressed()
		{
			PackedScene selectLevelButton = ResourceLoader.Load<PackedScene>(_levelScenePath);
			if (selectLevelButton != null)
			{
				GetTree().Paused = false;
				GetTree().ChangeSceneToPacked(selectLevelButton);
			}
			else
			{
				GD.Print("Level selection scene not found");
			}

		}


	}
}