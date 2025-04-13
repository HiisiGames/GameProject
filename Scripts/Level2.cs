using Godot;
using System;

namespace TruckGame
{
    public partial class Level2 : Node2D
    {
        CollisionDetector _collisionDetector;
        public override void _Ready()
        {
            AudioManager.Instance.bgMusic.StreamPaused = true;

            _collisionDetector = GetNode<CollisionDetector>("/root/Level2/PlayerVehicle/CollisionDetector");
            if(_collisionDetector == null)
            {
                GD.Print("Collision Detector not found");
            }
            GD.Print($"CollisionDetector _isPaused state: {_collisionDetector.IsPaused}");
        }
        public override void _Process(double delta)
        {
            if(_collisionDetector.IsPaused)
            {
                PauseGame();
                GD.Print("PauseGame was called in Level2 _Process");
                _collisionDetector.IsPaused = false;
            }
        }
        private void PauseGame()
        {
            GetTree().Paused = true;
            GD.Print(".Paused was set to true");
        }
        /*public void IsPaused(bool input)
        {
            GD.Print("IsPaused was called");
            _isPaused = input;
        }*/
    }
}

