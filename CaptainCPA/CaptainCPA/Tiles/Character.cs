/*
 * Project: CaptainCPA - Character.cs
 * Purpose: Character class
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *										Movement physics added
 *						Nov-26-2015:	Movement physics overhauled
 *						Nov-27-2015:	Physics adjusted again
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

			//If the character is on the ground, reset vertical velocity to 0
			if (onGround == true)
			{
				velocity.Y = 0;
			}

			//If the Left key is pressed, subtract horizontal velocity to move left
			if (ks.IsKeyDown(Keys.Left))
			{
				velocity.X -= 3.5f;
			}

			//If the Right key is pressed, add horizontal velocity to move right
			if (ks.IsKeyDown(Keys.Right))
			{
				velocity.X += 3.5f;
			}

			#region OldDebuggingMovement
			/*if (ks.IsKeyDown(Keys.Up))
			{
				velocity.Y = -4;
			}

			if (ks.IsKeyDown(Keys.Down))
			{
				velocity.Y = 4;
			}*/
			#endregion

			//If the Up key is pressed and the character is on the ground, add vertical velocity to jump (counteract gravity)
			if (ks.IsKeyDown(Keys.Up) && onGround == true)
			{
				velocity.Y = -10.0f;
				onGround = false;
			}

			//Debug Mode
			if (ks.IsKeyDown(Keys.Space))
			{
				Console.WriteLine("Debug Mode");
			}

			base.Update(gameTime);
		}
	}
}
