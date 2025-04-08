using Godot;
using GodotPlugins.Game;
using System;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class Settings : Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		[Export] private Slider _musicVolumeSlider = null;
		private TextureButton _selectBack;
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		private TextureButton _selectResume;


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

			_selectBack = GetNode<TextureButton>("BackButton");
			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ResumeButton");

			CheckScene(); // Checks if current scene is main menu
						  // CheckMusicSlider();

			_selectBack.Pressed += OnBackButtonPressed;
			_selectMainMenu.Pressed += OnMainMenuPressed;
			_selectRestart.Pressed += OnRestartPressed;
			_selectResume.Pressed += OnResumePressed;

			// _musicVolumeSlider.Connect(Slider.SignalName.ValueChanged, new Callable(this, nameof(OnMusicVolumeSliderChanged)));

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		private void OnBackButtonPressed() // deletes settings instance. In user eyes it just closes it
		{
			this.QueueFree();
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
			// PackedScene selectRestartButton = ResourceLoader.Load<PackedScene>(_levelScenePath);
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
				_selectMainMenu.Visible = false;
				_selectRestart.Visible = false;
				_selectBack.Visible = true;
				_selectResume.Visible = false;
			}
			else
			{
				_selectMainMenu.Visible = true;
				_selectBack.Visible = false;
				GetTree().Paused = true;
			}
		}
		private void OnResumePressed()
		{
			GetTree().Paused = false;
			this.QueueFree();
		}

		private void UpdateVolume()
		{
			if (_musicVolumeSlider != null)
			{
				float linearVolume = (float)_musicVolumeSlider.Value;

				float decibelVolume = Mathf.LinearToDb(linearVolume);
			}
		}
		private void OnMusicVolumeSliderChanged(float value)
		{
			UpdateVolume();
		}

	}
}