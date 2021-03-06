﻿/*
 * Project: CaptainCPA - LevelLoadManager.cs
 * Purpose: Loads the specified XML level file
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-27-2015:	Added Character property
 *						Nov-29-2015:	Optimizations
 *						Dec-12-2015:	Removed Character speed and jump
 */

using System;
using System.Collections.Generic;
using System.Xml;
using CaptainCPA.Components;
using CaptainCPA.Tiles;
using CaptainCPA.Tiles.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Utilities
{
	/// <summary>
	/// Loads the specified XMl level file
	/// </summary>
	public class LevelLoader
	{
		private Game game;
		private SpriteBatch spriteBatch;

		public List<MoveableTile> MoveableTileList { get; set; }

		public List<FixedTile> FixedTileList { get; set; }

		public Character Character { get; set; }

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
			MoveableTileList = new List<MoveableTile>();
			FixedTileList = new List<FixedTile>();

			#region LoadTextures
			//Load the different block textures
			Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/Character");
			//Texture2D characterTexture = game.Content.Load<Texture2D>("Sprites/braidSpriteSheet");

			List<Texture2D> grassTextures = new List<Texture2D>
			{
				game.Content.Load<Texture2D>("Sprites/Grass1"),
				game.Content.Load<Texture2D>("Sprites/Grass2"),
				game.Content.Load<Texture2D>("Sprites/Grass3"),
				game.Content.Load<Texture2D>("Sprites/Grass4"),
				game.Content.Load<Texture2D>("Sprites/Grass5")
			};

			List<Texture2D> grassEndTexures = new List<Texture2D>
			{
				game.Content.Load<Texture2D>("Sprites/GrassEnd1"),
				game.Content.Load<Texture2D>("Sprites/GrassEnd2"),
				game.Content.Load<Texture2D>("Sprites/GrassEnd3")
			};

			List<Texture2D> dirtTextures = new List<Texture2D>
			{
				game.Content.Load<Texture2D>("Sprites/Dirt1"),
				game.Content.Load<Texture2D>("Sprites/Dirt2"),
				game.Content.Load<Texture2D>("Sprites/Dirt3")
			};

			Texture2D blockTexture = game.Content.Load<Texture2D>("Sprites/Block");
			Texture2D ventLeftTexture = game.Content.Load<Texture2D>("Sprites/Vent-Left");
			Texture2D ventMiddleTexture = game.Content.Load<Texture2D>("Sprites/Vent-Middle");
			Texture2D ventRightTexture = game.Content.Load<Texture2D>("Sprites/Vent-Right");
			Texture2D levelBarrierTexture = game.Content.Load<Texture2D>("Sprites/LevelBarrier");
			Texture2D levelEndTexture = game.Content.Load<Texture2D>("Sprites/LevelEnd");
			Texture2D platformTexture = game.Content.Load<Texture2D>("Sprites/Platform");
			Texture2D platformMiddleTexture = game.Content.Load<Texture2D>("Sprites/Platform-Middle");
			Texture2D platformEndTexture = game.Content.Load<Texture2D>("Sprites/Platform-End");
			Texture2D floppyDiscTexture = game.Content.Load<Texture2D>("Sprites/FloppyDisc");
			Texture2D floppyDiscOverlayTexture = game.Content.Load<Texture2D>("Sprites/FloppyDiscOverlay");
			Texture2D spikeTexture = game.Content.Load<Texture2D>("Sprites/Spike");
			Texture2D spikeTopTexture = game.Content.Load<Texture2D>("Sprites/Spike-Top");
			Texture2D flagTexture = game.Content.Load<Texture2D>("Sprites/Flag");
			Texture2D dinoSkeletonTexture = game.Content.Load<Texture2D>("Images/Dino-skeleton");
			#endregion

			//Randomizer for generating random textures
			Random r = new Random();

			//Create a new XML document and load the selected save file
			XmlDocument loadFile = new XmlDocument();
			loadFile.Load(@"Content/Levels/" + levelName + ".xml");
			
			XmlNodeList rows = loadFile.SelectNodes("/XnaContent/PlatformGame/Rows/*");

			foreach (XmlNode row in rows)
			{
				//Store Y-value of current row
				float yValue = float.Parse(row.Attributes["y"].Value);

				foreach (XmlNode tile in row)
				{
					//Skip XML comments (will crash otherwise)
					if (tile is XmlComment)
					{
						continue;
					}

					//Store X-value of current column and the tiles type
					float xValue = float.Parse(tile.Attributes["x"].Value);
					string tileType = tile.Attributes["tileType"].Value;

					//Declare new Tile properties
					Tile newTile = null;
					Color color = Utilities.ConvertColor(tile.Attributes["color"].Value);
					Vector2 position = new Vector2(xValue * Utilities.TILE_SIZE, yValue * Utilities.TILE_SIZE);
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
						case "grass":							
							newTile = new Block(game, spriteBatch, grassTextures[r.Next(0, grassTextures.Count)], color, position, rotation, scale, layerDepth);
							break;
						case "grass-left":
							newTile = new Block(game, spriteBatch, grassEndTexures[r.Next(0, grassEndTexures.Count)], color, position, rotation, scale, layerDepth);
							break;
						case "grass-right":
							newTile = new Block(game, spriteBatch, grassEndTexures[r.Next(0, grassEndTexures.Count)], color, position, rotation, scale, layerDepth);
							newTile.SpriteEffects = SpriteEffects.FlipHorizontally;
							break;
						case "dirt":
							newTile = new Block(game, spriteBatch, dirtTextures[r.Next(0, dirtTextures.Count)], color, position, rotation, scale, layerDepth);
							break;
						case "vent-left":
							newTile = new Block(game, spriteBatch, ventLeftTexture, color, position, rotation, scale, layerDepth);
							break;
						case "vent-middle":
							newTile = new Block(game, spriteBatch, ventMiddleTexture, color, position, rotation, scale, layerDepth);
							break;
						case "vent-right":
							newTile = new Block(game, spriteBatch, ventRightTexture, color, position, rotation, scale, layerDepth);
							break;
						case "level-barrier":
							newTile = new LevelBarrier(game, spriteBatch, levelBarrierTexture, color, position, rotation, scale, layerDepth);
							break;
						case "level-end":
							newTile = new LevelEnd(game, spriteBatch, levelEndTexture, color, position, rotation, scale, layerDepth);
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
						case "moving-platform-middle":
							bool goRight = (tile.Attributes["init-dir"].Value == "L") ? false : true;
							int range = int.Parse(tile.Attributes["range"].Value);
							newTile = new MovingPlatform(game, spriteBatch, platformMiddleTexture, color, position, rotation, scale, layerDepth, range, goRight);
							break;
						case "moving-platform-left":
							goRight = (tile.Attributes["init-dir"].Value == "L") ? false : true;
							range = int.Parse(tile.Attributes["range"].Value);
							newTile = new MovingPlatform(game, spriteBatch, platformEndTexture, color, position, rotation, scale, layerDepth, range, goRight);
							break;
						case "moving-platform-right":
							goRight = (tile.Attributes["init-dir"].Value == "L") ? false : true;
							range = int.Parse(tile.Attributes["range"].Value);
							newTile = new MovingPlatform(game, spriteBatch, platformEndTexture, color, position, rotation, scale, layerDepth, range, goRight);
							newTile.SpriteEffects = SpriteEffects.FlipHorizontally;
							break;
						case "moving-platform":
							goRight = (tile.Attributes["init-dir"].Value == "L") ? false : true;
							range = int.Parse(tile.Attributes["range"].Value);
							newTile = new MovingPlatform(game, spriteBatch, platformTexture, color, position, rotation, scale, layerDepth, range, goRight);
							break;
						case "platform":
							newTile = new Platform(game, spriteBatch, platformTexture, color, position, rotation, scale, layerDepth);
							break;
						case "floppy-disc":
							int points = int.Parse(tile.Attributes["points"].Value);
							newTile = new FloppyDisc(game, spriteBatch, floppyDiscTexture, floppyDiscOverlayTexture, Color.White, color, position, rotation, scale, layerDepth, points);
							break;
						case "spike":
							newTile = new Spike(game, spriteBatch, spikeTexture, color, position, rotation, scale, layerDepth);
							break;
						case "spike-top":
							newTile = new Spike(game, spriteBatch, spikeTopTexture, color, position, rotation, scale, layerDepth);
							break;
						case "character":
							int lives = int.Parse(tile.Attributes["lives"].Value);
							newTile = new Character(game, spriteBatch, characterTexture, color, position, rotation, scale, layerDepth, Vector2.Zero, true, lives);
							Character = (Character)newTile;
							break;
						case "boulder":
							velocity = new Vector2(-2, 0);
							onGround = true;
							Texture2D texture = game.Content.Load<Texture2D>("Sprites/Meteor1");
							newTile = new Boulder(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, velocity, onGround);
							break;
						case "monstar":
							velocity = new Vector2(2, 0);
							onGround = true;
							newTile = new Monstar(game, spriteBatch, blockTexture, color, position, rotation, scale, layerDepth, velocity, onGround);
							break;
						case "mimic":
							velocity = new Vector2(3, 0);
							onGround = true;
							newTile = new Mimic(game, spriteBatch, blockTexture, color, position, rotation, scale, layerDepth, velocity, onGround);
							break;
						case "flag":
							newTile = new LevelEnd(game, spriteBatch, flagTexture, color, position, rotation, scale, layerDepth);
							break;
						case "dino-skeleton":
							newTile = new Block(game, spriteBatch, dinoSkeletonTexture, color, position, rotation, scale, layerDepth);
							break;
						default:
							break;
					}

					//Determine if tile is collideable
					if (tile.Attributes["collideable"] != null)
					{
						newTile.IsCollideable = bool.Parse(tile.Attributes["collideable"].Value);
					}

					//If the tile is not null, add it to the correct tile list
					if (newTile is MoveableTile)
					{
						MoveableTileList.Add((MoveableTile)newTile);
					}
					else if (newTile is FixedTile)
					{
						FixedTileList.Add((FixedTile)newTile);
					}
				}
			}
		}
	}
}