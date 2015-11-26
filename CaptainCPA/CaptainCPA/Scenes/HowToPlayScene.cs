/*
 * Project: CaptainCPA - HowToPlayScene.cs
 * Purpose: Displays the game instructions
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
    /// <summary>
    /// This is a game scene which displays gameplay instruction from a text file
    /// </summary>
    public class HowToPlayScene : GameScene
    {
        private string message;
        private SpriteFont font;
        public HowToPlayScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            font = game.Content.Load<SpriteFont>("Fonts/MenuFont");
            message = readFile(@"Text/HowToPlayMessage.txt");
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
            spriteBatch.DrawString(font, message, new Vector2(Settings.TILE_SIZE * 13, Settings.TILE_SIZE * 6), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
