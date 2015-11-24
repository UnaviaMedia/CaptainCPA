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


namespace CaptainCPA.Tiles
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class FixedTile : Tile
	{
		public FixedTile(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth)
			: base(game, spriteBatch, texture, tileType, color, position, rotation, scale, layerDepth)
		{
			// TODO: Construct any child components here
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
