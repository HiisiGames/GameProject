using Godot;
using System;

namespace TruckGame
{
    public partial class WreckingBall : RigidBody2D
    {
        private Vector2 _localForcePosition;
        private Vector2 _forceVector;
        public override void _Ready()
        {
            GD.Print($"Wrecking Ball global position: {this.GlobalPosition}");
            Vector2 GlobalForcePosition = GetNode<CollisionShape2D>("CollisionShape2D2").GlobalPosition;
            _localForcePosition = ToLocal(GlobalForcePosition);
            GD.Print($"Ball global position: {GlobalForcePosition}");
            GD.Print($"Ball local position: {_localForcePosition}");
        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            //this.ApplyForce();
        }
    }
}

