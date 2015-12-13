/*
 * Project:	PlatformGame - ScoreDisplay.cs
 * Purpose:	Displays the character's score
 *
 * History:
 *		Kendall Roth	Dec-12-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Displays the health for the character
	/// </summary>
	public class ScoreDisplay : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Texture2D texture;
		private SpriteFont font;
		private Vector2 position;
		private Character character;

		public ScoreDisplay(Game game, SpriteBatch spriteBatch, Character character)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.character = character;
			
			texture = game.Content.Load<Texture2D>("Displays/ScoreDisplay");
			font = game.Content.Load<SpriteFont>("Fonts/ScoreFont");
			
			position = new Vector2(75, 75);
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

			//Display the character's score
			spriteBatch.Draw(texture, position, Color.White);
			spriteBatch.DrawString(font, character.Score.ToString(),
				new Vector2((position.X + (texture.Width / 2)) - (font.MeasureString(character.Score.ToString()).X / 2),
				(position.Y + (texture.Height / 2)) - (font.MeasureString(character.Score.ToString()).Y / 2)), Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
