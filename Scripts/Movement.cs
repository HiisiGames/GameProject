using Godot;
using System;

namespace TruckGame {

	public partial class Movement : Sprite2D
	{
		float _PositionY = 0.0f;
		float _PositionX = 0.0f;
		[Export] float _speed = 10.0f; //Perus speed liikkumiselle
		[Export] float _acceleration = 10.0f; //Kiihtyvyys trukille koska ajoneuvot ei mene nollasta sataan samantien.
		[Export] float _slowdown = 10.0f; //Kuinka hitaasti trukki hidstaa kun kaasusta päästää irti.
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			GlobalPosition = new Vector2(_PositionX, _PositionY);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{	
			_Movement(delta);
		}
		public void _Movement(double delta)
		{
			//A
			if(Input.IsActionPressed("Break"))
			{
				GD.Print("vasemmalle");
				Vector2 Move = Vector2.Left;
				GlobalPosition += _speed * Move * (float)delta;
			}

			//D
			else if(Input.IsActionPressed("Accelerate"))
			{
				GD.Print("Oikealle");
				Vector2 Move = Vector2.Right;
				GlobalPosition += _speed * Move * (float)delta;
			} 
		}
	}
}
