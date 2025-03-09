using Godot;
using System;

namespace TruckGame
{
	public partial class MainMenu : Node
	{
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";
		private Button _selectPlay;
		private Button _selectSettings;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectPlay = GetNode<Button>("PlayButton");
			_selectSettings = GetNode<Button>("SettingsButton");

			_selectPlay.Pressed += OnPlayPressed;
			_selectSettings.Pressed += OnSettingsPressed;
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
			} else {
				GD.Print("Level selection scene not found");
			}
		}
		private void OnSettingsPressed()
		{
			PackedScene selectSettingsScene = ResourceLoader.Load<PackedScene>(_settingsScenePath);
			if (selectSettingsScene != null)
			{
				GetTree().ChangeSceneToPacked(selectSettingsScene);
			} else {
				GD.Print("Settings scene not found");
			}
		}
	}
}