using Godot;
using System;

namespace TruckGame
{
	public partial class MoveTruck : Node2D {
		[Export] private float _speed = 1;

		[Export] private Node2D _lapsi = null;
		private void Move(float delta) {
			Vector2 Movevement = Vector2.Zero; //Makes the movement and has it at 0, so it can be called later.

			if(Input.IsActionPressed("MoveRight")) {
				Movevement = Vector2.Right * delta * _speed;
			}
			if(Input.IsActionPressed("MoveLeft")) {
				Movevement = Vector2.Left * delta * _speed;
			}
			if(Input.IsActionPressed("MoveUp")) {
				Movevement = Vector2.Up * delta * _speed;
			}
			if(Input.IsActionPressed("MoveDown")) {
				Movevement = Vector2.Down * delta * _speed;
			}
			_lapsi.Translate(Movevement);
			// GD.Print(_lapsi.Position);
		}

    	// Called when the node enters the scene tree for the first time.
    	public override void _Ready()
		{
			// _lapsi.Position = GlobalPosition;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			Move((float) delta);
		}

	}
}

