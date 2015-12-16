/*
 * Project:	PlatformGame - LevelBarrier.cs
 * Purpose:	Impassable tile that can be used for level barriers
 *
 * History:
 *		Kendall Roth	Dec-14-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Impassable tile that can be used for level barriers
	/// </summary>
	public class LevelBarrier : FixedTile
	{
		public LevelBarrier(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation, float scale, float layerDepth)
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
