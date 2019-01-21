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
        public List<MobChoice> mobChoices = new List<MobChoice>();

        public JPTimer spawnTimer = new JPTimer(2400);

        public SpawnPoint(string Path, Vector2 Pos, Vector2 Dims, int OwnerId, XElement Data) : base(Path, Pos, Dims, OwnerId)
        {
            dead = false;

            health = 3;
            healthMax = health;

            LoadData(Data);

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

        public virtual void LoadData(XElement Data)
        {
            if(Data != null)
            {
                spawnTimer.AddToTimer(Convert.ToInt32(Data.Element("timerAdd").Value, Globals.culture));


                List<XElement> mobList = (from t in Data.Descendants("mob")
                                            select t).ToList<XElement>();


                for (int i = 0; i < mobList.Count; i++)
                {
                    mobChoices.Add(new MobChoice(mobList[i].Value, Convert.ToInt32(mobList[i].Attribute("rate").Value, Globals.culture)));
                }
            }
        }

        public override void Draw(Vector2 Offset)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue((float)texture.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)texture.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(Offset);
        }
    }
}
