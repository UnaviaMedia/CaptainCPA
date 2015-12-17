/*
 * Project: CaptainCPA - GameManager.cs
 * Purpose: Main game manager
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using CaptainCPA.Scenes;

namespace CaptainCPA
{
	/// <summary>
	/// Main game manager class
	/// </summary>
	public class GameManager : Game
	{
		const float HORIZONTAL_BLOCKS_ON_SCREEN = 25f;
		const float VERTICAL_BLOCKS_ON_SCREEN = 14f;
		const int NUMBER_OF_LEVELS = 4;

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
		private LevelOverScene levelOverScene;
		private GameOverMenuScene gameOverMenuScene;

		private GameScene baseScene;
		private GameScene enabledScene;

		#region SceneBackgrounds
		private Texture2D startSceneBackground;
		private Texture2D pauseMenuBackground;
		private Texture2D levelSelectBackground;
		private Texture2D helpSceneBackground;
		private Texture2D aboutSceneBackground;
		private Texture2D highScoreSceneBackground;
		private Texture2D howToPlaySceneBackground;
		private Texture2D levelOverSceneBackground;
		private Texture2D gameOverSceneBackground;
		#endregion
		#endregion

		private List<string> levelList;
		private int currentLevel;

		private List<Song> backgroundMusic;
		private int backgroundMusicCounter;

		private KeyboardState oldState;

		/*//FPS Tracking
		int totalFrames = 0;
		float elapsedTime = 0.0f;
		int fps = 0;*/


		public GameManager()
		{
			//Initialize graphics manager properties
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = (int)(Utilities.Utilities.TILE_SIZE * HORIZONTAL_BLOCKS_ON_SCREEN);
			graphics.PreferredBackBufferHeight = (int)(Utilities.Utilities.TILE_SIZE * VERTICAL_BLOCKS_ON_SCREEN);

			//Initialize GameScene list
			scenes = new List<GameScene>();

			Content.RootDirectory = "Content";

			Window.Title = "CaptainCPA";

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
			Utilities.Utilities.Stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

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


			#region BackgroundMusic
			//Reset the background music index counter
			backgroundMusicCounter = 0;

			MediaPlayer.Volume = 0.2f;

			//Instantiate list of songs
			backgroundMusic = new List<Song>();

			//If you could, please credit me as 'Joe Reynolds - Professorlamp' Refer people to my website at - jrtheories.webs.com Thank you!
			//http://opengameart.org/content/energetic-platformer-music-drop-table-bass
			backgroundMusic.Add(Content.Load<Song>("Sounds/CinderellasBallgagM"));

			//If you could, please credit me as 'Joe Reynolds - Professorlamp' Refer people to my website at - jrtheories.webs.com Thank you!
			//http://opengameart.org/content/cinderellas-ballgag
			backgroundMusic.Add(Content.Load<Song>("Sounds/DropTableBassFinal"));
			#endregion


			#region LevelCreation
			//Create level list
			levelList = new List<string>() { "Level1", "Level2", "Level3", "Level4" };
			#endregion


			#region SceneCreation
			startSceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/MainMenuScreen");
			pauseMenuBackground = Content.Load<Texture2D>("ScreenBackgrounds/PauseMenuScreen");
			levelSelectBackground = Content.Load<Texture2D>("ScreenBackgrounds/LevelSelectScreen");
			helpSceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/HelpScreen");
			aboutSceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/AboutScreen");
			highScoreSceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/HighScoreScreen");
			howToPlaySceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/ControlsScreen");
			levelOverSceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/LevelOverScreen");
			gameOverSceneBackground = Content.Load<Texture2D>("ScreenBackgrounds/GameOverScreen");

			//Create all scenes and add to the Components list
			startScene = new StartScene(this, spriteBatch, startSceneBackground);
			scenes.Add(startScene);

			actionScene = new ActionScene(this, spriteBatch, levelList[0]);
			currentLevel = 1;
			scenes.Add(actionScene);

			pauseMenuScene = new PauseMenuScene(this, spriteBatch, pauseMenuBackground);
			scenes.Add(pauseMenuScene);

			levelSelectScene = new LevelSelectScene(this, spriteBatch, levelSelectBackground, NUMBER_OF_LEVELS, Utilities.Utilities.CheckLevelProgression());
			scenes.Add(levelSelectScene);

			helpScene = new HelpScene(this, spriteBatch, helpSceneBackground);
			scenes.Add(helpScene);

			aboutScene = new AboutScene(this, spriteBatch, aboutSceneBackground);
			scenes.Add(aboutScene);

			howToPlayScene = new HowToPlayScene(this, spriteBatch, howToPlaySceneBackground);
			scenes.Add(howToPlayScene);

			levelOverScene = new LevelOverScene(this, spriteBatch, levelOverSceneBackground, 0);
			scenes.Add(levelOverScene);

			gameOverMenuScene = new GameOverMenuScene(this, spriteBatch, gameOverSceneBackground, 0);
			scenes.Add(gameOverMenuScene);

			highScoreScene = new HighScoreScene(this, spriteBatch, highScoreSceneBackground);
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
			#endregion
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
		/// Allows the game to run logic such as updating the world, checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			#region FPSTracking
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
			#endregion

			#region MenuNavigation
			int selectedIndex = 0;
			KeyboardState ks = Keyboard.GetState();

			//Handle menu navigation
			if (baseScene == startScene)
			{
				//Return to START scene from all sub-scenes
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.Show();
					enabledScene = startScene;
				}

				if (enabledScene == startScene)
				{
					//Get the selected item index from the menu
					selectedIndex = startScene.Menu.SelectedIndex;

					//Display the corresponding sub-scene based on user selection
					if (selectedIndex == (int)MenuItemTitles.START && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Display the Game scene
						hideAllScenes();

						//Reset game (to selected level)
						actionScene.Reset(this, spriteBatch, levelList[0]);
						//actionScene.Reset(this, spriteBatch, "Debug");
						currentLevel = 1;
						actionScene.Show();
						baseScene = actionScene;
						enabledScene = actionScene;
					}
					else if (selectedIndex == (int)MenuItemTitles.SELECT && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Remove level selector scene
						if (levelSelectScene != null)
						{
							scenes.Remove(levelSelectScene);
							this.Components.Remove(levelSelectScene);
						}

						//Create a new level selector scene
						levelSelectScene = new LevelSelectScene(this, spriteBatch, levelSelectBackground, NUMBER_OF_LEVELS, Utilities.Utilities.CheckLevelProgression());
						scenes.Add(levelSelectScene);
						this.Components.Add(levelSelectScene);

						//Display the LevelSelector scene
						hideAllScenes();
						levelSelectScene.Show();
						enabledScene = levelSelectScene;
					}
					else if (selectedIndex == (int)MenuItemTitles.HELP && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Display the HELP scene
						hideAllScenes();
						helpScene.Show();
						enabledScene = helpScene;
					}
					else if (selectedIndex == (int)MenuItemTitles.ABOUT && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Display the ABOUT scene
						hideAllScenes();
						aboutScene.Show();
						enabledScene = aboutScene;
					}
					else if (selectedIndex == (int)MenuItemTitles.HIGH_SCORE && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Remove old high score scene
						if (highScoreScene != null)
						{
							scenes.Remove(highScoreScene);
							this.Components.Remove(highScoreScene);
						}

						//Create a new high score scene
						highScoreScene = new HighScoreScene(this, spriteBatch, highScoreSceneBackground);
						scenes.Add(highScoreScene);
						this.Components.Add(highScoreScene);

						//Display the high score screen
						hideAllScenes();
						highScoreScene.Show();
						enabledScene = highScoreScene;
					}
					else if (selectedIndex == (int)MenuItemTitles.HOW_TO && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Display the HowToPlay scene
						hideAllScenes();
						howToPlayScene.Show();
						enabledScene = howToPlayScene;
					}
					else if (selectedIndex == (int)MenuItemTitles.QUIT && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Exit
						Exit();
					}
				}
				else if (enabledScene == levelSelectScene)
				{
					//DEBUG - Reset Level Progression
					if (ks.IsKeyDown(Keys.OemTilde) && ks.IsKeyDown(Keys.R))
					{
						Utilities.Utilities.ResetLevelProgression();
					}

					if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Get the selected item index from the level selector menu
						int selectedLevelIndex = levelSelectScene.SelectedIndex;

						//Display the Game scene
						hideAllScenes();

						//Load selected level
						actionScene.Reset(this, spriteBatch, levelList[selectedLevelIndex]);
						currentLevel = selectedLevelIndex + 1;
						actionScene.Show();
						baseScene = actionScene;
						enabledScene = actionScene;
					}
				}
				else if (enabledScene == highScoreScene)
				{
					//DEBUG - Reset the HighScores
					if (ks.IsKeyDown(Keys.OemTilde) && ks.IsKeyDown(Keys.R))
					{
						Utilities.Utilities.ResetHighScores();
					}
					if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						hideAllScenes();
						startScene.Show();
						enabledScene = startScene;
					}
				}
				else if ((enabledScene == highScoreScene || enabledScene == helpScene || enabledScene == aboutScene || enabledScene == howToPlayScene) && 
					(ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)))
				{
					hideAllScenes();
					startScene.Show();
					enabledScene = startScene;
				}
			}
			else if (baseScene == actionScene)
			{
				if (enabledScene == actionScene)
				{
					//Display pause menu
					if (ks.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
					{
						//Pause the game
						actionScene.DisableComponents();

						//Show the pause menu
						pauseMenuScene.Menu.SelectedIndex = 0;
						pauseMenuScene.Show();
						baseScene = pauseMenuScene;
						enabledScene = pauseMenuScene;
					}

					//Display the LevelOver menu when the level ends
					if (actionScene.Character.LevelComplete == true)
					{
						//Display the LevelOver menu if it is not the last level
						if (currentLevel < NUMBER_OF_LEVELS)
						{
							//Pause the game
							actionScene.DisableComponents();

							//Update the Level Progression if necessary
							if (currentLevel + 1 > Utilities.Utilities.CheckLevelProgression())
							{
								Utilities.Utilities.UpdateLevelProgression(currentLevel + 1);
							}

							//Remove the current LevelOver menu scene if one exists
							if (levelOverScene != null)
							{
								scenes.Remove(levelOverScene);
								this.Components.Remove(levelOverScene);
							}

							//Create a new LevelOver menu scene
							levelOverScene = new LevelOverScene(this, spriteBatch, levelOverSceneBackground, actionScene.Character.Score);
							scenes.Add(levelOverScene);
							this.Components.Add(levelOverScene);

							//Show the LevelOver menu
							levelOverScene.Show();
							baseScene = actionScene;
							enabledScene = levelOverScene;

							//Reset the level over tracker
							actionScene.Character.LevelComplete = false;
						}
						//Display the GameOver menu if the last level has been completed
						else
						{
							//Set game over to true
							actionScene.GameOver = true;
							actionScene.Character.LevelComplete = false;

							//Update the Level Progression file
							Utilities.Utilities.UpdateLevelProgression(NUMBER_OF_LEVELS);

							//Play the game over sound effect
							SoundEffect gameOver = Content.Load<SoundEffect>("Sounds/GameOver");
							gameOver.Play();
						}
					}

					//Display the Game Over menu when the game ends (player dies)
					if (actionScene.GameOver == true)
					{
						//Pause the game
						actionScene.DisableComponents();

						//Remove the current GameOver menu scene if one exists
						if (gameOverMenuScene != null)
						{
							scenes.Remove(gameOverMenuScene);
							this.Components.Remove(gameOverMenuScene);
						}

						//Create a new GameOver menu scene
						gameOverMenuScene = new GameOverMenuScene(this, spriteBatch, gameOverSceneBackground, actionScene.Character.Score);
						scenes.Add(gameOverMenuScene);
						this.Components.Add(gameOverMenuScene);

						//Show the GameOver menu
						gameOverMenuScene.Show();
						baseScene = gameOverMenuScene;
						enabledScene = gameOverMenuScene;
					}
				}
				else if (enabledScene == levelOverScene)
				{
					//Advance to the next level
					if (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
					{
						//Display the Game scene
						hideAllScenes();

						//Load selected level
						actionScene.Reset(this, spriteBatch, levelList[++currentLevel - 1], actionScene.Character.Score, actionScene.Character.Lives);
						actionScene.Show();
						baseScene = actionScene;
						enabledScene = actionScene;
					}
				}
			}
			else if (baseScene == pauseMenuScene)
			{
				if (enabledScene == pauseMenuScene)
				{
					//Get the selected menu item index from the Pause menu
					selectedIndex = pauseMenuScene.Menu.SelectedIndex;

					//Unpause the game
					if (selectedIndex == (int)PauseMenuItems.RESUME && ks.IsKeyDown(Keys.Enter))
					{
						actionScene.EnableComponents();

						pauseMenuScene.Hide();
						baseScene = actionScene;
						enabledScene = actionScene;
					}

					//Return to Main Menu (and reset game)
					if (selectedIndex == (int)PauseMenuItems.MAIN_MENU && ks.IsKeyDown(Keys.Enter))
					{
						hideAllScenes();
						startScene.Show();
						baseScene = startScene;
						enabledScene = startScene;
					}

					//Display How To Play menu
					if (selectedIndex == (int)PauseMenuItems.HOW_TO && (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)))
					{
						pauseMenuScene.Hide();
						howToPlayScene.Show();
						enabledScene = howToPlayScene;
					}

					//Exit the game
					if (selectedIndex == (int)PauseMenuItems.QUIT && ks.IsKeyDown(Keys.Enter))
					{
						Exit();
					}
				}
				else if (enabledScene == howToPlayScene)
				{
					//Return to the pause menu
					if (ks.IsKeyDown(Keys.Escape) || (ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)))
					{
						howToPlayScene.Hide();
						pauseMenuScene.Show();
						enabledScene = pauseMenuScene;
					}
				}
			}
			else if (baseScene == gameOverMenuScene)
			{
				//Handle game over event
				if (gameOverMenuScene.Enabled)
				{
					//Return to the Main Menu (and reset game)
					if (gameOverMenuScene.HighScoreComponent.NameEntered == true && ks.IsKeyDown(Keys.Enter))
					{
						hideAllScenes();
						startScene.Show();
						startScene.Menu.SelectedIndex = 0;
						baseScene = startScene;
						enabledScene = startScene;
					}
				}
			}
			else if (!startScene.Enabled)
			{
				//Should never get here!!!
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.Show();
					enabledScene = startScene;
				}
			}

			//Set the old keystate to the current keystate
			oldState = ks;
			#endregion

			//Debug Mode
			if (ks.IsKeyDown(Keys.Space))
			{
				Console.WriteLine("Debug Mode");
			}

			//Background music
			if (MediaPlayer.State != MediaState.Playing)
			{
				if (++backgroundMusicCounter > backgroundMusic.Count - 1)
				{
					backgroundMusicCounter = 0;
				}

				MediaPlayer.Play(backgroundMusic[backgroundMusicCounter]);
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			//Redraw the screen
			GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}
	}
}
