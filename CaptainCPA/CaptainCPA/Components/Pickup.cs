/*
 * Project: CaptainCPA - Pickup.cs
 * Purpose: Base class for character pickups
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

		public Pickup(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth, int points)
			: base(game, spriteBatch, texture, tileType, color, position, rotation, scale, layerDepth)
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
