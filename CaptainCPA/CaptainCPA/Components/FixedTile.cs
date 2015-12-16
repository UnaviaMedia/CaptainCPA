/*
 * Project: CaptainCPA - FixedTile.cs
 * Purpose: Base class for fixed tiles
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-28-2015:	Added X and Y coordinates
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CaptainCPA
{
	/// <summary>
	/// Base class for fixed tiles
	/// </summary>
	public abstract class FixedTile : Tile
	{
		protected int xPosition;
		protected int yPosition;

		public int XPosition
		{
			get
			{
				return (int)Math.Floor(position.X / Utilities.TILE_SIZE);
			}
		}

		public int YPosition
		{
			get
			{
				return (int)Math.Floor(position.Y / Utilities.TILE_SIZE);
			}
		}

		public FixedTile(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth)
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
