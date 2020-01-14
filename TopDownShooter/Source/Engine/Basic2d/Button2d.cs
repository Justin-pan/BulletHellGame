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
    public class Button2d : Basic2d
    {

        public bool isPressed, isHovered;
        public string text;

        public Color hoverColor;

        public SpriteFont font;

        public object info;

        PassObject buttonClicked;

        public Button2d(string Path, Vector2 Pos, Vector2 Dims, string FontPath, string Text, PassObject ButtonClicked, object Info) : base(Path, Pos, Dims)
        {
            text = Text;
            buttonClicked = ButtonClicked;
            info = Info;

            if (FontPath != "")
            {
                font = Globals.content.Load<SpriteFont>(FontPath);
            }

            isPressed = false;
            hoverColor = new Color(200, 230, 255);
        }

        public override void Update(Vector2 Offset)
        {
            if (Hover(Offset))
            {
                isHovered = true;
                if (Globals.mouse.LeftClick())
                {
                    isHovered = false;
                    isPressed = true;
                }
                else if (Globals.mouse.LeftClickRelease())
                {
                    RunBtnClick();
                }
            }
            else
            {
                isHovered = false;
            }

            if (!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
            {
                isPressed = false;
            }

            base.Update(Offset);
        }

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
        }

        public virtual void RunBtnClick()
        {
            if (buttonClicked != null)
            {
                buttonClicked(info);
            }
            Reset();
        }

        public override void Draw(Vector2 Offset)
        {
            Color tempColor = Color.White;
            if (isPressed)
            {
                tempColor = Color.Gray;
            }
            else if (isHovered)
            {
                tempColor = hoverColor;
            }

            Globals.normalEffect.Parameters["xSize"].SetValue((float)texture.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)texture.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(tempColor.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(Offset);

            Vector2 strDims = font.MeasureString(text);
            Globals.spriteBatch.DrawString(font, text, pos + Offset + new Vector2(-strDims.X/2, -strDims.Y/2), Color.Black);
        }
    }
}
