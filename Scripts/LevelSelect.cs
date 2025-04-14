using Godot;
using Godot.Collections;
using System;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class LevelSelect : Node
	{
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _level1ScenePath = "res://Levels/Level_1.tscn";
		[Export] private string _level2ScenePath = "res://Levels/Level_2.tscn";
		[Export] private string _level3ScenePath = "res://Levels/Level_3.tscn";
		private Button _selectBack;
		private Button _selectLevel1;
		private Button _selectLevel2;
		private Button _selectLevel3;
		public static int[] _isLevelComplete = new int[3];
		// private GameSave gameSave;

		// public void IsLevelComplete
		// {
		// 	get { return _isLevelComplete; }
		// 	set
		// 	{

		// 	}
		// }





		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			AudioManager.Instantiate.bgMusic.StreamPaused = false;
			// AudioManager.Instantiate.engineSound.StreamPaused = true;

			_selectBack = GetNode<Button>("BackButton");
			_selectLevel1 = GetNode<Button>("Level1");
			_selectLevel2 = GetNode<Button>("Level2");
			_selectLevel3 = GetNode<Button>("Level3");


			_selectBack.Pressed += OnBackButtonPressed;
			_selectLevel1.Pressed += OnLevel1ButtonPressed;
			_selectLevel2.Pressed += OnLevel2ButtonPressed;
			_selectLevel3.Pressed += OnLevel3ButtonPressed;
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
		private void OnLevel1ButtonPressed()
		{
			PackedScene selectLevel1Button = ResourceLoader.Load<PackedScene>(_level1ScenePath);
			if (selectLevel1Button != null)
			{
				GetTree().ChangeSceneToPacked(selectLevel1Button);
			}
			else
			{
				GD.Print("Level 1 scene not found");
			}
		}
		private void OnLevel2ButtonPressed()
		{
			PackedScene selectLevel2Button = ResourceLoader.Load<PackedScene>(_level2ScenePath);
			if (selectLevel2Button != null)
			{
				GetTree().ChangeSceneToPacked(selectLevel2Button);
			}
			else
			{
				GD.Print("Level 2 scene not found");
			}

		}
		private void OnLevel3ButtonPressed()
		{
			PackedScene selectLevel3Button = ResourceLoader.Load<PackedScene>(_level3ScenePath);
			if (selectLevel3Button != null)
			{
				GetTree().ChangeSceneToPacked(selectLevel3Button);
			}
			else
			{
				GD.Print("Level 3 scene not found");
			}

		}
	}
}