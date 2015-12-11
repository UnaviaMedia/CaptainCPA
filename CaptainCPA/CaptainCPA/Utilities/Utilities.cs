/*
 * Project: CaptainCPA - Utilities.cs
 * Purpose: Class that contains various utility methods
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *		*				Dec-10-2015:	High Score managing added
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace CaptainCPA
{
	/// <summary>
	/// Class that contains various utility methods
	/// </summary>
	public class Utilities
	{
		/// <summary>
		/// Convert a Point to Vector2
		/// </summary>
		/// <param name="point">Point to convert to Vector2</param>
		/// <returns>Vector2 representing the input Point</returns>
		public static Vector2 PointToVector2(Point point)
		{
			return new Vector2(point.X, point.Y);
		}


		/// <summary>
		/// Determine whether or not a pixel-perfect collision has occurred
		/// </summary>
		/// <param name="a">Possibly colliding tile</param>
		/// <param name="b">Tile to check against</param>
		/// <returns></returns>
		public static bool PerPixelCollision(Tile a, Tile b)
		{
			// Get Color data of each Texture
			Color[] bitsA = new Color[a.Texture.Width * a.Texture.Height];
			a.Texture.GetData(bitsA);
			Color[] bitsB = new Color[b.Texture.Width * b.Texture.Height];
			b.Texture.GetData(bitsB);

			//Get rough collision rectangle
			Rectangle intersection = Rectangle.Intersect(a.Bounds, b.Bounds);

			for (int y = intersection.Top; y < intersection.Bottom; ++y)
			{
				for (int x = intersection.Left; x < intersection.Right; ++x)
				{
					// Get the color from each texture
					Color colorA = bitsA[(x - a.Bounds.X) + (y - a.Bounds.Y) * a.Texture.Width];
					Color colorB = bitsB[(x - b.Bounds.X) + (y - b.Bounds.Y) * b.Texture.Width];

					// If both colors are not transparent (the alpha channel is not 0), then there is a collision
					if (colorA.A != 0 && colorB.A != 0)
					{
						return true;
					}
				}
			}

			return false;
		}


		#region HighScore List Management
		/// <summary>
		/// Create a list of high scores
		/// </summary>
		/// <returns>List of high score struct objects</returns>
		public static List<HighScore> LoadHighScores()
		{
			List<HighScore> highScores = new List<HighScore>();

			//Create a new XML document and load the selected save file
			XmlDocument loadFile = new XmlDocument();
			try
			{
				loadFile.Load(@"Content/HighScores.xml");
			}
			catch (System.Exception)
			{
				ResetHighScores();
				loadFile.Load(@"Content/HighScores.xml");
			}

			var scores = loadFile.SelectNodes("/XnaContent/PlatformGame/Scores/*");

			foreach (XmlNode highScore in scores)
			{
				//Get properties of the high score
				int score = int.Parse(highScore.Attributes["score"].Value);
				string name = highScore.Attributes["name"].Value;

				//Add the high score to the list of high scores
				highScores.Add(new HighScore() { Score = score, Name = name });
			}

			if (highScores.Count == 0)
			{
				highScores.Add(new HighScore() { Name = "-----------------------", Score = 0 });
			}

			//return highScores.OrderBy(h => h.Rank).ToList();
			return highScores.OrderByDescending(h => h.Score).ToList();
		}


		/// <summary>
		/// Reset the XML list of HighScores
		/// </summary>
		public static void ResetHighScores()
		{
			//Create a new XML document and load the selected save file
			XmlDocument newFile = new XmlDocument();
			XmlNode rootNode = newFile.CreateElement("XnaContent");
			newFile.AppendChild(rootNode);

			XmlNode platformGameNode = newFile.CreateElement("PlatformGame");
			rootNode.AppendChild(platformGameNode);

			XmlNode scoresNode = newFile.CreateElement("Scores");
			platformGameNode.AppendChild(scoresNode);

			newFile.Save(@"Content/HighScores.xml");
		}


		/// <summary>
		/// Create a list of high scores
		/// </summary>
		/// <param name="highScoreList">List of high scores</param>
		/// <returns>List of high score struct objects</returns>
		public static void SaveHighScores(List<HighScore> highScoreList)
		{
			int maxHighScores = 5;

			List<HighScore> highScores = highScoreList.OrderByDescending(h => h.Score).Take(maxHighScores).ToList();

			//Create a new XML document and create the root elements
			XmlDocument saveFile = new XmlDocument();
			saveFile.Load(@"Content/HighScores.xml");
			XmlNode root = saveFile.DocumentElement;
			XmlNode highScoresNode = root.SelectSingleNode("PlatformGame/Scores");
			highScoresNode.RemoveAll();

			//Add each high score node to the highScoresNode
			foreach (HighScore highScore in highScores)
			{
				if (highScore.Name == "-----------------------")
				{
					continue;
				}

				//Create a HighScore node
				XmlNode node = saveFile.CreateElement("HighScore");

				//Create the score attribute of the HighScore node
				XmlAttribute score = saveFile.CreateAttribute("score");
				score.Value = highScore.Score.ToString();
				node.Attributes.Append(score);

				//Create the name attribute of the HighScore node
				XmlAttribute name = saveFile.CreateAttribute("name");
				name.Value = highScore.Name;
				node.Attributes.Append(name);

				//Add the HighScore node to the list of HighScores
				highScoresNode.AppendChild(node);
			}

			saveFile.Save(@"Content/HighScores.xml");
		}
		#endregion
	}
}
