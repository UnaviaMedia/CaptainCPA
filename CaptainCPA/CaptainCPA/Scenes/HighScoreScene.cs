/*
 * Project: CaptainCPA - HighScore.cs
 * Purpose: Display the high scores from an XML file
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 *		Kendall Roth	Dec-09-2015:	Updated User Interface Design
 *										Added high score system
 *						Dec-10-2015:	Updated high score system
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace CaptainCPA
{
	/// <summary>
	/// Display the high scores from an XML file
	/// </summary>
	public class HighScoreScene : GameScene
	{
		private Vector2 position;
		private SpriteFont font;
		private Texture2D menuImage;
		private List<HighScore> highScores;

		public HighScoreScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			position = new Vector2(Settings.Stage.X / 2 - 160, Settings.Stage.Y / 2 - 107);
			font = game.Content.Load<SpriteFont>("Fonts/HighScoreFont");

			menuImage = game.Content.Load<Texture2D>("Images/HighScoreScreen");

			//Get the list of the 5 highest scores
			highScores = Utilities.LoadHighScores().Take(5).ToList();
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
			float tempPosition = position.Y;

			spriteBatch.Begin();

			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);

			//Display the highscores
			//foreach (HighScore highScore in highScores)
			for (int i = 0; i < highScores.Count; i++)
			{
				tempPosition += font.MeasureString(highScores[i].Name).Y;

				//Position and draw the player name
				spriteBatch.DrawString(font, highScores[i].Name, new Vector2(position.X, tempPosition), Color.White);

				//Position and draw the player score
				spriteBatch.DrawString(font, highScores[i].Score.ToString(), 
					new Vector2(position.X + 315 - font.MeasureString(highScores[i].Score.ToString()).X, tempPosition), Color.White);

				if (i == 0)
				{
					tempPosition += 25;
				}
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
