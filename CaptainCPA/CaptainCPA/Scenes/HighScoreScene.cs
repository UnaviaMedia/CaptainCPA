/*
 * Project: CaptainCPA - HighScore.cs
 * Purpose: Display the high scores from a text file
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 *		Kendall Roth	Dec-09-2015:	Updated User Interface Design
 *										Added high score system
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml;

namespace CaptainCPA
{
	/// <summary>
	/// Struct to track player highscores
	/// </summary>
	public struct HighScore
	{
		public int Rank { get; set; }
		public int Score { get; set; }
		public string Name { get; set; }
	}

	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class HighScoreScene : GameScene
	{
		private Texture2D menuImage;
		private List<HighScore> highScores;

		public List<HighScore> HighScores
		{
			get { return highScores; }
			set { highScores = value; }
		}

		//private string message;
		//private SpriteFont font;

		public HighScoreScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			//font = game.Content.Load<SpriteFont>("Fonts/MenuFont");
			//message = readFile(@"Text/HighScoreMessage.txt");
			menuImage = game.Content.Load<Texture2D>("Images/HighScoreScreen");
		}

		/// <summary>
		/// Create a list of player high scores
		/// </summary>
		public void LoadHighScores()
		{
			//Create a new XML document and load the selected save file
			XmlDocument loadFile = new XmlDocument();
			loadFile.Load(@"Content/HighScores.xml");

			var scores = loadFile.SelectNodes("/XnaContent/PlatformGame/Scores/*");

			foreach (XmlNode highScore in scores)
			{
				//Get properties of the high score
				int rank = int.Parse(highScore.Attributes["rank"].Value);
				int score = int.Parse(highScore.Attributes["score"].Value);
				string name = highScore.Attributes["name"].Value;

				//Add the high score to the list of high scores
				highScores.Add(new HighScore() { Rank = rank, Score = score, Name = name });
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
			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			//spriteBatch.DrawString(font, message, new Vector2(0, 0), Color.White);
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
