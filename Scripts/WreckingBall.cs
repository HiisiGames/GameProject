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
        //Reference to the TriggerZone node, the BodyEntered signal of which is used to trigger OnTriggerZoneBodyEntered - method.
        [Export]
        private TriggerZone _triggerZone;
        //The boolean which is checked in _PhysicsProcess(). If true, causes this to start moving.
	    private bool Triggered = false;
        //Reference to the parent StaticBody2D node.
        private StaticBody2D _wreckingBallParent;
        //Reference to the sibling PinJoint2D node.
        private PinJoint2D _wreckingBallJoint;
        //An instance of Timer, the Timeout signal of which is used to detach this from _wreckingBallJoint.
        private Timer _timer;
        //A boolean which is used to make sure that _timer isn't started every frame in _PhysicsProcess().
        private bool _hasTimerBeenStarted = false;
        //The time in seconds it takes for _timer to send the Timeoutsignal.
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
        //Checks Triggered every physics frame
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
        
        //Listens to TriggerZone's BodyEntered signal and sets the boolean Triggered to true, which causes the Freeze property to be set to false in _PhysicsProcess().
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
        //Creates an instance of class Timer and sets its properties WaitTime and OneShot, which means the timer doesn't restart after Timeout.
        public void CreateTimer()
        {
            _timer = new Timer();
            _timer.WaitTime = _timeOutSeconds;
            _timer.OneShot = true;
            AddChild(_timer);
            GD.Print("Wrecking ball timer created");
        }
        //Sets the boolean _hasTimerBeenStarted to true and starts _timer via its Start() method.
        public void StartTimer()
        {
            _hasTimerBeenStarted = true;
            _timer.Start();
            GD.Print("Wrecking ball timer started");
        }
    }
}

