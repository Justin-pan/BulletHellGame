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
    public class World
    {
        public Vector2 offset;

        public UI ui;

        public User user;
        public AIPlayer aiPlayer;

        public List<Projectile> projectiles = new List<Projectile>();

        PassObject resetWorld;
        
        public World(PassObject ResetWorld)
        {
            resetWorld = ResetWorld;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.CheckScroll = CheckScroll;

            user = new User();
            aiPlayer = new AIPlayer();

            offset = new Vector2(0, 0);

            ui = new UI();
        }

        public virtual void Update()
        {
            if (!user.hero.dead) {

                user.Update(aiPlayer, offset);
                aiPlayer.Update(user, offset);

                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, aiPlayer.units.ToList<Unit>());

                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }               
            }
            else
            {
                if (Globals.keyboard.GetPress("Enter"))
                {
                    resetWorld(null);
                }
            }

            ui.Update(this);
        }

        public virtual void AddMob(object Info)
        {
            aiPlayer.AddUnit((Mob)Info);
        }

        public virtual void AddProjectile(object Info)
        {
            projectiles.Add((Projectile)Info);
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

        public virtual void Draw(Vector2 Offset)
        {
            //stuff is drawn in order, so if you draw spawnpoints after mobs, they will show on top of mobs           

            user.Draw(offset);
            aiPlayer.Draw(offset);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            ui.Draw(this);
        }
    }
}
