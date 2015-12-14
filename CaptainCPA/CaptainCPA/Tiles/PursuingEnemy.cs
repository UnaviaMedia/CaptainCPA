using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class PursuingEnemy : Enemy
	{
		public PursuingEnemy(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, velocity, onGround)
		{

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
			//chase character to the left
			if (position.X > CharacterStateManager.CharacterPosition.X)
			{
				velocity.X = -(Math.Abs(xSpeed));
			}
			//chase character to the right
			else if (position.X < CharacterStateManager.CharacterPosition.X)
			{
				velocity.X = Math.Abs(xSpeed);
			}
			//If the enemy is directly above or below the character, pace back and forth
			if (position.X >= CharacterStateManager.CharacterPosition.X - (Utilities.TILE_SIZE) && position.X <= CharacterStateManager.CharacterPosition.X + Utilities.TILE_SIZE / 2)
			{
				velocity.X = (facingRight) ? Math.Abs(xSpeed) : -Math.Abs(xSpeed);
				//if (facingRight) velocity.X = Math.Abs(xSpeed);
				//else velocity.X = -Math.Abs(xSpeed);
			}

			base.Update(gameTime);
		}
	}
}