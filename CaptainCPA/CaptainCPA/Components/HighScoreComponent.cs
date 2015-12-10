/*
 * Project: CaptainCPA - HighScoreComponent.cs
 * Purpose: High Score component
 *
 * History:
 *		Kendall Roth	Dec-10-2015:	Created
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace CaptainCPA
{
	/// <summary>
	/// High Score component
	/// </summary>
	public class HighScoreComponent : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Vector2 position;
		private SpriteFont font;
		//private Texture2D selectorTexture;
		//private List<string> menuItems;
		private KeyboardState oldState;

		List<HighScore> highScores;
		private bool playerHasHighScore;
		private int highScore;
		private int playerScore;
		private string playerName;

		private List<Keys> highScoreKeys = new List<Keys>() {
			Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M,
			Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.Enter, Keys.Back };


		public HighScoreComponent(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 position)
			:base(game)
		{
			this.spriteBatch = spriteBatch;
			this.font = font;
			//this.menuItems = menuItems.ToList();
			this.position = position;
			this.playerScore = 0;
			//menuSelectorTexture = game.Content.Load<Texture2D>("Images/MenuSelector");

			playerHasHighScore = false;
			playerName = "";
			highScores = Utilities.LoadHighScores(3);

			//Determine whether or not the player has gotten a high score
			foreach (HighScore highScore in highScores)
			{
				if (playerScore > highScore.Score)
				{
					playerHasHighScore = true;
					break;
				}
			}
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

			if (playerHasHighScore == true)
			{
				foreach (Keys key in ks.GetPressedKeys())
				{
					if (highScoreKeys.Contains(key) == false)
					{
						continue;
					}

					if (oldState.IsKeyUp(key))
					{
						if (key == Keys.Back && playerName.Length > 0)
						{
							playerName = playerName.Remove(playerName.Length - 1, 1);
						}
						else if (key == Keys.Enter)
						{
							//Do I need this?
						}
						else if (font.MeasureString(playerName).X < 300)
						{
							playerName += key.ToString();
						}
					}
				}
			}

			//Set the old key state as the current key state
			oldState = ks;

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			float tempPosition = position.Y;

			spriteBatch.Begin();

			//Display previous high scores
			tempPosition += 50;

			foreach (HighScore highScore in highScores)
			{
				string highScoreMessage = highScore.Name + " - " + highScore.Score;
				tempPosition += font.MeasureString(highScoreMessage).Y;
				spriteBatch.DrawString(font, highScoreMessage, new Vector2(position.X, tempPosition), Color.White);
			}

			//Allow player to enter their name to save with their high score
			if (playerHasHighScore == true)
			{
				//Display the player's name
				spriteBatch.DrawString(font, "Enter Name:", new Vector2(position.X, tempPosition += 50), Color.White);
				spriteBatch.DrawString(font, playerName, new Vector2(position.X, tempPosition += 50), Color.White); 
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
