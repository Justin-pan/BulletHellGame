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
    public class SquareGrid
    {

        public bool showGrid;
        //slotDims, gridDims, physicalStartPos, totalPhysicalDims, currentHoverSlot
        public Vector2 slotDims, totalSlots, gridOffset, totalDims, currentHoverSlot;

        public Basic2d gridImg;

        public List<List<GridLocation>> slots = new List<List<GridLocation>>();

        public SquareGrid(Vector2 SlotDims, Vector2 StartPos, Vector2 TotalDims)
        {
            showGrid = false;

            slotDims = SlotDims;
            gridOffset = new Vector2((int)StartPos.X, (int)StartPos.Y);
            totalDims = new Vector2((int)TotalDims.X, (int)TotalDims.Y);

            currentHoverSlot = new Vector2(-1, -1);

            SetBaseGrid();

            //Pos needs to be set with an offset because the position of the grid should be top left corner but it is drawn from the middle
            //The -2 in dims is used to ensure that the slots don't overlap
            gridImg = new Basic2d("2d\\Misc\\shade", slotDims / 2, new Vector2(slotDims.X - 2, slotDims.Y - 2));
        }

        public virtual void Update(Vector2 Offset)
        {
            currentHoverSlot = GetSlotFromPixel(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), -Offset);
        }

        public virtual GridLocation GetSlotFromLocation(Vector2 Loc)
        {
            if (Loc.X >= 0 && Loc.Y >= 0 && Loc.X > slots.Count && Loc.Y > slots[(int) Loc.X].Count)
            {
                return slots[(int)Loc.X][(int)Loc.Y];
            } 
            else
            {
                return null;
            }
        }

        public virtual Vector2 GetSlotFromPixel(Vector2 Pixel, Vector2 Offset)
        {
            Vector2 adjustedPos = Pixel - gridOffset + Offset;

            Vector2 tempVec = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPos.X / slotDims.X)), slots.Count - 1), Math.Min(Math.Max(0, (int)(adjustedPos.Y / slotDims.Y)), slots[0].Count - 1));

            return tempVec;
        }

        public virtual void SetBaseGrid()
        {
            totalSlots = new Vector2((int)(totalDims.X / slotDims.X), (int)(totalDims.Y / slotDims.Y));

            slots.Clear();
            for(int i = 0; i < totalSlots.X; i++)
            {
                slots.Add(new List<GridLocation>());
                for (int j = 0; j < totalSlots.Y; j++)
                {
                    slots[i].Add(new GridLocation(1, false));
                }
            }
        }

        public virtual void Draw(Vector2 Offset)
        {
            if (showGrid)
            {
                //Vector2 topLeft = GetSlotFromPixel((new Vector2(0, 0)) / Globals.zoom - Offset, Vector2.Zero);
                //Vector2 botRight = GetSlotFromPixel((new Vector2(Globals.screenWidth, Globals.screenHeight)) / Globals.zoom - Offset, Vector2.Zero);
                Vector2 topLeft = GetSlotFromPixel(new Vector2(0, 0), Vector2.Zero);
                Vector2 botRight = GetSlotFromPixel(new Vector2(Globals.screenWidth, Globals.screenHeight), Vector2.Zero);

                Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

                for (int j = (int)topLeft.X; j <= botRight.X && j < slots.Count; j++)
                {
                    for (int k = (int)topLeft.Y; k <= botRight.Y && k < slots[(int)(totalSlots.X - 1)].Count; k++)
                    {
                        if (currentHoverSlot.X == j && currentHoverSlot.Y == k)
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();
                        }
                        else
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();
                        }

                        gridImg.Draw(Offset + gridOffset + new Vector2(j * slotDims.X, k * slotDims.Y));
                    }
                }
            }
        }
    }
}
