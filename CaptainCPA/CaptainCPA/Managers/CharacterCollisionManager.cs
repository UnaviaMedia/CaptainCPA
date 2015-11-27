/*
 * Project:	PlatformGame - CharacterCollisionManager.cs
 * Purpose:	Manage non-positioning collision (extra logic)
 *
 * History:
 *		Kendall Roth	Nov-22-2015:	Created
 *										Collision detection with observers added
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Manage non-positioning collisions (extra logic)
	/// </summary>
	public class CharacterCollisionManager : CollisionManager
	{
		private List<Character> characters;

		public CharacterCollisionManager(Game game, List<Tile> tiles)
			: base(game, tiles)
		{
			characters = new List<Character>();

			foreach (Tile tile in tiles)
			{
				if (tile is Character)
				{
					characters.Add((Character)tile);
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
			foreach (Character character in characters)
			{
				foreach (Tile tile in tiles)
				{
					//Skip collision detection if the moveable tile is hidden or disabled
					if (tile.Visible == false || tile.Enabled == false || character.Equals(tile))
					{
						continue;
					}

					//Skip collision detection if the moveable tile is too far away or doesn't intersect
					if (Vector2.Distance(Utilities.PointToVector2(character.Bounds.Center), Utilities.PointToVector2(tile.Bounds.Center)) > 100 ||
						character.Bounds.Intersects(tile.Bounds) == false)
					{
						continue;
					}

					if (tile is Gem)
					{
						tile.Notify(tile, "GemCollected", character);
					}
				}
			}

			base.Update(gameTime);
		}
	}
}