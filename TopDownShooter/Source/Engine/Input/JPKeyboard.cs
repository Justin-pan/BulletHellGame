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
    public class JPKeyboard
    {
        public KeyboardState newKeyboard, oldKeyboard;

        public List<JPKey> pressedKeys = new List<JPKey>(), previousPressedKeys = new List<JPKey>();

        public JPKeyboard()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Keyboard.GetState();

            GetPressedKeys();
        }
        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;
            previousPressedKeys = new List<JPKey>();
            for(int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }

        }

        public bool GetPress(string Key)
        {
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (pressedKeys[i].key == Key)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void GetPressedKeys()
        {
            bool found = false;

            pressedKeys.Clear();
            for(int i =0; i < newKeyboard.GetPressedKeys().Length; i++)
            {
                pressedKeys.Add(new JPKey(newKeyboard.GetPressedKeys()[i].ToString(), 1));
            }
        }
    }
}
