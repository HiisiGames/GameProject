using Godot;
using System;

namespace TruckGame
{
	public partial class Settings : Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		private Button _selectBack;
		private Button _selectLevel;
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectBack = GetNode<Button>("BackButton");
			_selectLevel = GetNode<Button>("LevelButton");
			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");

			CheckScene();

			_selectBack.Pressed += OnBackButtonPressed;
			_selectLevel.Pressed += OnLevelButtonPressed;
			_selectMainMenu.Pressed += OnMainMenuPressed;
			_selectRestart.Pressed += OnRestartPressed;

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		private void OnBackButtonPressed()
		{
			this.QueueFree();
		}
		private void OnLevelButtonPressed() // This will bring the user back to level selection
		{
			PackedScene selectLevelScene = ResourceLoader.Load<PackedScene>(_levelScenePath);
			if (selectLevelScene != null)
			{
				GetTree().ChangeSceneToPacked(selectLevelScene);
			}
			else
			{
				GD.Print("Level selection scene not found");
			}
		}

		private void OnMainMenuPressed() // Goes back to main menu
		{
			PackedScene selectBackButton = ResourceLoader.Load<PackedScene>(_mainMenuScenePath);
			if (selectBackButton != null)
			{
				GetTree().ChangeSceneToPacked(selectBackButton);
			}
			else
			{
				GD.Print("Main menu scene not found");
			}
		}
		private void OnRestartPressed()
		{
			// PackedScene selectRestartButton = ResourceLoader.Load<PackedScene>(_levelScenePath);
			Node currentScene = GetTree().CurrentScene;
			if (currentScene != null)
			{
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
				_selectLevel.Visible = false;
				_selectMainMenu.Visible = false;
				_selectRestart.Visible = false;
			}
			// else
			// {
			// 	_selectLevel.Visible = true;
			// 	_selectMainMenu.Visible = true;
			// }

		}

	}
}