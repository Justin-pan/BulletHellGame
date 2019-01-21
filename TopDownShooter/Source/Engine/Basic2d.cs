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
    public class Basic2d
    {
        public float rot;

        public Vector2 pos, dims;

        public Texture2D texture;

        public Basic2d(string Path, Vector2 Pos, Vector2 Dims)
        {
            pos = Pos;
            dims = Dims;

            texture = Globals.content.Load<Texture2D>(Path);
        }
        
        public virtual void Update(Vector2 Offset)
        {

        }

        public virtual bool Hover(Vector2 Offset)
        {
            return HoverImg(Offset);
        }

        public virtual bool HoverImg(Vector2 Offset)
        {
            Vector2 mousePos = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);

            if (mousePos.X >= (pos.X + Offset.X) - dims.X/2 && mousePos.X <= (pos.X + Offset.X) + dims.X / 2 && mousePos.Y >= (pos.Y + Offset.Y) - dims.Y / 2 && mousePos.Y <= (pos.Y + Offset.Y) + dims.Y / 2)
            {
                return true;
            }

            return false;
        }

        public virtual void Draw(Vector2 Offset)
        {
            if(texture != null)
            {
                // your texture, rectangle which is the size of your object(position it is at, and dimensions), source rectangle (piece of picture if you want)
                // color will tint your picture if not white, rotation, inside model bounds is pixel size so you draw from the middle, basic sprite effects normally create in construction and pass in
                // layer depth normally done seperately
                Globals.spriteBatch.Draw(texture, new Rectangle((int)(pos.X + Offset.X), (int)(pos.Y + Offset.Y), (int)dims.X, (int)dims.Y), null, Color.White, rot, new Vector2(texture.Bounds.Width/2, texture.Bounds.Height/2), new SpriteEffects(), 0);
            }
        }

        public virtual void Draw(Vector2 Offset, Vector2 Origin, Color Color)
        {
            if (texture != null)
            {
                // your texture, rectangle which is the size of your object(position it is at, and dimensions), source rectangle (piece of picture if you want)
                // color will tint your picture if not white, rotation, inside model bounds is pixel size so you draw from the middle, basic sprite effects normally create in construction and pass in
                // layer depth normally done seperately
                Globals.spriteBatch.Draw(texture, new Rectangle((int)(pos.X + Offset.X), (int)(pos.Y + Offset.Y), (int)dims.X, (int)dims.Y), null, Color, rot, new Vector2(Origin.X, Origin.Y), new SpriteEffects(), 0);
            }
        }
    }
}
