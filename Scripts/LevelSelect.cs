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
		[Export] private string _level1ScenePath = "res://Levels/Level1.tscn";
		[Export] private string _level2ScenePath = "res://Levels/Level2.tscn";
		[Export] private string _level3ScenePath = "res://Levels/Level3.tscn";
		private TextureButton _selectBack;
		private Button _selectLevel1;
		private Button _selectLevel2;
		private Button _selectLevel3;
		private Label _level1TimeLabel;
		private Label _level2TimeLabel;
		private Label _level3TimeLabel;
		public float _level1Time;
		public float _level2Time;
		public float _level3Time;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Instantiate = this;

			GameSave.Instantiate.Load();

			GD.Print(_level1Time);

			AudioManager.Instantiate.bgMusic.StreamPaused = false;

			_selectBack = GetNode<TextureButton>("BackButton");
			_selectLevel1 = GetNode<Button>("HBox/Level1");
			_selectLevel2 = GetNode<Button>("HBox/Level2");
			_selectLevel3 = GetNode<Button>("HBox/Level3");

			_level1TimeLabel = GetNode<Label>("Level1Time");
			_level2TimeLabel = GetNode<Label>("Level2Time");
			_level3TimeLabel = GetNode<Label>("Level3Time");

			UpdateTimeLabels();

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
		private void UpdateTimeLabels()
		{
			UpdateLevel1();
			UpdateLevel2();
			UpdateLevel3();
		}
		private string UpdateLevel1()
		{
			int level1Minutes = Mathf.FloorToInt(_level1Time / 60);
			int level1Seconds = Mathf.FloorToInt(_level1Time % 60);

			string level1Time = $"Time: {level1Minutes:D2}:{level1Seconds:D2}";


			_level1TimeLabel.Text = level1Time;

			return _level1TimeLabel.Text;

		}
		private string UpdateLevel2()
		{
			int level2Minutes = Mathf.FloorToInt(_level2Time / 60);
			int level2Seconds = Mathf.FloorToInt(_level2Time % 60);

			string level2Time = $"Time: {level2Minutes:D2}:{level2Seconds:D2}";

			_level2TimeLabel.Text = level2Time;

			return _level2TimeLabel.Text;

		}
		private string UpdateLevel3()
		{

			int level3Minutes = Mathf.FloorToInt(_level3Time / 60);
			int level3Seconds = Mathf.FloorToInt(_level3Time % 60);


			string level3Time = $"Time: {level3Minutes:D2}:{level3Seconds:D2}";

			_level3TimeLabel.Text = level3Time;

			return _level3TimeLabel.Text;
		}

	}
}