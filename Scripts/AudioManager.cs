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
            engineSound.VolumeDb = -10.0f;
            bgMusic.VolumeDb = 20.0f;
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