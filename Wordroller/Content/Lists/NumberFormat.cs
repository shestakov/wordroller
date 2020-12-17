namespace Wordroller.Content.Lists
{
	public enum NumberFormat
	{
		Bullet,
		CardinalText, // The cardinal text of the run language. (In English, One, Two, Three, etc.)
		Chicago, // Set of symbols from the Chicago Manual of Style. (e.g., *, †, ‡, §)
		Decimal, // Decimal numbering (1, 2, 3, 4, etc.)
		DecimalEnclosedCircle, // Decimal number enclosed in a circle
		DecimalEnclosedFullstop, // Decimal number followed by a period
		DecimalEnclosedParen, // Decimal number enclosed in parentheses
		DecimalZero, // Decimal number but with a zero added to numbers 1 through 9
		LowerLetter, // Based on the run language (e.g., a, b, c, etc.). Letters repeat for values greater than the size of the alphabet
		LowerRoman, // Lowercase Roman numerals (i, ii, iii, iv, etc.)
		None,
		OrdinalText, // Ordinal text of the run language. (In English, First, Second, Third, etc.)
		UpperLetter, // Based on the run language (e.g., A, B, C, etc.). Letters repeat for values greater than the size of the alphabet
		UpperRoman // Uppercase Roman numerals (I, II, III, IV, etc.)
	}
}