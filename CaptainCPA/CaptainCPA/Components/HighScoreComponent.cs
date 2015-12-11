/*
 * Project: CaptainCPA - HighScoreComponent.cs
 * Purpose: High Score component
 *
 * History:
 *		Kendall Roth	Dec-10-2015:	Created
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text.RegularExpressions;

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
		private KeyboardState oldState;
		private bool highScoreEntered;

		private List<HighScore> highScores;
		private bool playerHasHighScore;
		private int playerScore;
		private string playerName;

		public bool NameEntered
		{
			get { return highScoreEntered; }
		}

		public HighScoreComponent(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 position)
			:base(game)
		{
			this.spriteBatch = spriteBatch;
			this.font = font;
			this.position = position;

			playerScore = Character.Score;
			highScoreEntered = false;
			playerHasHighScore = false;
			playerName = "";
			highScores = Utilities.LoadHighScores().Take(3).ToList();

			//Determine whether or not the player has gotten a high score
			foreach (HighScore highScore in highScores)
			{
				if (playerScore > highScore.Score)
				{
					playerHasHighScore = true;
					break;
				}
			}

			playerHasHighScore = true;
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

			//Enable resetting of the High Score List
			if (playerName.ToLower() == "reset")
			{
				Utilities.ResetHighScores();
				highScores = new List<HighScore>();
				playerName = "";
				highScoreEntered = true;
			}

			if (playerHasHighScore == true)
			{
				//Indicate to the player that they need to enter their name
				if (playerName == "")
				{
					playerName = "<<Enter Name>>";
				}

				foreach (Keys key in ks.GetPressedKeys())
				{
					if (oldState.IsKeyUp(key))
					{
						if (key == Keys.Back && playerName.Length > 0)
						{
							//Backspace a character from the player's name
							if (playerName != "<<Enter Name>>")
							{
								playerName = playerName.Remove(playerName.Length - 1, 1);
							}
						}
						else if (key == Keys.Enter)
						{
							//If no player name was entered give a default name
							if (playerName == "<<Enter Name>>" || playerName.Trim() == "")
							{
								playerName = "Guest";
							}

							//Add the player's score to the high scores and save
							highScores.Add(new HighScore() { Name = playerName, Score = playerScore });

							Utilities.SaveHighScores(highScores);

							highScoreEntered = true;
						}
						else if (font.MeasureString(playerName).X < 250)
						{
							if (Regex.IsMatch(key.ToString(), @"^[A-Z]$", RegexOptions.IgnoreCase))
							{
								if (playerName == "<<Enter Name>>")
								{
									playerName = "";
								}

								//Uppercase or lowercase the letter as requierd
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
			}
			else
			{
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
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			float tempPosition = position.Y;
			bool playerScoreDrawn = false;

			spriteBatch.Begin();

			//Display previous high scores
			foreach (HighScore highScore in highScores)
			{
				//Allow the player to enter their name if they have managed to get a high score
				if (playerHasHighScore == true && playerScore >= highScore.Score && playerScoreDrawn == false)
				{
					//Display the player's name
					spriteBatch.DrawString(font, playerName, new Vector2(position.X, tempPosition += 45), Color.Gold);

					//Position and draw the player score
					spriteBatch.DrawString(font, playerScore.ToString(),
						new Vector2(position.X + 315 - font.MeasureString(highScore.Score.ToString()).X, tempPosition), Color.Gold);

					playerScoreDrawn = true;
				}

				tempPosition += font.MeasureString(highScore.Name).Y;
				
				//Position and draw the player name
				spriteBatch.DrawString(font, highScore.Name, new Vector2(position.X, tempPosition), Color.White);

				//Position and draw the player score
				spriteBatch.DrawString(font, highScore.Score.ToString(),
					new Vector2(position.X + 315 - font.MeasureString(highScore.Score.ToString()).X, tempPosition), Color.White);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
