/*
 * Project:	PlatformGame - ScoreDisplay.cs
 * Purpose:	Displays the character's score
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Displays the score for the character
	/// </summary>
	public class ScoreDisplay : DisplayString
	{
		public ScoreDisplay(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 position, Color color, string message = "")
			: base(game, spriteBatch, spriteFont, position, color, message)
		{
			
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			// TODO: Add your initialization code here

			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// TODO: Add your update code here

			base.Update(gameTime);
		}
	}
}
