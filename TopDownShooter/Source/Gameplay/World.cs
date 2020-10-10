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
    public class World
    {
        public Vector2 offset;

        public UI ui;

        public User user;
        public AIPlayer aiPlayer;

        public SquareGrid grid;

        public List<Projectile> projectiles = new List<Projectile>();
        public List<Effect2d> effects = new List<Effect2d>();
        public List<DestructibleObject> allObjects = new List<DestructibleObject>();
        public List<SceneItem> sceneItems = new List<SceneItem>();
        

        PassObject resetWorld, changeGameState;
        
        public World(PassObject ResetWorld, PassObject ChangeGameState)
        {
            resetWorld = ResetWorld;
            changeGameState = ChangeGameState;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassEffect = AddEffect;
            GameGlobals.PassGold = AddGold;
            GameGlobals.PassMob = AddMob;
            GameGlobals.PassBuilding = AddBuilding;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;
            GameGlobals.CheckScroll = CheckScroll;

            // GameGlobals.RePathNotif = SetRePath;

            GameGlobals.paused = false;

            offset = new Vector2(0, 0);

            LoadData(1);


            ui = new UI(ResetWorld);
        }

        public virtual void Update()
        {
            if (!user.hero.dead && user.buildings.Count > 0 && !GameGlobals.paused) {

                allObjects.Clear();
                allObjects.AddRange(user.GetAllObjects());
                allObjects.AddRange(aiPlayer.GetAllObjects());

                user.Update(aiPlayer, offset, grid);
                aiPlayer.Update(user, offset, grid);

                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, allObjects);

                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].Update(offset);

                    if (effects[i].done)
                    {
                        effects.RemoveAt(i);
                        i--;
                    }
                }

                for(int i = 0; i < sceneItems.Count; i++)
                {
                    sceneItems[i].Update(offset);
                }
            }
            else
            {
                if (Globals.keyboard.GetPress("Enter") && (user.hero.dead || user.buildings.Count <= 0))
                {
                    resetWorld(null);
                }
            }

            if(grid != null)
            {
                grid.Update(offset);
            }

            if (Globals.keyboard.GetSinglePress("Back"))
            {
                resetWorld(null);
                changeGameState(0);
            }

            if (Globals.keyboard.GetSinglePress("Space"))
            {
                GameGlobals.paused = !GameGlobals.paused;
            }

            if (Globals.keyboard.GetSinglePress("G"))
            {
                grid.showGrid = !grid.showGrid;
            }

            ui.Update(this);
        }

        public virtual void AddBuilding(object Info)
        {
            Building tempBuilding = (Building)Info;

            if (user.id == tempBuilding.ownerId)
            {
                user.AddBuilding(tempBuilding);
            }
            else if (aiPlayer.id == tempBuilding.ownerId)
            {
                aiPlayer.AddBuilding(tempBuilding);
            }
        }

        public virtual void AddEffect(object Info)
        {
            effects.Add((Effect2d)Info);
        }

        public virtual void AddGold(object Info)
        {
            PlayerValuePacket packet = (PlayerValuePacket)Info;

            if(user.id == packet.playerId)
            {
                user.hero.gold += (int)packet.value;
            }
            else if(aiPlayer.id == packet.playerId)
            {
                //aiPlayer.gold = (int)packet.value;
            }
        }

        public virtual void AddMob(object Info)
        {
            Unit tempUnit = (Unit)Info;

            if (user.id == tempUnit.ownerId)
            {
                user.AddUnit(tempUnit);
            }
            else if(aiPlayer.id == tempUnit.ownerId)
            {
                aiPlayer.AddUnit(tempUnit);
            }

            // aiPlyaer.AddUnit((Mob)Info)
        }

        public virtual void AddProjectile(object Info)
        {
            projectiles.Add((Projectile)Info);
        }

        public virtual void AddSpawnPoint(object Info)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)Info;

            if (user.id == tempSpawnPoint.ownerId)
            {
                user.AddSpawnPoint(tempSpawnPoint);
            }
            else if (aiPlayer.id == tempSpawnPoint.ownerId)
            {
                aiPlayer.AddSpawnPoint(tempSpawnPoint);
            }
        }

        public virtual void CheckScroll(object Info)
        {
            Vector2 tempPos = (Vector2)Info;

            if (tempPos.X < -offset.X + (Globals.screenWidth * .4f))
            {
                offset = new Vector2(offset.X + user.hero.speed * 2, offset.Y);
            }

            if(tempPos.X > -offset.X + (Globals.screenWidth * .6f))
            {
                offset = new Vector2(offset.X - user.hero.speed * 2, offset.Y);
            }
            if (tempPos.Y < -offset.Y + (Globals.screenHeight * .4f))
            {
                offset = new Vector2(offset.X, offset.Y + user.hero.speed * 2);
            }

            if (tempPos.Y > -offset.Y + (Globals.screenHeight * .6f))
            {
                offset = new Vector2(offset.X, offset.Y - user.hero.speed * 2);
            }
        }

        public virtual void LoadData(int Level)
        {
            XDocument xml = XDocument.Load("XML\\Levels\\Level"+Level+".xml");

            XElement tempElement = null;

            if (xml.Element("Root").Element("User") != null)
            {
                tempElement = xml.Element("Root").Element("User");
            }

            user = new User(1, tempElement);


            tempElement = null;

            if (xml.Element("Root").Element("AIPlayer") != null)
            {
                tempElement = xml.Element("Root").Element("AIPlayer");
            }

            grid = new SquareGrid(new Vector2(25, 25), new Vector2(0, 0), new Vector2(Globals.screenWidth, Globals.screenHeight), xml.Element("Root").Element("GridItems"));

            aiPlayer = new AIPlayer(2, tempElement);

            List<XElement> sceneItemList = (from t in xml.Element("Root").Element("Scene").Descendants("SceneItem")
                                            select t).ToList<XElement>();
            
            Type sType = null;
            for(int i = 0; i < sceneItemList.Count; i++)
            {
                sType = Type.GetType("TopDownShooter."+sceneItemList[i].Element("type").Value, true);
                
                sceneItems.Add((SceneItem)(Activator.CreateInstance(sType, new Vector2(Convert.ToInt32(sceneItemList[i].Element("Pos").Element("x").Value, Globals.culture), 
                                            Convert.ToInt32(sceneItemList[i].Element("Pos").Element("y").Value, Globals.culture)), 
                                            new Vector2((float)Convert.ToDouble(sceneItemList[i].Element("scale").Value, Globals.culture)))));
            }
        }

        public virtual void Draw(Vector2 Offset)
        {
            //stuff is drawn in order, so if you draw spawnpoints after mobs, they will show on top of mobs           

            grid.Draw(offset);

            user.Draw(offset);
            aiPlayer.Draw(offset);

            for(int i = 0; i < sceneItems.Count; i++)
            {
                sceneItems[i].Draw(offset);
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Draw(offset);
            }

            ui.Draw(this);
        }
    }
}
