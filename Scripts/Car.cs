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
		[Export] private float _rotationTorque = 10000000f;



		// Called when the node enters the scene tree for the first time.
		private Array<Node> _wheelArray = new Array<Node>();
		private Array<Node> _terrainDetectorArray = new Array<Node>();
		public override void _Ready()
		{
			_wheelArray = GetTree().GetNodesInGroup("wheel");

			GD.Print("Wheel array count after _Ready: " + _wheelArray.Count);  // Check count here

			_terrainDetectorArray = GetTree().GetNodesInGroup("TerrainDetectors");

			GD.Print("Terrain detector array count after _Ready: " + _terrainDetectorArray.Count);  // Check count here
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
		public override void _PhysicsProcess(double delta)
		{
			MovePlayerVehicle((float)delta);
		}

		public void MovementForward(float delta)
		// Make two different methods, one for gas and one for brake
		{
			//D
			if (Input.IsActionPressed("Accelerate"))
			{
				foreach (Node wheelNode in _wheelArray)
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
				foreach (Node wheelNode in _wheelArray)
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
		public void MovementWhileInAir(float delta)
		{
				if (Input.IsActionPressed("Accelerate"))
				{
					GD.Print("Applying torque");
					ApplyTorque(_rotationTorque * (float)delta);
				}
				if (Input.IsActionPressed("Break"))
				{
					GD.Print("Applying negative torque");
					ApplyTorque(-_rotationTorque * (float)delta);
				}
		}
		public void MovePlayerVehicle(float delta)
		{
			if(!IsInAir())
			{
				MovementForward((float)delta);
				MovementBackward((float)delta);
			}
			else
			{
				MovementWhileInAir((float)delta);
			}
		}
		public bool IsInAir()
		{

			foreach(TerrainDetector detector in _terrainDetectorArray)
				if(!detector.IsOnTerrain)
				{
					return true;
				}
			return false;
		}
	}
}

