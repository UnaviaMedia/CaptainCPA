/*
 * Project:	PlatformGame - CharacterCollisionManager.cs
 * Purpose:	Manage non-positioning collision (extra logic)
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created
 *										Collision detection with observers added
 *										Pixel-checking added
 *						Nov-28-2015:	Optimizations
 *										Removed observers to place in base class
 *						Dec-11-2015:	Removed Observer pattern
 *						Dec-12-2015:	Added collision with enemies (decreases character lives)
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace CaptainCPA
{
	/// <summary>
	/// Manage non-positioning collisions (extra logic)
	/// </summary>
	public class CharacterCollisionManager : CollisionManager
	{
		private Character character;		

		public CharacterCollisionManager(Game game, Character character, List<MoveableTile> moveableTiles, List<FixedTile> fixedTiles)
			: base(game, moveableTiles, fixedTiles)
		{
			this.character = character;
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
				//Don't check for collisions if the player is dead/ghost
				if (character.Enabled == false || character.Visible == false || character.IsGhost == true || character.IsAlive == false)
				{
					return;
				}

				//Skip collision detection if the moveable tile is hidden or disabled
				if (fixedTile.Visible == false || fixedTile.Enabled == false || fixedTile.IsCollideable == false)
				{
					continue;
				}

				//Skip collision detection if the moveable tile is too far away or doesn't intersect
				if (Vector2.Distance(Utilities.PointToVector2(character.Bounds.Center), Utilities.PointToVector2(fixedTile.Bounds.Center)) > 125 ||
					character.Bounds.Intersects(fixedTile.Bounds) == false || Utilities.PerPixelCollision(character, fixedTile) == false)
				{
					continue;
				}

				if (fixedTile.TileType == TileType.Pickup)
				{
					//Destroy the pickup
					fixedTile.Destroy();

					//Add the points to the character's score
					character.Score += ((Pickup)fixedTile).Points;

					//Play the collection sound effect
					//Generic ding
					SoundEffect ding = Game.Content.Load<SoundEffect>("Sounds/Ding");
					ding.Play();
				}
				else if (fixedTile.TileType == TileType.Obstacle)
				{
					//Update character health
					character.LoseLife();

					if (fixedTile is Spike)
					{
						//Color the spike tile
						((Spike)fixedTile).Color = Color.Red;
					}
				}
				else if (fixedTile.TileType == TileType.LevelEnd)
				{
					//End the level
					character.LevelComplete = true;

					//Hide the character
					character.Destroy();

					//TODO - Play a level complete sound
					//Generic ding
					SoundEffect ding = Game.Content.Load<SoundEffect>("Sounds/Ding");
					ding.Play();
				}
			}

			foreach (MoveableTile moveableTile in moveableTiles)
			{
				//Don't check for collisions if the player is dead/ghost
				if (character.Enabled == false || character.Visible == false || character.IsGhost == true || character.IsAlive == false)
				{
					return;
				}

				//Skip collision detection if the moveable tile is hidden or disabled
				if (moveableTile.Visible == false || moveableTile.Enabled == false || character.Equals(moveableTile))
				{
					continue;
				}

				//Skip collision detection if the moveable tile is too far away or doesn't intersect
				if (Vector2.Distance(Utilities.PointToVector2(character.Bounds.Center), Utilities.PointToVector2(moveableTile.Bounds.Center)) > 125 ||
					character.Bounds.Intersects(moveableTile.Bounds) == false || Utilities.PerPixelCollision(character, moveableTile) == false)
				{
					continue;
				}

				if (moveableTile is Enemy)
				{
					//Decrease character lives
					character.LoseLife();
				}
			}

			base.Update(gameTime);
		}
	}
}