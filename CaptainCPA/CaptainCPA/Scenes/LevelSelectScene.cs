/*
 * Project: CaptainCPA - LevelSelectScene.cs
 * Purpose: Display a Level Select screen
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created, updated User Interface Design
 *						Dec-11-2015:	Added level selector
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CaptainCPA
{
	/// <summary>
	/// This is the scene which allows users to select a level
	/// </summary>
	public class LevelSelectScene : GameScene
	{
		private Texture2D menuImage;
		private Texture2D levelSelector;
		private List<Vector2> levelSelectorPositions;
		private int selectedIndex;
		private int levels;
		private KeyboardState oldState;

		public int SelectedIndex
		{
			get { return selectedIndex; }
			set { selectedIndex = value; }
		}


		/// <summary>
		/// Constructor for LevelSelectScene
		/// </summary>
		/// <param name="game"></param>
		/// <param name="spriteBatch"></param>
		public LevelSelectScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			menuImage = game.Content.Load<Texture2D>("Images/LevelSelectMenu");
			levelSelector = game.Content.Load<Texture2D>("Images/LevelSelector");

			//Create list of level Selector positions
			levelSelectorPositions = new List<Vector2>();
			levelSelectorPositions.Add(new Vector2(420, 320));
			levelSelectorPositions.Add(new Vector2(620, 320));
			levelSelectorPositions.Add(new Vector2(820, 320));
			levelSelectorPositions.Add(new Vector2(1020, 320));
			levelSelectorPositions.Add(new Vector2(420, 520));
			levelSelectorPositions.Add(new Vector2(620, 520));
			levelSelectorPositions.Add(new Vector2(820, 520));
			levelSelectorPositions.Add(new Vector2(1020, 520));

			selectedIndex = 0;
			levels = 8;
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

			if ((ks.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right)) || (ks.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D)))
			{
				if (++selectedIndex == levels)
				{
					selectedIndex = 0;
				}
			}
			if ((ks.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left)) || (ks.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A)))
			{
				if (--selectedIndex == -1)
				{
					selectedIndex = levels - 1;
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
			spriteBatch.Begin();
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);

			spriteBatch.Draw(levelSelector, levelSelectorPositions[selectedIndex], Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}