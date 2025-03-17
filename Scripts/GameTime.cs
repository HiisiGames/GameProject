using Godot;
using System;
namespace TruckGame
{
	public partial class GameTime : Node
	{
		// TIMER WORKS DONT TOUCH
		private float _Time;
		private int _SecondCounter = 00;
		private int _MinuteCounter = 00;
		private Label _gameTimeLabel;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
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
			_Time += (float)delta;
			CountSeconds();
		}

		public void CountSeconds() // This counts seconds
		{
			// GD.Print("Alkaa");
			if (_Time >= 1.0f)
			{
				_SecondCounter++;
				if (_SecondCounter >= 60)
				{
					_MinuteCounter++;
					_SecondCounter -= 60;
				}
				GD.Print(_Time);
				GD.Print($"Time: {_MinuteCounter}:{_SecondCounter}");
				_gameTimeLabel.Text = $"Time: {_MinuteCounter:D2}:{_SecondCounter:D2}";
				_Time -= 1.0f;
			}
			// _gameTimeLabel.Text = $"Time: {_MinuteCounter:D2}:{_SecondCounter:D2}";
			// _Time -= 1.0f;
			// GD.Print(_Time);
			// GD.Print($"Time: {_MinuteCounter}:{_SecondCounter}");


		}

	}
}
