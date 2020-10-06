using System;
using Microsoft.Xna.Framework;

namespace TopDownShooter
{
    public class TargetCircle : Effect2d
    {
        public TargetCircle(Vector2 Pos, Vector2 Dims) : base("2d\\Skills\\TargetCircle", Pos, Dims, new Vector2(1, 1), 400)
        {
            noTimer = true;
        }
    }
}