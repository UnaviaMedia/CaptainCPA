/*
 * Project: CaptainCPA - LevelSelectScene.cs
 * Purpose: Display a Level Select screen
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created, updated User Interface Design
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// This is the scene which allows users to select a level
	/// </summary>
	public class LevelSelectScene : GameScene
	{
		private Texture2D menuImage;

		public LevelSelectScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			menuImage = game.Content.Load<Texture2D>("Images/LevelSelectMenu");
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

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
