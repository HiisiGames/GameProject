using Godot;
using System;

namespace TruckGame
{
    public partial class WreckingBall : RigidBody2D
    {
        private Vector2 _globalBallPosition;
        private Vector2 _currentBallPosition;
        private Vector2 _forcePosition;
        private Vector2 _forceVector;
        private Vector2 _directionVector;
        [Export]
        private float _force = 100.0f;
        public override void _Ready()
        {
            GD.Print($"Wrecking Ball global position: {this.GlobalPosition}");
            _globalBallPosition = GetNode<CollisionShape2D>("CollisionShape2D2").GlobalPosition;
            
            GD.Print($"Ball global position: {_globalBallPosition}");
            _forcePosition = new Vector2(0, 0);
            _directionVector = new Vector2(10, 0);
            _forceVector = _directionVector * _force;
            RotationDegrees = 90.0f;
            

        }
        public override void _PhysicsProcess(double delta)
        {
            //MoveWreckingBall((float)delta);
        }
        public void MoveWreckingBall(float delta)
        {
            GD.Print(_globalBallPosition);
            //bool MovingRight = false;
            if(this.Position.X > 200)
            {
                //MovingRight = true;
                _directionVector = new Vector2(10, 0);
                _forceVector = _directionVector * _force;
                this.ApplyTorqueImpulse(_force);
                GD.Print("Moving right");
            }
            else if(this.Position.X < 200)
            {
                //MovingRight = false;
                _directionVector = new Vector2(-10, 0);
                _forceVector = _directionVector * _force;
                this.ApplyTorqueImpulse(_force);
                GD.Print("Moving left");
            }
        }
    }
}

