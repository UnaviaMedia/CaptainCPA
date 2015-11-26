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

				int frontX;

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
				int topRow = Math.Floor(moveableTile.Bounds.Top / Settings.TILE_SIZE);
				int bottomRow = Math.Floor(moveableTile.Bounds.Bottom / Settings.TILE_SIZE);

				//Check these horizontal rows - in direction of movement - and determine which is fixed tile

				//Find total movement of player
				//	Minimum between distance to closest fixed tile and usual player movement

				//Update player's horizontal position 
			}

			base.Update(gameTime);
		}
	}
}
