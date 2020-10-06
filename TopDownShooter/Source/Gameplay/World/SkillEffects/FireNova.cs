using System;
using Microsoft.Xna.Framework;


namespace TopDownShooter
{
    public class FireNova : Effect2d
    {
        public FireNova(Vector2 Pos, Vector2 Dims, int MSec) : base("2d\\Skills\\FireNova", Pos, Dims, new Vector2(1, 1), MSec)
        {
            
        }

        public override void Update(Vector2 Offset)
        {
            rot += (float)Math.PI*2.0f/60.0f;

            base.Update(Offset);
        }
    }
}