using Godot;
using System;
using Godot.Collections;


//TODO: Change acceleration mechanism from torqueimpulse

namespace TruckGame
{
	public partial class Car : RigidBody2D
	{
		[Export] private float _speed = 60000f;

		[Export] private float _maxSpeed = 100f;


		// Called when the node enters the scene tree for the first time.
		private Array<Node> WheelArray = new Array<Node>();
		public override void _Ready()
		{
			WheelArray = GetTree().GetNodesInGroup("wheel");

			GD.Print("Wheel array count after _Ready: " + WheelArray.Count);  // Check count here

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		public override void _PhysicsProcess(double delta)
		{
			MovementForward((float)delta);
			MovementBackward((float)delta);
		}

		public void MovementForward(float delta)
		// Make two different methods, one for gas and one for brake
		{
			//D
			if (Input.IsActionPressed("Accelerate"))
			{
				foreach (Node wheelNode in WheelArray)
				{
					//GD.Print("Accel 1 ");
					if (wheelNode is RigidBody2D wheel)
					{
						//GD.Print("Accel 2");
						if (wheel.AngularVelocity < _maxSpeed)
						{
							//GD.Print("Accel toimii");
							wheel.ApplyTorqueImpulse(_speed * (float)delta);
						}
					}
				}
			}
		}
		public void MovementBackward(float delta)
		{
			//A
			if (Input.IsActionPressed("Break"))
			{
				//GD.Print("break 1");
				foreach (Node wheelNode in WheelArray)
				{
					//GD.Print("break 2");
					if (wheelNode is RigidBody2D wheel)
					{
						if (wheel.AngularVelocity > -_maxSpeed)
						{
							//GD.Print("break toimii");
							wheel.ApplyTorqueImpulse(-_speed * (float)delta);
						}
					}
				}
			}
		}
	}
}

