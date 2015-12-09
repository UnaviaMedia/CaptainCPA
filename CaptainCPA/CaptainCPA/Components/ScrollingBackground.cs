/*
 * Project: CaptainCPA - ScrollingBackground.cs
 * Purpose: Scrolling background for the main menu
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CaptainCPA
{
	/// <summary>
	/// Scrolling background for the main menu
	/// </summary>
	public class ScrollingBackground : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Texture2D texture1;
		private Texture2D texture2;
		private Vector2 position1;
		private Vector2 position2;
		private Vector2 speed;
		private List<Texture2D> backgroundImages;


		public ScrollingBackground(Game game, SpriteBatch spriteBatch)
			: base(game)
		{
			this.spriteBatch = spriteBatch;

			//Add the background scrolling images
			backgroundImages = new List<Texture2D>();
			backgroundImages.Add(game.Content.Load<Texture2D>("Images/Menu-Background-1"));
			backgroundImages.Add(game.Content.Load<Texture2D>("Images/Menu-Background-2"));
			backgroundImages.Add(game.Content.Load<Texture2D>("Images/Menu-Background-3"));

			//Randomize the assigned texure
			Random r = new Random();
			texture1 = backgroundImages[r.Next(0, backgroundImages.Count)];
			texture2 = backgroundImages[r.Next(0, backgroundImages.Count)];

			//Set original positions of the background
			position1 = Vector2.Zero;
			position2 = new Vector2(position1.X + texture1.Width, position1.Y);
			speed = new Vector2(2.0f, 0.0f);
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
			if (position1.X > -texture1.Width)
			{
				position1.X -= speed.X;
			}
			else
			{
				position1.X = position2.X + texture1.Width;
			}

			if (position2.X > -texture1.Width)
			{
				position2.X -= speed.X;
			}
			else
			{
				position2.X = position1.X + texture1.Width;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();

			spriteBatch.Draw(texture1, position1, Color.White);
			spriteBatch.Draw(texture2, position2, Color.White);
			//spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 0, SpriteEffects.None, 0);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
