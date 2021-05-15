namespace Wordroller.Content.Fields
{
	public enum CommonFieldGeneralFormatting
	{
		/// <summary>
		/// Formats a numeric result as one or more occurrences of an uppercase alphabetic Latin character. Value 1 results in the letter A, value 2 results in the letter B, and so on up to value 26, which results in the letter Z. For values greater than 26, 26 is repeatedly subtracted from the value until the result is 26 or less. The result value determines which letter to use, and the same letter is repeated for each time 26 was subtracted from the original value.
		/// </summary>
		AlphabeticUppercase,

		/// <summary>
		/// Formats a numeric result as one or more occurrences of an lowercase alphabetic Latin character. Value 1 results in the letter a, value 2 results in the letter b, and so on up to value 26, which results in the letter z. For values greater than 26, 26 is repeatedly subtracted from the value until the result is 26 or less. The result value determines which letter to use, and the same letter is repeated for each time 26 was subtracted from the original value.
		/// </summary>
		AlphabeticLowercase,

		/// <summary>
		/// Capitalizes the first letter of each word.
		/// </summary>
		Capitalized,

		/// <summary>
		/// Capitalizes the first letter of the first word.
		/// </summary>
		FirstLetterCapitalized,

		/// <summary>
		/// All letters are lowercase.
		/// </summary>
		Lowercase,

		/// <summary>
		/// All letters are uppercase.
		/// </summary>
		Uppercase,

		/// <summary>
		/// Formats a numeric result in the following form: integer-part-as-cardinal-text and nn/100 The fractional part is rounded to two decimal places, nn, and is formatted using Arabic cardinal numerals.
		/// </summary>
		DollarText
	}
}