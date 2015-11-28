using System;
using Microsoft.Xna.Framework;


namespace CaptainCPA
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
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

		public FPSManager(Game game)
			: base(game)
		{
			
		}

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

		public override void Draw(GameTime gameTime)
		{
			frameCounter++;

			Game.Window.Title = frameRate.ToString();
			/*string fps = string.Format("fps: {0}", frameRate);

			spriteBatch.Begin();

			spriteBatch.DrawString(spriteFont, fps, new Vector2(33, 33), Color.Black);
			spriteBatch.DrawString(spriteFont, fps, new Vector2(32, 32), Color.White);

			spriteBatch.End();*/
		}
	}
}