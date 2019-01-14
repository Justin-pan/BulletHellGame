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
    public class Spider : Mob
    {

        public JPTimer spawnTimer;

        public Spider(Vector2 Pos, int OwnerId) : base("2d\\Units\\Mobs\\Spider", Pos, new Vector2(45, 45), OwnerId)
        {
            speed = 1.5f;

            health = 3;
            healthMax = health;

            spawnTimer = new JPTimer(8000);
            spawnTimer.AddToTimer(4000);
        }

        public override void Update(Vector2 Offset, Player Enemy)
        {
            spawnTimer.UpdateTimer();
            if (spawnTimer.Test())
            {
                SpawnEggSac();
                spawnTimer.ResetToZero();
            }

            base.Update(Offset, Enemy);
        }

        public virtual void SpawnEggSac()
        {
            GameGlobals.PassSpawnPoint(new SpiderEggSac(new Vector2(pos.X, pos.Y), ownerId));
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
