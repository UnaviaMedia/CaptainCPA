/*
 * Project: CaptainCPA - ActionScene.cs
 * Purpose:	Displays the game to the user
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 *		Kendall Roth	Nov-27-2015:	Added physics manager, tile collision positioning manager, and character collision manager
 *										Added score display
 *						Nov-29-2015:	Optimizations
 *						Dec-09-2015:	Added game end and reset
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
		protected LevelLoader levelLoader;
		protected List<MoveableTile> moveableTileList;
		protected List<FixedTile> fixedTileList;
		protected Character character;

		protected PhysicsManager physicsManager;
		protected CollisionManager tileCollisionPositioningManager;
		protected CharacterCollisionManager characterCollisionManager;
		protected CharacterStateManager characterPositionManager;

		protected ScoreDisplay scoreDisplay;
		protected HealthDisplay healthDisplay;

		public ActionScene(Game game, SpriteBatch spriteBatch, string level)
			: base(game, spriteBatch)
		{
			//Add a level creator, and create the level
			levelLoader = new LevelLoader(game, spriteBatch);
			levelLoader.LoadGame(level);
			moveableTileList = levelLoader.MoveableTileList;
			fixedTileList = levelLoader.FixedTileList;
			character = levelLoader.Character;

			//Add each tile to the scene components
			foreach (MoveableTile moveableTile in moveableTileList)
			{
				this.Components.Add(moveableTile);
			}

			foreach (FixedTile fixedTile in fixedTileList)
			{
				this.Components.Add(fixedTile);
			}

			//Create physics manager and add it to list of components
			physicsManager = new PhysicsManager(game, moveableTileList, fixedTileList);
			this.Components.Add(physicsManager);

			//Create character collision manager (for pickups, death, etc) and add to list of components
			characterCollisionManager = new CharacterCollisionManager(game, character, moveableTileList, fixedTileList);
			this.Components.Add(characterCollisionManager);

			//Create tile collision manager (in case a collision actually does occur) and add to list of components
			tileCollisionPositioningManager = new TileCollisionPositioningManager(game, moveableTileList, fixedTileList);
			this.Components.Add(tileCollisionPositioningManager);

			//Create display components
			SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/ScoreFont");
			Vector2 scorePosition = new Vector2(Settings.TILE_SIZE + 15);
			scoreDisplay = new ScoreDisplay(game, spriteBatch, scoreFont, scorePosition, Color.Black);
			this.components.Add(scoreDisplay);
			
			healthDisplay = new HealthDisplay(game, spriteBatch, character);
			this.components.Add(healthDisplay);
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
			//Update the score
			scoreDisplay.Message = character.Score.ToString();

			base.Update(gameTime);
		}
	}
}
