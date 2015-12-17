/*
 * Project: CaptainCPA - MenuComponent.cs
 * Purpose: Game menu component
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 *		Kendall Roth	Dec-08-2015:	Added another constructor to accept a position
 *										Updated to new user interface design (menu selector)
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CaptainCPA.Components
{
	/// <summary>
	/// Game menu component
	/// </summary>
	public class MenuComponent : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Vector2 position;
		private Texture2D menuSelectorTexture;
		private SpriteFont font;
		private List<string> menuItems;
		private Color color = Color.White;
		private KeyboardState oldState;

		public int SelectedIndex { get; set; }

		public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont font, string[] menuItems, Vector2 position)
			:base(game)
		{
			this.spriteBatch = spriteBatch;
			this.font = font;
			this.menuItems = menuItems.ToList();
			this.position = position;
			menuSelectorTexture = game.Content.Load<Texture2D>("Images/MenuSelector");
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
			if ((ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down)) || (ks.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S)))
			{
				SelectedIndex++;
				if (SelectedIndex == menuItems.Count)
				{
					SelectedIndex = 0;
				}
			}
			if ((ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up)) || (ks.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W)))
			{
				SelectedIndex--;
				if (SelectedIndex == -1)
				{
					SelectedIndex = menuItems.Count - 1;
				}
			}
			oldState = ks;

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			Vector2 tempPos = position;

			int maxItemLength = 0;

			spriteBatch.Begin();

			foreach (string menuItem in menuItems)
			{
				//Record the longest menu item (for menu selector)
				if (font.MeasureString(menuItem).X > maxItemLength)
				{
					maxItemLength = (int)font.MeasureString(menuItem).X;
				}
			}

			for (int i = 0; i < menuItems.Count; i++)
			{
				if (SelectedIndex == i)
				{
					//Draw the menu selector
					Vector2 selectorPosition = new Vector2(tempPos.X - 65, tempPos.Y - (menuSelectorTexture.Height / 2) + (font.LineSpacing / 2) + 2);

					//Draw the left section of the selector
					spriteBatch.Draw(menuSelectorTexture, selectorPosition, new Rectangle(0, 0, 67, 60), Color.White);

					//Draw the middle section of the selector
					spriteBatch.Draw(menuSelectorTexture, new Vector2(selectorPosition.X + 67, selectorPosition.Y), new Rectangle(75, 0, maxItemLength, 60), Color.White);

					//Draw the right section of the selector
					spriteBatch.Draw(menuSelectorTexture, new Vector2(selectorPosition.X + 67 + maxItemLength, selectorPosition.Y), new Rectangle(471, 0, 29, 60), Color.White);

					//Draw the menu item
					spriteBatch.DrawString(font, menuItems[i], tempPos, color);
					tempPos.Y += font.LineSpacing;
				}
				else
				{
					spriteBatch.DrawString(font, menuItems[i], tempPos, color);
					tempPos.Y += font.LineSpacing;
				}				

				tempPos.Y += 10;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
