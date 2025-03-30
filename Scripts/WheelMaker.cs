using Godot;
using System;

namespace TruckGame
{
public partial class WheelMaker : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 Sprite2D sprite = new Sprite2D();

        // Ladataan tekstuuri ja asetetaan se spritelle
        Texture2D texture = ResourceLoader.Load<Texture2D>("res://Arts/TutorialAssets/Images/Car/WheelBig.png");
        sprite.Texture = texture;

        // Asetetaan spriten sijainti
        sprite.GlobalPosition = new Vector2(0, 0);

        // Lisätään sprite juuriskenen lapseksi
        AddChild(sprite);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
}
