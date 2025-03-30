using Godot;
using System;

namespace TruckGame
{
	public partial class GameSave : Resource
	{
		[Export] public int[] CurrentLevelComplete { get; set; }
		[Export] public int CurrentLevelOn { get; set; }
		[Export] public float AudioVolume { get; set; }
		[Export] public int FastestTime { get; set; }

		// [Export] public bool ReverseControls { get; set; }


		public GameSave()
		{
			CurrentLevelComplete = new int[3];
			AudioVolume = 1.0f;
			// ReverseControls = false;

		}
	}
}


