/*
 * Project: CaptainCPA - Obstacle.cs
 * Purpose: Base class for obstacles for the character to avoid
 *
 * History:
 *		Kendall Roth	Dec-16-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Components
{
	/// <summary>
	/// Base class for obstacles for the character to avoid
	/// </summary>
	public abstract class Obstacle : FixedTile
	{
		public Obstacle(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth)
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
