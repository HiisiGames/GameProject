using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// Autoloaded, refer to HUDSettings.cs for more information
	/// </summary>
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