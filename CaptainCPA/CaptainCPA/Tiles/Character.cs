/*
 * Project: CaptainCPA - Character.cs
 * Purpose: Character class
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *										Movement physics added
 *						Nov-26-2015:	Movement physics overhauled
 *						Nov-27-2015:	Physics adjusted again
 *						Nov-29-2015:	Added speed, jumpspeed, lives, and losing life methods
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CaptainCPA
{
	/// <summary>
	/// Character tile and logic
	/// </summary>
	public class Character : MoveableTile
	{
		protected int lives;
		protected int score;
		protected float speed;
		protected float jumpSpeed;

		//Store characters's starting position
		protected Vector2 startingPosition;

		public int Lives
		{
			get { return lives; }
			set { lives = value; }
		}

		public int Score
		{
			get { return score;}
			set { score = value; }
		}

		public float Speed
		{
			get { return speed; }
			set { speed = value; }
		}
		
		public float JumpSpeed
		{
			get { return jumpSpeed; }
			set { jumpSpeed = value; }
		}

		public Vector2 StartingPosition
		{
			get { return startingPosition; }
		}

		public Character(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth,
							Vector2 velocity, bool onGround, int lives, float speed, float jumpSpeed)
			: base(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround)
		{
			this.lives = lives;
			this.speed = speed;
			this.jumpSpeed = jumpSpeed;
			startingPosition = position;

			//Reset player score
			ResetScore();
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
		/// Reset player score
		/// </summary>
		private void ResetScore()
		{
			score = 0;
		}

		/// <summary>
		/// Make the character lose one life. If the number of lives is under zero, the character loses
		/// </summary>
		public void LoseLife()
		{
			if (lives-- <= 0)
			{
				Die();
			}
			else
			{
				ResetPosition();
			}
		}
		
		/// <summary>
		/// Reset the character's position
		/// </summary>
		private void ResetPosition()
		{
			position = startingPosition;
		}

		/// <summary>
		/// Make the character die once he runs out of lives
		/// </summary>
		private void Die()
		{

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
				velocity.X -= speed;
			}

			//If the Right key is pressed, add horizontal velocity to move right
			if (ks.IsKeyDown(Keys.Right))
			{
				velocity.X += speed;
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
				velocity.Y = jumpSpeed;
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
