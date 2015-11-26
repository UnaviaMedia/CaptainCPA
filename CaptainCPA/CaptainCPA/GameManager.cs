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
		private ActionScene actionScene;
		private HelpScene helpScene;
        private AboutScene aboutScene;
		#endregion

		public GameManager()
		{
			//Initialize graphics manager properties
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = (int)(Settings.TILE_SIZE * X_SCALE_FACTOR);
			graphics.PreferredBackBufferHeight = (int)(Settings.TILE_SIZE * Y_SCALE_FACTOR);

			//Initialize GameScene list
			scenes = new List<GameScene>();

			Content.RootDirectory = "Content";
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

			//Initialize game stage viarable
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

			actionScene = new ActionScene(this, spriteBatch);
			scenes.Add(actionScene);

			helpScene = new HelpScene(this, spriteBatch);
			scenes.Add(helpScene);

            aboutScene = new AboutScene(this, spriteBatch);
            scenes.Add(aboutScene);

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
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here
			int selectedIndex = 0;
			KeyboardState ks = Keyboard.GetState();

			if (startScene.Enabled)
			{
				selectedIndex = startScene.Menu.SelectedIndex;
				if (selectedIndex == (int)menuItemTitles.Start && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					actionScene.Show();
				}
				if (selectedIndex == (int)menuItemTitles.Help && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					helpScene.Show();
				}
                if(selectedIndex==(int)menuItemTitles.About && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.Show();
                }
				//if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
				//{
				//    hideAllScenes();
				//    //highScoreScene.show();
				//}
				//if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
				//{
				//    hideAllScenes();
				//    //aboutCreditScene.show();
				//}
				//if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
				//{
				//    hideAllScenes();
				//    //howToPlayScene.show();
				//}
				//... other scenes here

				if (selectedIndex == (int)menuItemTitles.Quit && ks.IsKeyDown(Keys.Enter))
				{
					Exit();
				}
			}

			if (!startScene.Enabled)
			{
				if (ks.IsKeyDown(Keys.Escape))
				{
					hideAllScenes();
					startScene.Show();
				}
			}

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
