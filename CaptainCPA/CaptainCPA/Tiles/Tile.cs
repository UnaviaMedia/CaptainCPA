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
	public class Tile : DrawableGameComponent
	{
		protected SpriteBatch spriteBatch;
		protected Texture2D texture;
		protected Color color;
		protected Vector2 position;

		protected float rotation = 0.0f;
		protected float scale = 1.0f;
		protected Vector2 origin;
		protected SpriteEffects spriteEffect;
		protected float layerDepth = 1.0f;

		protected TileType tileType;
		protected Rectangle bounds;

		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public TileType TileType
		{
			get { return tileType; }
			set { tileType = value; }
		}

		public Rectangle Bounds
		{
			get { return bounds; }
		}


		public Tile(Game game, SpriteBatch spriteBatch, Texture2D texture, TileType tileType, Color color, Vector2 position, float rotation, float scale, float layerDepth)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			this.texture = texture;
			origin = new Vector2(texture.Width / 2, texture.Height / 2);
			this.color = color;
			this.position = new Vector2(position.X + origin.X, position.Y + origin.Y);
			this.tileType = tileType;

			this.rotation = rotation;
			this.scale = scale;
			spriteEffect = SpriteEffects.None;
			this.layerDepth = layerDepth;

			UpdateCollisionBounds();
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
		/// Updates the collision bounds of the Tile
		/// </summary>
		public virtual void UpdateCollisionBounds()
		{
			//Find the rectangle representing the collision bounds of the object
			bounds = new Rectangle(
				(int)(position.X - (texture.Width / 2 * scale)),
				(int)(position.Y - (texture.Height / 2 * scale)),
				(int)(texture.Width * scale),
				(int)(texture.Height * scale));
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			UpdateCollisionBounds();

			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to draw itself
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values</param>
		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, spriteEffect, layerDepth);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
