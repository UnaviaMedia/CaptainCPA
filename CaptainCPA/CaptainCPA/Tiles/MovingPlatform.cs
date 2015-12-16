using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class MovingPlatform : Platform
    {
        private int range;
        private bool goRight;
        private bool movingRight;
        private float fixedPosition;

        public float FixedPosition
        {
            get { return fixedPosition; }
            set { fixedPosition = value; }
        }


        public MovingPlatform(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth, int range, bool goRight)
            : base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth)
        {
            fixedPosition = position.X;
            this.range = range;
            this.goRight = goRight;
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
            if (goRight)
            {
                if (movingRight)
                {
                    position.X += 1;
                    if (position.X >= fixedPosition + Utilities.TILE_SIZE * range)
                    {
                        movingRight = false;
                    }
                }
                else
                {
                    position.X -= 1;
                    if (position.X <= fixedPosition)
                    {
                        movingRight = true;
                    }
                } 
            }
            else
            {
                if (!movingRight)
                {
                    position.X -= 1;
                    if (position.X <= fixedPosition - Utilities.TILE_SIZE * range)
                    {
                        movingRight = true;
                    }
                }
                else
                {
                    position.X += 1;
                    if (position.X >= fixedPosition)
                    {
                        movingRight = false;
                    }
                } 
            }
        }
    }
}
