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

			_selectBack = GetNode<TextureButton>("Panel/BackButton");
			_selectMainMenu = GetNode<TextureButton>("Panel/MainMenuButton");
			_selectRestart = GetNode<TextureButton>("Panel/RestartButton");
			_selectResume = GetNode<TextureButton>("Panel/ResumeButton");

			_musicVolumeSlider = GetNode<Slider>("Panel/MusicSlider");
			_sfxVolumeSlider = GetNode<Slider>("Panel/SFXSlider");

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

		/// <summary>
		/// deletes settings instance. In user eyes it just closes it
		/// </summary>
		private void OnBackButtonPressed()
		{
			this.QueueFree();
		}

		/// <summary>
		/// Goes back to main menu
		/// </summary>
		private void OnMainMenuPressed()
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
		/// Reloads the current scene
		/// </summary>
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

		/// <summary>
		/// This switches the seen buttons depending on if you are using settings in main menu or in levels.
		/// </summary>
		private void CheckScene()
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
				AudioManager.Instantiate.bgMusic.StreamPaused = false;
				GetTree().Paused = true;
			}
		}

		/// <summary>
		/// Continues the game / unpauses the scene tree.
		/// </summary>
		private void OnResumePressed()
		{
			AudioManager.Instantiate.engineSound.StreamPaused = false;
			AudioManager.Instantiate.bgMusic.StreamPaused = true;
			GetTree().Paused = false;
			this.QueueFree();
		}
		/// <summary>
		/// Saves the audiochanges when changed by the player.
		/// </summary>
		/// <param name="value"></param>
		private void OnMusicVolumeChanged(double value)
		{
			float db = Mathf.LinearToDb((float)value);
			AudioManager.Instantiate.bgMusic.VolumeDb = db;

			GameSave.Instantiate.SaveAudio();
		}

		/// <summary>
		/// Saves the sound effects value changes when changed by the player
		/// </summary>
		/// <param name="value"></param>
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