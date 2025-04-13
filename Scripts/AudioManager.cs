using Godot;
using System;

namespace TruckGame

{
    public partial class AudioManager : Node
    {
        public static AudioManager Instance;
        public Node SFX;
        public Node Music;
        public AudioStreamPlayer bgMusic;
        public AudioStreamPlayer clickButtonSound;
        public AudioStreamPlayer engineSound;
        public AudioStreamPlayer collideSound;
        public bool isEngineOn = false;


        public override void _Ready()
        {
            Instance = this;

            bgMusic = GetNode<AudioStreamPlayer>("Music/BackgroundMusic");
            clickButtonSound = GetNode<AudioStreamPlayer>("SFX/ClickButtonSound");
            engineSound = GetNode<AudioStreamPlayer>("SFX/EngineSound");
            // Engine();
        }
        // public void Engine()
        // {
        //     if (isEngineOn == true)
        //     {
        //         engineSound = GetNode<AudioStreamPlayer>("SFX/EngineSound");
        //         engineSound.Play();
        //     }
        // }
    }


}