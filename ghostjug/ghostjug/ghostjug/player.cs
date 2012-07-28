using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ghostjug
{
    class player:entity
    {
        ContentManager mContentManager;

        const string PLAYER_ASSETNAME = "nethack";
        const int START_POSITION_X = 0;
        const int START_POSITION_Y = 0;
        const int PLAYER_SPEED = 32;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        //used for selecting different sprites
        public int spritePosX = 0;
        public int spritePosY = 0;

        enum State
        {
            Walking,
            Jumping,
            Ducking
        }
        State mCurrentState = State.Walking;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;

        Vector2 mStartingPosition = Vector2.Zero;

        KeyboardState mPreviousKeyboardState;
        GamePadState mPreviousGamePadState;

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;

            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            Source = new Rectangle(spritePosX, spritePosY, 32, 32);
            base.LoadContent(theContentManager, PLAYER_ASSETNAME);
            
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            GamePadState aCurrentGamePadState = GamePad.GetState(PlayerIndex.One);

            UpdateMovement(aCurrentKeyboardState, aCurrentGamePadState);

            mPreviousKeyboardState = aCurrentKeyboardState;
            mPreviousGamePadState = aCurrentGamePadState;
            base.Update(theGameTime, mSpeed, mDirection);
        }

        private void UpdateMovement(KeyboardState aCurrentKeyboardState, GamePadState aCurrentGamePadState)
        {
            if (mCurrentState == State.Walking)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) && !mPreviousKeyboardState.IsKeyDown(Keys.Left))
                {
                    mSpeed.X = PLAYER_SPEED*Scale;
                    mDirection.X = MOVE_LEFT;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) && !mPreviousKeyboardState.IsKeyDown(Keys.Right))
                {
                    mSpeed.X = PLAYER_SPEED * Scale;
                    mDirection.X = MOVE_RIGHT;
                }

                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) && !mPreviousKeyboardState.IsKeyDown(Keys.Up))
                {
                    mSpeed.Y = PLAYER_SPEED * Scale;
                    mDirection.Y = MOVE_UP;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down)&& !mPreviousKeyboardState.IsKeyDown(Keys.Down))
                {
                    mSpeed.Y = PLAYER_SPEED * Scale;
                    mDirection.Y = MOVE_DOWN;
                }

                if (aCurrentGamePadState.IsButtonDown(Buttons.DPadRight) && !mPreviousGamePadState.IsButtonDown(Buttons.DPadRight))
                    spritePosX = (spritePosX > 28) ? 0 : spritePosX + 1;
                if (aCurrentGamePadState.IsButtonDown(Buttons.DPadLeft) && !mPreviousGamePadState.IsButtonDown(Buttons.DPadLeft))
                    spritePosX = (spritePosX <= 0) ? 29 : spritePosX - 1;
                if (aCurrentGamePadState.IsButtonDown(Buttons.DPadDown) && !mPreviousGamePadState.IsButtonDown(Buttons.DPadDown))
                    spritePosY = (spritePosY > 28) ? 0 : spritePosY + 1;
                if (aCurrentGamePadState.IsButtonDown(Buttons.DPadUp) && !mPreviousGamePadState.IsButtonDown(Buttons.DPadUp))
                    spritePosY = (spritePosY <= 0) ? 29 : spritePosY - 1;
                if (aCurrentGamePadState.IsButtonDown(Buttons.RightShoulder) && !mPreviousGamePadState.IsButtonDown(Buttons.RightShoulder))
                    Scale = (Scale > 4) ? 5 : Scale + 1;
                if (aCurrentGamePadState.IsButtonDown(Buttons.LeftShoulder) && !mPreviousGamePadState.IsButtonDown(Buttons.LeftShoulder))
                    Scale = (Scale < 2) ? 1 : Scale - 1;

            }
        }
        public override void Draw(SpriteBatch theSpriteBatch)
        {
            Source = new Rectangle(spritePosX*32, spritePosY*32, 32, 32);
            base.Draw(theSpriteBatch);
        }

    }
}
