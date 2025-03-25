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
        private float _force = 10.0f;
        public override void _Ready()
        {
            GD.Print($"Wrecking Ball global position: {this.GlobalPosition}");
            Vector2 GlobalForcePosition = GetNode<CollisionShape2D>("CollisionShape2D2").GlobalPosition;
            _localForcePosition = ToLocal(GlobalForcePosition);
            GD.Print($"Ball global position: {GlobalForcePosition}");
            GD.Print($"Ball local position: {_localForcePosition}");
            _directionVector = new Vector2(100, 0);
            _forceVector = _directionVector * _force;
            

            //this.ApplyForce(_force, _localForcePosition);

        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            //this.ApplyForce(_forceVector, _localForcePosition);
            MoveWreckingBall((float)delta);
        }
        public void MoveWreckingBall(float delta)
        {
            if(_localForcePosition.X < 400)
            {
                _directionVector = new Vector2(-100, 0);
                _forceVector = _directionVector * _force;
                this.ApplyImpulse(_forceVector, _localForcePosition);
            }
            else
            {
                _directionVector = new Vector2(100, 0);
                _forceVector = _directionVector * _force;
                 this.ApplyImpulse(_forceVector , _localForcePosition);
            }
        }
    }
}

