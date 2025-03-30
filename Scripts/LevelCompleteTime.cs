using Godot;
using System;

namespace TruckGame
{
	public partial class LevelCompleteTime : Node
	{
		private GameTime _gameTime;

        public override void _Ready()
        {
			_gameTime = GetNode<GameTime>("PlayerVehicle/Camera2D/CanvasLayer/GameTime");

			if (_gameTime != null)
			{
				float  totalTime = _gameTime.TotalTime;
				GD.Print("GAMETIME NODE FOUND");
			}
			else
			{
				GD.Print("Gametime NOT FOUND");
			}

            base._Ready();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
        }
    }
}