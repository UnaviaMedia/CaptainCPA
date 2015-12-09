/*
 * Project: CaptainCPA - CollisionManager.cs
 * Purpose: Base CollisionManager component
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-29-2015:	Optimizations
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace CaptainCPA
{
	/// <summary>
	/// Base CollisionManager component
	/// </summary>
	public class CollisionManager : Subject
	{
		protected List<MoveableTile> moveableTiles;
		protected List<FixedTile> fixedTiles;

		//Observers
		protected Observer audioManager;
		protected Observer soundManager;
		protected Observer levelEventsManager;

		public CollisionManager(Game game, List<MoveableTile> moveableTiles, List<FixedTile> fixedTiles)
			: base(game)
		{
			this.moveableTiles = moveableTiles;
			this.fixedTiles = fixedTiles;

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
			base.Update(gameTime);
		}
	}
}
