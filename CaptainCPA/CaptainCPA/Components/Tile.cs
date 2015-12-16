/*
 * Project: CaptainCPA - Tile.cs
 * Purpose: Base class for all tiles in a platform level
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Dec-11-2015:	Removed Observer pattern
 *		Doug Epp						Added initial position property (for level reset after death)
 *		Kendall Roth	Dec-12-2015:	Removed IBounds implementation
 *						Dec-13-2015:	Added IsCollideable property
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Base class for all tiles in a platform level
	/// </summary>
	public class Tile : DrawableGameComponent
	{
		protected SpriteBatch spriteBatch;
		protected Texture2D texture;
		protected Color color;
		protected Vector2 position;
		protected Vector2 initPosition;
		protected Rectangle bounds;
		protected bool isCollideable;

		protected float rotation = 0.0f;
		protected float scale = 1.0f;
		protected Vector2 origin;
		protected SpriteEffects spriteEffect;
		protected float layerDepth = 1.0f;

		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
		}

		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public Vector2 InitPosition
		{
			get { return initPosition; }
			set { initPosition = value; }
		}

		public bool IsCollideable
		{
			get { return isCollideable; }
			set { isCollideable = value; }
		}

		public SpriteEffects SpriteEffects
		{
			get { return spriteEffect; }
			set { spriteEffect = value; }
		}

		/// <summary>
		/// Bounding Rectangle of Tile
		/// </summary>
		public Rectangle Bounds
		{
			get { return new Rectangle(
				(int)(position.X - (texture.Width / 2 * scale)),
				(int)(position.Y - (texture.Height / 2 * scale)),
				(int)(texture.Width * scale),
				(int)(texture.Height * scale));
			}
		}


		public Tile(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position,
			float rotation, float scale, float layerDepth, bool isCollideable = true)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.texture = texture;
			origin = new Vector2(texture.Width / 2, texture.Height / 2);
			this.color = color;
			this.position = new Vector2(position.X + origin.X, position.Y + origin.Y);
			this.isCollideable = isCollideable;
			this.initPosition = position;

			this.rotation = rotation;
			this.scale = scale;
			spriteEffect = SpriteEffects.None;
			this.layerDepth = layerDepth;
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
		/// Destroy the tile by hiding and disabling it
		/// </summary>
		public virtual void Destroy()
		{
			this.Visible = false;
			this.Enabled = false;
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
		/// Allows the game component to draw itself
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();

			//Draw if tile is on screen
			if (position.X >= -texture.Width && position.X + texture.Width <= Utilities.Stage.X + (2 * texture.Width))
			{
				spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, spriteEffect, layerDepth);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
