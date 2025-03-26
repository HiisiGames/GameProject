using Godot;
using System;

namespace TruckGame
{
	public partial class FinishLine : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		//Level2 _level2;
		private bool _isPaused = false;
		[Export] private string _levelCompletePath = "res://GUI/LevelComplete.tscn";
		private PackedScene _selectLevelCompleteScene;

		public bool IsPaused
		{
			get { return _isPaused; }
			set { _isPaused = value; }
		}

		public override void _Ready()
		{
			_selectLevelCompleteScene = ResourceLoader.Load<PackedScene>(_levelCompletePath);

			this.BodyEntered += OnCollisionDetected;
			//_level2 = GetNode<Level2>("Level2");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void OnCollisionDetected(Node node)
		{
			GD.Print("Node collided with Finish Line");
            GD.Print("Node type: ", node.GetType());

			if (node is RigidBody2D Car)

			{
				GD.Print("Collided with the finish line");
				GD.Print("Game won");

				if (_selectLevelCompleteScene != null)
				{
					Node LevelCompletePanel = _selectLevelCompleteScene.Instantiate();
					LevelCompletePanel.Name = "LevelCompletePanel";
					AddChild(LevelCompletePanel);

					GD.Print("Level complete panel created");
				}
				else
				{
					GD.Print("Level complete scene not found");
				}

			}

		}
	}
}

