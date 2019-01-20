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
        public AIPlayer(int Id, XElement Data) : base(Id, Data)
        {

            /* spawnPoints.Add(new Portal(new Vector2(50, 50), id));

            spawnPoints.Add(new Portal(new Vector2(Globals.screenWidth / 2, 50), id));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);

            spawnPoints.Add(new Portal(new Vector2(Globals.screenWidth - 50, 50), id));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);
            */
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
