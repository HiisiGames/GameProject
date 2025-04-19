using Godot;
using System;

namespace TruckGame
{
	/// <summary>
	/// This class is used to detect whether the PlayerVehicle is colliding with terrain or not, based on which different movement methods are called in Car.cs.
	/// </summary>
	public partial class TerrainDetector : RayCast2D
	{
		//A boolean value used to determine, whether PlayerVehicle is colliding with terrain or not.
		private bool _isOnTerrain = true;
		//The file manager path of GameOver - scene.
		[Export] 
		private string _gameOverScenePath = "res://GUI/GameOver.tscn";
		//A reference to the GameOver - scene.
		private PackedScene _gameOverScene;

		//Public property used to access _isOnTerrain outside of TerrainDetector - class.
		public bool IsOnTerrain
		{
			get { return _isOnTerrain; }
			set { _isOnTerrain = value; }
		}

		// Called when the node enters the scene tree for the first time. 
		public override void _Ready()
		{
			_gameOverScene = ResourceLoader.Load<PackedScene>(_gameOverScenePath);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame. Checks whether IsColliding() returns true or false every physics frame.
		// If true, gets the collider and checks whether it is terrain or an obstacle. If the collider is an obstacle, instantiates GameOver - scene.
		public override void _PhysicsProcess(double delta)
		{
			if(IsColliding())
			{
				var Collider = (Node)GetCollider();
				if(Collider.Name == "Terrain")
				{
					//GD.Print("Collided with terrain");
					_isOnTerrain = true;
				}
				else if(Collider.IsInGroup("Obstacles"))
				{
					InstantiateGameOverPanel();
				}
			}
			else
			{
				_isOnTerrain = false;
				//GD.Print("Not colliding with terrain");
			}
		}
		// Makes an instance of GameOver - scene and adds it to the scene tree as a child.
		public void InstantiateGameOverPanel()
		{
			if (_gameOverScene != null)
				{
					Node gameOverPanel = _gameOverScene.Instantiate();
					//gameOverPanel.Name = "GameOverPanel";
					AddChild(gameOverPanel);

					GD.Print("game over panel created");
				}
				else
				{
					GD.Print("Game over scene not found");
				}
		}
	}
}
