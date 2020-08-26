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
    public class GridLocation
    {
        //grid is filled, cannot walk through and filled, can't walk through but may be empty
        public bool filled, impassible, unPathable, hasBeenUsed, isViewable;
        //fScore is sort of like weight in djikstras, cost is overall weight of path, and then currentDist is distance travelled from previous node to current node
        //cost and currentDist different in the sense that crossing a grid maybe more difficult than the distance suggests like travelling through mud or water
        public float fScore, cost, currentDist;
        //parent used for A* purposes, position for both A* and grid location
        public Vector2 parent, position;

        public GridLocation(float Cost, bool Filled)
        {
            cost = Cost;
            filled = Filled;

            hasBeenUsed = false;
            isViewable = false;
            unPathable = false;
            impassible = false;
        }

        public GridLocation(Vector2 Pos, float Cost, bool Filled, float FScore)
        {
            cost = Cost;
            filled = Filled;
            impassible = Filled;
            unPathable = false;
            hasBeenUsed = false;
            isViewable = false;

            position = Pos;

            fScore = FScore;
        }

        public void SetNode(Vector2 Parent, float Score, float CurrentDist)
        {
            parent = Parent;
            fScore = Score;
            currentDist = CurrentDist;
        }

        public virtual void SetToFilled(bool Impassible)
        {
            filled = true;
            impassible = Impassible;
        }
    }
}
