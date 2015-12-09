/*
 * Project: CaptainCPA - GameManager.cs
 * Purpose: Main game manager
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
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
		private HelpScene helpScene;
		private AboutScene aboutScene;
		private HighScoreScene highScoreScene;
		private HowToPlayScene howToPlayScene;
		#endregion

		private KeyboardState oldState;

		//FPS Tracking
		int totalFrames = 0;
		float elapsedTime = 0.0f;
		int fps = 0;


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

			//Create all scenes and add to the Components list
			startScene = new StartScene(this, spriteBatch);
			scenes.Add(startScene);

			pauseMenuScene = new PauseMenuScene(this, spriteBatch);
			scenes.Add(pauseMenuScene);

			actionScene = new ActionScene(this, spriteBatch, "Level1");
			scenes.Add(actionScene);

			helpScene = new HelpScene(this, spriteBatch);
			scenes.Add(helpScene);

			aboutScene = new AboutScene(this, spriteBatch);
			scenes.Add(aboutScene);

			highScoreScene = new HighScoreScene(this, spriteBatch);
			scenes.Add(highScoreScene);

			howToPlayScene = new HowToPlayScene(this, spriteBatch);
			scenes.Add(howToPlayScene);

			//Add each GameScene to the game's components
			foreach (GameScene gameScene in scenes)
			{
				this.Components.Add(gameScene);
			}

			//Display the home screen
			startScene.Show();
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

			//1 Second has passed
			if (elapsedTime > 1000.0f)
			{
				fps = totalFrames;
				totalFrames = 0;
				elapsedTime = 0;
				//Window.Title = fps.ToString();
			}*/

			#region menu navigation
			int selectedIndex = 0;
			KeyboardState ks = Keyboard.GetState();
			if (startScene.Enabled)
			{
				selectedIndex = startScene.Menu.SelectedIndex;
				if (selectedIndex == (int)menuItemTitles.Start && ks.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
				{
					hideAllScenes();
					//TODO: Reset action scene
					actionScene.Show();
				}
				if (selectedIndex == (int)menuItemTitles.Select && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					//TODO: Add level select scene
				}
				if (selectedIndex == (int)menuItemTitles.Help && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					helpScene.Show();
				}
				if (selectedIndex == (int)menuItemTitles.About && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					aboutScene.Show();
				}
				if (selectedIndex == (int)menuItemTitles.HighScore && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					highScoreScene.Show();
				}
				if (selectedIndex == (int)menuItemTitles.HowTo && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					howToPlayScene.Show();
				}
				if (selectedIndex == (int)menuItemTitles.Quit && ks.IsKeyDown(Keys.Enter))
				{
					Exit();
				}
			}

			if (actionScene.Enabled)
			{
				if (ks.IsKeyDown(Keys.Escape))//display pause menu
				{
					foreach (var item in actionScene.Components)
					{
						item.Enabled = false;
					}
					pauseMenuScene.Show();
				}
				if (pauseMenuScene.Enabled)
				{
					selectedIndex = pauseMenuScene.Menu.SelectedIndex;

					if (selectedIndex == (int)PauseMenuItems.Resume && ks.IsKeyDown(Keys.Enter))
					{
						foreach (var item in actionScene.Components)
						{
							item.Enabled = true;
						}
						pauseMenuScene.Hide();
					}
					if (selectedIndex == (int)PauseMenuItems.MainMenu && ks.IsKeyDown(Keys.Enter))
					{
						hideAllScenes();
						startScene.Show();
					}
					if (selectedIndex == (int)PauseMenuItems.HowTo && ks.IsKeyDown(Keys.Enter))
					{
						pauseMenuScene.Hide();
						howToPlayScene.Show();
					}
					if (selectedIndex == (int)PauseMenuItems.Quit && ks.IsKeyDown(Keys.Enter))
					{
						Exit();
					}
				}
				if (howToPlayScene.Enabled)
				{
					if (ks.IsKeyDown(Keys.Escape))
					{
						howToPlayScene.Hide();
						pauseMenuScene.Show();
					}
				}
			}
			else if (!startScene.Enabled)
			{
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.Show();
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
