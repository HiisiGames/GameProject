using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// GameTime is used to calculate the time and how many stars at the end of the game the player gets
	/// </summary>
	public partial class GameTime : Node
	{
		// TIMER WORKS DONT TOUCH
		private float _time;
		private int _secondCounter = 00;
		private int _minuteCounter = 00;
		private Label _gameTimeLabel;
		public int _starCount = 0;
		[Export] private float _firstStar = 0;
		[Export] private float _secondStar = 0;
		[Export] private float _thirdStar = 0;
		public float _totalTime;

		public float TotalTime
		{
			get { return _totalTime; }
			private set { _totalTime = value; }

		}


		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			GD.Print(GetTree().CurrentScene.Name);
			_gameTimeLabel = GetNode<Label>("GameTime");
			if (_gameTimeLabel == null)
			{
				GD.Print("Error: Gametime label not found");
			}
			_gameTimeLabel.Text = "Time: 00:00";
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			_time += (float)delta;
			CountSeconds();
		}

		public void CountSeconds() // This counts seconds
		{
			// GD.Print("Alkaa");
			if (_time >= 1.0f)
			{
				_totalTime++;
				_secondCounter++;
				if (_secondCounter >= 60)
				{

					_minuteCounter++;
					_secondCounter -= 60;
				}
				GD.Print(_time);
				GD.Print($"Time: {_minuteCounter}:{_secondCounter}");
				_gameTimeLabel.Text = $"Time: {_minuteCounter:D2}:{_secondCounter:D2}";
				_time -= 1.0f;
				GD.Print($"Total time: {_totalTime}");
			}
		}

		public int CountStars()
		{
			CheckLevelScene();

			if (_thirdStar >= _totalTime)
			{
				_starCount = 3;
				GD.Print("Full stars");
			}
			else if (_secondStar >= _totalTime)
			{
				_starCount = 2;
				GD.Print("Two stars");
			}
			else if (_firstStar >= _totalTime)
			{
				_starCount = 1;
				GD.Print("One star");
			}
			else
			{
				_starCount = 0;
			}
			return _starCount;
		}
		private void CheckLevelScene()
		{
			Node currentLevel = GetTree().CurrentScene;

			if (currentLevel.SceneFilePath == "res://Levels/Level_1.tscn")
			{
				_firstStar = 90;
				_secondStar = 70;
				_thirdStar = 30;
			}
			else if (currentLevel.SceneFilePath == "res://Levels/Level_2.tscn")
			{
				_firstStar = 90;
				_secondStar = 60;
				_thirdStar = 30;
			}
			else if (currentLevel.SceneFilePath == "res://Levels/Level_3.tscn")
			{
				_firstStar = 90;
				_secondStar = 60;
				_thirdStar = 45;
			}
			GD.Print("Tämä on:");
			GD.Print(currentLevel.SceneFilePath);
		}
	}
}
