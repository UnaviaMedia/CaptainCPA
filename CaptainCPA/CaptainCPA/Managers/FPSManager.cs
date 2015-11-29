/*
 * Project: CaptainCPA - FPSManager.cs
 * Purpose: Track FPS to increase optimizations
 *
 * History:
 *		Kendall Roth	Nov-28-2015:	Created
 */

using System;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Track FPS to increase optimisability
	/// </summary>
	public class FPSManager : DrawableGameComponent
	{
		private int frameRate = 0;
		private int frameCounter = 0;
		private TimeSpan elapsedTime = TimeSpan.Zero;

		public int FrameRate
		{
			get { return frameRate; }
		}

		/// <summary>
		/// Constructor for FPSManager
		/// </summary>
		/// <param name="game">Reference to the Game instance</param>
		public FPSManager(Game game)
			: base(game)
		{
			
		}

		/// <summary>
		/// Allows the game component to update itself
		/// </summary>
		/// <param name="gameTime">Reference to game timing values</param>
		public override void Update(GameTime gameTime)
		{
			elapsedTime += gameTime.ElapsedGameTime;

			if (elapsedTime > TimeSpan.FromSeconds(1))
			{
				elapsedTime -= TimeSpan.FromSeconds(1);
				frameRate = frameCounter;
				frameCounter = 0;
			}
		}

		/// <summary>
		/// Allows the game components to draw itself (or at least update)
		/// </summary>
		/// <param name="gameTime">Reference to game timing values</param>
		public override void Draw(GameTime gameTime)
		{
			frameCounter++;

			Game.Window.Title = frameRate.ToString();
		}
	}
}