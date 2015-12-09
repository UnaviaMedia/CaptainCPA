/*
 * Project: CaptainCPA - StartScene.cs
 * Purpose: Display a StartScene screen with a game menu
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Dec-08-2015:	Added draw method, menu image
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CaptainCPA
{
	/// <summary>
	/// Enumeration of Main Menu Items
	/// </summary>
	public enum menuItemTitles
	{
		Start, Select, Help, HighScore, About, HowTo, Quit
	}

	/// <summary>
	/// Display a StartScene screen with a game menu
	/// </summary>
	public class StartScene : GameScene
	{
		private Texture2D scrollingTexture1;
		private Texture2D scrollingTexture2;
		private Vector2 scrollingPosition1;
		private Vector2 scrollingPosition2;
		private Vector2 scrollingSpeed;
		private List<Texture2D> backgroundImages;

		private MenuComponent menu;
		private Texture2D menuImage;
		
		public MenuComponent Menu
		{
			get { return menu; }
			set { menu = value; }
		}

		private string[] menuItems = {"Start Game",
							 "Select Level",
							 "Help",
							 "High Score",
							 "About/Credit",
							 "How to play",
							 "Quit"};


		public StartScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			this.spriteBatch = spriteBatch;

			//Add the background scrolling images
			backgroundImages = new List<Texture2D>();
			backgroundImages.Add(game.Content.Load<Texture2D>("Images/Menu-Background-1"));
			backgroundImages.Add(game.Content.Load<Texture2D>("Images/Menu-Background-2"));
			backgroundImages.Add(game.Content.Load<Texture2D>("Images/Menu-Background-3"));

			//Randomize the assigned texure
			Random r = new Random();
			scrollingTexture1 = backgroundImages[r.Next(0, backgroundImages.Count)];
			scrollingTexture2 = backgroundImages[r.Next(0, backgroundImages.Count)];

			//Set original positions of the scrolling backgrounds
			scrollingPosition1 = Vector2.Zero;
			scrollingPosition2 = new Vector2(scrollingPosition1.X + scrollingTexture1.Width, scrollingPosition1.Y);
			scrollingSpeed = new Vector2(1.0f, 0.0f);

			//Set up the menu
			Vector2 menuPosition = new Vector2(Settings.Stage.X / 2 + 140, Settings.Stage.Y / 2 - 60);
			menu = new MenuComponent(game, spriteBatch,
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				game.Content.Load<SpriteFont>("Fonts/MenuFont"),
				menuItems, menuPosition);
			this.Components.Add(menu);

			menuImage = game.Content.Load<Texture2D>("Images/MainMenu");
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
			if (scrollingPosition1.X > -scrollingTexture1.Width)
			{
				scrollingPosition1.X -= scrollingSpeed.X;
			}
			else
			{
				scrollingPosition1.X = scrollingPosition2.X + scrollingTexture1.Width;
			}

			if (scrollingPosition2.X > -scrollingTexture1.Width)
			{
				scrollingPosition2.X -= scrollingSpeed.X;
			}
			else
			{
				scrollingPosition2.X = scrollingPosition1.X + scrollingTexture1.Width;
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

			//Draw the background images
			spriteBatch.Draw(scrollingTexture1, scrollingPosition1, Color.White);
			spriteBatch.Draw(scrollingTexture2, scrollingPosition2, Color.White);

			//Draw main image background
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}