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
    public enum PauseMenuItems
    {
        Resume, MainMenu, HowTo, Quit
    }
    /// <summary>
    /// Display a StartScene screen with a game menu
    /// </summary>
    public class PauseMenuScene : GameScene
    {
        private MenuComponent menu;

        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        string[] menuItems = {"Resume Game",
                             "Return to Main Menu",
                             "How to play",
                             "Quit"};

        public PauseMenuScene(Game game, SpriteBatch spriteBatch)
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
