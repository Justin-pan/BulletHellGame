using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace TopDownShooter
{
    public class FrameAnimation
    {
        //This is for the case where an animation has some sort action beyond just the animation like a hitbox to an animation
        public bool hasFired;
        //total frames in animation, current frame in animation, how many repeats, current iteration, the frame when you do a secondary action like evaluating hitbox
        public int frames, currentFrame, maxPasses, currentPass, fireFrame;
        //name of animation
        public string name;
        //size of spritesheet, the start of the animation in the sheet, sheetframe sometimes changes so startframe is a constant sheetframe, spritedims is just how big the spritesheet is
        public Vector2 sheet, startFrame, sheetFrame, spriteDims;
        //how long each frame shows for
        public JPTimer frameTimer;

        //the actual side action related to fireframe
        public PassObject FireAction;

        //constructor with no fireaction
        public FrameAnimation(Vector2 SpriteDims, Vector2 sheetDims, Vector2 start, int totalframes, int timePerFrame, int MAXPASSES, string NAME = "")
        {
            spriteDims = SpriteDims;
            sheet = sheetDims;
            startFrame = start;
            sheetFrame = new Vector2(start.X, start.Y);
            frames = totalframes;
            currentFrame = 0;
            frameTimer = new JPTimer(timePerFrame);
            maxPasses = MAXPASSES;
            currentPass = 0;
            name = NAME;
            FireAction = null;
            hasFired = false;

            fireFrame = 0;
        }

        //constructor with fireaction
        public FrameAnimation(Vector2 SpriteDims, Vector2 sheetDims, Vector2 start, int totalframes, int timePerFrame, int MAXPASSES, int FIREFRAME, PassObject FIREACTION, string NAME = "")
        {
            spriteDims = SpriteDims;
            sheet = sheetDims;
            startFrame = start;
            sheetFrame = new Vector2(start.X, start.Y);
            frames = totalframes;
            currentFrame = 0;
            frameTimer = new JPTimer(timePerFrame);
            maxPasses = MAXPASSES;
            currentPass = 0;
            name = NAME;
            FireAction = FIREACTION;
            hasFired = false;

            fireFrame = FIREFRAME;
        }

        #region Properties
        public int Frames
        {
            get { return frames; }
        }
        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public int CurrentPass
        {
            get
            {
                return currentPass;
            }
        }
        public int MaxPasses
        {
            get
            {
                return maxPasses;
            }
        }

        #endregion

        //update function for the frame animation class
        public void Update()
        {
            //check if there is anything even required to animate
            if(frames > 1)
            {
                //update timer for the frame
                frameTimer.UpdateTimer();
                //check if the timer is ready(test function) and if the passes are ok, less than max or only one pass
                if (frameTimer.Test() && (maxPasses == 0 || maxPasses > currentPass))
                {
                    //iterate to next frame
                    currentFrame++;
                    if(currentFrame >= frames)
                    {
                        currentPass++;
                    }
                    if(maxPasses == 0 || maxPasses > currentPass)
                    {
                        //go to the next frame in the sprite sheet
                        sheetFrame.X += 1;
                        //if the current frame is larger than the total frames in the sprite sheets columns
                        if(sheetFrame.X >= sheet.X)
                        {
                            //reset column and increase rows
                            sheetFrame.X = 0;
                            sheetFrame.Y += 1;
                        }
                        //if the current frame is larger than the amount of frames
                        if(currentFrame >= frames)
                        {       
                            //reset current frame, has fired, and create a new sheet frame
                            currentFrame = 0;
                            hasFired = false;
                            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
                        }
                    }
                    //have to reset timer in case the animation is ever used again
                    frameTimer.Reset();
                }
            }
            //check if there is a side action in the animation
            if(FireAction != null && fireFrame == currentFrame && !hasFired)
            {
                //do action and set has fired
                FireAction(null);
                hasFired = true;
            }
        }
        
        //reset frames, passes, and start point in sprite sheet
        public void Reset()
        {
            currentFrame = 0;
            currentPass = 0;
            sheetFrame = new Vector2(startFrame.X, startFrame.Y);
            hasFired = false;
        }

        public bool IsAtEnd()
        {
            if(currentFrame+1 >= frames)
            {
                return true;
            }
            return false;
        }

        public void Draw(Texture2D myModel, Vector2 dims, Vector2 imageDims, Vector2 screenShift, Vector2 pos, float ROT, Color color, SpriteEffects spriteEffect)
        {
            Globals.spriteBatch.Draw(myModel, new Rectangle((int)((pos.X + screenShift.X)), (int)((pos.Y + screenShift.Y)), (int)Math.Ceiling(dims.X), (int)Math.Ceiling(dims.Y)), new Rectangle((int)(sheetFrame.X*imageDims.X), (int)(sheetFrame.Y*imageDims.Y), (int)imageDims.X, (int)imageDims.Y), color, ROT, imageDims/2, spriteEffect, 0);
        }

    }
}
