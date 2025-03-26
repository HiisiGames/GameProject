using Godot;
using System;

namespace TruckGame
{
	public partial class HUDSettings : Node
	{

		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";
		private TextureButton _selectSettings;
		private PackedScene _selectSettingsScene;
		public bool _isSettingsOpen = false;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectSettingsScene = ResourceLoader.Load<PackedScene>(_settingsScenePath);

			_selectSettings = GetNode<TextureButton>("SettingsButton");

			_selectSettings.Pressed += OnSettingsPressed;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}
		// private void OnSettingsPressed()
		// {

		// 	if (_selectSettingsScene != null)
		// 	{
		// 		Node settingsPanel = _selectSettingsScene.Instantiate();
		// 		AddChild(settingsPanel);
		// 		GD.Print("Settings works");
		// 	}
		// 	else
		// 	{
		// 		GD.Print("Settings scene not found");
		// 	}
		// }
		private void OnSettingsPressed() // This will bring settings to the user after pressing settings button
		{

			if (!_isSettingsOpen)
			{
				if (_selectSettingsScene != null)
				{
					Node settingsPanel = _selectSettingsScene.Instantiate();
					settingsPanel.Name = "SettingsPanel";
					AddChild(settingsPanel);
					_isSettingsOpen = true;
				}
				else
				{
					GD.Print("Settings scene not found");
				}
			}
		}

	}
}