/*
 * Project:	PlatformGame - AudioManager.cs
 * Purpose:	Manage audio for the levels by implementing an Observer pattern
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace CaptainCPA
{
	/// <summary>
	/// Manage audio for the levels by implementing an Observer pattern
	/// </summary>
	public class AudioManager : Observer
	{
		public AudioManager(Game game)
			: base(game)
		{

		}

		public override void OnNotify(object sender, string notification, object secondarySender = null)
		{
			switch (notification)
			{
				case "GemCollected":
					SoundEffect ding = Game.Content.Load<SoundEffect>("Sounds/Ding");
					ding.Play();
					break;
				case "PlayerLandedOnSpike":
					SoundEffect spike = Game.Content.Load<SoundEffect>("Sounds/hurtflesh3");
					spike.Play();
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