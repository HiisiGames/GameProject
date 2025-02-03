using Godot;
using System;

public partial class Movement : Node2D
{
	[Export] int _speed = 10; //Perus speed liikkumiselle
	[Export] int _acceleration = 10; //Kiihtyvyys trukille koska ajoneuvot ei mene nollasta sataan samantien.
	[Export] int _slowdown = 10; //Kuinka hitaasti trukki hidstaa kun kaasusta päästää irti.
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Input.IsActionPressed("Break"); //A

		Input.IsActionPressed("Accelerator"); //D
	}
}
