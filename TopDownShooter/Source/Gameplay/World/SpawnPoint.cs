using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    public class SpawnPoint : DestructibleObject
    {

        public JPTimer spawnTimer = new JPTimer(2400);

        public SpawnPoint(string Path, Vector2 Pos, Vector2 Dims, int OwnerId) : base(Path, Pos, Dims, OwnerId)
        {
            dead = false;

            health = 3;
            healthMax = health;

            hitDist = 35.0f;
        }

        public override void Update(Vector2 Offset)
        {
            spawnTimer.UpdateTimer();
            if (spawnTimer.Test())
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }

            base.Update(Offset);
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Imp(new Vector2(pos.X, pos.Y), ownerId));
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
