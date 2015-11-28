/*
 * Project: CaptainCPA - ActionScene.cs
 * Purpose:	Displays the game to the user
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 *		Kendall Roth	Nov-27-2015:	Added physics manager, tile collision positioning manager, and character collision manager
 *										Added score display
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Displays the game to the user
	/// </summary>
	public class ActionScene : GameScene
	{
		protected LevelLoadManager levelLoader;
		protected List<Tile> tileList;
		protected Character character;

		protected PhysicsManager physicsManager;
		protected CollisionManager tileCollisionPositioningManager;
		protected CharacterCollisionManager characterCollisionManager;

		protected ScoreDisplay scoreDisplay;

		public ActionScene(Game game, SpriteBatch spriteBatch, string level)
			: base(game, spriteBatch)
		{
			//Add a level creator, and create the level
			levelLoader = new LevelLoadManager(game, spriteBatch);
			levelLoader.LoadGame(level);
			tileList = levelLoader.TileList;
			character = levelLoader.Character;

			//Add each tile to the tile list
			foreach (Tile tile in tileList)
			{
				this.Components.Add(tile);
			}

			//Create physics manager and add it to list of components
			physicsManager = new PhysicsManager(game, tileList);
			this.Components.Add(physicsManager);

			//Create tile collision manager (in case a collision actually does occur) and add to list of components
			//tileCollisionPositioningManager = new TileCollisionPositioningManager(game, tileList);
			//this.Components.Add(tileCollisionPositioningManager);

			//Create character collision manager (for pickups, death, etc) and add to list of components
			characterCollisionManager = new CharacterCollisionManager(game, tileList);
			this.Components.Add(characterCollisionManager);

			//Create display components
			SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/ScoreFont");
			Vector2 scorePosition = new Vector2(Settings.TILE_SIZE + 15);
			scoreDisplay = new ScoreDisplay(game, spriteBatch, scoreFont, scorePosition, Color.Black);
			this.components.Add(scoreDisplay);			
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
			scoreDisplay.Message = character.Score.ToString();

			base.Update(gameTime);
		}
	}
}
