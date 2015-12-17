/*
 * Project: CaptainCPA - HighScoreComponent.cs
 * Purpose: High Score component
 *
 * History:
 *		Kendall Roth	Dec-10-2015:	Created
 *						Dec-11-2015:	Removed empty (default) score from showing
 *										Added player score to display regardless of high score placement
 */

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CaptainCPA.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CaptainCPA.Components
{
	/// <summary>
	/// High Score component
	/// </summary>
	public class HighScoreComponent : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Vector2 position;
		private SpriteFont font;
		private List<HighScore> highScores;
		private int playerScore;
		private string playerName;
		private bool playerHasHighScore;
		private bool highScoreEntered;
		private const string DEFAULT_NAME = "<<Enter Name>>";
		private KeyboardState oldState;

		public bool NameEntered
		{
			get { return highScoreEntered; }
		}

		public HighScoreComponent(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 position, int playerScore)
			:base(game)
		{
			this.spriteBatch = spriteBatch;
			this.font = font;
			this.position = position;
			this.playerScore = playerScore;
			
			highScoreEntered = false;
			playerHasHighScore = false;
			playerName = "";
			highScores = Utilities.Utilities.LoadHighScores().Take(3).ToList();

			//Determine whether or not the player has gotten a high score
			if (highScores.Count == 3)
			{
				foreach (HighScore highScore in highScores)
				{
					if (playerScore > highScore.Score)
					{
						playerHasHighScore = true;

						//Remove the last high score so that there are always only three high scores being displayed
						highScores.RemoveAt(highScores.Count - 1);
						break;
					}
				} 
			}
			else
			{
				playerHasHighScore = true;
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
				//Indicate to the player that they need to enter their name
				if (playerName == "")
				{
					playerName = DEFAULT_NAME;
				}

				//Get the player name from the player
				foreach (Keys key in ks.GetPressedKeys())
				{
					if (!oldState.IsKeyUp(key))
					{
						continue;
					}

					if (key == Keys.Back && playerName.Length > 0)
					{
						//Backspace a character from the player's name (set to default if name is empty)
						if (playerName != DEFAULT_NAME)
						{
							playerName = playerName.Remove(playerName.Length - 1, 1);
						}
					}
					else if (key == Keys.Enter)
					{
						//If no player name was entered give a default name
						if (playerName == DEFAULT_NAME || playerName.Trim() == "")
						{
							playerName = "Guest";
						}
							
						//Add the high score to the list of high scores
						Utilities.Utilities.UpdateHighScores(new HighScore() { Name = playerName, Score = playerScore });

						//The high score has been entered, and the game is now finished
						highScoreEntered = true;
					}
					else if (font.MeasureString(playerName).X < 225 || playerName == DEFAULT_NAME)
					{
						//Ensure the player can only enter valid characters and digits for their name
						if (Regex.IsMatch(key.ToString(), @"^[A-Z0-9]$", RegexOptions.IgnoreCase))
						{
							//If the default player name is currently entered, replace it with the user input
							if (playerName == DEFAULT_NAME)
							{
								playerName = "";
							}

							//Uppercase or lowercase the letter as indicated by Shift keys
							if (ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift))
							{
								playerName += key.ToString();
							}
							else
							{
								playerName += key.ToString().ToLower();
							}
						}
					}
				}
			}
			else
			{
				//If the user doesn't get a high score, return to the main menu when they press 'Enter'
				if (ks.IsKeyDown(Keys.Enter))
				{
					highScoreEntered = true;
				}
			}
			
			//Set the old key state as the current key state
			oldState = ks;

			base.Update(gameTime);
		}


		/// <summary>
		/// Display a high score name and score 
		/// </summary>
		/// <param name="name">High score name</param>
		/// <param name="score">High score value</param>
		/// <param name="tempPosition">Reference to temporary variable for Y-positioning</param>
		/// <param name="color">Color to draw the high score in</param>
		private void drawHighScore(string name, int score, ref float tempPosition, Color color)
		{
			spriteBatch.DrawString(font, name, new Vector2(position.X, tempPosition += 45), color);
			spriteBatch.DrawString(font, score.ToString(), new Vector2(position.X + 315 - font.MeasureString(score.ToString()).X, tempPosition), color);
		}


		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			float tempPosition = position.Y;
			bool playerScoreDrawn = false;

			spriteBatch.Begin();

			//Draw the player's score regardless of whether or not they got a high score
			spriteBatch.DrawString(font, playerScore.ToString(), 
				new Vector2((Utilities.Utilities.Stage.X / 2) - (font.MeasureString(playerScore.ToString()).X / 2), position.Y - 28), Color.White);

			if (highScores.Count != 0)
			{
				//Display previous high scores
				foreach (HighScore highScore in highScores)
				{
					//Allow the player to enter their name if they have managed to get a high score
					if (playerHasHighScore == true)
					{
						if ((playerScore > highScore.Score ) && playerScoreDrawn == false)
						{
							//Draw the player's high score if it is higher than the curent high score
							drawHighScore(playerName, playerScore, ref tempPosition, Color.Gold);

							playerScoreDrawn = true;
						}
					}

					//Draw the high score
					drawHighScore(highScore.Name, highScore.Score, ref tempPosition, Color.White);
				}

				if (playerScoreDrawn == false && playerHasHighScore)
				{
					//Draw the player's high score if it has not yet been drawn (ie. lowest high score)
					drawHighScore(playerName, playerScore, ref tempPosition, Color.Gold);

					playerScoreDrawn = true;
				}
			}
			else
			{
				//Draw the player's high score if there are no other high scores yet
				drawHighScore(playerName, playerScore, ref tempPosition, Color.Gold);

				playerScoreDrawn = true;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}