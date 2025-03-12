using Godot;
using System;

namespace TruckGame
{
	public partial class MainMenu : Node
	{
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";
		private TextureButton _selectPlay;
		private TextureButton _selectSettings;
		private TextureButton _selectCredits;
		private PackedScene _selectSettingsScene;
		private Panel _settingsPanel;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectSettingsScene = ResourceLoader.Load<PackedScene>(_settingsScenePath);

			_selectPlay = GetNode<TextureButton>("PlayButton");
			_selectSettings = GetNode<TextureButton>("SettingsButton");
			_selectCredits = GetNode<TextureButton>("CreditsButton");

			_selectPlay.Pressed += OnPlayPressed;
			_selectSettings.Pressed += OnSettingsPressed;
			_selectCredits.Pressed += OnCreditsPressed;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		private void OnPlayPressed()
		{
			PackedScene selectLevelScene = ResourceLoader.Load<PackedScene>(_levelScenePath);
			if (selectLevelScene != null)
			{
				GetTree().ChangeSceneToPacked(selectLevelScene);
			} else
			{
				GD.Print("Level selection scene not found");
			}
		}
		private void OnSettingsPressed()
		{

			if (_selectSettingsScene != null)
			{
				_settingsPanel = (Panel)_selectSettingsScene.Instantiate();
				AddChild(_settingsPanel);
			} else {
				GD.Print("Settings scene not found");
			}
		}
		private void OnCreditsPressed()
		{
			PackedScene selectLevelScene = ResourceLoader.Load<PackedScene>(_levelScenePath);
			if (selectLevelScene != null)
			{
				GetTree().ChangeSceneToPacked(selectLevelScene);
			} else
			{
				GD.Print("Level selection scene not found");
			}
		}
	}
}