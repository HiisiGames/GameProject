using Godot;
using System;

// This file checks if playervehicle is in contact with certain elements (in this case the obstacles)
namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class CollisionDetector : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		//Level2 _level2;
		private bool _isPaused = false;
		[Export] private string _gameOverScenePath = "res://GUI/GameOver.tscn";
		private PackedScene _selectGameOverScene;

		public bool IsPaused
		{
			get { return _isPaused; }
			set { _isPaused = value; }
		}

		public override void _Ready()
		{
			_selectGameOverScene = ResourceLoader.Load<PackedScene>(_gameOverScenePath);

			this.BodyEntered += OnCollisionDetected;
			//_level2 = GetNode<Level2>("Level2");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void OnCollisionDetected(Node node)
		// This checks if the player vehicle touches a "triggerzone"
		{
			GD.Print("Node collided with collision detector");
			if (node.IsInGroup("Obstacles"))
			{
				GD.Print("Collided with obstacle");
				GD.Print("Game over");

				ChangeMusic();
				InstantiateGameOverPanel();


			}
			if (node is StaticBody2D staticBody2D)
			{
				GD.Print("Node is StaticBody2D");
				if (staticBody2D.Name == "Boundary")
				{
					GD.Print("Collided with map boundary");
				}
				else if (staticBody2D.Name == "Terrain")
				{
					ChangeMusic();
					InstantiateGameOverPanel();
				}
			}

		}
		public void InstantiateGameOverPanel()
		// This will instantiate the game over panel
		{
			if (_selectGameOverScene != null)
				{
					Node gameOverPanel = _selectGameOverScene.Instantiate();
					AddChild(gameOverPanel);
					// Adds a child node to the scene tree

					GD.Print("game over panel created");
				}
				else
				{
					GD.Print("Game over scene not found");
				}
		}
		public void ChangeMusic()
		{
			AudioManager.Instantiate.engineSound.StreamPaused = true;
			AudioManager.Instantiate.collideSound.Play();
		}
	}
}

