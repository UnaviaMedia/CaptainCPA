/*
 * Project:	PlatformGame - ScoreManager.cs
 * Purpose:	Manage level score
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Manage level score
	/// </summary>
	public class ScoreManager : Observer
	{
		public ScoreManager(Game game)
			: base(game)
		{

		}

		public override void OnNotify(object sender, string notification, object secondarySender = null)
		{
			switch (notification)
			{
				case "GemCollected":
					((Gem)sender).Destroy();
					((Character)secondarySender).Score += ((Gem)sender).Points;
					break;
				default:
					break;
			}
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