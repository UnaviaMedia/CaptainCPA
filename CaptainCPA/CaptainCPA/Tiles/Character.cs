/*
 * Project: CaptainCPA - Character.cs
 * Purpose: Character class
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *										Movement physics added
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
	/// Character tile and logic
	/// </summary>
	public class Character : MoveableTile
	{
		public Character(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround)
			: base(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround)
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
			KeyboardState ks = Keyboard.GetState();

			//Reset horizontal velocity to zero
			velocity.X = 0;
			
			if (ks.IsKeyDown(Keys.Left))
			{
				velocity.X = -3.5f;
			}

			if (ks.IsKeyDown(Keys.Right))
			{
				velocity.X = 3.5f;
			}

			/*if (ks.IsKeyDown(Keys.Up))
			{
				velocity.Y = -4;
			}

			if (ks.IsKeyDown(Keys.Down))
			{
				velocity.Y = 4;
			}*/

			if (ks.IsKeyDown(Keys.Up) && onGround == true)
			{
				velocity.Y = -8.0f;
				onGround = false;
			}

			if (ks.IsKeyDown(Keys.Space))
			{
				Console.WriteLine("d");
			}

			base.Update(gameTime);
		}
	}
}
