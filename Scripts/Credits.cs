using Godot;
using System;

namespace TruckGame
{
	public partial class Credits: Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		private Button _selectBack;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectBack = GetNode<Button>("BackButton");

			_selectBack.Pressed += OnBackButtonPressed;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		private void OnBackButtonPressed()
		{
			PackedScene selectBackButton = ResourceLoader.Load<PackedScene>(_mainMenuScenePath);
			if (selectBackButton != null)
			{
				GetTree().ChangeSceneToPacked(selectBackButton);
			}
			else
			{
				GD.Print("Main menu scene not found");
			}
		}
	}
}