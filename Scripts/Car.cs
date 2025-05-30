using Godot;
using System;
using Godot.Collections;


namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class Car : RigidBody2D
	{
		[Export] private float _wheelTorque = 60000f;

		[Export] private float _maxAngularVelocity = 100f;
		[Export] private float _maxAngularVelocityInAir = 200f;
		[Export] private float _bodyTorque = 5000000f;
		private float _editorMaxSpeed;
		private float _tempWheelAngularVelocity;
		private float _wheelsAngularVelocity;



		// Called when the node enters the scene tree for the first time.
		private Array<Node> _wheelArray = new Array<Node>();
		private Array<Node> _terrainDetectorArray = new Array<Node>();
		public override void _Ready()
		{
			_editorMaxSpeed = _maxAngularVelocity;
			_wheelArray = GetTree().GetNodesInGroup("wheel");

			GD.Print("Wheel array count after _Ready: " + _wheelArray.Count);  // Check count here

			_terrainDetectorArray = GetTree().GetNodesInGroup("TerrainDetectors");

			GD.Print("Terrain detector array count after _Ready: " + _terrainDetectorArray.Count);  // Check count here
			GD.Print($"Car global position: {GlobalPosition}");
			GD.Print($"Car center of mass: {ToGlobal(CenterOfMass)}");
		}

		public override void _PhysicsProcess(double delta)
		{
			MovePlayerVehicle((float)delta);
			GetWheelsAngularVelocity();
			//GD.Print($"Wheels angular velocity: {_wheelsAngularVelocity}");
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
						if (wheel.AngularVelocity < _maxAngularVelocity)
						{
							//GD.Print("Accel toimii");
							wheel.ApplyTorqueImpulse(_wheelTorque * (float)delta);
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
						if (wheel.AngularVelocity > -_maxAngularVelocity)
						{
							//GD.Print("break toimii");
							wheel.ApplyTorqueImpulse(-_wheelTorque * (float)delta);
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
					ApplyTorque(-_bodyTorque * (float)delta);
				}
				if (Input.IsActionPressed("Break"))
				{
					GD.Print("Applying negative torque");
					ApplyTorque(_bodyTorque * (float)delta);
				}
		}
		public void MovePlayerVehicle(float delta)
		{
			if(!IsInAir())
			{
				_maxAngularVelocity = _editorMaxSpeed;
				MovementForward((float)delta);
				MovementBackward((float)delta);
			}
			else
			{
				_maxAngularVelocity = _maxAngularVelocityInAir;
				MovementWhileInAir((float)delta);
				MovementForward((float)delta);
				MovementBackward((float)delta);
			}
		}
		public bool IsInAir()
		{

			foreach(TerrainDetector detector in _terrainDetectorArray)
				if(detector.IsOnTerrain)
				{
					return false;
				}
			return true;
		}
		
		public float GetWheelsAngularVelocity()
		{
			_tempWheelAngularVelocity = 0f;
			foreach (Node wheelNode in _wheelArray)
				{
					//GD.Print("break 2");
					if (wheelNode is RigidBody2D wheel)
					{
						_tempWheelAngularVelocity += Math.Abs(wheel.AngularVelocity);
					}
					else
					{
						GD.Print("Could not get wheels angular velocity");
					}
				}
			if(_wheelArray.Count > 0)
			{
				_wheelsAngularVelocity = _tempWheelAngularVelocity / _wheelArray.Count;
			}
			else
			{
				_wheelsAngularVelocity = 0f;
			}
			return _wheelsAngularVelocity;
		}
	}
}

