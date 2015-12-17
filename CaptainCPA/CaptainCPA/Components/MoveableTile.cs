/*
 * Project: CaptainCPA - MoveableTile.cs
 * Purpose: Moveable platform tile
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Components
{
	/// <summary>
	/// Moveable platform tile
	/// </summary>
	public abstract class MoveableTile : Tile
	{
		protected Vector2 velocity;
		protected Vector2 gravity = new Vector2(0.0f, 0.2f);
		protected bool onGround;
		protected bool isMoving;
		protected bool facingRight;

		public Vector2 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		public Vector2 Gravity
		{
			get { return gravity; }
			set { gravity = value; }
		}

		public bool OnGround
		{
			get { return onGround; }
			set { onGround = value; }
		}

		protected MoveableTile(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth, 
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth)
		{
			this.velocity = velocity;
			this.onGround = onGround;
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
			if(facingRight)
			{
				spriteEffect = SpriteEffects.None;
			}
			else
			{
				spriteEffect = SpriteEffects.FlipHorizontally;
			}

			if (velocity.X == 0)
			{
				isMoving = false;
			}
			else
			{
				isMoving = true;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
		}
	}
}
