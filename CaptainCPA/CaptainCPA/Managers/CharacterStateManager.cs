/*
 * Project: CaptainCPA - CharacterStateManager.cs
 * Purpose: Character state manager that manages character state
 *
 * History:
 *		Doug Epp		Nov-26-2015:	Created
 */

using Microsoft.Xna.Framework;

namespace CaptainCPA.Managers
{
	/// <summary>
	/// Manages the state of the character
	/// </summary>
	public class CharacterStateManager : GameComponent
	{
		public static Vector2 Velocity { get; set; }

		public static bool IsMoving { get; set; }

		public static bool OnGround { get; set; }

		public static bool TooFarRight { get; set; }

		public static bool ScreenMoving { get; set; }

		public static Vector2 CharacterPosition { get; set; }

		public static bool FacingRight { get; set; }


		public CharacterStateManager(Game game)
			: base(game)
		{

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
