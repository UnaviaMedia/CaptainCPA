/*
 * Project:	PlatformGame - FloppyDisc.cs
 * Purpose:	Disc pickup that gives character points
 *
 * History:
 *		Kendall Roth	Dec-12-2015:	Created
 */

using CaptainCPA.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA.Tiles
{
	/// <summary>
	/// Disc pickup that gives character points
	/// </summary>
	public class FloppyDisc : Pickup
	{
		private Texture2D overlayTexture;
		private Color overlayColor;

		public FloppyDisc(Game game, SpriteBatch spriteBatch, Texture2D texture, Texture2D overlayTexture, Color color, Color overlayColor,
			Vector2 position, float rotation , float scale, float layerDepth, int points)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth, points)
		{
			this.overlayTexture = overlayTexture;
			this.overlayColor = overlayColor;
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
			
		}

		/// <summary>
		/// Allows the game component to draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			//Necessary override in order to draw the overlay texture properly
			spriteBatch.Begin();
			spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, spriteEffect, layerDepth);
			spriteBatch.Draw(overlayTexture, position - origin, overlayColor);
			spriteBatch.End();
		}
	}
}
