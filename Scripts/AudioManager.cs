// using Godot;
// using System;

// namespace TruckGame
// {
// 	public partial class AudioManager : Node
// 	{
// 		[Export] public AudioStreamPlayer _musicPlayer;
// 		[Export] private string _busName = "Music";
// 		private Slider _volumeSlider;
// 		private int _busIndex = 0;

// 		public override void _Ready()
// 		{
// 			_volumeSlider.Connect(Slider.SignalName.ValueChanged,
// 				new Callable(this, nameof(SetVolume)));
// 		}

// 		public override void _Process(double delta)
// 		{

// 		}
// 		public void SetVolume(float volume)
// 		{
// 			if (data == null)


// 			// Set volume for the Music bus
// 			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(_busName), volumeDb);
// 		}
// 	}
// }