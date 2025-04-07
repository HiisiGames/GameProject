using Godot;
using System;

namespace TruckGame
{
    public partial class Crate : Droppable
    {
        private TriggerZone _triggerZone;
        private bool Triggered = false;
        public override void _Ready()
        {
            Freeze = true;
            GD.Print($"triggered = {Triggered}");
		    GD.Print($"freeze state: {this.Freeze}");
		    GD.Print($"freeze mode: {this.FreezeMode}");
		    GD.Print($"sleep state: {this.Sleeping}");
            _triggerZone = GetNode<TriggerZone>("/root/Level1/TriggerZone");
            _triggerZone.BodyEntered += OnTriggerZoneBodyEntered;
        }
        public override void _PhysicsProcess(double delta)
        {
            if(Triggered)
            {
                Drop();
                GD.Print($"Setting crate freeze state");
                GD.Print($"Crate freeze state: {this.Freeze}");
                Triggered = false;
            }
        }
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
}


