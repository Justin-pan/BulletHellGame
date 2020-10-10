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
        public Basic2d pauseOverlay;

        public Button2d resetBtn;

        public SpriteFont font;

        public QuantityDisplayBar healthBar;

        public UI(PassObject Reset)
        {
            pauseOverlay = new Basic2d("2d\\Misc\\PauseOverlay", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(300, 300));

            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");

            resetBtn = new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0,0), new Vector2(96, 32), "Fonts\\Arial16", "Reset", Reset, null);

            healthBar = new QuantityDisplayBar(new Vector2(104, 16), 2, Color.Red);
        }

        public void Update(World World)
        {
            healthBar.Update(World.user.hero.health, World.user.hero.healthMax);

            if (World.user.hero.dead || World.user.buildings.Count <= 0)
            {
                resetBtn.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
            }
        }

        public void Draw(World World)
        {
            Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Black.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            string goldStr = "Gold = " + World.user.hero.gold;
            Vector2 goldStrDims = font.MeasureString(goldStr);
            Globals.spriteBatch.DrawString(font, goldStr, new Vector2(Globals.screenWidth/2 - goldStrDims.X/2, Globals.screenHeight - 50), Color.Black);

            string tempStr = "Score = " + GameGlobals.score;
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth/2 - strDims.X/2, Globals.screenHeight - 35), Color.Black);

            if (World.user.hero.dead || World.user.buildings.Count <= 0)
            {
                tempStr = "Press Enter or click the button to Restart";
                strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight/2), Color.Black);

                resetBtn.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
            }

            healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));

            if (GameGlobals.paused)
            {
                pauseOverlay.Draw(Vector2.Zero);
            }
        }
    }
}
