using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ghostjug
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GamePadState oldGPState;
        KeyboardState oldKBState;

        //display parameters
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 800;
        private const int BackBufferHeight = 600;

        //debug string parameters
        bool debugging = true;
        SpriteFont Font1;
        Vector2 FontPos;
        string DebugStringOutput = "";

        MouseState ms;

        player mPlayerSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            // Set vertical trace with the back buffer  
            graphics.SynchronizeWithVerticalRetrace = false;

            // Use multi-sampling to smooth corners of objects  
            graphics.PreferMultiSampling = true;

            // Set the update to run as fast as it can go or  
            // with a target elapsed time between updates  
            IsFixedTimeStep = false;

            // Make the mouse appear  
            IsMouseVisible = true;

            // Set back buffer resolution  
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;

            Content.RootDirectory = "Content";

            // Framerate differs between platforms.
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFrameRate);
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

            mPlayerSprite = new player();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load debug font
            Font1 = Content.Load<SpriteFont>("Courier New");
            mPlayerSprite.LoadContent(this.Content);
                      

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            HandleInput();
            // TODO: Add your update logic here
            mPlayerSprite.Update(gameTime);
            base.Update(gameTime);
        }


        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            // Exit the game when back is pressed.
            if (gamepadState.Buttons.Back == ButtonState.Pressed)
                Exit();

            //use the right stick to turn the debbugging on or off
            if (gamepadState.Buttons.RightStick == ButtonState.Pressed && oldGPState.Buttons.RightStick !=ButtonState.Pressed)
                debugging = !debugging;

            ms= Mouse.GetState();

            oldGPState = gamepadState;
            oldKBState = keyboardState;

        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            DrawHud();
            mPlayerSprite.Draw(this.spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawHud()
        {
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            Vector2 hudLocation = new Vector2(titleSafeArea.X, titleSafeArea.Y);
            Vector2 center = new Vector2(titleSafeArea.X + titleSafeArea.Width / 2.0f,
                                         titleSafeArea.Y + titleSafeArea.Height / 2.0f);
            if (debugging)
            {
                DebugStringOutput = "***Debugging***\nMouse X :" + ms.X.ToString() + "\nMouse Y :" + ms.Y.ToString() + 
                    "\nSprite Position\nX:"+mPlayerSprite.Position.X.ToString()+"\nY: " + mPlayerSprite.Position.Y.ToString()+
                    "\nSprite map position\nX: " + mPlayerSprite.spritePosX.ToString() +"\nY:" + mPlayerSprite.spritePosY.ToString() ;
                FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width - 300, 10);
                // Find the center of the string
                //Vector2 FontOrigin = Font1.MeasureString(output);
                Vector2 FontOrigin = new Vector2(0, 0);
                // Draw the string
                spriteBatch.DrawString(Font1, DebugStringOutput, FontPos, Color.LightGreen,
                    0, FontOrigin, 0.5f, SpriteEffects.None, 0.5f);
            }
        }
    }
}
