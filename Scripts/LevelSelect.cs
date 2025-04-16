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
		public static LevelSelect Instantiate;
		[Export] private string _mainMenuScenePath = "res://GUI/MainMenu.tscn";
		[Export] private string _level1ScenePath = "res://Levels/Level_1.tscn";
		[Export] private string _level2ScenePath = "res://Levels/Level_2.tscn";
		[Export] private string _level3ScenePath = "res://Levels/Level_3.tscn";
		private TextureButton _selectBack;
		private Button _selectLevel1;
		private Button _selectLevel2;
		private Button _selectLevel3;
		public Label _level1TimeLabel;
		public Label _level2TimeLabel;
		public Label _level3TimeLabel;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Instantiate = this;
			AudioManager.Instantiate.bgMusic.StreamPaused = false;
			GameSave.Instantiate.Load();
			// AudioManager.Instantiate.engineSound.StreamPaused = true;

			_selectBack = GetNode<TextureButton>("BackButton");
			_selectLevel1 = GetNode<Button>("HBox/Level1");
			_selectLevel2 = GetNode<Button>("HBox/Level2");
			_selectLevel3 = GetNode<Button>("HBox/Level3");


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
		public Dictionary TimeData()
		{
			Dictionary data = new Dictionary();
			data.Add("FastestTimeLevel1", LevelCompleteTime.Instantiate.Level1Time());
			data.Add("FastestTimeLevel2", LevelCompleteTime.Instantiate.Level2Time());
			data.Add("FastestTimeLevel3", LevelCompleteTime.Instantiate.Level3Time());
			return data;
		}
	}
}