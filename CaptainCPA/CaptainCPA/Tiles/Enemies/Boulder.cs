using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CaptainCPA
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Boulder : Enemy
    {
        private Vector2 dimension;
        private float rotationFactor = 0f;
        private float rotationChange = 0.1f;
        private int counter;
        public Boulder(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth,
                            Vector2 velocity, bool onGround)
            : base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, velocity, onGround)
        {
            dimension = new Vector2(128, 128);
            texture = game.Content.Load<Texture2D>("Sprites/Meteor1");
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            counter++;
            if (isMoving)
            {
                rotationFactor -= rotationChange;
            }
            if (counter == 300)
            {
                counter = 0;
                position = initPosition;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotationFactor, origin, 1f, spriteEffect, layerDepth);
            //spriteBatch.Draw(bigTexture, position, frames[frameIndex], Color.White, rotation, new Vector2(dimension.X / 2 - 20, dimension.Y / 2 - 15), 0.5f, spriteEffect, layerDepth);
            spriteBatch.End();
        }
    }
}
