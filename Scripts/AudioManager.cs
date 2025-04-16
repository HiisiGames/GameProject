using Godot;
using System;
using Godot.Collections;

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
        }
        public void setVolume()
        {
            bgMusic.VolumeDb = 0f;
            engineSound.VolumeDb = 0f;
        }
        public Dictionary SFXData()
		{
			Dictionary data = new Dictionary();
			data.Add("CollideSound", collideSound.VolumeDb);
			data.Add("EngineSound", engineSound.VolumeDb);
			data.Add("VictorySound", victorySound.VolumeDb);
            data.Add("ClickSound", clickButtonSound.VolumeDb);
			return data;
		}
        public Dictionary MusicData()
        {
            Dictionary data = new Dictionary();
            data.Add("bgMusic", bgMusic.VolumeDb);
            return data;
        }

    }


}