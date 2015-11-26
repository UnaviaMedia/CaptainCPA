/*
 * Project: CaptainCPA - StartScene.cs
 * Purpose: Display a StartScene screen with a game menu
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
    public enum menuItemTitles
    {
        Start, Select, Help, HighScore, About, HowTo, Quit
    }
    /// <summary>
    /// Display a StartScene screen with a game menu
    /// </summary>
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        
        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        string[] menuItems = {"Start Game",
                             "Select Level",
                             "Help",
                             "High Score",
                             "About/Credit",
                             "How to play",
                             "Quit"};

        public StartScene(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            menu = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("Fonts/MenuFont"),
                game.Content.Load<SpriteFont>("Fonts/MenuFont"),
                menuItems);
            this.Components.Add(menu);
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
    }
}
