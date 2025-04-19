using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// This class is used in scripts to refer to its signals, while distinguishing it from other partial classes of Area2D.
	/// </summary>
	public partial class TriggerZone : Area2D
	{
		//private Beam _beam;
		public override void _Ready()
		{
			/*_beam = GetNode<Beam>("/root/Level2/Beam");
			GD.Print($"is beam null = {_beam == null}");
			this.BodyEntered += OnTriggerZoneBodyEntered;*/
		}
		//public void
		/*public void OnTriggerZoneBodyEntered(Node node)
		{
			GD.Print("Vehicle entered the trigger zone");
			if(node is RigidBody2D vehicle)
			{
				GD.Print("Vehicle is RigidBody2D");
				if(_beam != null)
				{
					GD.Print($"Beam freeze state: {_beam.Freeze}");
					_beam.UnFreeze();
					GD.Print($"Setting beam freeze state");
					GD.Print($"Beam freeze state: {_beam.Freeze}");
				}
				else
				{
					GD.Print("Beam not found!");
				}
			}
		}*/
	}
}
