/*
 * Project: CaptainCPA - Pursuing Enemy.cs
 * Purpose: Base class for enemies that will patrol a level regardless of player position
 *
 * History:
 *		Kendall Roth	Dec-16-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Components
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public abstract class PatrollingEnemy : Enemy
	{
		public PatrollingEnemy(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth,
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
			base.Update(gameTime);
		}
	}
}