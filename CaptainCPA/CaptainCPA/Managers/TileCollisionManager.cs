/*
 * Project: CaptainCPA - TileCollisionManager.cs
 * Purpose: Manages collisions and post-collision positioning between tiles
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
	/// Manages collisions and post-collision positioning between tiles
	/// </summary>
	public class TileCollisionManager : CollisionManager
	{
		public TileCollisionManager(Game game, List<Tile> tiles)
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
			foreach (FixedTile fixedTile in fixedTiles)
			{
				//If the tile is hidden or disabled, skip collision detection for it
				if (fixedTile.Visible == false || fixedTile.Enabled == false)
				{
					continue;
				}

				//If the tile is a decoration tile, skip collision positioning for it
				if (fixedTile.TileType == TileType.Decoration || fixedTile.TileType == TileType.Pickup)
				{
					continue;
				}

				foreach (MoveableTile moveableTile in moveableTiles)
				{
					//If the moveable tile is hidden or disabled, skip collsion detection for it.
					//	Also skip tiles that are too far away
					if (moveableTile.Visible == false || moveableTile.Enabled == false ||
						Vector2.Distance(Utilities.PointToVector2(fixedTile.Bounds.Center), Utilities.PointToVector2(moveableTile.Bounds.Center)) > 100)
					{
						continue;
					}

					//Check for collision
					if (moveableTile.Bounds.Intersects(fixedTile.Bounds))
					{

						//Get the intersection rectangle
						Rectangle collisionRectangle = Rectangle.Intersect(moveableTile.Bounds, fixedTile.Bounds);

						//Check for pixel collision
						/*if (Utilities.PerPixelCollision(moveableTile, fixedTile, collisionRectangle) == false)
						{
							continue;
						}*/

						//Temporarily store post-collision position
						Vector2 collisionPosition = moveableTile.Position;

						if (collisionRectangle.Width > collisionRectangle.Height)
						{
							//Vertical collision
							if (moveableTile.Bounds.Top - fixedTile.Bounds.Top < 0)
							{
								//Top collision
								collisionPosition = new Vector2(moveableTile.Position.X, fixedTile.Bounds.Top - (moveableTile.Bounds.Height / 2));

								//Moveable tile is on the ground (just hit it)
								moveableTile.OnGround = true;
							}
							else
							{
								//Bottom collision
								collisionPosition = new Vector2(moveableTile.Position.X, fixedTile.Bounds.Bottom + (moveableTile.Bounds.Height / 2));

								//If the moveable tile collides at its top, reset its velocity in order to cause it to fall instantly
								moveableTile.Velocity = new Vector2(moveableTile.Velocity.X, 1);
							}
						}
						else if (collisionRectangle.Width < collisionRectangle.Height)
						{
							//If the moveable tile collides sideways with something and is still going up, make it drop straight down
							if (moveableTile.Velocity.Y < 0)
							{
								moveableTile.Velocity = new Vector2(0.0f, 0.0f);
							}
							else
							{
								moveableTile.Velocity = new Vector2(0.0f, moveableTile.Velocity.Y);
								moveableTile.Gravity = new Vector2(0.0f, moveableTile.Gravity.Y);
							}

							//Horizontal collision
							if (moveableTile.Bounds.Right - fixedTile.Bounds.Right < 0)
							{
								//Left Collision
								collisionPosition = new Vector2(fixedTile.Bounds.Left - (moveableTile.Bounds.Width / 2), moveableTile.Position.Y);
							}
							else
							{
								//Right Collision
								collisionPosition = new Vector2(fixedTile.Bounds.Right + (moveableTile.Bounds.Width / 2), moveableTile.Position.Y);
							}
						}
						else
						{
							//DEBUG - Nothing needs to happen here, just for testing
							//Console.WriteLine("Perfectly square collision rectangle has occurred");
						}

						//Update the position of the moveable tile
						moveableTile.Position = collisionPosition;
					}
				}
			}

			base.Update(gameTime);
		}
	}
}
