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


		/// <summary>
		/// Determine whether or not a pixel-perfect collision has occurred
		/// </summary>
		/// <param name="a">Possibly colliding tile</param>
		/// <param name="b">Tile to check against</param>
		/// <returns></returns>
		public static bool PerPixelCollision(Tile a, Tile b)
		{
			// Get Color data of each Texture
			Color[] bitsA = new Color[a.Texture.Width * a.Texture.Height];
			a.Texture.GetData(bitsA);
			Color[] bitsB = new Color[b.Texture.Width * b.Texture.Height];
			b.Texture.GetData(bitsB);

			//Get rough collision rectangle
			Rectangle intersection = Rectangle.Intersect(a.Bounds, b.Bounds);

			for (int y = intersection.Top; y < intersection.Bottom; ++y)
			{
				for (int x = intersection.Left; x < intersection.Right; ++x)
				{
					// Get the color from each texture
					Color colorA = bitsA[(x - a.Bounds.X) + (y - a.Bounds.Y) * a.Texture.Width];
					Color colorB = bitsB[(x - b.Bounds.X) + (y - b.Bounds.Y) * b.Texture.Width];

					// If both colors are not transparent (the alpha channel is not 0), then there is a collision
					if (colorA.A != 0 && colorB.A != 0)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
