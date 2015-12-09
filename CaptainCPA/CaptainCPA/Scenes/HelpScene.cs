/*
 * Project: CaptainCPA - HelpScene.cs
 * Purpose: Display a HelpScene screen
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 *		Kendall Roth	Dec-09-2015:	Updated User Interface Design
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// This is the scene which displays help information
	/// </summary>
	public class HelpScene : GameScene
	{
		private Texture2D menuImage;

//		private string message;
		//private SpriteFont font;

		public HelpScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			//font = game.Content.Load<SpriteFont>("Fonts/MenuFont");
			//message = readFile(@"Text/HelpMessage.txt");
			menuImage = game.Content.Load<Texture2D>("Images/HelpScreen");
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
			//spriteBatch.DrawString(font, message, new Vector2(0, 0), Color.White);
			spriteBatch.Draw(menuImage, Vector2.Zero, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
