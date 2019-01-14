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
    public class AIPlayer : Player
    {
        public AIPlayer() : base()
        {

            spawnPoints.Add(new SpawnPoint("2d\\Misc\\circle", new Vector2(50, 50), new Vector2(35, 35)));

            spawnPoints.Add(new SpawnPoint("2d\\Misc\\circle", new Vector2(Globals.screenWidth / 2, 50), new Vector2(35, 35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            spawnPoints.Add(new SpawnPoint("2d\\Misc\\circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35)));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
        }

        public override void Update(Player Enemy, Vector2 Offset)
        {
            base.Update(Enemy, Offset);
        }

        public override void ChangeScore(int Score)
        {
            GameGlobals.score += Score;
        }
    }
}
