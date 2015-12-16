/*
 * Project: CaptainCPA - Block.cs
 * Purpose: Base class for enemies for the character to avoid
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace CaptainCPA
{
	/// <summary>
	/// Base class for enemies for the character to avoid
	/// </summary>
	public abstract class Enemy : MoveableTile
	{
		protected float xSpeed;


		public Enemy(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, velocity, onGround)
		{
			xSpeed = velocity.X;
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
			if (velocity.X == Math.Abs(velocity.X))
			{
				facingRight = true;
			}
			else
			{
				facingRight = false;
			}
			base.Update(gameTime);
		}
	}
}
