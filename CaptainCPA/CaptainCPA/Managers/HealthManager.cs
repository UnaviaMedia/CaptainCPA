/*
 * Project:	PlatformGame - HealthManager.cs
 * Purpose:	Manage health display for the character
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 */

using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Manage health display for the character
	/// </summary>
	public class HealthManager : Observer
	{
		public HealthManager(Game game)
			: base(game)
		{

		}

		public override void OnNotify(object sender, string notification, object secondarySender = null)
		{
			switch (notification)
			{
				case "PlayerLostLife":
					//Update character health
					((Character)sender).LoseLife();
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