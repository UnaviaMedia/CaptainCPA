/*
 * Project: CaptainCPA - ActionScene.cs
 * Purpose:	Displays the game to the user
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 *		Kendall Roth	Nov-27-2015:	Added physics manager, tile collision positioning manager, and character collision manager
 *										Added score display
 *						Nov-29-2015:	Optimizations
 *						Dec-09-2015:	Added health display, added game over, added game reset
 *						Dec-12-2015:	Added level over
 *		Doug Epp						Added level reset after character loses life
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
		protected const float RIGHT_CHARACTER_BUFFER = 400;

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
		private int counter;
		private bool gameOver;

		public bool GameOver
		{
			get { return gameOver; }
			set { gameOver = value; }
		}

		public Character Character
		{
			get { return character; }
			set { character = value; }
		}

		/// <summary>
		/// A list of all tiles excepting the Character
		/// </summary>
		protected List<Tile> tiles;


		public ActionScene(Game game, SpriteBatch spriteBatch, string level)
			: base(game, spriteBatch)
		{
			#region OldConstructor
			/*//Add a level creator, and create the level
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

			#region Managers
			//Create physics manager and add it to list of components
			physicsManager = new PhysicsManager(game, moveableTileList, fixedTileList);
			this.Components.Add(physicsManager);

			//Create character collision manager (for pickups, death, etc) and add to list of components
			characterCollisionManager = new CharacterCollisionManager(game, character, moveableTileList, fixedTileList);
			this.Components.Add(characterCollisionManager);

			//Create tile collision manager (in case a collision actually does occur) and add to list of components
			tileCollisionPositioningManager = new TileCollisionPositioningManager(game, moveableTileList, fixedTileList);
			this.Components.Add(tileCollisionPositioningManager);
			#endregion

			#region DisplayComponents
			//Create display components
			SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/ScoreFont");
			Vector2 scorePosition = new Vector2(Settings.TILE_SIZE + 15);
			scoreDisplay = new ScoreDisplay(game, spriteBatch, scoreFont, scorePosition, Color.Black);
			this.components.Add(scoreDisplay);
			
			healthDisplay = new HealthDisplay(game, spriteBatch, character);
			this.components.Add(healthDisplay);
			#endregion

			//Set game over to false
			GameOver = false;*/
			#endregion

			Reset(game, spriteBatch, level, 0, Character.MAX_LIVES);
			counter = 0;
		}

		/// <summary>
		/// Resets the specified level
		/// </summary>
		/// <param name="game">Game reference</param>
		/// <param name="spriteBatch">Spritebatch reference</param>
		/// <param name="level">Level to reload</param>
		/// <param name="playerScore">Player score</param>
		/// <param name="playerLives">Player lives</param>
		public void Reset(Game game, SpriteBatch spriteBatch, string level, int playerScore, int playerLives)
		{
			//Reset component list
			this.Components = new List<GameComponent>();

			//Add a level creator, and create the level
			levelLoader = new LevelLoader(game, spriteBatch);
			levelLoader.LoadGame(level);
			moveableTileList = levelLoader.MoveableTileList;
			fixedTileList = levelLoader.FixedTileList;
			character = levelLoader.Character;
			character.Score = playerScore;
			character.Lives = playerLives;

			//Add each tile to the scene components
			foreach (MoveableTile moveableTile in moveableTileList)
			{
				this.Components.Add(moveableTile);
			}

			foreach (FixedTile fixedTile in fixedTileList)
			{
				this.Components.Add(fixedTile);
			}

			#region Managers
			//Create physics manager and add it to list of components
			physicsManager = new PhysicsManager(game, moveableTileList, fixedTileList);
			this.Components.Add(physicsManager);

			//Create character collision manager (for pickups, death, etc) and add to list of components
			characterCollisionManager = new CharacterCollisionManager(game, character, moveableTileList, fixedTileList);
			this.Components.Add(characterCollisionManager);

			//Create tile collision manager (in case a collision actually does occur) and add to list of components
			tileCollisionPositioningManager = new TileCollisionPositioningManager(game, moveableTileList, fixedTileList);
			this.Components.Add(tileCollisionPositioningManager);
			#endregion

			#region DisplayComponents
			//Create display components
			SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/ScoreFont");
			Vector2 scorePosition = new Vector2(Settings.TILE_SIZE + 15);
			scoreDisplay = new ScoreDisplay(game, spriteBatch, character);
			this.components.Add(scoreDisplay);			

			healthDisplay = new HealthDisplay(game, spriteBatch, character);
			this.components.Add(healthDisplay);
			#endregion

			//Set game over to false
			gameOver = false;

			//Keep track of all tiles in the scene
			tiles = new List<Tile>();
			foreach (GameComponent component in components)
			{
				if (component is Tile && component.GetType() != typeof(Character))
				{
					tiles.Add(component as Tile);
				}
			}

		}


		/// <summary>
		/// Resets the level to its beinning
		/// </summary>
		/// <param name="game">Game reference</param>
		/// <param name="spriteBatch">Spritebatch reference</param>
		/// <param name="level">Level to reset</param>
		public void Reset(Game game, SpriteBatch spriteBatch, string level)
		{
			Reset(game, spriteBatch, level, 0, Character.MAX_LIVES);
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
		/// Slides the level back to its initial position
		/// </summary>
		public void slideBack()
		{
			foreach (FixedTile t in fixedTileList)
			{
				if (t.Position.X > t.InitPosition.X)
				{
					t.Position = new Vector2(t.Position.X - 4, t.Position.Y);
				}
				else if (t.Position.X < t.InitPosition.X)
				{
					t.Position = new Vector2(t.Position.X + 4, t.Position.Y);
				}
			}
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			//Track level completion
			if (character.LevelComplete == true)
			{
				return;
			}

			//Track player death
			if (character.IsAlive == false)
			{
				gameOver = true;
				return;
			}

			//smoothly reset level
			if (character.IsGhost)
			{
				counter++;

				if (counter == 6000)
				{
					counter = 0;
				}

				if (fixedTileList[0].XPosition != fixedTileList[0].Position.X)
				{
					foreach (MoveableTile m in moveableTileList)
					{
						//m.Enabled = false;
						m.Position = m.InitPosition;
						m.Visible = false;
					}
					if (counter % 2 == 0)
					{
						slideBack();
					}
				}
				else //scene is in its initial position; reset all moveable components
				{
					foreach (MoveableTile m in moveableTileList)
					{
						//m.Enabled = true;
						m.Visible = true;
						m.Position = m.InitPosition;
					}
					character.IsGhost = false;
				}
			}

			CharacterStateManager.TooFarRight = false;
			if (CharacterStateManager.IsMoving)
			{
				//Character is within range of the right side of the screen
				if (character.Bounds.Right >= Settings.Stage.X - RIGHT_CHARACTER_BUFFER)
				{
					CharacterStateManager.TooFarRight = true;
					if (CharacterStateManager.FacingRight) //Character is moving to the right
					{
						foreach (Tile t in tiles)
						{
							t.Position = new Vector2(t.Position.X - character.Speed, t.Position.Y);
						}
						CharacterStateManager.ScreenMoving = true;
					}
				}

				//character is within range of the left side of the screen
				else if (character.Bounds.Left <= RIGHT_CHARACTER_BUFFER)
				{
					if (!CharacterStateManager.FacingRight) //character is moving to the left
					{
						foreach (Tile t in tiles)
						{
							t.Position = new Vector2(t.Position.X + character.Speed, t.Position.Y);
						}
						CharacterStateManager.ScreenMoving = true;
					}
				}
				else CharacterStateManager.ScreenMoving = false;
			}

			base.Update(gameTime);
		}
	}
}
