/*
 * Project: CaptainCPA - PauseMenuScene.cs
 * Purpose: Display the pause menu
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 *		Kendall Roth	Dec-08-2015:	Added draw method, menu image
 */

using CaptainCPA.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Scenes
{
	/// <summary>
	/// Enumerates the menu items in the Pause menu
	/// </summary>
	public enum PauseMenuItems
	{
		Resume, HowTo, MainMenu, Quit
	}

	/// <summary>
	/// Displays the pause menu while game is being played
	/// </summary>
	public class PauseMenuScene : GameScene
	{
		private Texture2D menuImage;
		private MenuComponent menu;
		string[] menuItems = {"Resume Game", "How to play", "Exit to Main Menu", "Quit"};

		public MenuComponent Menu
		{
			get { return menu; }
			set { menu = value; }
		}

		/// <summary>
		/// Constructor for the Pause Menu Scene
		/// </summary>
		/// <param name="game">The game which calls the pause menu</param>
		/// <param name="spriteBatch">The spritebatch used to draw this menu</param>
		public PauseMenuScene(Game game, SpriteBatch spriteBatch, Texture2D menuImage)
			: base(game, spriteBatch)
		{
			Vector2 menuPosition = new Vector2(Utilities.Utilities.Stage.X / 2 - 155, Utilities.Utilities.Stage.Y / 2 - 110);
			menu = new MenuComponent(game, spriteBatch,
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				menuItems, menuPosition);
			this.Components.Add(menu);

			this.menuImage = menuImage;
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
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
