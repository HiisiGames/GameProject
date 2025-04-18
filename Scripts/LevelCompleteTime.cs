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
		public float _newLevel1Time;
		public float _newLevel2Time;
		public float _newLevel3Time;
		public float _currentLevel1Time;
		public float _currentLevel2Time;
		public float _currentLevel3Time;

		public override void _Ready()
		{
			Instantiate = this;

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

			GameSave.Instantiate.Load();

			UpdateStars();
			FastestTime();

			UpdateTimes();

			GameSave.Instantiate.Save();
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
			GD.Print("UpdateStars Method works");
		}
		private string GetCurrentLevel()
		{
			Node currentScene = GetTree().CurrentScene;

			if (currentScene.Name == "Level1")
				return currentScene.Name;

			if (currentScene.Name == "Level2")
				return currentScene.Name;

			if (currentScene.Name == "Level3")
				return currentScene.Name;

			return null;
		}
		// private string FastestTimeToText()
		// {
		// 	_fastestTime = _gameTime._totalTime;
		// 	int minutes = Mathf.FloorToInt(_fastestTime / 60);
		// 	int seconds = Mathf.FloorToInt(_fastestTime % 60);

		// 	string realTime = $"Fastest Time: {minutes:D2}:{seconds:D2}";
		// 	GD.Print($"Time: {realTime}. USING FastestTime() method ");
		// 	return realTime;
		// }
		private float FastestTime()
		{
			return _gameTime._totalTime;
		}

		private void UpdateTimes()
		{
			UpdateLevel1Time();
			UpdateLevel2Time();
			UpdateLevel3Time();
		}
		private float UpdateLevel1Time()
		{
			if (GetCurrentLevel() == "Level1")
			{
				if (_currentLevel1Time > FastestTime() || _currentLevel1Time == 0)
				{
					_currentLevel1Time = FastestTime(); // Store the fastest time for Level 1
					GD.Print($"Level 1 Time: {_currentLevel1Time}, UPDATELEVEL1TIME METHOD");
					return _currentLevel1Time;
				}
			}
			GD.Print("UpdateTime Method works");
			return _currentLevel1Time;

		}
		private float UpdateLevel2Time()
		{
			if (GetCurrentLevel() == "Level2")
			{
				if (_currentLevel2Time > FastestTime() || _currentLevel2Time == 0)
				{
					_currentLevel2Time = FastestTime(); // Store the fastest time for Level 1
					GD.Print($"Level 2 Time: {_currentLevel2Time}");
				}
			}
			return _currentLevel2Time;
		}
		private float UpdateLevel3Time()
		{
			if (GetCurrentLevel() == "Level3")
			{
				if (_currentLevel3Time > FastestTime() || _currentLevel3Time == 0)
				{
					_currentLevel3Time = FastestTime(); // Store the fastest time for Level 1
					GD.Print($"Level 3 Time: {_currentLevel3Time}");
				}
			}
			return _currentLevel3Time;
		}
		public Dictionary TimeData()
		{
			Dictionary data = new Dictionary();

			if (_currentLevel1Time >= 0)
			{
				data.Add("FastestTimeLevel1", _currentLevel1Time);
				GD.Print("Level 1 data added");
			}
			if (_currentLevel2Time >= 0)
			{
				data.Add("FastestTimeLevel2", _currentLevel2Time);
				GD.Print("Level 2 data added");
			}
			if (_currentLevel3Time >= 0)
			{
				data.Add("FastestTimeLevel3", _currentLevel3Time);
				GD.Print("Level 3 data added");
			}
			return data;
		}
	}
}