using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaptainCPA
{
	public class LevelLoadManager
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
		/// <param name="levelName">File path to the save file</param>
		public void LoadGame(string levelName)
		{
			//Create list of level tiles
			tileList = new List<Tile>();

			//Load the different block textures
			Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/Character");
			Texture2D blockTexture = game.Content.Load<Texture2D>("Sprites/Block");
			Texture2D platformTexture = game.Content.Load<Texture2D>("Sprites/Platform-Middle");
			Texture2D platformEndTexture = game.Content.Load<Texture2D>("Sprites/Platform-End");

			//Create a new XML document and load the selected save file
			XmlDocument loadFile = new XmlDocument();
			loadFile.Load(@"Content/Levels/" + levelName + ".xml");
			
			var rows = loadFile.SelectNodes("/XnaContent/PlatformGame/Rows/*");

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
					Tile newTile = null;
					Texture2D texture;
					string colorString = tile.Attributes["color"].Value;
					Color color = ColorConverter.ConvertColor(colorString);
					Vector2 position = new Vector2(xValue * Settings.TILE_SIZE, yValue * Settings.TILE_SIZE);
					float rotation = 0.0f;
					float scale = 1.0f;
					float layerDepth = 1.0f;

					//Initialize the tile depending on its type
					switch (tileType)
					{
						case "":
						case " ":
							break;
						case "block":
							texture = blockTexture;
							newTile = new Block(game, spriteBatch, blockTexture, color, position, rotation, scale, layerDepth);
							break;
						case "platform":
							texture = platformTexture;
							newTile = new Platform(game, spriteBatch, platformTexture, color, position, rotation, scale, layerDepth);
							break;
						case "platform-left":
							newTile = new Platform(game, spriteBatch, platformEndTexture, color, position, rotation, scale, layerDepth);
							break;
						case "platform-right":
							newTile = new Platform(game, spriteBatch, platformEndTexture, color, position, rotation, scale, layerDepth);
							newTile.SpriteEffects = SpriteEffects.FlipHorizontally;
							break;
						case "character":
							texture = characterTexture;
							Vector2 velocity = new Vector2(float.Parse(tile.Attributes["velocityX"].Value), float.Parse(tile.Attributes["velocityY"].Value));
							bool onGround = true;
							newTile = new Character(game, spriteBatch, texture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround);
							break;
						default:
							break;
					}

					//If the tile is not null, add it to the tile list
					if (newTile != null)
					{
						tileList.Add(newTile); 
					}
				}
			}
		}
	}
}