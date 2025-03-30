using Godot;
using System;

namespace TruckGame
{
	public partial class GameTime : Node
	{
		// TIMER WORKS DONT TOUCH
		private float _time;
		private int _secondCounter = 00;
		private int _minuteCounter = 00;
		private Label _gameTimeLabel;
		public int _starCount = 0;
		[Export] private float _firstStar = 90;
		[Export] private float _secondStar = 60;
		[Export] private float _thirdStar = 30;
		private float _totalTime;

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
			if (_thirdStar >= _totalTime)
			{
				_starCount = 3;
				GD.Print("Full star");
			}
			else if (_secondStar >= _totalTime)
			{
				_starCount = 2;
				GD.Print("Full star");
			}
			else if (_firstStar >= _totalTime)
			{
				_starCount = 1;
				GD.Print("Full star");
			}
			else
			{
				_starCount = 0;
			}
			return _starCount;
		}

		// public void StarsAtTheEndGame()
		// {

		// }
	}
}
