/*
 * Project:	PlatformGame - LevelEnd.cs
 * Purpose:	Semi-transparent block that indicates the end of a level once reached
 *
 * History:
 *		Kendall Roth	Dec-13-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaptainCPA
{
	/// <summary>
	/// Semi-transparent block that indicates the end of a level once reached
	/// </summary>
	public class LevelEnd : FixedTile
	{
		public LevelEnd(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation , float scale, float layerDepth)
			: base(game, spriteBatch, texture, color, position, rotation, scale, layerDepth)
		{
			
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
	}
}
