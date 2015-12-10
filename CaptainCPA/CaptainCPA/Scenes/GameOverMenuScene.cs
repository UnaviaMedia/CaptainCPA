/*
 * Project: CaptainCPA - GameOverMenuScene.cs
 * Purpose: Display the game over menu
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Enumerates the menu items in the Game Over menu
	/// </summary>
	public enum GameOverMenuItems
	{
		MainMenu
	}

	/// <summary>
	/// Displays the game over menu after the game has ended
	/// </summary>
	public class GameOverMenuScene : GameScene
	{
		private MenuComponent menu;
		private Texture2D menuImage;
		private HighScoreComponent highScoreComponent;
		private string[] menuItems = {"Exit to Main Menu"};

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
		public GameOverMenuScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			//Add the game over menu
			Vector2 menuPosition = new Vector2(Settings.Stage.X / 2 - 155, Settings.Stage.Y / 2 + 125);
			menu = new MenuComponent(game, spriteBatch,
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				menuItems, menuPosition);
			this.Components.Add(menu);

			menuImage = game.Content.Load<Texture2D>("Images/GameOverMenu");

			//Add the high score component
			Vector2 highScorePosition = new Vector2(Settings.Stage.X / 2 - 175, Settings.Stage.Y / 2 - 175);
			highScoreComponent = new HighScoreComponent(game, spriteBatch, game.Content.Load<SpriteFont>("Fonts/HighScoreFont"), highScorePosition);
			this.components.Add(highScoreComponent);
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

			//Draw the game over menu
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
