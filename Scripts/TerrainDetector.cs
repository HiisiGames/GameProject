using Godot;
using System;

namespace TruckGame
{
	public partial class TerrainDetector : RayCast2D
	{
		private bool _isOnTerrain = true;

		public bool IsOnTerrain
		{
			get { return _isOnTerrain; }
			set { _isOnTerrain = value; }
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			if(IsColliding())
			{
				var Collider = (Node)GetCollider();
				if(Collider.Name == "Terrain")
				{
					//GD.Print("Collided with terrain");
					_isOnTerrain = true;
				}
			}
			else
			{
				_isOnTerrain = false;
				//GD.Print("Not colliding with terrain");
			}
		}
	}
}
