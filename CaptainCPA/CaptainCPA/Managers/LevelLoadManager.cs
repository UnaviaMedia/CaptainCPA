/*
 * Project: CaptainCPA - LevelLoadManager.cs
 * Purpose: Loads the specified XML level file
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-27-2015:	Added Character property
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CaptainCPA
{
	/// <summary>
	/// Loads the specified XMl level file
	/// </summary>
	public class LevelLoadManager
	{
		private Game game;
		private SpriteBatch spriteBatch;
		private List<Tile> tileList;
		private Character character;

		public List<Tile> TileList
		{
			get { return tileList; }
			set { tileList = value; }
		}

		public Character Character
		{
			get { return character; }
			set { character = value; }
		}

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

			#region LoadTextures
			//Load the different block textures
			Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/Character");
			Texture2D blockTexture = game.Content.Load<Texture2D>("Sprites/Block");
			Texture2D platformTexture = game.Content.Load<Texture2D>("Sprites/Platform");
			Texture2D platformMiddleTexture = game.Content.Load<Texture2D>("Sprites/Platform-Middle");
			Texture2D platformEndTexture = game.Content.Load<Texture2D>("Sprites/Platform-End");
			Texture2D gemTexture = game.Content.Load<Texture2D>("Sprites/Gem");
			#endregion

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
							newTile = new Block(game, spriteBatch, blockTexture, color, position, rotation, scale, layerDepth);
							break;
						case "platform-middle":
							newTile = new Platform(game, spriteBatch, platformMiddleTexture, color, position, rotation, scale, layerDepth);
							break;
						case "platform-left":
							newTile = new Platform(game, spriteBatch, platformEndTexture, color, position, rotation, scale, layerDepth);
							break;
						case "platform-right":
							newTile = new Platform(game, spriteBatch, platformEndTexture, color, position, rotation, scale, layerDepth);
							newTile.SpriteEffects = SpriteEffects.FlipHorizontally;
							break;
						case "platform":
							newTile = new Platform(game, spriteBatch, platformTexture, color, position, rotation, scale, layerDepth);
							break;
						case "gem":
							int points = int.Parse(tile.Attributes["points"].Value);
							newTile = new Gem(game, spriteBatch, gemTexture, color, position, rotation, scale, layerDepth, points);
							break;
						case "character":
							Vector2 velocity = new Vector2(float.Parse(tile.Attributes["velocityX"].Value), float.Parse(tile.Attributes["velocityY"].Value));
							bool onGround = true;
							newTile = new Character(game, spriteBatch, characterTexture, TileType.Character, color, position, rotation, scale, layerDepth, velocity, onGround);
							character = (Character)newTile;
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