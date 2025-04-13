using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	///
	/// </summary>
	public partial class FinishLine : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		//Level2 _level2;
		// private bool _isPaused = false;

		/// <summary>
		/// The path to where the level complete is located in the project directory
		/// </summary>
		[Export] private string _levelCompletePath = "res://GUI/LevelComplete.tscn";
		private PackedScene _selectLevelCompleteScene;

		// public bool IsPaused
		// {
		// 	get { return _isPaused; }
		// 	set { _isPaused = value; }
		// }

		public override void _Ready()
		{
			_selectLevelCompleteScene = ResourceLoader.Load<PackedScene>(_levelCompletePath);
			// Loads up the level complete scene to be used

			this.BodyEntered += OnCollisionDetected;
			//_level2 = GetNode<Level2>("Level2");
		}


		/// <summary>
		/// This method checks if the area (finish line) is in touch with the playerVehicle
		/// </summary>

		public void OnCollisionDetected(Node node)
		// Similar to "CollisionDetector.cs", checks if certain element, in this case the finish line is touching the player vehicle
		{
			GD.Print("Node collided with Finish Line");
			// GD.Print("Node type: ", node.GetType());

			if (node is RigidBody2D Car)
			// If the node is rigidbody2D Car, execute the code
			{
				GD.Print("Collided with the finish line");
				GD.Print("Game won");

				InstantiateLevelComplete();

				// if (_selectLevelCompleteScene != null) // This brings up the level complete scene
				// {
				// 	Node LevelCompletePanel = _selectLevelCompleteScene.Instantiate();
				// 	//LevelCompletePanel.Name = "LevelCompletePanel";
				// 	AddChild(LevelCompletePanel);

				// 	GD.Print("Level complete panel created");
				// }
				// else
				// {
				// 	GD.Print("Level complete scene not found");
				// }
			}
		}

		/// <summary>
		/// This method instantiates the level complete scene and adds it as child node to the current scene
		/// </summary>
		private void InstantiateLevelComplete()
		{
			if (_selectLevelCompleteScene != null) // This brings up the level complete scene
				{
					Node LevelCompletePanel = _selectLevelCompleteScene.Instantiate();
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

