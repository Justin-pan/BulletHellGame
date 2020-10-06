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

        public virtual Vector2 GetPosFromLoc(Vector2 Loc)
        {
            return gridOffset + new Vector2((int)Loc.X * slotDims.X, (int)Loc.Y * slotDims.Y);
        }

        public virtual GridLocation GetSlotFromLocation(Vector2 Loc)
        {
            if (Loc.X >= 0 && Loc.Y >= 0 && Loc.X < slots.Count && Loc.Y < slots[(int) Loc.X].Count)
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

        #region A* (A Star)

        public List<Vector2> GetPath(Vector2 Start, Vector2 End, bool AllowDiagonals)
        {
            List<GridLocation> viewable = new List<GridLocation>(), used = new List<GridLocation>();

            List<List<GridLocation>> masterGrid = new List<List<GridLocation>>();

            // copying grid so that you can make changes to copy instead of original grid
            bool impassable = false;
            float cost = 1;
            for(int i = 0; i < slots.Count; i++)
            {
                masterGrid.Add(new List<GridLocation>());
                for(int j = 0; j < slots[i].Count; j++)
                {
                    impassable = slots[i][j].impassible;

                    if(impassable || slots[i][j].filled)
                    {
                        impassable = true;
                    }

                    cost = slots[i][j].cost;

                    masterGrid[i].Add(new GridLocation(new Vector2(i, j), cost, impassable, 99999999));
                }
            }

            viewable.Add(masterGrid[(int)Start.X][(int)Start.Y]);

            while(viewable.Count > 0 && !(viewable[0].position.X == End.X && viewable[0].position.Y == End.Y))
            {
                TestAStarNode(masterGrid, viewable, used, End, AllowDiagonals);
            }

            List<Vector2> path = new List<Vector2>();

            if(viewable.Count > 0)
            {
                int currentViewableStart = 0;
                GridLocation currentNode = viewable[currentViewableStart];

                path.Clear();
                Vector2 tempPos;

                while(true)
                {
                    //nodes in grid copy are virtual and only recognized by actual grid indexes, this is to convert from virtual location to actual grid location
                    tempPos = GetPosFromLoc(currentNode.position) + slotDims/2;
                    path.Add(new Vector2(tempPos.X, tempPos.Y));

                    if(currentNode.position == Start)
                    {   
                        break;
                    }
                    else
                    {
                        if((int)currentNode.parent.X != -1 && (int)currentNode.parent.Y != -1)
                        {
                            if(currentNode.position.X == masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y].position.X && currentNode.position.Y == masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y].position.Y)
                            {
                                //Current node points to itself
                                currentNode = viewable[currentViewableStart];
                                currentViewableStart++;
                            }

                            currentNode = masterGrid[(int)currentNode.parent.X][(int)currentNode.parent.Y];
                        }
                        else
                        {
                            //Node is off grid
                            currentNode = viewable[currentViewableStart];
                            currentViewableStart++;
                        }
                    }
                }

                path.Reverse();

                if(path.Count > 1)
                {
                    path.RemoveAt(0);
                }
            }

            return path;
        }

        public void TestAStarNode(List<List<GridLocation>> MasterGrid, List<GridLocation> Viewable, List<GridLocation> Used, Vector2 End, bool AllowDiagonals)
        {
            GridLocation currentNode;
            bool up = true, down = true, left = true, right = true;

            //Above
            if(Viewable[0].position.Y > 0 && Viewable[0].position.Y < MasterGrid[0].Count && !MasterGrid[(int)Viewable[0].position.X][(int)Viewable[0].position.Y - 1].impassible)
            {
                currentNode = MasterGrid[(int)Viewable[0].position.X][(int)Viewable[0].position.Y - 1];
                up = currentNode.impassible;
                SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, 1);
            }

            //Below
            if(Viewable[0].position.Y >= 0 && Viewable[0].position.Y + 1 < MasterGrid[0].Count && !MasterGrid[(int)Viewable[0].position.X][(int)Viewable[0].position.Y + 1].impassible)
            {
                currentNode = MasterGrid[(int)Viewable[0].position.X][(int)Viewable[0].position.Y + 1];
                down = currentNode.impassible;
                SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, 1);
            }

            //Left
            if(Viewable[0].position.X > 0 && Viewable[0].position.X < MasterGrid.Count && !MasterGrid[(int)Viewable[0].position.X - 1][(int)Viewable[0].position.Y].impassible)
            {
                currentNode = MasterGrid[(int)Viewable[0].position.X - 1][(int)Viewable[0].position.Y];
                left = currentNode.impassible;
                SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, 1);
            }

            //Right
            if(Viewable[0].position.X >= 0 && Viewable[0].position.X + 1 < MasterGrid.Count && !MasterGrid[(int)Viewable[0].position.X + 1][(int)Viewable[0].position.Y].impassible)
            {
                currentNode = MasterGrid[(int)Viewable[0].position.X + 1][(int)Viewable[0].position.Y];
                right = currentNode.impassible;
                SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, 1);
            }

            if(AllowDiagonals)
            {

                //Up and right
                if(Viewable[0].position.X >= 0 && Viewable[0].position.X+1 < MasterGrid.Count && Viewable[0].position.Y > 0 && Viewable[0].position.Y < MasterGrid[0].Count && !MasterGrid[(int)Viewable[0].position.X + 1][(int)Viewable[0].position.Y - 1].impassible && (!up || !right))
                {
                    currentNode = MasterGrid[(int)Viewable[0].position.X + 1][(int)Viewable[0].position.Y - 1];

                    SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, (float)Math.Sqrt(2));
                }

                //Down and right
                if(Viewable[0].position.X >= 0 && Viewable[0].position.X + 1 < MasterGrid.Count && Viewable[0].position.Y >= 0 && Viewable[0].position.Y + 1 < MasterGrid[0].Count && !MasterGrid[(int)Viewable[0].position.X + 1][(int)Viewable[0].position.Y + 1].impassible && (!down || !right))
                {
                    currentNode = MasterGrid[(int)Viewable[0].position.X + 1][(int)Viewable[0].position.Y + 1];

                    SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, (float)Math.Sqrt(2));
                }

                //Up and Left
                if(Viewable[0].position.X > 0 && Viewable[0].position.X < MasterGrid.Count && Viewable[0].position.Y > 0 && Viewable[0].position.Y < MasterGrid[0].Count && !MasterGrid[(int)Viewable[0].position.X - 1][(int)Viewable[0].position.Y - 1].impassible &&(!up || !left))
                {
                    currentNode = MasterGrid[(int)Viewable[0].position.X - 1][(int)Viewable[0].position.Y - 1];

                    SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, (float)Math.Sqrt(2));
                }

                //Down and Left
                if(Viewable[0].position.X > 0 && Viewable[0].position.X < MasterGrid.Count && Viewable[0].position.Y >= 0 && Viewable[0].position.Y + 1 < MasterGrid[0].Count && !MasterGrid[(int)Viewable[0].position.X - 1][(int)Viewable[0].position.Y + 1].impassible && (!down || !left))
                {
                    currentNode = MasterGrid[(int)Viewable[0].position.X - 1][(int)Viewable[0].position.Y + 1];

                    SetAStarNode(Viewable, Used, currentNode, new Vector2(Viewable[0].position.X, Viewable[0].position.Y), Viewable[0].currentDist, End, (float)Math.Sqrt(2));
                }
            }

            Viewable[0].hasBeenUsed = true;
            Used.Add(Viewable[0]);
            Viewable.RemoveAt(0);


            //sort
            /*
             * Viewable.Sort(delegate(AStarNode n1, AStarNode n2)
             * {
             *     return n1.fScore.CompareTo(n2.fScore);
             * }
             */
        }

        public void SetAStarNode(List<GridLocation> Viewable, List<GridLocation> Used, GridLocation nextNode, Vector2 nextParent, float D, Vector2 Target, float DistMult)
        {
            float f = D;
            float addedDist = (nextNode.cost * DistMult);

            if(!nextNode.isViewable && !nextNode.hasBeenUsed)
            {
                nextNode.SetNode(nextParent, f, D + addedDist);
                nextNode.isViewable = true;

                SetAStarNodeInsert(Viewable, nextNode);
            }
            else if(nextNode.isViewable)
            {
                if(f < nextNode.fScore)
                {
                    nextNode.SetNode(nextParent, f, D + addedDist);
                }
            }
        }

        public virtual void SetAStarNodeInsert(List<GridLocation> List, GridLocation NewNode)
        {
            bool added = false;
            for(int i = 0; i < List.Count; i++)
            {
                if(List[i].fScore > NewNode.fScore)
                {
                    List.Insert(Math.Max(1, i), NewNode);

                    added = true;
                    break;
                }
            }

            if(!added)
            {
                List.Add(NewNode);
            }
        }

        #endregion

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
                        else if (slots[j][k].filled)
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.DarkGray.ToVector4());
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
