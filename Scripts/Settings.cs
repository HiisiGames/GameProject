using Godot;
using System;

namespace TruckGame
{
	public partial class Settings : Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _levelScenePath = "res://GUI/LevelSelection.tscn";
		private Button _selectBack;
		private Button _selectLevel;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectBack = GetNode<Button>("BackButton");
			_selectLevel = GetNode<Button>("LevelButton");

			_selectBack.Pressed += OnBackButtonPressed;
			_selectLevel.Pressed += OnLevelButtonPressed;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		private void OnBackButtonPressed()
		{
			this.QueueFree();
		}
		private void OnLevelButtonPressed()
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

	}
}