using System.Diagnostics.CodeAnalysis;

namespace Wordroller.Content.Properties.Borders
{
	/// <summary>
	///     Line border style
	/// </summary>
	/// <remarks>
	///     Enum value for all elements but None and Nil is weight used to resolve border conflicts
	/// </remarks>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public enum LineBorderStyle
	{
		None = 0,
		Nil,
		Single = 1,
		Thick = 2,
		Double = 3,
		Dotted = 4,
		Dashed = 5,
		DotDash = 6,
		DotDotDash = 7,
		Triple = 8,
		ThinThickSmallGap = 9,
		ThickThinSmallGap = 10,
		ThinThickThinSmallGap = 11,
		ThinThickMediumGap = 12,
		ThickThinMediumGap = 13,
		ThinThickThinMediumGap = 14,
		ThinThickLargeGap = 15,
		ThickThinLargeGap = 16,
		ThinThickThinLargeGap = 17,
		Wave = 18,
		DoubleWave = 19,
		DashSmallGap = 20,
		DashDotStroked = 21,
		ThreeDEmboss = 22,
		ThreeDEngrave = 23,
		Outset = 24,
		Inset = 25
	}
}