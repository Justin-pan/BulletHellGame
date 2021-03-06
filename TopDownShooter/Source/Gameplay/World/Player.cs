﻿using System;
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
    public class Player
    {
        public int id;
        public Hero hero;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
        public List<Building> buildings = new List<Building>();

        public Player(int Id, XElement Data)
        {
            id = Id;

            LoadData(Data);
        }

        public virtual void Update(Player Enemy, Vector2 Offset, SquareGrid Grid)
        {
            if(hero != null)
            {
                hero.Update(Offset, Enemy, Grid);
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Update(Offset, Enemy, Grid);

                if (units[i].dead)
                {
                    ChangeScore(1);
                    units.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(Offset, Enemy, Grid);

                if (spawnPoints[i].dead)
                {
                    spawnPoints.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].Update(Offset, Enemy, Grid);

                if (buildings[i].dead)
                {
                    buildings.RemoveAt(i);
                    i--;
                }
            }

        }

        public virtual void AddBuilding(object Info)
        {
            Building tempBuilding = (Building)Info;
            tempBuilding.ownerId = id;
            buildings.Add((Building)Info);
        }

        public virtual void AddSpawnPoint(object Info)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)Info;
            tempSpawnPoint.ownerId = id;
            spawnPoints.Add((SpawnPoint)Info);
        }

        public virtual void AddUnit(object Info)
        {
            Unit tempUnit = (Unit)Info;
            tempUnit.ownerId = id;
            units.Add((Unit)Info);
        }

        public virtual void ChangeScore(int Score)
        {
            GameGlobals.score += Score;
        }

        public virtual List<DestructibleObject> GetAllObjects()
        {
            List<DestructibleObject> tempObjects = new List<DestructibleObject>();
            tempObjects.AddRange(units.ToList<DestructibleObject>());
            tempObjects.AddRange(spawnPoints.ToList<DestructibleObject>());
            tempObjects.AddRange(buildings.ToList<DestructibleObject>());

            return tempObjects;
        }

        public virtual void LoadData(XElement Data)
        {
            List<XElement> spawnList = (from t in Data.Descendants("SpawnPoint")
                                        select t).ToList<XElement>();


            Type sType = null;
            for (int i = 0; i < spawnList.Count; i++)
            {
                sType = Type.GetType("TopDownShooter."+ spawnList[i].Element("type").Value, true);


                spawnPoints.Add((SpawnPoint)(Activator.CreateInstance(sType,new Vector2(Convert.ToInt32(spawnList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(spawnList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(1, 1), id, spawnList[i])));
            }

            List<XElement> buildingList = (from t in Data.Descendants("Building")
                                        select t).ToList<XElement>();

            for (int i = 0; i < buildingList.Count; i++)
            {
                sType = Type.GetType("TopDownShooter." + buildingList[i].Element("type").Value, true);

                buildings.Add((Building)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(buildingList[i].Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(buildingList[i].Element("Pos").Element("y").Value, Globals.culture)), new Vector2(1, 1), id)));
            }

            if (Data.Element("Hero") != null)
            {
                hero = new Hero("2d\\Units\\HeroSheet", new Vector2(Convert.ToInt32(Data.Element("Hero").Element("Pos").Element("x").Value, Globals.culture), Convert.ToInt32(Data.Element("Hero").Element("Pos").Element("y").Value, Globals.culture)), new Vector2(64, 64), new Vector2(4, 1), id);
            }
        }

        public virtual void Draw(Vector2 Offset)
        {
            if(hero != null)
            {
                hero.Draw(Offset);
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Draw(Offset);
            }

            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].Draw(Offset);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(Offset);
            }

        }
    }
}
