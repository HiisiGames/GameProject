using Godot;
using System;

namespace TruckGame
{
	public partial class RayCast2d : RayCast2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			if(IsColliding())
			{
				var Collider = (Node)GetCollider();
				if(Collider.Name == "Terrain")
				{

				}
			}
		}
	}
}
