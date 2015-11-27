/*
 * Project:	PlatformGame - AudioManager.cs
 * Purpose:	Manage audio for the levels by implementing an Observer pattern
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 *										Points added
 */

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


namespace CaptainCPA
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class Gem : FixedTile
	{
		private int points;

		//Observers
		private Observer audioManager;
		private Observer soundManager;

		public int Points
		{
			get { return points; }
			set { points = value; }
		}

		public Gem(Game game, SpriteBatch spriteBatch, Texture2D texture, Color color, Vector2 position, float rotation , float scale, float layerDepth, int points)
			: base(game, spriteBatch, texture, TileType.Pickup, color, position, rotation, scale, layerDepth)
		{
			this.points = points;

			//Add observers
			audioManager = new AudioManager(game);
			this.AddObserver(audioManager);

			soundManager = new ScoreManager(game);
			this.AddObserver(soundManager);
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
