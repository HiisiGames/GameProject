using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class MainMenu : Node
	{
		/// <summary>
		/// The path to where the level selection is located in the project directory
		/// </summary>
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";

		/// <summary>
		/// The path to where the settings scene is located in the project directory
		/// </summary>
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";

		/// <summary>
		/// The path to where the credits scene is located in the project directory
		/// </summary>
		[Export] private string _creditsScenePath = "res://GUI/Credits.tscn";
		private TextureButton _selectPlay;
		private TextureButton _selectSettings;
		private TextureButton _selectCredits;
		private PackedScene _selectSettingsScene;
		private Button _selectQuit;
		public bool _isSettingsOpenFromMainMenu = false;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			AudioManager.Instance.bgMusic.StreamPaused = false;
			AudioManager.Instance.engineSound.Stop();

			_selectSettingsScene = ResourceLoader.Load<PackedScene>(_settingsScenePath);

			_selectPlay = GetNode<TextureButton>("PlayButton");
			_selectSettings = GetNode<TextureButton>("SettingsButton");
			_selectCredits = GetNode<TextureButton>("CreditsButton");
			_selectQuit = GetNode<Button>("QuitButton");

			_selectPlay.Pressed += OnPlayPressed;
			_selectSettings.Pressed += OnSettingsPressed;
			_selectCredits.Pressed += OnCreditsPressed;
			_selectQuit.Pressed += OnQuitButtonPressed;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		/// <summary>
		/// This method loads the level selection scene and changes the current scene to it
		/// </summary>
		private void OnPlayPressed() // This will let the user to the level selection scene
		{
			PackedScene selectLevelScene = ResourceLoader.Load<PackedScene>(_levelScenePath);
			if (selectLevelScene != null)
			{
				PlayClickSound();
				GetTree().ChangeSceneToPacked(selectLevelScene);
			}
			else
			{
				GD.Print("Level selection scene not found");
			}
		}


		/// <summary>
		/// This method instantiates the settings scene and adds it as child to the current scene
		/// </summary>
		private void OnSettingsPressed() // This will bring settings to the user after pressing settings button
		{
			{
				if (_selectSettingsScene != null && GetNodeOrNull<Node>("SettingsPanel") == null)
				{
					PlayClickSound();
					Node settingsPanel = _selectSettingsScene.Instantiate();
					settingsPanel.Name = "SettingsPanel"; // IMPORTANT DONT DELETE
					AddChild(settingsPanel);
				}
				else
				{
					GD.Print("Settings scene not found");
				}
			}
		}

		/// <summary>
		/// This method loads credits scene and changes the current scene to it
		/// </summary>
		private void OnCreditsPressed() // this brings player to the credits scene
		{
			PackedScene selectCreditsScene = ResourceLoader.Load<PackedScene>(_creditsScenePath);
			if (selectCreditsScene != null)
			{
				PlayClickSound();
				GetTree().ChangeSceneToPacked(selectCreditsScene);
			}
			else
			{
				GD.Print("credits scene scene not found");
			}
		}

		private void OnQuitButtonPressed() // this will quit the game
		{
			PlayClickSound();
			GetTree().Quit(); // Exits the game with the press of a button
		}

		private void PlayClickSound()
		{
			AudioManager.Instance.clickButtonSound.Play();
		}
	}
}