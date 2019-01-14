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
    public class QuantityDisplayBar
    {
        public int border;

        public Basic2d bar, barBKG;

        public Color color;

        public QuantityDisplayBar(Vector2 Dims, int Border, Color Color)
        {
            border = Border;
            color = Color;
            bar = new Basic2d("2d\\Misc\\solid", new Vector2(0, 0), new Vector2(Dims.X - border * 2, Dims.Y - border * 2));
            barBKG = new Basic2d("2d\\Misc\\shade", new Vector2(0, 0), new Vector2(Dims.X, Dims.Y));
        }

        public virtual void Update(float Current, float Max)
        {
            bar.dims = new Vector2(Current/Max * (barBKG.dims.X - border * 2), bar.dims.Y);
        }

        public virtual void Draw(Vector2 Offset)
        {
            barBKG.Draw(Offset, new Vector2(0, 0), Color.Black);
            bar.Draw(Offset + new Vector2(border, border), new Vector2(0, 0), color);
        }

    }
}
