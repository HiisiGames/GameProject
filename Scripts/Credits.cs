using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// This is the cs file for GUI/Credits.tscn
	/// </summary>
	public partial class Credits: Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		private TextureButton _selectBack;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_selectBack = GetNode<TextureButton>("BackButton");

			_selectBack.Pressed += OnBackButtonPressed;
			//
		}

		private void OnBackButtonPressed()
		// Takes the user back to main menu from the credits scene
		{
			PackedScene selectBackButton = ResourceLoader.Load<PackedScene>(_mainMenuScenePath);
			if (selectBackButton != null)
			{
				GetTree().ChangeSceneToPacked(selectBackButton);
				// Changes the current scene to main menu
			}
			else
			{
				GD.Print("Main menu scene not found");
			}
		}
	}
}