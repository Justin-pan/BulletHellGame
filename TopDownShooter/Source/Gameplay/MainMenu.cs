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
    class MainMenu
    {

        public Basic2d bkg;

        public PassObject PlayDel, ExitDel;

        public List<Button2d> buttons = new List<Button2d>();
        public MainMenu(PassObject PLAYDEL, PassObject EXITDEL)
        {
            PlayDel = PLAYDEL;
            ExitDel = EXITDEL;

            bkg = new Basic2d("2d\\Menu\\Backgrounds\\MainMenuBkg", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));

            buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\Arial16", "Play", PlayDel, 1));

            buttons.Add(new Button2d("2d\\Misc\\SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\Arial16", "Exit", ExitDel, null));
        }

        public virtual void Update()
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(new Vector2(250, 550 + 45 * i));
            }
        }

        public virtual void Draw()
        {
            bkg.Draw(Vector2.Zero);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(new Vector2(250, 550 + 45 * i));
            }
        }
    }
}
