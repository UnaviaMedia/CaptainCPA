/*
 * Project:	PlatformGame - HealthDisplay.cs
 * Purpose:	Displays the character's health
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Displays the health for the character
	/// </summary>
	public class HealthDisplay : DrawableGameComponent, IBounds
	{
		private SpriteBatch spriteBatch;
		private Texture2D texture;
		private Vector2 position;
		private Rectangle bounds;

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Rectangle Bounds
		{
			get { return bounds; }
			set { bounds = value; }
		}

		public HealthDisplay(Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
			: base(game)
		{
			this.texture = texture;
			this.spriteBatch = spriteBatch;
			this.position = position;

			UpdateBounds();
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
		/// Find the rectangle representing the bounds of the object
		/// </summary>
		public void UpdateBounds()
		{			
			Bounds = new Rectangle(
				(int)(Position.X),
				(int)(Position.Y),
				(int)(50),
				(int)(50));
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			UpdateBounds();

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(texture, position, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
