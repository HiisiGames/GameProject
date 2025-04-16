using Godot;
using Godot.Collections;
using System;

namespace TruckGame
{
	/// <summary>
	/// This updates the star scene and the timer to LevelComplete.tscn
	/// </summary>
	public partial class LevelCompleteTime : Node
	{
		public static LevelCompleteTime Instantiate;
		private GameTime _gameTime;
		private Label _levelCompleteTimer;
		private Sprite2D _starOne;
		private Sprite2D _starTwo;
		private Sprite2D _starThree;
		private int _starsAtTheEnd;
		private float _fastestTime;
		private string _level1Time;
		private string _level2Time;
		private string _level3Time;

		public override void _Ready()
		{
			Instantiate = this;
			// GD.Print(GetTree().CurrentScene.Name);
			Node currentScene = GetTree().CurrentScene;

			_gameTime = currentScene.GetNode<GameTime>("PlayerVehicle/Camera2D/CanvasLayer"); //Finds the node with the script

			_levelCompleteTimer = GetNode<Label>("TimeInTheEnd");

			if (_gameTime != null)
			{
				GD.Print("gametime FOUND");

				_starsAtTheEnd = _gameTime.CountStars();

				GD.Print($"Stars: {_starsAtTheEnd}");

				string gameTimeEnd = _gameTime.GetNode<Label>("GameTime").Text;

				if (_levelCompleteTimer != null)
				{
					_levelCompleteTimer.Text = gameTimeEnd;
				}
			}
			else
			{
				GD.Print("Gametime NOT FOUND");
			}

			_starOne = GetNode<Sprite2D>("StarOne");
			_starTwo = GetNode<Sprite2D>("StarTwo");
			_starThree = GetNode<Sprite2D>("StarThree");

			UpdateStars();
			FastestTime();

		}

		private void UpdateStars()
		{
			if (_starsAtTheEnd >= 1)
			{
				_starOne.Texture = (Texture2D)GD.Load("res://Arts/UI/Star.png");
				GD.Print("Luotu eka tähti");
			}
			if (_starsAtTheEnd >= 2)
			{
				_starTwo.Texture = (Texture2D)GD.Load("res://Arts/UI/Star.png");
				GD.Print("Luotu toka tähti");
			}
			if (_starsAtTheEnd >= 3)
			{
				_starThree.Texture = (Texture2D)GD.Load("res://Arts/UI/Star.png");
				GD.Print("Luotu kolmas tähti");
				AudioManager.Instantiate.victorySound.Play();
			}
			GD.Print("UpdateStars Methods works");
		}
		private string FastestTime()
		{
			_fastestTime = _gameTime._totalTime;
			int minutes = Mathf.FloorToInt(_fastestTime / 60);
			int seconds = Mathf.FloorToInt(_fastestTime % 60);

			string realTime = $"{minutes:D2}:{seconds:D2}";
			GD.Print($"Time: {realTime}. USING FastestTime() method ");
			return realTime;
		}
		public string Level1Time()
		{
			Node currentScene = GetTree().CurrentScene;
			if (currentScene.Name == "Level1")
			{
				_level1Time = FastestTime();
				return _level1Time;
			}
			else
			{
				return "Couldn't get level 1 time";
			}

		}
		public string Level2Time()
		{
			Node currentScene = GetTree().CurrentScene;
			if (currentScene.Name == "Level2")
			{
				_level2Time = FastestTime();
				return _level2Time;
			}
			else
			{
				return "Couldn't get level 2 time";
			}
		}
		public string Level3Time()
		{
			Node currentScene = GetTree().CurrentScene;
			if (currentScene.Name == "Level1")
			{
				_level3Time = FastestTime();
				return _level3Time;
			}
			else
			{
				return "Couldn't get level 3 time";
			}
		}
	}
}