using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// This is cs file for GUI/HUD.tscn
	/// </summary>
	public partial class HUDSettings : Node
	{
		/// <summary>
		/// The path to where settings scene is located in the project directory
		/// </summary>
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";
		public static HUDSettings Instantiate;
		public TextureButton _selectSettings;
		private PackedScene _selectSettingsScene;
		private TouchScreenButton _startGameButton;
		private Label _gasLabel;
		private Label _brakeLabel;
		private Label _countDown;
		private Timer _gamePaused;
		private bool _isGamePaused = true;
		private bool _hasStarted = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Instantiate = this;

			_selectSettingsScene = ResourceLoader.Load<PackedScene>(_settingsScenePath);

			_selectSettings = GetNode<TextureButton>("SettingsButton");
			_startGameButton = GetNode<TouchScreenButton>("StartGameButton");

			_gasLabel = GetNode<Label>("Gas");
			_brakeLabel = GetNode<Label>("Brake");
			_countDown = GetNode<Label>("CountDown");

			_selectSettings.Pressed += OnSettingsPressed;


			if (!GameState.Instantiate.isTheGameOn) // makes sure that pausebeforestart method is called only once
			{
				PauseBeforeStart();
				GameState.Instantiate.isTheGameOn = true;
				GD.Print("Game has started / isTheGameOn = true ");
			}
			else
			{
				PauseIsOver();
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			StartGame();
		}
		private void OnSettingsPressed() // This will bring settings to the user after pressing settings button
		{
			{
				if (_selectSettingsScene != null)
				{
					if (GetNodeOrNull<Node>("SettingsPanel") == null)
					{
						Node settingsPanel = _selectSettingsScene.Instantiate();
						settingsPanel.Name = "SettingsPanel"; // THIS IS IMPORTANT DONT DELETE
						AddChild(settingsPanel);

						AudioManager.Instantiate.engineSound.StreamPaused = true;
					}
				}
				else
				{
					GD.Print("Settings scene not found");
				}
			}
		}

		/// <summary>
		/// pauses the scene tree, hides few buttons and brings up the Timer node
		/// </summary>
		private void PauseBeforeStart()
		{
			GetTree().Paused = true;

			_countDown.Visible = false;

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

		/// <summary>
		/// Hides and brings up appropriate buttons, pauses bg music and starts enginesound, unpauses the game.
		/// </summary>
		private void PauseIsOver()
		{
			_gasLabel.Visible = false;
			_brakeLabel.Visible = false;
			_selectSettings.Visible = true;
			_countDown.Visible = false;

			if (_startGameButton.Visible == true) // this is to make sure that if you restart the game, it wont show up
			{
				_startGameButton.Visible = false;
			}

			AudioManager.Instantiate.bgMusic.StreamPaused = true;
			AudioManager.Instantiate.engineSound.Play();

			GetTree().Paused = false;
			GD.Print("Unpaused, PauseIsOver method");
		}

		/// <summary>
		///	Starting the game first time
		/// </summary>
		private void StartGame()
		{
			if (Input.IsActionJustPressed("StartGame"))
			{
				GD.Print("Input detected (ScreenTouch, StartGame Method)");
				if (_hasStarted == false)
				{
					_gamePaused.Start();

					_startGameButton.Visible = false;

					_countDown.Visible = true;

					CountDown();

					_gamePaused.Timeout += PauseIsOver;

					GD.Print("Game is going to unpause)");

					_hasStarted = true;
				}
			}
		}

		/// <summary>
		///  CountDown method makes sure that the user has time to react when the game starts when
		/// its first opened.
		/// <para> it creates a timer 3 times each lasting a second, while updating its text </para>
		/// </summary>
		private async void CountDown()
		{
			int time = 3;

			for (time = 3; time >= 0; time--)
			{
				_countDown.Text = $"{time}";
				Timer timer = new Timer();
				AddChild(timer);
				timer.WaitTime = 1.0f;
				timer.OneShot = true;
				timer.Start();

				await ToSignal(timer, "timeout");

				timer.QueueFree();
			}
		}

	}
}
