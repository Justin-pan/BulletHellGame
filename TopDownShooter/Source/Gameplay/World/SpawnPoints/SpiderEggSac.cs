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

        public SpiderEggSac(Vector2 Pos, int OwnerId) : base("2d\\SpawnPoints\\EggSac", Pos, new Vector2(45, 45), OwnerId)
        {
            totalSpawns = 0;
            maxSpawns = 3;
        }

        public override void Update(Vector2 Offset)
        {

            base.Update(Offset);
        }

        public override void SpawnMob()
        {

            Mob tempMob = new Spiderling(new Vector2(pos.X, pos.Y), ownerId);

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
