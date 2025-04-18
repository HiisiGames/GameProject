using Godot;
using System;

namespace TruckGame
{
    /// <summary>
    ///
    /// </summary>
    public partial class WreckingBall : RigidBody2D
    {
        private Vector2 _globalBallPosition;
        private Vector2 _currentBallPosition;
        [Export]
        private float _force = 100.0f;
        [Export]
        private TriggerZone _triggerZone;
	    private bool Triggered = false;
        private StaticBody2D _wreckingBallParent;
        private PinJoint2D _wreckingBallJoint;
        private Timer _timer;
        private bool _hasTimerBeenStarted = false;
        [Export] private float _timeOutSeconds = 6.0f;

        public override void _Ready()
        {
            _wreckingBallParent = GetParent<StaticBody2D>();
            _wreckingBallJoint = _wreckingBallParent.GetNode<PinJoint2D>("WreckingBallJoint");
            GD.Print($"Wrecking Ball global position: {this.GlobalPosition}");
            _globalBallPosition = GetNode<CollisionShape2D>("CollisionShape2D2").GlobalPosition;

            GD.Print($"Ball global position: {_globalBallPosition}");
            RotationDegrees = -90.0f;
            Freeze = true;
            CreateTimer();
            _timer.Timeout += OnTimeOut;
            _triggerZone.BodyEntered += OnTriggerZoneBodyEntered;

        }
        public override void _PhysicsProcess(double delta)
        {
            if(Triggered)
            {
                Freeze = false;
                GD.Print($"Setting wrecking ball freeze state");
                GD.Print($"Wrecking ball freeze state: {this.Freeze}");
                if(!_hasTimerBeenStarted)
                {
                    StartTimer();
                }
                Triggered = false;
            }
        }

        //Listens to TriggerZone's BodyEntered signal and sets the boolean Triggered to true, which causes the Freeze property to be set to false.
	    public void OnTriggerZoneBodyEntered(Node node)
		{
			GD.Print("Vehicle entered the trigger zone");
			if(node is RigidBody2D vehicle)
			{
				GD.Print("Vehicle is RigidBody2D");
				GD.Print($"WreckingBall freeze state: {this.Freeze}");
				Triggered = true;
				GD.Print($"Trigger activated");
			}
	    }
        public void OnTimeOut()
        {
            GD.Print("OnTimeOut() was called");
            if(_wreckingBallJoint != null)
            {
                _wreckingBallJoint.NodeA = new NodePath();
                _wreckingBallJoint.NodeB = new NodePath();
            }
            else
            {
                GD.Print("_wreckingBallJoint not found!");
            }
            GD.Print("Deleted wrecking ball joint");
            if(this.GlobalPosition.Y > 5000)
            {
                GD.Print("Deleting wrecking ball");
                this.QueueFree();
            }
        }
        public void CreateTimer()
        {
            _timer = new Timer();
            _timer.WaitTime = _timeOutSeconds;
            _timer.OneShot = true;
            AddChild(_timer);
            GD.Print("Wrecking ball timer created");
        }
        public void StartTimer()
        {
            _hasTimerBeenStarted = true;
            _timer.Start();
            GD.Print("Wrecking ball timer started");
        }
    }
}

