using Godot;
using System;

namespace TruckGame

{
    public partial class AudioManager : Node
    {
        public static AudioManager Instantiate;
        public Node SFX;
        public Node Music;
        public AudioStreamPlayer bgMusic;
        public AudioStreamPlayer clickButtonSound;
        public AudioStreamPlayer engineSound;
        public AudioStreamPlayer collideSound;
        public AudioStreamPlayer victorySound;
        public bool isEngineOn = false;


        public override void _Ready()
        {
            Instantiate = this;

            bgMusic = GetNode<AudioStreamPlayer>("Music/BackgroundMusic");
            clickButtonSound = GetNode<AudioStreamPlayer>("SFX/ClickButtonSound");
            engineSound = GetNode<AudioStreamPlayer>("SFX/EngineSound");
            collideSound = GetNode<AudioStreamPlayer>("SFX/CollideSound");
            victorySound = GetNode<AudioStreamPlayer>("SFX/VictorySound");
            engineSound.VolumeDb = -10.0f;
            bgMusic.VolumeDb = 20.0f;
        }

    }


}