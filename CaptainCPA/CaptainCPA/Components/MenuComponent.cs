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


namespace CaptainCPA
{
	/// <summary>
	/// Game menu component
	/// </summary>
	public class MenuComponent : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Vector2 position;
		private Texture2D menuSelectorTexture;
		private SpriteFont regularFont, highlightFont;
		private List<string> menuItems;
		private int selectedIndex = 0;
		private Color regularColor = Color.White;
		private Color highlightColor = Color.White;
		private KeyboardState oldState; //Why??

		public int SelectedIndex
		{
			get { return selectedIndex; }
			set { selectedIndex = value; }
		}


		public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont highlightFont, string[] menuItems)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.regularFont = regularFont;
			this.highlightFont = highlightFont;
			this.menuItems = menuItems.ToList();
			menuSelectorTexture = game.Content.Load<Texture2D>("Images/MenuSelector");
			position = new Vector2(Settings.Stage.X / 2, Settings.Stage.Y / 2);
		}

		public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont highlightFont, string[] menuItems, Vector2 position)
			:base(game)
		{
			this.spriteBatch = spriteBatch;
			this.regularFont = regularFont;
			this.highlightFont = highlightFont;
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
				selectedIndex++;
				if (selectedIndex == menuItems.Count)
				{
					selectedIndex = 0;
				}
			}
			if ((ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up)) || (ks.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W)))
			{
				selectedIndex--;
				if (selectedIndex == -1)
				{
					selectedIndex = menuItems.Count - 1;
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

			for (int i = 0; i < menuItems.Count; i++)
			{
				//Record the longest menu item (for menu selector)
				if (highlightFont.MeasureString(menuItems[i]).X > maxItemLength)
				{
					maxItemLength = (int)highlightFont.MeasureString(menuItems[i]).X;
				}
			}

			for (int i = 0; i < menuItems.Count; i++)
			{
				if (selectedIndex == i)
				{
					//Draw the menu selector
					Vector2 selectorPosition = new Vector2(tempPos.X - 65, tempPos.Y - (menuSelectorTexture.Height / 2) + (highlightFont.LineSpacing / 2) + 2);

					//Draw the left section of the selector
					spriteBatch.Draw(menuSelectorTexture, selectorPosition, new Rectangle(0, 0, 67, 60), Color.White);

					//Draw the middle section of the selector
					spriteBatch.Draw(menuSelectorTexture, new Vector2(selectorPosition.X + 67, selectorPosition.Y), new Rectangle(75, 0, maxItemLength, 60), Color.White);

					//Draw the right section of the selector
					spriteBatch.Draw(menuSelectorTexture, new Vector2(selectorPosition.X + 67 + maxItemLength, selectorPosition.Y), new Rectangle(471, 0, 29, 60), Color.White);

					//Draw the menu item
					spriteBatch.DrawString(highlightFont, menuItems[i], tempPos, highlightColor);
					tempPos.Y += highlightFont.LineSpacing;
				}
				else
				{
					spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
					tempPos.Y += regularFont.LineSpacing;
				}				

				tempPos.Y += 10;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
