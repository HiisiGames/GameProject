using Godot;
using System;

public partial class CollisionDetector : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += OnCollisionDetected;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnCollisionDetected(Node node)
	{
		GD.Print("Node collided with collision detector");
		if(node is Beam beam)
		{	
			GD.Print("Collided with beam");
			GD.Print("Game over");
		}
		if(node is StaticBody2D staticBody2D)
		{
			GD.Print("Node is StaticBody2D");
			if(staticBody2D.Name == "Boundary")
			{
				GD.Print("Collided with map boundary");
			}
			else if(staticBody2D.Name == "Terrain")
			{
				GD.Print("Collided with terrain");
				GD.Print("Game Over");
			}
		}

	}
}
