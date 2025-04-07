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
		private void OnSettingsPressed() // This will bring settings to the user after pressing settings button
		{
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
		}

	}
}