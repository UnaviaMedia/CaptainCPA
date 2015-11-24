using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaptainCPA
{
	class LevelLoadManager
	{
		private Game game;
		private SpriteBatch spriteBatch;
		public List<Tile> tileList;

		public LevelLoadManager(Game game, SpriteBatch spriteBatch)
		{
			this.game = game;
			this.spriteBatch = spriteBatch;
		}

		/// <summary>
		/// Loads an nPuzzle game save and sets up the board
		/// </summary>
		/// <param name="filePath">File path to the save file</param>
		public void LoadGame(string filePath = "C:\\Level.xml")
		{
			//Create list of level tiles
			tileList = new List<Tile>();

			//Load the different block textures
			Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/Character");

			//Create a new XML document and load the selected save file
			XmlDocument loadFile = new XmlDocument();
			loadFile.Load(filePath);
			var rows = loadFile.SelectNodes("/PlatformGame/Rows/*");

			foreach (XmlNode row in rows)
			{
				//Store Y-value of current row
				int yValue = int.Parse(row.Attributes["y"].Value);

				foreach (XmlNode tile in row)
				{
					//Store X-value of current column and the tiles type
					int xValue = int.Parse(tile.Attributes["x"].Value);
					string tileType = tile.Attributes["tileType"].Value;

					//Declare new Tile properties
					Tile newTile;
					Texture2D texture;
					string colorString = tile.Attributes["color"].Value;
					//Color color = Settings.TileColors[colorString];
					Color color = Color.White;
					Vector2 position = new Vector2(xValue * Settings.TileSize, yValue * Settings.TileSize);
					float rotation = 0.0f;
					float scale = 1.0f;
					float layerDepth = 1.0f;

					//Initialize the tile depending on its type
					switch (tileType)
					{
						case " ":
							break;
						case "character":
							texture = characterTexture;
							//Vector2 velocity = new Vector2(int.Parse(tile.Attributes["velocityX"].Value), int.Parse(tile.Attributes["velocityY"].Value));
							Vector2 velocity = new Vector2(0.0f, 0.0f);
							bool onGround = true;
							newTile = new Character(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround);
							tileList.Add(newTile);
							break;
						default:
							break;
					}
				}
			}
		}
	}
}