/*
 * Project:	PlatformGame - Disc.cs
 * Purpose:	Disc pickup that gives character points
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 *										Points added
 *										Removed Observers (put into other places)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Disc pickup that gives character points
	/// </summary>
	public class Disc : FixedTile
	{
		private int points;

		public int Points
		{
			get { return points; }
			set { points = value; }
		}

		public Disc(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation , float scale, float layerDepth, int points)
			: base(game, spriteBatch, texture, TileType.Pickup, color, position, rotation, scale, layerDepth)
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
