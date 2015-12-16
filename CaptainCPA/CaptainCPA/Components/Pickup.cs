/*
 * Project: CaptainCPA - Pickup.cs
 * Purpose: Base class for character pickups
 *
 * History:
 *		Kendall Roth	Dec-12-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Base class for character pickups
	/// </summary>
	public class Pickup : FixedTile
	{
		protected int points;

		public int Points
		{
			get { return points; }
		}

		public Pickup(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth, int points)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth)
		{
			this.points = points;
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
