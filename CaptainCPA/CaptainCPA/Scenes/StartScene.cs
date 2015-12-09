/*
 * Project: CaptainCPA - StartScene.cs
 * Purpose: Display a StartScene screen with a game menu
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Dec-08-2015:	Added draw method, menu image
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CaptainCPA
{
	/// <summary>
	/// Enumeration of Main Menu Items
	/// </summary>
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
		private Texture2D menuImage;
		
		public MenuComponent Menu
		{
			get { return menu; }
			set { menu = value; }
		}

		private string[] menuItems = {"Start Game",
							 "Select Level",
							 "Help",
							 "High Score",
							 "About/Credit",
							 "How to play",
							 "Quit"};


		public StartScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{			
			//Set up the menu
			Vector2 menuPosition = new Vector2(Settings.Stage.X / 2 + 140, Settings.Stage.Y / 2 - 60);
			menu = new MenuComponent(game, spriteBatch,
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				menuItems, menuPosition);
			this.Components.Add(menu);

			menuImage = game.Content.Load<Texture2D>("Images/MainMenu");
						
			ScrollingBackground background = new ScrollingBackground(game, spriteBatch);

			this.Components.Add(background);
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
			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();

			//Draw main image background
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}