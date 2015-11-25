/*
 * Project: CaptainCPA - GameScene.cs
 * Purpose: Base GameScene component
 *
 * History:
 *		Doug Epp		Nov-24-2015:	Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CaptainCPA
{
	/// <summary>
	/// Base GameScene component
	/// </summary>
	public class GameScene : DrawableGameComponent
	{
		protected SpriteBatch spriteBatch;
		protected List<GameComponent> components;

		public List<GameComponent> Components
		{
			get { return components; }
			set { components = value; }
		}

		public virtual void Show()
		{
			this.Enabled = true;
			this.Visible = true;
		}

		public virtual void Hide()
		{
			this.Enabled = false;
			this.Visible = false;
		}

		public GameScene(Game game, SpriteBatch spriteBatch)
			: base(game)
		{
			this.spriteBatch = spriteBatch;
			components = new List<GameComponent>();
			Hide();
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
			foreach (GameComponent item in components)
			{
				if (item.Enabled)
				{
					item.Update(gameTime);
				}
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			DrawableGameComponent comp = null;
			foreach (GameComponent item in components)
			{
				if (item is DrawableGameComponent)
				{
					comp = (DrawableGameComponent)item;
					if (comp.Visible)
					{
						comp.Draw(gameTime);
					}

				}
			}

			base.Draw(gameTime);
		}
	}
}
