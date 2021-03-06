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

using System;
using System.Collections.Generic;
using CaptainCPA.Components;
using CaptainCPA.Components.Display;
using CaptainCPA.Managers;
using CaptainCPA.Tiles;
using CaptainCPA.Tiles.Enemies;
using CaptainCPA.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Scenes
{
	/// <summary>
	/// Displays the game to the user
	/// </summary>
	public class ActionScene : GameScene
	{
		public const float CHARACTER_SCREEN_BUFFER = 400;

		private LevelLoader levelLoader;
		private List<MoveableTile> moveableTileList;
		private List<FixedTile> fixedTileList;

		private PhysicsManager physicsManager;
		private CollisionManager tileCollisionPositioningManager;
		private CharacterCollisionManager characterCollisionManager;
		private CharacterStateManager characterStateManager;

		private ScoreDisplay scoreDisplay;
		private HealthDisplay healthDisplay;
		private int slideBackCounter;
		private int slideBackCounterLimit;

		public Character Character { get; set; }

		public bool GameOver { get; set; }

		/// <summary>
		/// A list of all tiles excepting the Character
		/// </summary>
		protected List<Tile> tiles;


		public ActionScene(Game game, SpriteBatch spriteBatch, string level)
			: base(game, spriteBatch)
		{
			Reset(game, spriteBatch, level, 0, Character.MAX_LIVES);
			//slideBackCounter = 0;
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
			Character = levelLoader.Character;
			Character.Score = playerScore;
			Character.Lives = playerLives;

			#region DisplayComponents
			//Create display components
			scoreDisplay = new ScoreDisplay(game, spriteBatch, Character);
			this.components.Add(scoreDisplay);			

			healthDisplay = new HealthDisplay(game, spriteBatch, Character);
			this.components.Add(healthDisplay);
			#endregion

			foreach (FixedTile fixedTile in fixedTileList)
			{
				this.Components.Add(fixedTile);
			}

			//Add each tile to the scene components
			foreach (MoveableTile moveableTile in moveableTileList)
			{
				this.Components.Add(moveableTile);
			}

			#region Managers
			//Create physics manager and add it to list of components
			physicsManager = new PhysicsManager(game, moveableTileList, fixedTileList);
			this.Components.Add(physicsManager);

			//Create character collision manager (for pickups, death, etc) and add to list of components
			characterCollisionManager = new CharacterCollisionManager(game, Character, moveableTileList, fixedTileList);
			this.Components.Add(characterCollisionManager);

			//Create tile collision manager (in case a collision actually does occur) and add to list of components
			tileCollisionPositioningManager = new TileCollisionPositioningManager(game, moveableTileList, fixedTileList);
			this.Components.Add(tileCollisionPositioningManager);
			#endregion

			//Set game over to false
			GameOver = false;

			//Keep track of all tiles in the scene
			tiles = new List<Tile>();
			foreach (GameComponent component in components)
			{
				if (component is Tile && component.GetType() != typeof(Character))
				{
					tiles.Add(component as Tile);
				}
			}

			//Reset the slide back counter
			slideBackCounter = 0;
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
		/// Slides the level back to its initial position
		/// </summary>
		public void SlideBack()
		{
			foreach (FixedTile t in fixedTileList)
			{
				if (t.Position.X > t.InitPosition.X)
				{
					t.Position = new Vector2(t.Position.X - 4, t.Position.Y);
					if (t is MovingPlatform)
					{
						(t as MovingPlatform).FixedPosition -= 4;
					}
				}
				else if (t.Position.X < t.InitPosition.X)
				{
					t.Position = new Vector2(t.Position.X + 4, t.Position.Y);
					if (t is MovingPlatform)
					{
						(t as MovingPlatform).FixedPosition += 4;
					}
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
			if (Character.LevelComplete)
			{
				return;
			}

			//Track player death
			if (Character.IsAlive == false)
			{
				GameOver = true;
				return;
			}

			//smoothly reset level
			if (Character.IsGhost)
			{
				CharacterStateManager.IsMoving = false;
				slideBackCounter++;

				if (slideBackCounter == 6000)
				{
					slideBackCounter = 1;
				}
				//Tiles are not in their initial position
				if (fixedTileList[0].XPosition != fixedTileList[0].Position.X)
				{
					float diff = fixedTileList[0].XPosition - fixedTileList[0].InitPosition.X;

					if (Math.Abs(diff) > 7)
					{
						slideBackCounterLimit = 1;
						Character.Color = Color.Red;
					}
					else
				{
						slideBackCounterLimit = 2;
						Character.Color = Color.Red;
					}

					foreach (MoveableTile m in moveableTileList)
					{
						if (m.Enabled && !(m is Boulder))
						{
							m.Position = new Vector2(-1000, -1000);
						m.Visible = false;
							m.Enabled = false;
						}
					}
					if (slideBackCounter % slideBackCounterLimit == 0)
					{
						SlideBack();
					}
				}
				else //scene is in its initial position; reset all moveable components
				{
					foreach (MoveableTile m in moveableTileList)
					{
						if (!(m is Boulder))
						{

						m.Position = m.InitPosition;
						m.Enabled = true;
						m.Visible = true;
					}
					}
					Character.IsGhost = false;
				}
			}

			CharacterStateManager.TooFarRight = false;
			if (CharacterStateManager.IsMoving)
			{
				//Character is within range of the right side of the screen
				if (Character.Bounds.Right >= Utilities.Utilities.Stage.X - CHARACTER_SCREEN_BUFFER)
				{
					CharacterStateManager.TooFarRight = true;
					if (CharacterStateManager.FacingRight) //Character is moving to the right
					{
						foreach (Tile t in tiles)
						{
							t.Position = new Vector2(t.Position.X - Character.MOVE_SPEED, t.Position.Y);
							if (t is MovingPlatform)
							{
								(t as MovingPlatform).FixedPosition -= Character.MOVE_SPEED;
							}
						}
						CharacterStateManager.ScreenMoving = true;
					}
				}

				//character is within range of the left side of the screen
				else if (Character.Bounds.Left <= CHARACTER_SCREEN_BUFFER)
				{
					if (!CharacterStateManager.FacingRight) //character is moving to the left
					{
						foreach (Tile t in tiles)
						{
							t.Position = new Vector2(t.Position.X + Character.MOVE_SPEED, t.Position.Y);
							if (t is MovingPlatform)
							{
								(t as MovingPlatform).FixedPosition += Character.MOVE_SPEED;
							}
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
