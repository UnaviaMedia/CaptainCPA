/*
 * Project:	PlatformGame - Subject.cs
 * Purpose:	Represents the subject in an observer pattern
 *
 * History:
 *		Kendall Roth	Nov-22-2015:	Created
 *										Add and remove observer methods added
 *										Notify method added
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Represents the subject in an observer pattern
	/// </summary>
	public abstract class Subject : DrawableGameComponent
	{
		protected List<Observer> observers;

		public List<Observer> Observers
		{
			get { return observers; }
			set { observers = value; }
		}

		public void AddObserver(Observer observer)
		{
			observers.Add(observer);
		}

		public void RemoveObserver(Observer observer)
		{
			observers.Remove(observer);
		}

		protected void Notify(object sender, string notification, object secondarySender = null)
		{
			foreach (Observer observer in observers)
			{
				observer.OnNotify(sender, notification, secondarySender);
			}
		}


		public Subject(Game game)
			: base(game)
		{
			observers = new List<Observer>();
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