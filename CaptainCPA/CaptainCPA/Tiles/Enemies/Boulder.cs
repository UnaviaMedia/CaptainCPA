/*
 * Project:	PlatformGame - Boulder.cs
 * Purpose:	Type of patrolling enemy
 *
 * History:
 *		Kendall Roth	Nov-28-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Type of patrolling enemy
	/// </summary>
	public class Boulder : Enemy
	{
		private float rotationFactor = 0f;
		private float rotationChange = 0.06f;


		public Boulder(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, velocity, onGround)
		{
			initPosition = new Vector2(Utilities.Stage.X, 3 * Utilities.TILE_SIZE);
			texture = game.Content.Load<Texture2D>("Sprites/Meteor1");
			origin = new Vector2(texture.Width / 2, texture.Height / 2);
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
			if (isMoving)
			{
				rotationFactor -= rotationChange;
			}
			if (position.X <= 0 - Utilities.TILE_SIZE * 3)
			{
				position = initPosition;
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(texture, new Vector2(position.X, position.Y - 10), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotationFactor, origin, 2f, spriteEffect, layerDepth);
			spriteBatch.End();
		}
	}
}
