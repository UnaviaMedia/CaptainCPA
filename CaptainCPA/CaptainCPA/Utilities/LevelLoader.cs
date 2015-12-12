/*
 * Project: CaptainCPA - LevelLoadManager.cs
 * Purpose: Loads the specified XML level file
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-27-2015:	Added Character property
 *						Nov-29-2015:	Optimizations
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
	public class LevelLoader
	{
		private Game game;
		private SpriteBatch spriteBatch;
		private List<MoveableTile> moveableTileList;
		private List<FixedTile> fixedTileList;
		private Character character;

		public List<MoveableTile> MoveableTileList
		{
			get { return moveableTileList; }
			set { moveableTileList = value; }
		}

		public List<FixedTile> FixedTileList
		{
			get { return fixedTileList; }
			set { fixedTileList = value; }
		}

		public Character Character
		{
			get { return character; }
			set { character = value; }
		}

		public LevelLoader(Game game, SpriteBatch spriteBatch)
		{
			this.game = game;
			this.spriteBatch = spriteBatch;
		}

		/// <summary>
		/// Loads the specified CaptainCPA XML level file
		/// </summary>
		/// <param name="levelName">File path to the save file</param>
		public void LoadGame(string levelName)
		{
			//Create lists of level tiles
			moveableTileList = new List<MoveableTile>();
			fixedTileList = new List<FixedTile>();

			#region LoadTextures
			//Load the different block textures
			Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/Character");
			//Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/braidSpriteSheet");

			Texture2D blockTexture = game.Content.Load<Texture2D>("Sprites/Block");
			Texture2D platformTexture = game.Content.Load<Texture2D>("Sprites/Platform");
			Texture2D platformMiddleTexture = game.Content.Load<Texture2D>("Sprites/Platform-Middle");
			Texture2D platformEndTexture = game.Content.Load<Texture2D>("Sprites/Platform-End");
			Texture2D discTexture = game.Content.Load<Texture2D>("Sprites/Disc");
			Texture2D spikeTexture = game.Content.Load<Texture2D>("Sprites/Spike");
			Texture2D computerTexture = game.Content.Load<Texture2D>("Sprites/Computer");
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
					Color color = ColorConverter.ConvertColor(tile.Attributes["color"].Value);
					Vector2 position = new Vector2(xValue * Settings.TILE_SIZE, yValue * Settings.TILE_SIZE);
					float rotation = 0.0f;
					float scale = 1.0f;
					float layerDepth = 1.0f;
					Vector2 velocity = Vector2.Zero;
					bool onGround = true;

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
						case "disc":
							int points = int.Parse(tile.Attributes["points"].Value);
							newTile = new Disc(game, spriteBatch, discTexture, color, position, rotation, scale, layerDepth, points);
							break;
						case "spike":
							newTile = new Spike(game, spriteBatch, spikeTexture, color, position, rotation, scale, layerDepth);
							break;
						case "character":
							int lives = int.Parse(tile.Attributes["lives"].Value);
							float speed = float.Parse(tile.Attributes["speed"].Value);
							float jumpSpeed = float.Parse(tile.Attributes["jumpSpeed"].Value);
							newTile = new Character(game, spriteBatch, characterTexture, TileType.Character, color, position, rotation, scale, layerDepth, 
								Vector2.Zero, true, lives, speed, jumpSpeed);
							character = (Character)newTile;
							break;
						case "enemy":
							velocity = new Vector2(float.Parse(tile.Attributes["velocityX"].Value), float.Parse(tile.Attributes["velocityY"].Value));
							onGround = true;
							newTile = new Enemy(game, spriteBatch, blockTexture, TileType.Enemy, color, position, rotation, scale, layerDepth, velocity, onGround);
							break;
						case "pursuingEnemy":
							velocity = new Vector2(float.Parse(tile.Attributes["velocityX"].Value), float.Parse(tile.Attributes["velocityY"].Value));
							onGround = true;
							newTile = new PursuingEnemy(game, spriteBatch, blockTexture, TileType.Enemy, color, position, rotation, scale, layerDepth, velocity, onGround);
							break;
						case "computer":
							newTile = new Computer(game, spriteBatch, computerTexture, color, position, rotation, scale, layerDepth);
							break;
						default:
							break;
					}

					//If the tile is not null, add it to the correct tile list
					if (newTile is MoveableTile)
					{
						moveableTileList.Add((MoveableTile)newTile);
					}
					else if (newTile is FixedTile)
					{
						fixedTileList.Add((FixedTile)newTile);
					}
				}
			}
		}
	}
}