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
		public static Settings Instantiate;
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		private Slider _musicVolumeSlider;
		private Slider _sfxVolumeSlider;
		private TextureButton _selectBack;
		private TextureButton _selectMainMenu;
		private TextureButton _selectRestart;
		private TextureButton _selectResume;


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Instantiate = this;

			_selectBack = GetNode<TextureButton>("BackButton");
			_selectMainMenu = GetNode<TextureButton>("MainMenuButton");
			_selectRestart = GetNode<TextureButton>("RestartButton");
			_selectResume = GetNode<TextureButton>("ResumeButton");

			_musicVolumeSlider = GetNode<Slider>("MusicSlider");
			_sfxVolumeSlider = GetNode<Slider>("SFXSlider");

			CheckScene(); // Checks if current scene is main menu
						  // CheckMusicSlider();

			_selectBack.Pressed += OnBackButtonPressed;
			_selectMainMenu.Pressed += OnMainMenuPressed;
			_selectRestart.Pressed += OnRestartPressed;
			_selectResume.Pressed += OnResumePressed;

			// These sync up the sliders to the audio.
			_musicVolumeSlider.Value = Mathf.DbToLinear(AudioManager.Instantiate.bgMusic.VolumeDb);
			_sfxVolumeSlider.Value = Mathf.DbToLinear(AudioManager.Instantiate.engineSound.VolumeDb);

			_musicVolumeSlider.ValueChanged += OnMusicVolumeChanged;
			_sfxVolumeSlider.ValueChanged += OnSFXVolumeChanged;

			// _musicVolumeSlider.Connect(Slider.SignalName.ValueChanged, new Callable(this, nameof(OnMusicVolumeSliderChanged)));

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
			AudioManager.Instantiate.engineSound.StreamPaused = false;
			GetTree().Paused = false;
			this.QueueFree();
		}
		private void OnMusicVolumeChanged(double value)
		{
			float db = Mathf.LinearToDb((float)value);
			AudioManager.Instantiate.bgMusic.VolumeDb = db;

			GameSave.Instantiate.SaveAudio();
		}
		private void OnSFXVolumeChanged(double value)
		{
			float db = Mathf.LinearToDb((float)value);
			AudioManager audio = AudioManager.Instantiate;
			audio.engineSound.VolumeDb = db;
			audio.collideSound.VolumeDb = db;
			audio.clickButtonSound.VolumeDb = db;
			audio.victorySound.VolumeDb = db;

			GameSave.Instantiate.SaveAudio();
		}
	}
}