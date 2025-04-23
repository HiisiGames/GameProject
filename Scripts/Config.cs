using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// Creates savefolder + the names for different save files, can be called from different parts of the code.
	/// <para>Used in GameSave.cs </para>
	/// </summary>
	public static class Config
	{
		public const string SaveFolder = "Save";
		public const string AudioSaveFile = "saveAudio.json";
		public const string AutoSaveFile = "autoSave.json";
	}
}