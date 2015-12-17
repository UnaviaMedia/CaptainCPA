/*
 * Project: CaptainCPA - LevelSelectScene.cs
 * Purpose: Display a Level Select screen
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created, updated User Interface Design
 *						Dec-11-2015:	Added level selector
 *						Dec-12-2015:	Added locked level indicator and unavailable level indicator
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CaptainCPA.Scenes
{
	/// <summary>
	/// This is the scene which allows users to select a level
	/// </summary>
	public class LevelSelectScene : GameScene
	{
		private Texture2D menuImage;
		private Texture2D levelSelector;
		private Texture2D lockedLevelIndicator;
		private Texture2D unavailableLockedLevelIndicator;
		private List<Vector2> levelSelectorPositions;
		private int numberOfLevels;
		private int numberOfUnlockedLevels;
		private KeyboardState oldState;

		public int SelectedIndex { get; set; }


		/// <summary>
		/// Constructor for LevelSelectScene
		/// </summary>
		/// <param name="game"></param>
		/// <param name="spriteBatch"></param>
		/// <param name="numberOfLevels">Number of levels in game</param>
		/// <param name="numberOfUnlockedLevels">Number of unlocked levels</param>
		public LevelSelectScene(Game game, SpriteBatch spriteBatch, Texture2D menuImage, int numberOfLevels, int numberOfUnlockedLevels)
			: base(game, spriteBatch)
		{
			this.menuImage = menuImage;
			levelSelector = game.Content.Load<Texture2D>("Images/LevelSelector");
			lockedLevelIndicator = game.Content.Load<Texture2D>("Images/LockedLevelIndicator");
			unavailableLockedLevelIndicator = game.Content.Load<Texture2D>("Images/UnavailableLockedLevelIndicator");

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

			this.numberOfLevels = numberOfLevels;
			this.numberOfUnlockedLevels = numberOfUnlockedLevels;

			SelectedIndex = 0;
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
				if (++SelectedIndex == numberOfUnlockedLevels)
				{
					SelectedIndex = 0;
				}
			}
			if ((ks.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left)) || (ks.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A)))
			{
				if (--SelectedIndex == -1)
				{
					SelectedIndex = numberOfUnlockedLevels - 1;
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

			//Display locked (but available) levels
			for (int i = numberOfUnlockedLevels; i < numberOfLevels; i++)
			{
				spriteBatch.Draw(lockedLevelIndicator, levelSelectorPositions[i] + new Vector2(5.5f), Color.White);
			}

			//Display locked (and unavailable levels)
			for (int i = numberOfLevels; i < 8; i++)
			{
				spriteBatch.Draw(unavailableLockedLevelIndicator, levelSelectorPositions[i] + new Vector2(5.5f), Color.White);
			}
			
			spriteBatch.Draw(levelSelector, levelSelectorPositions[SelectedIndex], Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}