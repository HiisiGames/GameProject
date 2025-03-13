using Godot;
using System;

namespace TruckGame
{
	public partial class HUD : Node
	{
		[Export] private string _settingsScenePath = "res://GUI/Settings.tscn";

		private Button _selectSettings;
		private Button _gasButton;
		private Button _brakeButton;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
