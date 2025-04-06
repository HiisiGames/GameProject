using Godot;
using System;
using TruckGame;

public partial class Beam : RigidBody2D
{
	private TriggerZone _triggerZone;
	private bool Triggered = false;
	public override void _Ready()
	{
		this.Freeze = true;
		GD.Print($"triggered = {Triggered}");
		GD.Print($"freeze state: {this.Freeze}");
		GD.Print($"freeze mode: {this.FreezeMode}");
		GD.Print($"sleep state: {this.Sleeping}");
		_triggerZone = GetNode<TriggerZone>("/root/Level2/TriggerZone");
		GD.Print($"is triggerzone null = {_triggerZone == null}");
		_triggerZone.BodyEntered += OnTriggerZoneBodyEntered;


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(Triggered)
		{
			Freeze = false;
			GD.Print($"Setting beam freeze state");
			GD.Print($"Beam freeze state: {this.Freeze}");
			Triggered = false;
		}
	}
	
	//Listens to TriggerZone's BodyEntered signal and sets the boolean Triggered to true, which causes the Freeze property to be set to false.
	public void OnTriggerZoneBodyEntered(Node node) 
	{	
		GD.Print("Vehicle entered the trigger zone");
		if(node is RigidBody2D vehicle)
		{
			GD.Print("Vehicle is RigidBody2D");
			GD.Print($"Beam freeze state: {this.Freeze}");
			Triggered = true;
			GD.Print($"Trigger activated");
		}
	}
}
