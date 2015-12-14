/*
 * Project:	PlatformGame - HealthDisplay.cs
 * Purpose:	Displays the character's health
 *
 * History:
 *		Kendall Roth	Dec-09-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Displays the health for the character
	/// </summary>
	public class HealthDisplay : DrawableGameComponent
	{
		private SpriteBatch spriteBatch;
		private Texture2D mainTexture;
		private Texture2D rightTexture;
		private Texture2D skullTexture;
		private Vector2 position;
		private Rectangle sourceRectangle;
		private Character character;

		public HealthDisplay(Game game, SpriteBatch spriteBatch, Character character)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.character = character;

			//Import necessary mainTextures
			mainTexture = game.Content.Load<Texture2D>("Displays/HealthDisplay-Main");
			rightTexture = game.Content.Load<Texture2D>("Displays/HealthDisplay-Right");
			skullTexture = game.Content.Load<Texture2D>("Displays/HealthDisplay-Skull");
			sourceRectangle = new Rectangle(0, 0, mainTexture.Width, mainTexture.Height);
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
			//Update the source rectangle of the Health Display
			sourceRectangle.Width = 10 + (character.Lives * 35);

			//Update the position of the Health Display
			position = new Vector2((Utilities.Stage.X / 2) - ((sourceRectangle.Width + rightTexture.Width) / 2), 30);

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();

			//If the character still has lives, display the life counter
			//	Otherwise, display a death symbol (skull) :)
			if (character.Lives > 0)
			{
				spriteBatch.Draw(mainTexture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
				spriteBatch.Draw(rightTexture, new Vector2(position.X + sourceRectangle.Width, position.Y), Color.White); 
			}
			else
			{
				spriteBatch.Draw(skullTexture, new Vector2((Utilities.Stage.X / 2) - (skullTexture.Width / 2), position.Y), Color.White);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
