using Godot;
using GodotPlugins.Game;
using System;
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
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		private TextureButton _selectResume;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ContinueButton");

			CheckScene(); // Checks if current scene is main menu

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

		private void CheckScene() // This will hide the specific buttons from the main menu
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
			PackedScene selectLevelButton = ResourceLoader.Load<PackedScene>(_levelScenePath);
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


	}
}