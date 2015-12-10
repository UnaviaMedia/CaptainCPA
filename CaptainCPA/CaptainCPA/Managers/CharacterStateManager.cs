using Microsoft.Xna.Framework;


namespace CaptainCPA
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class CharacterStateManager : GameComponent
	{
		private static Vector2 characterPosition;

		public static Vector2 CharacterPosition
		{
			get { return CharacterStateManager.characterPosition; }
			set { CharacterStateManager.characterPosition = value; }
		}
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
			// TODO: Add your initialization code here

			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// TODO: Add your update code here

			base.Update(gameTime);
		}
	}
}
