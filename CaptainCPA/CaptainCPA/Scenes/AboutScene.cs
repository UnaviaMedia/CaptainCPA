/*
 * Project: CaptainCPA - About.cs
 * Purpose: Display the game's about information
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
    /// <summary>
    /// Displays the game's about information from a text file
    /// </summary>
    class AboutScene : GameScene
    {
        string message;
        
        private SpriteFont font;

        public AboutScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            font = game.Content.Load<SpriteFont>("Fonts/MenuFont");
            message = readFile(@"Text/AboutMessage.txt");
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
