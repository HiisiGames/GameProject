using Godot;
using System;

namespace TruckGame

{
    public partial class AudioManager : Node
    {
        public static AudioManager InstanceAudioManager;
        public AudioStreamPlayer Music;
        public AudioStreamPlayer SFX;

        public override void _Ready()
        {
            InstanceAudioManager = this;

            Music = GetNode<AudioStreamPlayer>("Music");
            SFX = GetNode<AudioStreamPlayer>("SFX");
        }
    }

}