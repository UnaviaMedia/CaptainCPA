/*
 * Project:	PlatformGame - Computer.cs
 * Purpose:	Computer that indicates the end of the level
 *
 * History:
 *		Kendall Roth	Dec-12-2015:	Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CaptainCPA
{
	/// <summary>
	/// Gem pickup that gives character points
	/// </summary>
	public class Computer : FixedTile
	{
		public Computer(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation , float scale, float layerDepth)
			: base(game, spriteBatch, texture, TileType.LevelEnd, color, position, rotation, scale, layerDepth)
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