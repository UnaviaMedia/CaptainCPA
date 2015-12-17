/*
 * Project: CaptainCPA - HighScore.cs
 * Purpose: Data structure for player highscores
 *
 * History:
 *		Kendall Roth	Dec-10-2015:	Created
 */


namespace CaptainCPA.Utilities
{
	/// <summary>
	/// Data structure for player highscores
	/// </summary>
	public struct HighScore
	{
		public int Score { get; set; }
		public string Name { get; set; }
	}
}
