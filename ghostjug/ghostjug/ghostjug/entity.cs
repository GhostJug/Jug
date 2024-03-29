﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using directives
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ghostjug
{
    class entity
    {
        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;

        //the name of the asset for the sprite's texture
        public String AssetName;

        //The size of the Sprite
        public Rectangle Size;

        //Used to size the Sprite up or down from the original image
        private float mScale = 1.0f;

        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            }

        }

        //the entities position in the game world
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //The Rectangular area from the original image that 
        //defines the Sprite. 
        Rectangle mSource;
        public Rectangle Source
        {
            get { return mSource; }
            set
            {
                mSource = value;
                Size = new Rectangle(0, 0, (int)(mSource.Width * Scale), (int)(mSource.Height * Scale));
            }
        }

        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Source = new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }

        //Update the Sprite and change it's position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            Position += theDirection * theSpeed;// *(float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        //Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mSpriteTexture, Position, Source,
               Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}
