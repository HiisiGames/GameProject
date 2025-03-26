using Godot;
using GodotPlugins.Game;
using System;

namespace TruckGame
{
	public partial class Settings : Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		private Button _selectBack;
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		public MainMenu _mainMenuFile;
		public bool _settingsOpen = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_mainMenuFile = GetNode<MainMenu>("res://GUI/MainMenu");
			_selectBack = GetNode<Button>("BackButton");
			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");

			CheckScene();

			_selectBack.Pressed += OnBackButtonPressed;
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
			_mainMenuFile.SettingsOpen = false;
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
				_selectMainMenu.Visible = false;
				_selectRestart.Visible = false;
			}
			// else THIS IS NOT NEEDED, FOR CLARITY ONLY
			// {
			// 	_selectMainMenu.Visible = true;
			// }

		}
		public bool SettingsOpen
		{
			get { return _settingsOpen; }

			set { _settingsOpen = value; }
		}

	}
}