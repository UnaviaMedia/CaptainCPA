/*
 * Project: CaptainCPA - MoveableTile.cs
 * Purpose: Moveable platform tile
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
	/// Moveable platform tile
	/// </summary>
	public class MoveableTile : Tile
	{
		protected Vector2 velocity;
		protected Vector2 gravity = new Vector2(0.0f, 0.2f);
		protected bool onGround;

		public Vector2 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		public Vector2 Gravity
		{
			get { return gravity; }
			set { gravity = value; }
		}

		public bool OnGround
		{
			get { return onGround; }
			set { onGround = value; }
		}

		public MoveableTile(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth, 
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, tileType, color, position, rotation, scale, layerDepth)
		{
			this.velocity = velocity;
			this.onGround = onGround;
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
			#region OldCode
			/*if (onGround == false)
			{
				velocity.Y += gravity.Y;
				position.Y += velocity.Y;
				position.X += gravity.X;
				position.X += velocity.X * 0.25f;
			}
			else
			{
				position.X += velocity.X;
			}*/
			#endregion

			//------------------------------------------------------------------------------
			//X-axis
			//------------------------------------------------------------------------------

			/*int frontX;

			//Get forward rectangle bounds x-coordinate
			if (velocity.X < 0) //Left movement
			{
				//Console.WriteLine("Left movement");
				frontX = bounds.Left;
			}
			else if (velocity.X > 0) //Right movement
			{
				//Console.WriteLine("Right movement");
				frontX = bounds.Right;
			}

			//Find horizontal rows character intersects with (two max)
			//	Horizontal row that contains upper bounds (y)
			//	Horizontal row that contains lower bounds (y)
			foreach (FixedTile fixedTile in fixedTiles)
			{
				
			}*/

			//Check these horizontal rows - in direction of movement - and determine which is fixed tile

			//Find total movement of player
			//	Minimum between distance to closest fixed tile and usual player movement

			//Update player's horizontal position

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
		}
	}
}
