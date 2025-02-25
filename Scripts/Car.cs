using Godot;
using System;
using System.Collections.Generic;

namespace TruckGame
{
	public partial class Car : RigidBody2D
	{
		// Called when the node enters the scene tree for the first time.
		private ArrayList<Wheel> WheelArray = new ArrayList<Wheel>();
		public override void _Ready()
		{
			WheelArray = GetTree().GetNodesInGroup("Wheels");
			
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}

