/*
 * Project:	PlatformGame - Observer.cs
 * Purpose:	Base observer class
 *
 * History:
 *		Kendall Roth	Nov-27-2015:	Created and abstract notification method added
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CaptainCPA
{
	/// <summary>
	/// Class that will serve as base class for all observers
	/// </summary>
	public abstract class Observer : GameComponent
	{
		public Observer(Game game)
			: base(game)
		{

		}

		public abstract void OnNotify(object sender, string notification, object secondarySender = null);
	}
}