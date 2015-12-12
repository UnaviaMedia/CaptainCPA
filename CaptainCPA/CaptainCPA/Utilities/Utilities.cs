/*
 * Project: CaptainCPA - Utilities.cs
 * Purpose: Class that contains various utility methods
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *		*				Dec-10-2015:	High Score managing added
 *						Dec-12-2015:	Added level progression checking
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
			//	If the file does not exist create a base file
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

			//Select the high score nodes
			var scores = loadFile.SelectNodes("/XnaContent/PlatformGame/Scores/*");

			foreach (XmlNode highScore in scores)
			{
				//Get properties of the high score
				int score = int.Parse(highScore.Attributes["score"].Value);
				string name = highScore.Attributes["name"].Value;

				//Add the high score to the list of high scores
				highScores.Add(new HighScore() { Score = score, Name = name });
			}
			
			//Return the list of high scores
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

			//Create a new base content node
			XmlNode platformGameNode = newFile.CreateElement("PlatformGame");
			rootNode.AppendChild(platformGameNode);

			//Create a new scores node to track game scores
			XmlNode scoresNode = newFile.CreateElement("Scores");
			platformGameNode.AppendChild(scoresNode);

			//Save the modified file
			newFile.Save(@"Content/HighScores.xml");
		}

		/// <summary>
		/// Update the list of high scores
		/// </summary>
		/// <param name="highScore">HighScore to add</param>
		public static void UpdateHighScores(HighScore addHighScore)
		{
			//Create a new XML document and create the root elements
			XmlDocument updateFile = new XmlDocument();
			updateFile.Load(@"Content/HighScores.xml");
			XmlNode root = updateFile.DocumentElement;
			XmlNode highScoresNode = root.SelectSingleNode("PlatformGame/Scores");
			
			//Create a HighScore node
			XmlNode node = updateFile.CreateElement("HighScore");

			//Create the score attribute of the HighScore node
			XmlAttribute score = updateFile.CreateAttribute("score");
			score.Value = addHighScore.Score.ToString();
			node.Attributes.Append(score);

			//Create the name attribute of the HighScore node
			XmlAttribute name = updateFile.CreateAttribute("name");
			name.Value = addHighScore.Name;
			node.Attributes.Append(name);

			//Add the HighScore node to the list of HighScores
			highScoresNode.AppendChild(node);

			updateFile.Save(@"Content/HighScores.xml");
		}
		#endregion


		#region LevelProgression
		/// <summary>
		/// Returns the highest unlocked level
		/// </summary>
		/// <returns>Highest unlocked level</returns>
		public static int CheckLevelProgression()
		{
			//Create a new XML document and load the level progression file
			//	If the file does not exist create a base file
			XmlDocument levelsFile = new XmlDocument();
			try
			{
				levelsFile.Load(@"Content/LevelProgression.xml");
			}
			catch (System.Exception)
			{
				ResetLevelProgression();
				levelsFile.Load(@"Content/LevelProgression.xml");
			}

			//Select the unlocked level node
			var unlockedLevel = levelsFile.SelectSingleNode("/XnaContent/PlatformGame/LevelProgression/UnlockedLevel");

			try
			{
				return int.Parse(unlockedLevel.Attributes["level"].Value);
			}
			catch (System.Exception)
			{
				return 1;
			};
		}

		/// <summary>
		/// Updates the LevelProgression file with a higher level number
		/// </summary>
		/// <param name="unlockedLevel"></param>
		public static void UpdateLevelProgression(int unlockedLevel)
		{
			//Create a new XML document and create the root elements
			XmlDocument updateFile = new XmlDocument();
			updateFile.Load(@"Content/LevelProgression.xml");
			XmlNode unlockedLevelNode = updateFile.SelectSingleNode("/XnaContent/PlatformGame/LevelProgression/UnlockedLevel");
			unlockedLevelNode.Attributes["level"].Value = unlockedLevel.ToString();

			updateFile.Save(@"Content/LevelProgression.xml");
		}

		/// <summary>
		/// Resets the LevelProgression file
		/// </summary>
		public static void ResetLevelProgression()
		{
			//Create a new XML document
			XmlDocument newFile = new XmlDocument();
			XmlNode rootNode = newFile.CreateElement("XnaContent");
			newFile.AppendChild(rootNode);

			//Create a new base content node
			XmlNode platformGameNode = newFile.CreateElement("PlatformGame");
			rootNode.AppendChild(platformGameNode);

			//Create a new levels node to track the highest unlocked level
			XmlNode levelsNode = newFile.CreateElement("LevelProgression");
			platformGameNode.AppendChild(levelsNode);

			//Create a new node with the highest unlocked level
			XmlNode unlockedLevel = newFile.CreateElement("UnlockedLevel");
			XmlAttribute unlockedLevelValue = newFile.CreateAttribute("level");
			unlockedLevelValue.Value = "1";
			unlockedLevel.Attributes.Append(unlockedLevelValue);
			levelsNode.AppendChild(unlockedLevel);

			//Save the modified file
			newFile.Save(@"Content/LevelProgression.xml");
		}
		#endregion
	}
}