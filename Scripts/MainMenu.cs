using Godot;
using System;

namespace TruckGame
{
	public partial class MainMenu : Node
	{
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";
		[Export] private string _creditsScenePath = "res://GUI/Credits.tscn";
		private TextureButton _selectPlay;
		private TextureButton _selectSettings;
		private TextureButton _selectCredits;
		private PackedScene _selectSettingsScene;
		private Button _selectQuit;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
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
		private void OnPlayPressed() // This will let the user to the level selection scene
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
		private void OnSettingsPressed() // This will bring settings to the user after pressing settings button
		{

			if (_selectSettingsScene != null)
			{
				Node settingsPanel = _selectSettingsScene.Instantiate();
				settingsPanel.Name = "SettingsPanel";
				AddChild(settingsPanel);
			}
			else
			{
				GD.Print("Settings scene not found");
			}
		}
		private void OnCreditsPressed() // this brings player to the credits scene
		{
			PackedScene selectCreditsScene = ResourceLoader.Load<PackedScene>(_creditsScenePath);
			if (selectCreditsScene != null)
			{
				GetTree().ChangeSceneToPacked(selectCreditsScene);
			}
			else
			{
				GD.Print("credits scene scene not found");
			}
		}

		private void OnQuitButtonPressed() // this will quit the game
		{
			GetTree().Quit(); // This code seems to work only on pc
		}
	}
}