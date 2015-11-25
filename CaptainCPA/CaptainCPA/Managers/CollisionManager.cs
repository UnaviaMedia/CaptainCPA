/*
 * Project: CaptainCPA - CollisionManager.cs
 * Purpose: Base CollisionManager component
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CaptainCPA
{
	/// <summary>
	/// Base CollisionManager component
	/// </summary>
	public class CollisionManager : GameComponent
	{
		protected List<Tile> tiles;
		protected List<FixedTile> fixedTiles;
		protected List<MoveableTile> moveableTiles;

		public CollisionManager(Game game, List<Tile> tiles)
			: base(game)
		{
			this.tiles = tiles;

			fixedTiles = new List<FixedTile>();
			moveableTiles = new List<MoveableTile>();

			foreach (Tile tile in tiles)
			{
				if (tile is FixedTile)
				{
					fixedTiles.Add((FixedTile)tile);
				}
				else if (tile is MoveableTile)
				{
					moveableTiles.Add((MoveableTile)tile);
				}
			}
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
