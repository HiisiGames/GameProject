using Godot;
using System;

namespace TruckGame
{
	public partial class TriggerZone : Area2D
		{
			private RigidBody2D _beam;
			public void OnTriggerZoneBodyEntered(PhysicsBody2D vehicle) 
			{
				_beam = GetNode<RigidBody2D>("res://");
				_beam.Freeze = false;
			}
		}
}
