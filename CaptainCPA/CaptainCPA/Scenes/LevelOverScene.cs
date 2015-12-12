/*
 * Project: CaptainCPA - LevelOverScene.cs
 * Purpose: Display the level over scene and allow the user to advance to the next level
 *
 * History:
 *		Kendall Roth	Dec-12-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Displays the pause menu while game is being played
	/// </summary>
	public class LevelOverScene : GameScene
	{
		private Texture2D menuImage;
		private SpriteFont font;
		private int currentScore;


		/// <summary>
		/// Constructor for the Pause Menu Scene
		/// </summary>
		/// <param name="game">The game which calls the pause menu</param>
		/// <param name="spriteBatch">The spritebatch used to draw this menu</param>
		public LevelOverScene(Game game, SpriteBatch spriteBatch, int currentScore)
			: base(game, spriteBatch)
		{			
			menuImage = game.Content.Load<Texture2D>("Images/LevelOverScreen");
			font = game.Content.Load<SpriteFont>("Fonts/HighScoreFont");
			this.currentScore = currentScore;
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

			//Draw the pause menu
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);

			//Draw the current score
			spriteBatch.DrawString(font, currentScore.ToString(),
				new Vector2((Settings.Stage.X / 2) - (font.MeasureString(currentScore.ToString()).X / 2), (Settings.Stage.Y / 2) - 22), Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
