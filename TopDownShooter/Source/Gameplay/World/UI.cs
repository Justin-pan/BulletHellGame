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
    public class UI
    {

        public SpriteFont font;

        public QuantityDisplayBar healthBar;

        public UI()
        {
            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");

            healthBar = new QuantityDisplayBar(new Vector2(104, 16), 2, Color.Red);
        }

        public void Update(World World)
        {
            healthBar.Update(World.user.hero.health, World.user.hero.healthMax);
        }

        public void Draw(World World)
        {
            string tempStr = "Score = " + GameGlobals.score;
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth/2 - strDims.X/2, Globals.screenHeight - 35), Color.Black);

            healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));

            if (World.user.hero.dead || World.user.buildings.Count <= 0)
            {
                tempStr = "Press Enter to Restart";
                strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight/2), Color.Black);
            }
        }
    }
}
