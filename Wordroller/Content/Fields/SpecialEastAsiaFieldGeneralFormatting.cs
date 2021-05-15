using System.Diagnostics.CodeAnalysis;
using Wordroller.Content.Lists;

namespace Wordroller.Content.Fields
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	[SuppressMessage("ReSharper", "IdentifierTypo")]
	public enum SpecialEastAsiaFieldGeneralFormatting
	{
		/// <summary>
		/// Formats a numeric result using the given Thai style.
		/// </summary>
		BAHTTEXT,

		/// <summary>
		/// Formats a numeric result using a Japanese style using sequential digital ideographs, using the appropriate character.
		/// </summary>
		KANJINUM1,

		/// <summary>
		/// Formats a numeric result using the Japanese counting system. Equals to <see cref="NumberFormat.JapaneseCounting"/>
		/// </summary>
		KANJINUM2,

		/// <summary>
		/// Formats a numeric result using the Japanese legal counting system.
		/// </summary>
		KANJINUM3
	}
}