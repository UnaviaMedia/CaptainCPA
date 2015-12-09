/*
 * Project: CaptainCPA - About.cs
 * Purpose: Display the game's about information
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 *						Dec-09-2015:	Updated User Interface Design
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Displays the game's about information from a text file
	/// </summary>
	class AboutScene : GameScene
	{
		private Texture2D menuImage;

		//string message;        
		//private SpriteFont font;

		public AboutScene(Game game, SpriteBatch spriteBatch)
			: base(game, spriteBatch)
		{
			//font = game.Content.Load<SpriteFont>("Fonts/MenuFont");
			//message = readFile(@"Text/AboutMessage.txt");

			menuImage = game.Content.Load<Texture2D>("Images/AboutScreen");
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
