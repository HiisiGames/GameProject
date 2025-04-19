using Godot;
using System;

namespace TruckGame
{
    /// <summary>
	/// Droppable obstacles in the game inherit from this class.
	/// </summary>
    public abstract partial class Droppable : RigidBody2D
    {
        protected bool Triggered = false;
        [Export] protected TriggerZone _triggerZone;
        //Drops the object by setting its Freeze property to false.
        protected void OnReady()
        {
            Freeze = true;
            GD.Print($"triggered = {Triggered}");
		    GD.Print($"freeze state: {Freeze}");
		    GD.Print($"freeze mode: {FreezeMode}");
		    GD.Print($"sleep state: {Sleeping}");
            //_triggerZone = GetNode<TriggerZone>("/root/Level1/TriggerZone");
            _triggerZone.BodyEntered += OnTriggerZoneBodyEntered;
        }
        protected void Drop()
        {
            Freeze = false;
            GD.Print($"Setting {this.Name} freeze state");
            GD.Print($"Crate {this.Name} state: {this.Freeze}");
            Triggered = false;
        }
        protected void OnTriggerZoneBodyEntered(Node node) 
	    {	
            GD.Print("Vehicle entered the trigger zone");
            if(node is RigidBody2D vehicle)
            {
                GD.Print("Vehicle is RigidBody2D");
                GD.Print($"{this.Name} freeze state: {this.Freeze}");
                Triggered = true;
                GD.Print($"Trigger activated");
            }
	    }
    }
}
