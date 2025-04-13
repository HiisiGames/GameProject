using Godot;
using System;

namespace TruckGame
{
	public partial class GameState : Node
	{
		public static GameState Instantiate;
		public bool isTheGameOn = false;
		public override void _Ready()
		{
			Instantiate = this;
		}
	}
}