/*
 * Project:	PlatformGame - DisplayString.cs
 * Purpose:	Display a message to the user
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 *						Dec-12-2015:	Removed IBounds implementation
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Base class for displaying strings
	/// </summary>
	public class DisplayString : DrawableGameComponent
	{
		protected SpriteBatch spriteBatch;
		protected SpriteFont spriteFont;
		protected Vector2 position;
		protected Rectangle bounds;
		protected Color color;
		protected string message;

		public SpriteFont SpriteFont
		{
			get { return spriteFont; }
			set { spriteFont = value; }
		}

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Rectangle Bounds
		{
			get { return new Rectangle(
				(int)(Position.X),
				(int)(Position.Y),
				(int)(SpriteFont.MeasureString(Message).X),
				(int)(SpriteFont.MeasureString(Message).Y));
			}
		}

		public string Message
		{
			get { return message; }
			set { message = value; }
		}

		public DisplayString(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 position, Color color, string message = "")
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.SpriteFont = spriteFont;
			this.position = position;
			this.color = color;
			this.message = message;
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
			spriteBatch.DrawString(spriteFont, message, position, color);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
