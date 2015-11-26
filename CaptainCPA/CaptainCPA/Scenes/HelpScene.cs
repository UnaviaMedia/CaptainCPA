/*
 * Project: CaptainCPA - HelpScene.cs
 * Purpose: Display a HelpScene screen
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
    /// <summary>
    /// This is the scene which displays help information
    /// </summary>
    public class HelpScene : GameScene
    {
        private string message;
        private SpriteFont font;

        public HelpScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            font = game.Content.Load<SpriteFont>("Fonts/MenuFont");
            message = readFile(@"Text/HelpMessage.txt");
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
