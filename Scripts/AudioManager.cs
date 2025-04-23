using Godot;
using System;
using Godot.Collections;

namespace TruckGame
{
    /// <summary>
    /// This file is for all the sounds + music for the game. Its autoloaded in the Godot project settings.
    /// <para> Scenes/Audiomanager.tscn </para>
    /// </summary>
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
        private float _wheelsAngularVelocity;
        private float _enginePitch;
        private float _idleEnginePitch = 0.7f;
        private Car _playerVehicle;
        
        public override void _Ready()
        {
            Instantiate = this;

            bgMusic = GetNode<AudioStreamPlayer>("Music/BackgroundMusic");
            clickButtonSound = GetNode<AudioStreamPlayer>("SFX/ClickButtonSound");
            engineSound = GetNode<AudioStreamPlayer>("SFX/EngineSound");
            collideSound = GetNode<AudioStreamPlayer>("SFX/CollideSound");
            victorySound = GetNode<AudioStreamPlayer>("SFX/VictorySound");
        }

        /// <summary>
        /// Holds the sound effects data
        /// <para>Refer to GameSave.cs to more information on how it's used </para>
        /// </summary>
        /// <returns>.VolumeDb from
        /// <para>Enginesound</para>
        /// <para>VictorySound</para>
        /// <para>ClickSound</para>
        /// <para>CollideSound</para></returns>
        public Dictionary SFXData()
		{
			Dictionary data = new Dictionary();
			data.Add("CollideSound", collideSound.VolumeDb);
			data.Add("EngineSound", engineSound.VolumeDb);
			data.Add("VictorySound", victorySound.VolumeDb);
            data.Add("ClickSound", clickButtonSound.VolumeDb);
			return data;
		}

        /// <summary>
        /// Holds the music data
        /// <para>Refer to GameSave.cs to more information on how it's used </para>
        /// </summary>
        /// <returns>VolumeDb from
        /// bgMusic
        /// </returns>
        public Dictionary MusicData()
        {
            Dictionary data = new Dictionary();
            data.Add("bgMusic", bgMusic.VolumeDb);
            return data;
        }
        public override void _Process(double delta)
        {
            ChangeEnginePitch();
        }
        public void ChangeEnginePitch()
        {
            Node CurrentScene = GetTree().CurrentScene;
            _playerVehicle = GetTree().CurrentScene.GetNode<Car>("PlayerVehicle");
            if(_playerVehicle != null)
            {
                _wheelsAngularVelocity = Math.Abs(_playerVehicle.GetWheelsAngularVelocity());
            }
            else
            {
                GD.Print("PlayerVehicle was null when ChangeEnginePitch was called");
            }
            _enginePitch = Mathf.Clamp(_idleEnginePitch + _wheelsAngularVelocity * 0.03f, 0.8f, 2.2f);
            engineSound.PitchScale = _enginePitch;
        }
    }
}