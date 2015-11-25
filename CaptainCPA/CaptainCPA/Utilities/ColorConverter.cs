using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CaptainCPA
{
	public static class ColorConverter
	{
		public static Dictionary<string, Color> TileColors = new Dictionary<string, Color>()
		{
			{ "White", Color.White },
			{ "Red", Color.Red },
			{ "Green", Color.Green },
			{ "Blue", Color.Blue },
			{ "LightRed", Color.PaleVioletRed },
			{ "LightGreen", Color.LightGreen },
			{ "LightBlue", Color.LightBlue }
		};

		public static Color ConvertColor(string input)
		{
			try
			{
				return TileColors[input];
			}
			catch (Exception)
			{
				return Color.White;
			}
		}
	}
}
