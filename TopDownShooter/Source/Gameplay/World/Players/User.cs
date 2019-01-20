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
    public class User : Player
    {
        public User(int Id, XElement Data) : base(Id, Data)
        {
            // hero = new Hero("2d\\Units\\Hero", new Vector2(300, 300), new Vector2(64, 64), id);

            // buildings.Add(new Tower(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 - 40), id));
        }

        public override void Update(Player Enemy, Vector2 Offset)
        {
            base.Update(Enemy, Offset);
        }
    }
}
