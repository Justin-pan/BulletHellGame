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
    public class GameGlobals
    {
        public static bool paused = false;
        
        public static int score = 0;

        public static PassObject PassProjectile, PassEffect, PassMob, PassBuilding, PassSpawnPoint, CheckScroll;

        // public static Notif RePathNotif;
    }
}
