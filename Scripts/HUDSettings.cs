using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class HUDSettings : Node
	{
		/// <summary>
		/// The path to where settings scene is located in the project directory
		/// </summary>
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";
		private TextureButton _selectSettings;
		private PackedScene _selectSettingsScene;
		private TouchScreenButton _startGameButton;
		public Label _gasLabel;
		public Label _brakeLabel;
		private Timer _gamePaused;
		private bool _isGamePaused = true;
		private bool _hasStarted = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectSettingsScene = ResourceLoader.Load<PackedScene>(_settingsScenePath);

			_selectSettings = GetNode<TextureButton>("SettingsButton");
			_startGameButton = GetNode<TouchScreenButton>("StartGameButton");
			_gasLabel = GetNode<Label>("Gas");
			_brakeLabel = GetNode<Label>("Brake");

			_selectSettings.Pressed += OnSettingsPressed;

			// StartGame();

			PauseBeforeStart();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			StartGame();
		}
		private void OnSettingsPressed() // This will bring settings to the user after pressing settings button
		{
			{
				if (_selectSettingsScene != null) //_isSettingsOpenFromLevels == false
				{
					if (GetNodeOrNull<Node>("SettingsPanel") == null)
					{
						Node settingsPanel = _selectSettingsScene.Instantiate();
						settingsPanel.Name = "SettingsPanel"; // THIS IS IMPORTANT DONT DELETE
						AddChild(settingsPanel);
					}
				}
				else
				{
					GD.Print("Settings scene not found");
				}
			}
		}
		private void PauseBeforeStart()
		{
			GetTree().Paused = true;

			_gamePaused = GetNode<Timer>("PauseTimer");

			if (_gamePaused != null)
			{
				GD.Print("PauseTimer Found");
			}
			_gamePaused.Stop();

			GD.Print("Game is paused, PauseBeforeStart Method");

			_gamePaused.WaitTime = 3.0f;
			_gamePaused.OneShot = true;

			_selectSettings.Visible = false;
		}
		private void PauseIsOver()
		{
			_startGameButton.Visible = false;
			_gasLabel.Visible = false;
			_brakeLabel.Visible = false;
			_selectSettings.Visible = true;

			GetTree().Paused = false;
			GD.Print("Unpaused, PauseIsOver method");
		}
		private void StartGame()
		{
			if (Input.IsActionJustPressed("StartGame"))
			{
				GD.Print("Input detected (ScreenTouch, StartGame Method)");
				if (_hasStarted == false)
				{
					_gamePaused.Start();

					_gamePaused.Timeout += PauseIsOver;

					GD.Print("Game is going to unpause)");

					_hasStarted = true;
				}
			}
		}

	}
}
