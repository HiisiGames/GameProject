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
		private Label _newHighScore;
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

			GD.Print("LEVELCOMPLETETIMER.CS STARTS");
			GD.Print();

			Node currentScene = GetTree().CurrentScene;

			_gameTime = currentScene.GetNode<GameTime>("PlayerVehicle/Camera2D/CanvasLayer"); //Finds the node with the GameTime script

			_levelCompleteTimer = GetNode<Label>("TimeInTheEnd");
			_newHighScore = GetNode<Label>("HighScore");

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

			if (_newHighScore != null)
			{
				_newHighScore.Visible = false;
			}

			_starOne = GetNode<Sprite2D>("StarOne");
			_starTwo = GetNode<Sprite2D>("StarTwo");
			_starThree = GetNode<Sprite2D>("StarThree");

			NewTime();

			GameSave.Instantiate.Load();

			UpdateStars();

			GD.Print($"CurrentScene Name: {GetTree().CurrentScene.Name}");
			GD.Print($"GetCurrentLevel() returns: {GetCurrentLevel()}");

			UpdateTimes();

			GD.Print();
			GD.Print("LEVELCOMPLETETIMER.CS ENDS");
		}

		/// <summary>
		/// Updates the textures to full stars depending on how many stars you had at the end.
		/// <para>Stars are calculated in GameTime.cs refer to it for more information. </para>
		/// </summary>
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

		/// <summary>
		/// Gets the current scene name
		/// </summary>
		/// <returns>currentScene.Name or null</returns>
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
		/// <summary>
		/// This method saves the total time it took the player to fnish a level.
		/// </summary>
		/// <returns>_gametime._totalTime</returns>
		private float NewTime()
		{
			return _gameTime._totalTime;
		}

		/// <summary>
		/// If the user gets new highscore, make the button visible. <para>Used in UpdateLevel1/2/3Time methods. </para>
		/// </summary>
		private void UpdateHighScore()
		{
			_newHighScore.Visible = true;
		}

		/// <summary>
		/// Runs trough the update level methods and then saves.
		/// </summary>
		private void UpdateTimes()
		{
			UpdateLevel1Time();
			UpdateLevel2Time();
			UpdateLevel3Time();
			GameSave.Instantiate.Save();
		}
		/// <summary>
		/// Takes the new time the user got and compares it to the current one saved.
		/// <para>If the newtime is smaller, ie the player was "faster" updates the current time to the new one </para>
		/// </summary>
		/// <returns>_currentLevel1Time</returns>
		private float UpdateLevel1Time()
		{
			float newTime = NewTime();
			GD.Print($"_currentLevel1Time: {_currentLevel1Time}, NewTime(): {newTime}");
			if (GetCurrentLevel() == "Level1")
			{
				if (_currentLevel1Time > newTime || _currentLevel1Time == 0)
				{
					UpdateHighScore();
					_currentLevel1Time = newTime; // Store the fastest time for Level 1
					GD.Print($"Level 1 Time: {_currentLevel1Time}, UPDATELEVEL1TIME METHOD");
					GD.Print("Current Level: " + GetCurrentLevel());
					GD.Print("UpdateTime Method works, Level 1");
				}
			}
			return _currentLevel1Time;

		}

		/// <summary>
		/// Takes the new time the user got and compares it to the current one saved.
		/// <para>If the newtime is smaller, ie the player was "faster" updates the current time to the new one </para>
		/// </summary>
		/// <returns>_currentLevel2Time</returns>
		private float UpdateLevel2Time()
		{
			float newTime = NewTime();
			GD.Print($"_currentLevel2Time: {_currentLevel2Time}, NewTime(): {newTime}");
			if (GetCurrentLevel() == "Level2")
			{
				if (_currentLevel2Time > newTime || _currentLevel2Time == 0)
				{
					UpdateHighScore();
					_currentLevel2Time = newTime; // Store the fastest time for Level 1
					GD.Print($"Level 2 Time: {_currentLevel2Time}");
					GD.Print("Current Level: " + GetCurrentLevel());
					GD.Print("UpdateTime Method works, Level 2");
				}
			}
			return _currentLevel2Time;
		}

		/// <summary>
		/// Takes the new time the user got and compares it to the current one saved.
		/// <para>If the newtime is smaller, ie the player was "faster" updates the current time to the new one </para>
		/// </summary>
		/// <returns>_currentLevel3Time</returns>
		private float UpdateLevel3Time()
		{
			float newTime = NewTime();
			GD.Print($"_currentLevel3Time: {_currentLevel3Time}, NewTime(): {newTime}");
			if (GetCurrentLevel() == "Level3")
			{
				if (_currentLevel3Time > newTime || _currentLevel3Time == 0)
				{
					UpdateHighScore();
					_currentLevel3Time = newTime; // Store the fastest time for Level 1
					GD.Print($"Level 3 Time: {_currentLevel3Time}");
					GD.Print("Current Level: " + GetCurrentLevel());
					GD.Print("UpdateTime Method works, Level 3");
				}
			}
			return _currentLevel3Time;
		}

		/// <summary>
		/// Adds the best time of each level to the TimeData
		/// <para>Refer to GameSave.cs for more information on how it is used </para>
		/// </summary>
		/// <returns>BestTimeLevel1, BestTimeLevel2, BestTimeLevel3 as data</returns>
		public Dictionary TimeData()
		{
			Dictionary data = new Dictionary();

			if (_currentLevel1Time >= 0)
			{
				data.Add("BestTimeLevel1", _currentLevel1Time);
				GD.Print("Level 1 data added");
			}
			if (_currentLevel2Time >= 0)
			{
				data.Add("BestTimeLevel2", _currentLevel2Time);
				GD.Print("Level 2 data added");
			}
			if (_currentLevel3Time >= 0)
			{
				data.Add("BestTimeLevel3", _currentLevel3Time);
				GD.Print("Level 3 data added");
			}
			return data;
		}
	}
}