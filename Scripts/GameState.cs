using Godot;
using System;

namespace TruckGame
{
	public partial class GameState : Node
	{
		public static GameState InstanceGameState;
		public bool isTheGameOn = false;
		public override void _Ready()
		{
			InstanceGameState = this;
		}
	}
}