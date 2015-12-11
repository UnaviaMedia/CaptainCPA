/*
 * Project: CaptainCPA - GameManager.cs
 * Purpose: Main game manager
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CaptainCPA
{
	/// <summary>
	/// Main game manager class
	/// </summary>
	public class GameManager : Game
	{
		const float X_SCALE_FACTOR = 25f;
		const float Y_SCALE_FACTOR = 14f;
		
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		#region Scenes
		//Scene declaration
		private List<GameScene> scenes;
		private StartScene startScene;
		private PauseMenuScene pauseMenuScene;
		private ActionScene actionScene;
		private LevelSelectScene levelSelectScene;
		private HelpScene helpScene;
		private AboutScene aboutScene;
		private HighScoreScene highScoreScene;
		private HowToPlayScene howToPlayScene;
		private GameOverMenuScene gameOverMenuScene;

		private GameScene baseScene;
		private GameScene enabledScene;
		#endregion

		private string selectedLevel;

		private KeyboardState oldState;

		/*//FPS Tracking
		int totalFrames = 0;
		float elapsedTime = 0.0f;
		int fps = 0;*/


		public GameManager()
		{
			//Initialize graphics manager properties
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = (int)(Settings.TILE_SIZE * X_SCALE_FACTOR);
			graphics.PreferredBackBufferHeight = (int)(Settings.TILE_SIZE * Y_SCALE_FACTOR);

			//Initialize GameScene list
			scenes = new List<GameScene>();

			Content.RootDirectory = "Content";

			//Enable to uncap FPS (unplayable)
			//graphics.SynchronizeWithVerticalRetrace = false;
			//IsFixedTimeStep = false;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			IsMouseVisible = true;

			//Initialize game stage variable
			Settings.Stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//Initialize first level
			selectedLevel = "Level1";

			//Create all scenes and add to the Components list
			startScene = new StartScene(this, spriteBatch);
			scenes.Add(startScene);

			actionScene = new ActionScene(this, spriteBatch, selectedLevel);
			scenes.Add(actionScene);

			pauseMenuScene = new PauseMenuScene(this, spriteBatch);
			scenes.Add(pauseMenuScene);

			levelSelectScene = new LevelSelectScene(this, spriteBatch);
			scenes.Add(levelSelectScene);

			helpScene = new HelpScene(this, spriteBatch);
			scenes.Add(helpScene);

			aboutScene = new AboutScene(this, spriteBatch);
			scenes.Add(aboutScene);

			howToPlayScene = new HowToPlayScene(this, spriteBatch);
			scenes.Add(howToPlayScene);

			gameOverMenuScene = new GameOverMenuScene(this, spriteBatch);
			scenes.Add(gameOverMenuScene);

			highScoreScene = new HighScoreScene(this, spriteBatch);
			scenes.Add(highScoreScene);

			//Add each GameScene to the game's components
			foreach (GameScene gameScene in scenes)
			{
				this.Components.Add(gameScene);
			}

			//Display the home screen
			startScene.Show();
			baseScene = startScene;
			enabledScene = startScene;
		}

		/// <summary>
		/// Hide all game scenes
		/// </summary>
		private void hideAllScenes()
		{
			foreach (GameScene gameScene in scenes)
			{
				gameScene.Hide();
			}
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			/*//Update FPS
			elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			totalFrames++;

			//1 Second has passed
			if (elapsedTime > 1000.0f)
			{
				fps = totalFrames;
				totalFrames = 0;
				elapsedTime = 0;
				Window.Title = fps.ToString();
			}*/

			#region menu navigation
			int selectedIndex = 0;
			KeyboardState ks = Keyboard.GetState();
			if (baseScene == startScene)
			{
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.Show();
					enabledScene = startScene;
				}

				selectedIndex = startScene.Menu.SelectedIndex;
				if (selectedIndex == (int)menuItemTitles.Start && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
				{
					hideAllScenes();

					//Reset game (to selected level)
					actionScene.Reset(this, spriteBatch, selectedLevel);
					actionScene.Show();
					baseScene = actionScene;
					enabledScene = actionScene;
				}
				else if (selectedIndex == (int)menuItemTitles.Select && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					levelSelectScene.Show();
					enabledScene = levelSelectScene;
				}
				else if (selectedIndex == (int)menuItemTitles.Help && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					helpScene.Show();
					enabledScene = helpScene;
				}
				else if (selectedIndex == (int)menuItemTitles.About && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					aboutScene.Show();
					enabledScene = aboutScene;
				}
				else if (selectedIndex == (int)menuItemTitles.HighScore && ks.IsKeyDown(Keys.Enter))
				{
					//Remove old high score scene
					if (highScoreScene != null)
					{
						scenes.Remove(highScoreScene);
						this.Components.Remove(highScoreScene);
					}

					//Create a new high score scene
					highScoreScene = new HighScoreScene(this, spriteBatch);
					scenes.Add(highScoreScene);
					this.Components.Add(highScoreScene);

					//Display the high score screen
					hideAllScenes();
					highScoreScene.Show();
					enabledScene = highScoreScene;
				}
				else if (selectedIndex == (int)menuItemTitles.HowTo && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					howToPlayScene.Show();
					enabledScene = howToPlayScene;
				}
				else if (selectedIndex == (int)menuItemTitles.Quit && ks.IsKeyDown(Keys.Enter))
				{
					Exit();
				}
			}
			else if (baseScene == actionScene)
			{
				//Display pause menu
				if (ks.IsKeyDown(Keys.Escape))
				{
					//Pause the game
					foreach (var item in actionScene.Components)
					{
						item.Enabled = false;
					}

					//Show the pause menu
					pauseMenuScene.Menu.SelectedIndex = 0;
					pauseMenuScene.Show();
					baseScene = pauseMenuScene;
					enabledScene = pauseMenuScene;
				}

				//Display the Game Over menu when the game ends (player dies)
				if (actionScene.GameOver == true)
				{
					//Pause (end) the game
					/*foreach (var item in actionScene.Components)
					{
						item.Enabled = false;
					}*/
					actionScene.DisableComponents();

					if (gameOverMenuScene != null)
					{
						scenes.Remove(gameOverMenuScene);
						this.Components.Remove(gameOverMenuScene);
					}

					gameOverMenuScene = new GameOverMenuScene(this, spriteBatch);
					scenes.Add(gameOverMenuScene);
					this.Components.Add(gameOverMenuScene);

					//Show the game over menu
					gameOverMenuScene.Show();
					baseScene = gameOverMenuScene;
					enabledScene = gameOverMenuScene;
				}
			}
			else if (baseScene == pauseMenuScene)
			{
				if (enabledScene == pauseMenuScene)
				{
					selectedIndex = pauseMenuScene.Menu.SelectedIndex;

					//Unpause the game
					if (selectedIndex == (int)PauseMenuItems.Resume && ks.IsKeyDown(Keys.Enter))
					{
						foreach (var item in actionScene.Components)
						{
							item.Enabled = true;
						}

						pauseMenuScene.Hide();
						baseScene = actionScene;
						enabledScene = actionScene;
					}

					//Return to Main Menu (and reset game)
					if (selectedIndex == (int)PauseMenuItems.MainMenu && ks.IsKeyDown(Keys.Enter))
					{
						hideAllScenes();
						startScene.Show();
						baseScene = startScene;
						enabledScene = startScene;
					}

					//Display How To Play menu
					if (selectedIndex == (int)PauseMenuItems.HowTo && ks.IsKeyDown(Keys.Enter))
					{
						pauseMenuScene.Hide();
						howToPlayScene.Show();
						enabledScene = howToPlayScene;
					}

					//Exit the game
					if (selectedIndex == (int)PauseMenuItems.Quit && ks.IsKeyDown(Keys.Enter))
					{
						Exit();
					}
				}
				else if (enabledScene == howToPlayScene)
				{
					if (ks.IsKeyDown(Keys.Escape))
					{
						howToPlayScene.Hide();
						pauseMenuScene.Show();
						enabledScene = pauseMenuScene;
					}
				}
			}
			else if (baseScene == gameOverMenuScene)
			{
				//Handle game over
				if (gameOverMenuScene.Enabled)
				{
					//Return to the Main Menu (and reset game)
					if (gameOverMenuScene.HighScoreComponent.NameEntered == true && ks.IsKeyDown(Keys.Enter))
					{
						hideAllScenes();
						startScene.Show();
						baseScene = startScene;
						enabledScene = startScene;
					}
				}
			}
			else if (!startScene.Enabled)
			{
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.Show();
					enabledScene = startScene;
				}
			}
			oldState = ks;
			#endregion

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}
	}
}
