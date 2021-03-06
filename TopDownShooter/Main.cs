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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GamePlay gamePlay;

        MainMenu menu;

        Basic2d cursor;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Initializing settings or getting resolution

            Globals.screenHeight = 800;
            Globals.screenWidth = 1200;

            graphics.PreferredBackBufferHeight = Globals.screenHeight;
            graphics.PreferredBackBufferWidth = Globals.screenWidth;

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // Anything not loaded in load thread, loaded here, start load thread at the end of the function
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            cursor = new Basic2d("2d\\Misc\\CursorArrow", new Vector2(0, 0), new Vector2(28, 28));

            Globals.normalEffect = Globals.content.Load<Effect>("Shaders\\2d shaders\\NormalFlat");

            Globals.keyboard = new JPKeyboard();
            Globals.mouse = new JPMouseControl();

            menu = new MainMenu(ChangeGameState, ExitGame);
            gamePlay = new GamePlay(ChangeGameState);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // Get rid of anything you need to, good for memory management
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // All of the game logic updates here, if not done here, lose efficiency

            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            if(Globals.gameState == 0)
            {
                menu.Update();
            } 
            else if(Globals.gameState == 1)
            {
                gamePlay.Update();
            }


            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        public virtual void ChangeGameState(object INFO)
        {
            Globals.gameState = Convert.ToInt32(INFO, Globals.culture);
        }

        public virtual void ExitGame(object INFO)
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Try to run no logic in this function

            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if(Globals.gameState == 0)
            {
                menu.Draw();
            } 
            else if(Globals.gameState == 1)
            {
                gamePlay.Draw();
            }

            Globals.normalEffect.Parameters["xSize"].SetValue((float)cursor.texture.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)cursor.texture.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)cursor.dims.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)cursor.dims.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();
            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
#endif
}
