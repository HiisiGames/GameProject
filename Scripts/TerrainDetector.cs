using Godot;
using System;

namespace TruckGame
{
	public partial class TerrainDetector : RayCast2D
	{
		private bool _isOnTerrain = true;
		[Export] 
		private string _gameOverScenePath = "res://GUI/GameOver.tscn";
		private PackedScene _gameOverScene;

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

		// Called every frame. 'delta' is the elapsed time since the previous frame.
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
