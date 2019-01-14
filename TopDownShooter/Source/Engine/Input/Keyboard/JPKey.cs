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
    public class JPKey
    {
        public int state;
        public string key, print, display;

        public JPKey(string Key, int State)
        {
            key = Key;
            state = State;
            MakePrint(key);
        }

        public virtual void Update()
        {
            state = 2;
        }

        public void MakePrint(string Key)
        {
            display = Key;

            string tempStr = "";

            if (Key == "A" || Key == "B" || Key == "C" || Key == "D" || Key == "E" || Key == "F" || Key == "G" || Key == "H" || Key == "I" || Key == "J" || Key == "K" || Key == "L" || Key == "M" || Key == "N" || Key == "O" || Key == "P" || Key == "Q" || Key == "R" || Key == "S" || Key == "T" || Key == "U" || Key == "V" || Key == "W" || Key == "X" || Key == "Y" || Key == "Z")
            {
                tempStr = Key;
            }
            if (Key == "Space")
            {
                tempStr = " ";
            }
            if (Key == "OemCloseBrackets")
            {
                tempStr = "]";
                display = tempStr;
            }
            if (Key == "OemOpenBrackets")
            {
                tempStr = "[";
                display = tempStr;
            }
            if (Key == "OemMinus")
            {
                tempStr = "-";
                display = tempStr;
            }
            if (Key == "OemPeriod" || Key == "Decimal")
            {
                tempStr = ".";
            }
            if (Key == "D1" || Key == "D2" || Key == "D3" || Key == "D4" || Key == "D5" || Key == "D6" || Key == "D7" || Key == "D8" || Key == "D9" || Key == "D0")
            {
                tempStr = Key.Substring(1);
            }
            else if (Key == "NumPad1" || Key == "NumPad2" || Key == "NumPad3" || Key == "NumPad4" || Key == "NumPad5" || Key == "NumPad6" || Key == "NumPad7" || Key == "NumPad8" || Key == "NumPad9" || Key == "NumPad0")
            {
                tempStr = Key.Substring(6);
            }


            print = tempStr;
        }
    }
}
