/*
 * Project: CaptainCPA - TileCollisionPositioningManager.cs
 * Purpose: Manages collisions and post-collision positioning between tiles (backup to PhysicsManager)
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-26-2015:	Removed dependency on TileCollisionPositioningManager, using PhysicsManager instead
 *						Nov-29-2015:	Optimizations
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace CaptainCPA
{
    /// <summary>
    /// Manages collisions and post-collision positioning between tiles
    /// </summary>
    public class TileCollisionPositioningManager : CollisionManager
    {
        public TileCollisionPositioningManager(Game game, List<MoveableTile> moveableTiles, List<FixedTile> fixedTiles)
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
            foreach (FixedTile fixedTile in fixedTiles)
            {
                //If the tile is hidden or disabled, skip collision detection for it
                //	Also skip detection if the TileType shouldn't have detection for it
                if (fixedTile.Visible == false || fixedTile.Enabled == false || fixedTile.IsCollideable == false ||
                    fixedTile.TileType == TileType.Decoration || fixedTile.TileType == TileType.Pickup ||
                    fixedTile.TileType == TileType.LevelEnd || fixedTile.TileType == TileType.Obstacle)
                {
                    continue;
                }

                foreach (MoveableTile moveableTile in moveableTiles)
                {
                    //If the moveable tile is hidden or disabled, skip collsion detection for it.
                    //	Also skip tiles that are too far away
                    if (moveableTile.Visible == false || moveableTile.Enabled == false ||
                        Vector2.Distance(Utilities.PointToVector2(fixedTile.Bounds.Center), Utilities.PointToVector2(moveableTile.Bounds.Center)) > Utilities.TILE_SIZE * 4)
                    {
                        continue;
                    }

                    if (moveableTile is Boulder && fixedTile is LevelBarrier)
                    {
                        continue;
                    }
                    //Check for collision
                    if (moveableTile.Bounds.Intersects(fixedTile.Bounds))
                    {
                        //Get the intersection rectangle
                        Rectangle collisionRectangle = Rectangle.Intersect(moveableTile.Bounds, fixedTile.Bounds);

                        //Temporarily store post-collision position
                        Vector2 collisionPosition = moveableTile.Position;

                        if (collisionRectangle.Width > collisionRectangle.Height)
                        {
                            //Vertical collision
                            if (moveableTile.Bounds.Top - fixedTile.Bounds.Top < 0)
                            {
                                //Top collision
                                collisionPosition = new Vector2(moveableTile.Position.X, fixedTile.Bounds.Top - (moveableTile.Bounds.Height / 2));
                            }
                            else
                            {
                                //Bottom collision
                                collisionPosition = new Vector2(moveableTile.Position.X, fixedTile.Bounds.Bottom + (moveableTile.Bounds.Height / 2));
                            }

                            //moveableTile.Velocity = new Vector2(moveableTile.Velocity.X, 0.0f);
                        }
                        else if (collisionRectangle.Width < collisionRectangle.Height)
                        {
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

                            //moveableTile.Velocity = new Vector2(0.0f, moveableTile.Velocity.Y);
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