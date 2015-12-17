/*
 * Project: CaptainCPA - GameOverMenuScene.cs
 * Purpose: Display the Game Over screen
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created
 *						Dec-10-2015:	Removed dependency on menu system
 */

using CaptainCPA.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Scenes
{
	/// <summary>
	/// Displays the Game Over screen after the game has ended
	/// </summary>
	public class GameOverMenuScene : GameScene
	{
		private Texture2D menuImage;

		public HighScoreComponent HighScoreComponent { get; }

		/// <summary>
		/// Constructor for the Pause Menu Scene
		/// </summary>
		/// <param name="game">The game which calls the pause menu</param>
		/// <param name="spriteBatch">The spritebatch used to draw this menu</param>
		public GameOverMenuScene(Game game, SpriteBatch spriteBatch, Texture2D menuImage, int playerScore)
			: base(game, spriteBatch)
		{
			this.menuImage = menuImage;

			//Add the high score component
			Vector2 highScorePosition = new Vector2(Utilities.Utilities.Stage.X / 2 - 175, Utilities.Utilities.Stage.Y / 2 - 30);
			HighScoreComponent = new HighScoreComponent(game, spriteBatch, game.Content.Load<SpriteFont>("Fonts/HighScoreFont"), highScorePosition, playerScore);
			this.components.Add(HighScoreComponent);
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
