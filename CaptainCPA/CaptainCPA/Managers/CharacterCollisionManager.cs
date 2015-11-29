/*
 * Project:	PlatformGame - CharacterCollisionManager.cs
 * Purpose:	Manage non-positioning collision (extra logic)
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 *										Collision detection with observers added
 *										Pixel-checking added
 *						Nov-28-2015:	Optimizations
 *										Improved Observer pattern
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Manage non-positioning collisions (extra logic)
	/// </summary>
	public class CharacterCollisionManager : CollisionManager
	{
		private Character character;

		//Observers
		private Observer audioManager;
		private Observer soundManager;
		private Observer levelEventsManager;

		public CharacterCollisionManager(Game game, Character character, List<MoveableTile> moveableTiles, List<FixedTile> fixedTiles)
			: base(game, moveableTiles, fixedTiles)
		{
			this.character = character;

			//Add observers
			audioManager = new AudioManager(game);
			this.AddObserver(audioManager);

			soundManager = new ScoreManager(game);
			this.AddObserver(soundManager);

			levelEventsManager = new LevelEventManager(game);
			this.AddObserver(levelEventsManager);
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
			foreach (FixedTile fixedTile in fixedTiles)
			{
				//Skip collision detection if the moveable tile is hidden or disabled
				if (fixedTile.Visible == false || fixedTile.Enabled == false || character.Equals(fixedTile))
				{
					continue;
				}

				//Skip collision detection if the moveable tile is too far away or doesn't intersect
				if (Vector2.Distance(Utilities.PointToVector2(character.Bounds.Center), Utilities.PointToVector2(fixedTile.Bounds.Center)) > 100 ||
					character.Bounds.Intersects(fixedTile.Bounds) == false || Utilities.PerPixelCollision(character, fixedTile) == false)
				{
					continue;
				}

				if (fixedTile is Gem)
				{
					Notify(fixedTile, "GemCollected", character);
				}
				else if (fixedTile is Spike)
				{
					Notify(fixedTile, "PlayerLandedOnSpike", character);
				}
			}

			base.Update(gameTime);
		}
	}
}