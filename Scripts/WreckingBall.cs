using Godot;
using System;

namespace TruckGame
{
    public partial class WreckingBall : RigidBody2D
    {
        private Vector2 _localForcePosition;
        private Vector2 _forceVector;
        private Vector2 _directionVector;
        [Export]
        private float _force = 5.0f;
        public override void _Ready()
        {
            GD.Print($"Wrecking Ball global position: {this.GlobalPosition}");
            Vector2 GlobalForcePosition = GetNode<CollisionShape2D>("CollisionShape2D2").GlobalPosition;
            _localForcePosition = ToLocal(GlobalForcePosition);
            GD.Print($"Ball global position: {GlobalForcePosition}");
            GD.Print($"Ball local position: {_localForcePosition}");
            _directionVector = new Vector2(10, 0);
            _forceVector = _directionVector * _force;
            //RotationDegrees = 90.0f;
            

            //this.ApplyForce(_force, _localForcePosition);

        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            //this.ApplyForce(_forceVector, _localForcePosition);
            //MoveWreckingBall((float)delta);
        }
        public void MoveWreckingBall(float delta)
        {
            GD.Print(_localForcePosition);
            //bool MovingRight = false;
            if(_localForcePosition.X > 200)
            {
                //MovingRight = true;
                _directionVector = new Vector2(10, 0);
                _forceVector = _directionVector * _force;
                //this.ApplyForce(_forceVector, _localForcePosition);
                GD.Print("Moving left");
            }
            else
            {
                //MovingRight = false;
                _directionVector = new Vector2(-10, 0);
                _forceVector = _directionVector * _force;
                this.ApplyForce(_forceVector , _localForcePosition);
                GD.Print("Moving right");
            }
        }
    }
}

