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
    public class Player
    {
        public Hero hero;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Player()
        {

        }

        public virtual void Update(Player Enemy, Vector2 Offset)
        {
            if(hero != null)
            {
                hero.Update(Offset);
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Update(Offset, Enemy);

                if (units[i].dead)
                {
                    ChangeScore(1);
                    units.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(Offset);
            }

        }

        public virtual void AddUnit(object Info)
        {
            units.Add((Unit)Info);
        }

        public virtual void ChangeScore(int Score)
        {
            GameGlobals.score += Score;
        }

        public virtual void Draw(Vector2 Offset)
        {
            if(hero != null)
            {
                hero.Draw(Offset);
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Draw(Offset);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(Offset);
            }

        }
    }
}
