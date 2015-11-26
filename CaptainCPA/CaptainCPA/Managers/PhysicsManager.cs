/*
 * Project: CaptainCPA - PhysicsManager.cs
 * Purpose: Manages physics
 *
 * History:
 *		Kendall Roth	Nov-26-2015:	Created
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
	/// Manages collisions and post-collision positioning between tiles
	/// </summary>
	public class PhysicsManager : CollisionManager
	{
		public PhysicsManager(Game game, List<Tile> tiles)
			: base(game, tiles)
		{
			// TODO: Construct any child components here
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			// TODO: Add your initialization code here

			base.Initialize();
		}

		
		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			foreach (MoveableTile moveableTile in tiles)
			{
				//------------------------------------------------------------------------------
				//X-axis
				//------------------------------------------------------------------------------

				int frontX = (int)moveableTile.Position.X;

				//Get forward rectangle bounds x-coordinate
				if (moveableTile.Velocity.X < 0) //Left movement
				{
					//Console.WriteLine("Left movement");
					frontX = moveableTile.Bounds.Left;
				}
				else if (moveableTile.Velocity.X > 0) //Right movement
				{
					//Console.WriteLine("Right movement");
					frontX = moveableTile.Bounds.Right;
				}

				//Find horizontal rows character intersects with (two max)
				//	Horizontal row that contains upper bounds (y)
				//	Horizontal row that contains lower bounds (y)
				int topRow = (int)Math.Floor(moveableTile.Bounds.Top / Settings.TILE_SIZE);
				int bottomRow = (int)Math.Floor(moveableTile.Bounds.Bottom / Settings.TILE_SIZE);

				FixedTile closestTile = null;

				//Check these horizontal rows - in direction of movement - and determine which is the closest fixed tile
				foreach (FixedTile fixedTile in tiles)
				{
					//Get the row that the current fixed tile is in
					int fixedTileRow = (int)Math.Floor(moveableTile.Position.Y / Settings.TILE_SIZE);

					//If the current fixed tile is not on the moveable tile's levels skip to the next iteration
					if (fixedTileRow != topRow && fixedTileRow != bottomRow)
					{
						continue;
					}

					//Left movement
					if (moveableTile.Velocity.X < 0)
					{
						//If the current fixed tile is behind the player (to the right) skip to the next iteration
						if (frontX > fixedTile.Bounds.Right)
						{
							continue;
						}

						if (closestTile == null)
						{
							closestTile = fixedTile;
						}
						else
						{
							if (Vector2.Distance(moveableTile.Position, closestTile.Position) > Vector2.Distance(moveableTile.Position, fixedTile.Position))
							{
								closestTile = fixedTile;
							}
						}
					}
					//Right movement
					else if (moveableTile.Velocity.X > 0)
					{
						//If the current fixed tile is behind the player (to the left) skip to the next iteration
						if (frontX > fixedTile.Bounds.Left)
						{
							continue;
						}

						if (closestTile == null)
						{
							closestTile = fixedTile;
						}
						else
						{
							if (Vector2.Distance(moveableTile.Position, closestTile.Position) > Vector2.Distance(moveableTile.Position, fixedTile.Position))
							{
								closestTile = fixedTile;
							}
						}
					}
				}

				//Find total movement of player
				//	Minimum between distance to closest fixed tile and usual player movement
				float distanceToMove = 0;
				
				if (moveableTile.Velocity.X < 0)
				{
					distanceToMove = Math.Min(moveableTile.Velocity.X, closestTile.Bounds.Right - moveableTile.Bounds.Left);
				}
				else if (moveableTile.Velocity.X > 0)
				{
					distanceToMove = Math.Min(moveableTile.Velocity.X, closestTile.Bounds.Left - moveableTile.Bounds.Right);
				}

				//Update player's horizontal position
				moveableTile.Position = new Vector2(moveableTile.Position.X + distanceToMove, moveableTile.Position.Y);
			}

			base.Update(gameTime);
		}
	}
}
