using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TopDownShooter
{
    public class Animated2d : Basic2d
    {
        //This is the size of the spritesheet in terms of frames, if there is 1 row of frames and 4 frames in that row it will be a 4 by 1 vector
        public Vector2 frames;
        //List of the animations that you are using
        public List<FrameAnimation> frameAnimationList = new List<FrameAnimation>();
        public Color color;
        //Just a bool to see if using frame animations
        public bool frameAnimations;
        //Which animation currently used
        public int currentAnimation = 0;

        public Animated2d(string PATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, Color COLOR) : base(PATH, POS, DIMS)
        {
            Frames = new Vector2(FRAMES.X, FRAMES.Y);

            color = COLOR;
        }

        #region Properties
        //This is just a setter and a getter for changing the frames after you load
        public Vector2 Frames
        {
            set
            {
                frames = value;
                if (texture != null)
                {
                    frameSize = new Vector2(texture.Bounds.Width / frames.X, texture.Bounds.Height / frames.Y);
                }
            }
            get
            {
                return frames;
            }
        }
        #endregion

        //Call the parent update and if there are frames to animate then update those frames
        public override void Update(Vector2 OFFSET)
        {
            if(frameAnimations && frameAnimationList != null && frameAnimationList.Count > currentAnimation)
            {
                frameAnimationList[currentAnimation].Update();
            }

            base.Update(OFFSET);
        }

        //Given a string, return the index to the animation in the list of that name
        public virtual int GetAnimationFromName(string ANIMATIONNAME)
        {
            for(int i=0;i<frameAnimationList.Count;i++)
            {
                if(frameAnimationList[i].name == ANIMATIONNAME)
                {
                    return i;
                }
            }

            return -1;
        }

        //If animation you want is not current then reset in case that the animation had been interrupted and not allowed to end, set currentAnimation to tempAnimation
        public virtual void SetAnimationByName(string NAME)
        {
            int tempAnimation = GetAnimationFromName(NAME);

            if(tempAnimation != -1)
            {
                if(tempAnimation != currentAnimation)
                {
                    frameAnimationList[tempAnimation].Reset();
                }

                currentAnimation = tempAnimation;
                
            }
        }

        //if else just to make sure that there are actually animations for this object
        public override void Draw(Vector2 screenShift)
        {

            if(frameAnimations && frameAnimationList[currentAnimation].Frames > 0)
            {
                //Globals.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X+screenShift.X), (int)(pos.Y+screenShift.Y), (int)dims.X, (int)dims.Y), new Rectangle((int)((currentFrame.X-1)*dims.X), (int)((currentFrame.Y-1)*dims.Y), (int)(currentFrame.X*dims.X), (int)(currentFrame.Y*dims.Y)), color, rot, new Vector2(myModel.Bounds.Width/2, myModel.Bounds.Height/2), new SpriteEffects(), 0);
                frameAnimationList[currentAnimation].Draw(texture, dims, frameSize, screenShift, pos, rot, color, new SpriteEffects());

            }
            else
            {
                base.Draw(screenShift);
            }
        }


    }
}
