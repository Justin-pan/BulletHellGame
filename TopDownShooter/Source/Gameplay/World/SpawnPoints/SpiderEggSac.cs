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
    public class SpiderEggSac : SpawnPoint
    {

        int maxSpawns, totalSpawns;

        public SpiderEggSac(Vector2 Pos, Vector2 Frames, int OwnerId, XElement Data) : base("2d\\SpawnPoints\\EggSac", Pos, new Vector2(45, 45), Frames, OwnerId, Data)
        {
            totalSpawns = 0;
            maxSpawns = 3;

            health = 5;
            healthMax = health;

            spawnTimer = new JPTimer(4000);
        }

        public override void Update(Vector2 Offset, Player Enemy, SquareGrid Grid)
        {

            base.Update(Offset, Enemy, Grid);
        }

        public override void SpawnMob()
        {

            Mob tempMob = new Spiderling(new Vector2(pos.X, pos.Y), new Vector2(1, 1), ownerId);

            if(tempMob != null)
            {
                GameGlobals.PassMob(tempMob);

                totalSpawns++;
                if(totalSpawns >= maxSpawns)
                {
                    dead = true;
                }
            }
        }

        public override void Draw(Vector2 Offset)
        {
            base.Draw(Offset);
        }
    }
}
