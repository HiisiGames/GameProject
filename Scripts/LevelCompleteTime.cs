using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class LevelCompleteTime : Node
	{
		private GameTime _gameTime;
		private Label _levelCompleteTimer;
		private Sprite2D _starOne;
		private Sprite2D _starTwo;
		private Sprite2D _starThree;
		private int _starsAtTheEnd;

		public override void _Ready()
		{
			// GD.Print(GetTree().CurrentScene.Name);
			Node currentScene = GetTree().CurrentScene;

			_gameTime =  currentScene.GetNode<GameTime>("PlayerVehicle/Camera2D/CanvasLayer"); //Finds the node with the script

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
		}

		public override void _Process(double delta)
		{

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
			}
		}
	}
}