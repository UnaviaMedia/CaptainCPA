/*
 * Project: CaptainCPA - PhysicsManager.cs
 * Purpose: Manages physics for moveable tiles
 *
 * History:
 *		Kendall Roth	Nov-26-2015:	Created
 *										TileCollision checking and avoidance positioning added
 *						Nov-27-2015:	Added top collision checking (player immediately starts falling again)
 *						Nov-29-2015:	Optimizations
 *						Dec-12-2015:	Updated with better logic for determining if tile is withing the necessary columns (bounds) for collision checking
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Manages physics and collision avoidance for tiles
	/// </summary>
	public class PhysicsManager : CollisionManager
	{
		public PhysicsManager(Game game, List<MoveableTile> moveableTiles, List<FixedTile> fixedTiles)
			: base(game, moveableTiles, fixedTiles)
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
			foreach (MoveableTile moveableTile in moveableTiles)
			{
				//------------------------------------------------------------------------------
				//X-axis
				//------------------------------------------------------------------------------

				Direction horizontalDirection = Direction.None;
				int frontX = (int)moveableTile.Position.X;

				//Get forward rectangle bounds x-coordinate
				if (moveableTile.Velocity.X < 0) //Left movement
				{
					//Console.WriteLine("Left movement");
					horizontalDirection = Direction.Left;
					frontX = moveableTile.Bounds.Left;
				}
				else if (moveableTile.Velocity.X > 0) //Right movement
				{
					//Console.WriteLine("Right movement");
					horizontalDirection = Direction.Right;
					frontX = moveableTile.Bounds.Right;
				}

				FixedTile closestHorizontalTile = null;

				//Check these horizontal rows - in direction of movement - and determine which is the closest fixed tile
				foreach (FixedTile fixedTile in fixedTiles)
				{
					if (fixedTile.TileType == TileType.Decoration || fixedTile.TileType == TileType.Pickup ||
						fixedTile.TileType == TileType.LevelEnd || fixedTile.TileType == TileType.Obstacle || fixedTile.IsCollideable == false)
					{
						continue;
					}

					//Check whether or not the moveable tile is actually within the vertical bounds of the fixed tile (same columns)
					//if (f.t <= m.t && f.b > m.t)
					//else if (f.b >= m.b && f.t < m.b)
					//else -> block should fall
					if ((fixedTile.Bounds.Top <= moveableTile.Bounds.Top && fixedTile.Bounds.Bottom > moveableTile.Bounds.Top) != true &&
						(fixedTile.Bounds.Bottom >= moveableTile.Bounds.Bottom && fixedTile.Bounds.Top < moveableTile.Bounds.Bottom) != true)
					{
						continue;
					}

					if (horizontalDirection == Direction.Left)
					{
						//If the current fixed tile is behind the player (to the right) skip to the next iteration
						if (frontX < fixedTile.Bounds.Right)
						{
							continue;
						}
					}
					else if (horizontalDirection == Direction.Right)
					{
						//If the current fixed tile is behind the player (to the left) skip to the next iteration
						if (frontX > fixedTile.Bounds.Left)
						{
							continue;
						}
					}

					//Determine if the current fixed tile is closer than the current closest horizontal tile
					if (closestHorizontalTile == null)
					{
						closestHorizontalTile = fixedTile;
					}
					else
					{
						if (Vector2.Distance(moveableTile.Position, closestHorizontalTile.Position) > Vector2.Distance(moveableTile.Position, fixedTile.Position))
						{
							closestHorizontalTile = fixedTile;
						}
					}
				}

				//Find total movement of player
				//	Minimum between distance to closest fixed tile and usual player movement
				float horizontalMoveDistance = 0;

				if (horizontalDirection == Direction.Left)
				{
					if (closestHorizontalTile != null)
					{
						//Moveable distance to player's left (negative)
						horizontalMoveDistance = Math.Max(moveableTile.Velocity.X, closestHorizontalTile.Bounds.Right - moveableTile.Bounds.Left); 
					}
					else
					{
						horizontalMoveDistance = moveableTile.Velocity.X;
					}
				}
				else if (horizontalDirection == Direction.Right)
				{
					if (closestHorizontalTile != null)
					{
						//Moveable distance to player's right (positive)
						horizontalMoveDistance = Math.Min(moveableTile.Velocity.X, closestHorizontalTile.Bounds.Left - moveableTile.Bounds.Right); 
					}
					else
					{
						horizontalMoveDistance = moveableTile.Velocity.X;
					}
				}

				//Update player's horizontal position
				moveableTile.Position = new Vector2(moveableTile.Position.X + horizontalMoveDistance, moveableTile.Position.Y);


				//------------------------------------------------------------------------------
				//Y-axis
				//------------------------------------------------------------------------------
				
				Direction verticalDirection = Direction.None;
				int frontY = (int)moveableTile.Position.Y;

				//Get forward rectangle bounds y-coordinate
				if (moveableTile.Velocity.Y < 0) //Up movement
				{
					verticalDirection = Direction.Up;
					frontY = moveableTile.Bounds.Top;
				}
				else //Down movement (default - gravity :) )
				{
					verticalDirection = Direction.Down;
					frontY = moveableTile.Bounds.Bottom;
				}

				FixedTile closestVerticalTile = null;

				//Check these horizontal rows - in direction of movement - and determine which is the closest fixed tile
				foreach (FixedTile fixedTile in fixedTiles)
				{
					if (fixedTile.TileType == TileType.Pickup || fixedTile.TileType == TileType.Decoration ||
						fixedTile.TileType == TileType.LevelEnd || fixedTile.TileType == TileType.Obstacle)
					{
						continue;
					}
					
					//Check whether or not the moveable tile is actually within the horizontal bounds of the fixed tile (same columns)
					//if (f.r <= m.r && f.r >= m.l)
					//else if (f.l >= m.l && f.l <= m.r)
					//else -> block should fall
					if ((fixedTile.Bounds.Right <= moveableTile.Bounds.Right && fixedTile.Bounds.Right > moveableTile.Bounds.Left) != true &&
						(fixedTile.Bounds.Left >= moveableTile.Bounds.Left && fixedTile.Bounds.Left < moveableTile.Bounds.Right) != true)
					{
						continue;
					}

					//Up movement
					if (verticalDirection == Direction.Up)
					{
						//If the current fixed tile is behind the player (below) skip to the next iteration
						if (frontY < fixedTile.Bounds.Bottom)
						{
							continue;
						}
					}
					//Down movement
					else if (verticalDirection == Direction.Down)
					{
						//If the current fixed tile is behind the player (above) skip to the next iteration
						if (frontY > fixedTile.Bounds.Top)
						{
							continue;
						}
					}

					//Determine if the current fixed tile is closer than the current closest vertical tile
					if (closestVerticalTile == null)
					{
						closestVerticalTile = fixedTile;
					}
					else
					{
						if (Vector2.Distance(moveableTile.Position, closestVerticalTile.Position) > Vector2.Distance(moveableTile.Position, fixedTile.Position))
						{
							closestVerticalTile = fixedTile;
						}
					}
				}

				//Find total movement of player
				//	Minimum between distance to closest fixed tile and usual player movement
				float verticalMoveDistance = 0;
				
				//Update moveable tile with gravity
				if (moveableTile.OnGround == false)
				{
					moveableTile.Velocity = new Vector2(moveableTile.Velocity.X, moveableTile.Velocity.Y + moveableTile.Gravity.Y);
				}

				if (verticalDirection == Direction.Up)
				{
					if (closestVerticalTile != null)
					{
						//Moveable distance to player's top (negative)
						verticalMoveDistance = Math.Max(moveableTile.Velocity.Y, closestVerticalTile.Bounds.Bottom - moveableTile.Bounds.Top);

						//If the distance to the nearest tile is less than the moveable tile's velocity, it will be hit at its top in the next frame
						if (verticalMoveDistance > moveableTile.Velocity.Y)
						{
							//Start the moveable tile falling again
							moveableTile.Velocity = new Vector2(moveableTile.Velocity.X, 0.0f);
						}
					}
					else
					{
						//If this is ever reached there may be a problem (will happen if there are no tiles above it)
						verticalMoveDistance = moveableTile.Velocity.Y;
					}
				}
				else if (verticalDirection == Direction.Down)
				{
					if (closestVerticalTile != null)
					{

						//If the closest tile is right below, the moveable tile is on the ground
						if (closestVerticalTile.Bounds.Top - moveableTile.Bounds.Bottom == 0)
						{
							moveableTile.OnGround = true;
						}
						else
						{
							//Moveable distance to player's bottom (positive)
							verticalMoveDistance = Math.Min(moveableTile.Velocity.Y, closestVerticalTile.Bounds.Top - moveableTile.Bounds.Bottom);

							//If the distance to the nearest tile is less than the moveable tile's velocity, it will be on the ground in the next frame
							if (verticalMoveDistance < moveableTile.Velocity.Y)
							{
								//Tile is on the ground
								moveableTile.OnGround = true;
							}
							else
							{
								//Tile is still in the air
								moveableTile.OnGround = false;
							}
						}						
					}
					else
					{
						//If this is ever reached there may be a problem (will happen if there are no tiles below it)
						verticalMoveDistance = moveableTile.Velocity.Y;
					}
				}

				//Game.Window.Title = notified;

				//Update player's vertical position
				moveableTile.Position = new Vector2(moveableTile.Position.X, moveableTile.Position.Y + verticalMoveDistance);
			}

			base.Update(gameTime);
		}
	}
}