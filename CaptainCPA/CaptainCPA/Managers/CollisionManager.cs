/*
 * Project: CaptainCPA - CollisionManager.cs
 * Purpose: Base CollisionManager component
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 *						Nov-29-2015:	Optimizations
 *						Dec-11-2015:	Removed Observer pattern
 */

using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace CaptainCPA
{
	/// <summary>
	/// Base CollisionManager component
	/// </summary>
	public class CollisionManager : GameComponent
	{
		protected List<MoveableTile> moveableTiles;
		protected List<FixedTile> fixedTiles;


		public CollisionManager(Game game, List<MoveableTile> moveableTiles, List<FixedTile> fixedTiles)
			: base(game)
		{
			this.moveableTiles = moveableTiles;
			this.fixedTiles = fixedTiles;
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