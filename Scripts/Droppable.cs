using Godot;
using System;

namespace TruckGame
{
    public abstract partial class Droppable : RigidBody2D
    {
        protected void Drop()
        {
            Freeze = false;
        }
    }
}
