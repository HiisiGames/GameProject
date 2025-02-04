using Godot;
using System;
namespace TruckGame
{

	public partial class GameTime : Node
	{
		private float _Time;
		private int _SecondCounter = 00;

		private int _MinuteCounter = 00;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			_Time += (float)delta;
			CountSeconds();


		}

		public void CountSeconds()
		{
			// GD.Print("Alkaa");
			if (_Time >= 1.0f)
			{
				_SecondCounter++;
				if(_SecondCounter >= 60) {
					_MinuteCounter++;
					_SecondCounter -= 60;
				}
				GD.Print(_Time);
				GD.Print($"Time: {_MinuteCounter}:{_SecondCounter}");
				_Time -= 1.0f;
			}

		}
	}
}