/*
 * Project: CaptainCPA - ActionScene.cs
 * Purpose: Displays the game to the user
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CaptainCPA
{
	/// <summary>
	/// Displays the game to the user
	/// </summary>
	public class ActionScene : GameScene
	{
		protected LevelLoadManager levelLoader;
		protected List<Tile> tileList;

		protected CollisionManager tileCollisionManager;
		protected PhysicsManager physicsManager;

		public ActionScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			//Add a level creator, and create the level
			levelLoader = new LevelLoadManager(game, spriteBatch);
			levelLoader.LoadGame("Level1");
			tileList = levelLoader.tileList;

			//Add each tile to the tile list
			foreach (Tile tile in tileList)
			{
				this.Components.Add(tile);
			}

			//Create tile collision manager and add to list of components
			//tileCollisionManager = new TileCollisionManager(game, tileList);
			//this.Components.Add(tileCollisionManager);

			physicsManager = new PhysicsManager(game, tileList);
			this.Components.Add(physicsManager);
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
	}
}
