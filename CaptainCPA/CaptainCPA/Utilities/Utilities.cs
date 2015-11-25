/*
 * Project: CaptainCPA - Utilities.cs
 * Purpose: Class that contains various utility methods
 *
 * History:
 *		Kendall Roth	Nov-24-2015:	Created
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaptainCPA
{
	/// <summary>
	/// Class that contains various utility methods
	/// </summary>
	public class Utilities
	{
		/// <summary>
		/// Convert a Point to Vector2
		/// </summary>
		/// <param name="point">Point to convert to Vector2</param>
		/// <returns>Vector2 representing the input Point</returns>
		public static Vector2 PointToVector2(Point point)
		{
			return new Vector2(point.X, point.Y);
		}
	}
}
