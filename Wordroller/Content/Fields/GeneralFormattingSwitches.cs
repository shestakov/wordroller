using System.Diagnostics.CodeAnalysis;

namespace Wordroller.Content.Fields
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	[SuppressMessage("ReSharper", "IdentifierTypo")]
	public static class GeneralFormattingSwitches
	{
		/// <summary>
		/// Formats a numeric result as one or more occurrences of an uppercase alphabetic Latin character. Value 1 results in the letter A, value 2 results in the letter B, and so on up to value 26, which results in the letter Z. For values greater than 26, 26 is repeatedly subtracted from the value until the result is 26 or less. The result value determines which letter to use, and the same letter is repeated for each time 26 was subtracted from the original value.
		/// </summary>
		public const string ALPHABETIC = "ALPHABETIC";

		/// <summary>
		/// Formats a numeric result as one or more occurrences of an lowercase alphabetic Latin character. Value 1 results in the letter a, value 2 results in the letter b, and so on up to value 26, which results in the letter z. For values greater than 26, 26 is repeatedly subtracted from the value until the result is 26 or less. The result value determines which letter to use, and the same letter is repeated for each time 26 was subtracted from the original value.
		/// </summary>
		public const string alphabetic = "alphabetic";

		/// <summary>
		/// Capitalizes the first letter of each word.
		/// </summary>
		public const string Caps = "Caps";

		//Cap Capitalizes the first letter of the first word.
		public const string FirstCap = "FirstCap";

		/// <summary>
		/// All letters are lowercase.
		/// </summary>
		public const string Lower = "Lower";

		/// <summary>
		/// All letters are uppercase.
		/// </summary>
		public const string Upper = "Upper";

		//Text Formats a numeric result in the following form: integer-part-as-cardinal-text and nn/100 The fractional part is rounded to two decimal places, nn, and is formatted using Arabic cardinal numerals.
		public const string DollarText = "DollarText";

		/// <summary>
		/// See the discussion following this table.
		/// </summary>
		public const string CHARFORMAT = "CHARFORMAT";

		/// <summary>
		/// See the discussion following this table.
		/// </summary>
		public const string MERGEFORMAT = "MERGEFORMAT";

		/// <summary>
		/// Formats a numeric result using the given Thai style.
		/// </summary>
		public const string BAHTTEXT = "BAHTTEXT";

		/// <summary>
		/// Formats a numeric result using a Japanese style using sequential digital ideographs, using the appropriate character.
		/// </summary>
		public const string KANJINUM1 = "KANJINUM1";

		/// <summary>
		/// Formats a numeric result using the Japanese legal counting system.
		/// </summary>
		public const string KANJINUM3 = "KANJINUM3";

		/// <summary>
		/// Formats a numeric result using hiragana characters in the traditional a-i-u-e-o order. Corresponds to an ST_NumberFormat enumeration value of aiueo.
		/// </summary>
		public const string AIUEO = "AIUEO";

		/// <summary>
		/// Formats a numeric result using Arabic cardinal numerals. Corresponds to an ST_NumberFormat enumeration value of decimal.
		/// </summary>
		public const string Arabic = "Arabic";

		/// <summary>
		/// Formats a numeric result using ascending Abjad numerals. Corresponds to an ST_NumberFormat enumeration value of arabicAbjad.
		/// </summary>
		public const string ARABICABJAD = "ARABICABJAD";

		/// <summary>
		/// Formats a numeric result using characters in the Arabic alphabet. Corresponds to an ST_NumberFormat enumeration value of arabicAlpha.
		/// </summary>
		public const string ARABICALPHA = "ARABICALPHA";

		/// <summary>
		/// Formats a numeric result using Arabic cardinal numerals, with a prefix of "- " and a suffix of " -". Corresponds to an ST_NumberFormat enumeration value of numberInDash.
		/// </summary>
		public const string ArabicDash = "ArabicDash";

		/// <summary>
		/// Formats a numeric result as lowercase cardinal text. Corresponds to an ST_NumberFormat enumeration value of cardinalText.
		/// </summary>
		public const string CardText = "CardText";

		/// <summary>
		/// Formats a numeric result using ascending numbers from the Chinese counting system. Corresponds to an ST_NumberFormat enumeration value of chineseCounting.
		/// </summary>
		public const string CHINESENUM1 = "CHINESENUM1";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Chinese simplified legal format. Corresponds to an ST_NumberFormat enumeration value of chineseLegalSimplified.
		/// </summary>
		public const string CHINESENUM2 = "CHINESENUM2";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Chinese counting thousand system. Corresponds to an ST_NumberFormat enumeration value of chineseCountingThousand.
		/// </summary>
		public const string CHINESENUM3 = "CHINESENUM3";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Korean Chosung format. Corresponds to an ST_NumberFormat enumeration value of chosung.
		/// </summary>
		public const string CHOSUNG = "CHOSUNG";

		/// <summary>
		/// Formats a numeric result using decimal numbering enclosed in a circle, using the enclosed alphanumeric glyph character for numbers in the range 1–20. For non-negative numbers outside this range, formats them as with ARABIC. Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedCircle.
		/// </summary>
		public const string CIRCLENUM = "CIRCLENUM";

		/// <summary>
		/// Formats a numeric result using double-byte Arabic numbering. Corresponds to an ST_NumberFormat enumeration value of decimalFullWidth.
		/// </summary>
		public const string DBCHAR = "DBCHAR";

		/// <summary>
		/// Formats a numeric result using sequential digital ideographs, using the appropriate character. Corresponds to an ST_NumberFormat enumeration value of ideographDigital.
		/// </summary>
		public const string DBNUM1 = "DBNUM1";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Korean counting system. Corresponds to an ST_NumberFormat enumeration value of koreanCounting.
		/// </summary>
		public const string DBNUM2 = "DBNUM2";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Japanese legal counting system. Corresponds to an ST_NumberFormat enumeration value of japaneseLegal.
		/// </summary>
		public const string DBNUM3 = "DBNUM3";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Japanese digital ten thousand counting system. Corresponds to an ST_NumberFormat enumeration value of japaneseDigitalTenThousand.
		/// </summary>
		public const string DBNUM4 = "DBNUM4";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Korean Ganada format. Corresponds to an ST_NumberFormat enumeration value of ganada.
		/// </summary>
		public const string GANADA = "GANADA";

		/// <summary>
		/// Formats a numeric result using decimal numbering followed by a period, using the enclosed alphanumeric glyph character. Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedFullstop.
		/// </summary>
		public const string GB1 = "GB1";

		/// <summary>
		/// Formats a numeric result using decimal numbering enclosed in parenthesis, using the enclosed alphanumeric glyph character. Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedParen.
		/// </summary>
		public const string GB2 = "GB2";

		/// <summary>
		/// Formats a numeric result using decimal numbering enclosed in a circle, using the enclosed alphanumeric glyph character. Once the specified sequence reaches 11, the numbers may be replaced with non-enclosed equivalents. Corresponds to an ST_NumberFormat enumeration value of decimalEnclosedCircleChinese.
		/// </summary>
		public const string GB3 = "GB3";

		/// <summary>
		/// Formats a numeric result using decimal numbering enclosed in a circle, using the enclosed alphanumeric glyph character. Once the specified sequence reaches 11, the numbers may be replaced with non-enclosed equivalents. Corresponds to an ST_NumberFormat enumeration value of ideographEnclosedCircle.
		/// </summary>
		public const string GB4 = "GB4";

		/// <summary>
		/// Formats a numeric result using Hebrew numerals. Corresponds to an ST_NumberFormat enumeration value of hebrew1.
		/// </summary>
		public const string HEBREW1 = "HEBREW1";

		/// <summary>
		/// Formats a numeric result using the Hebrew alphabet. Corresponds to an ST_NumberFormat enumeration value of hebrew2.
		/// </summary>
		public const string HEBREW2 = "HEBREW2";

		/// <summary>
		/// Formats the numeric result using uppercase hexadecimal digits. Corresponds to an ST_NumberFormat enumeration value of hex.
		/// </summary>
		public const string Hex = "Hex";

		/// <summary>
		/// Formats a numeric result using Hindi numbers. Corresponds to an ST_NumberFormat enumeration value of hindiNumbers.
		/// </summary>
		public const string HINDIARABIC = "HINDIARABIC";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Hindi counting system. Corresponds to an ST_NumberFormat enumeration value of hindiCounting.
		/// </summary>
		public const string HINDICARDTEXT = "HINDICARDTEXT";

		/// <summary>
		/// Formats a numeric result using Hindi vowels. Corresponds to an ST_NumberFormat enumeration value of hindiVowels.
		/// </summary>
		public const string HINDILETTER1 = "HINDILETTER1";

		/// <summary>
		/// Formats a numeric result using Hindi consonants. Corresponds to an ST_NumberFormat enumeration value of hindiConsonants.
		/// </summary>
		public const string HINDILETTER2 = "HINDILETTER2";

		/// <summary>
		/// Formats a numeric result using the Japanese iroha. Corresponds to an ST_NumberFormat enumeration value of iroha.
		/// </summary>
		public const string IROHA = "IROHA";

		/// <summary>
		/// Formats a numeric result using the Japanese counting system. Corresponds to an ST_NumberFormat enumeration value of japaneseCounting.
		/// </summary>
		public const string KANJINUM2 = "KANJINUM2";

		/// <summary>
		/// Formats a numeric result using lowercase ordinal Arabic numerals. Corresponds to an ST_NumberFormat enumeration value of ordinal.
		/// </summary>
		public const string Ordinal = "Ordinal";

		/// <summary>
		/// Formats a numeric result as lowercase ordinal text. Apart from being used to round off the whole number part, the fractional part is not used. Corresponds to an ST_NumberFormat enumeration value of ordinalText.
		/// </summary>
		public const string OrdText = "OrdText";

		/// <summary>
		/// Formats a numeric result using uppercase Roman numerals. Corresponds to an ST_NumberFormat enumeration value of upperRoman.
		/// </summary>
		public const string Roman = "Roman";

		/// <summary>
		/// Formats a numeric result using lowercase Roman numerals. Corresponds to an ST_NumberFormat enumeration value of lowerRoman.
		/// </summary>
		public const string roman = "roman";

		/// <summary>
		/// Formats a numeric result using single-byte Arabic numbering. Corresponds to an ST_NumberFormat enumeration value of decimalHalfWidth.
		/// </summary>
		public const string SBCHAR = "SBCHAR";

		/// <summary>
		/// Formats a numeric result using Thai numbers. Corresponds to an ST_NumberFormat enumeration value of thaiNumbers.
		/// </summary>
		public const string THAIARABIC = "THAIARABIC";

		/// <summary>
		/// Formats a numeric result using sequential numbers from the Thai counting system. Corresponds to an ST_NumberFormat enumeration value of thaiCounting.
		/// </summary>
		public const string THAICARDTEXT = "THAICARDTEXT";

		/// <summary>
		/// Formats a numeric result using Thai letters. Corresponds to an ST_NumberFormat enumeration value of thaiLetters.
		/// </summary>
		public const string THAILETTER = "THAILETTER";

		/// <summary>
		/// Formats a numeric result using Vietnamese numerals. Corresponds to an ST_NumberFormat enumeration value of vietnameseCounting.
		/// </summary>
		public const string VIETCARDTEXT = "VIETCARDTEXT";

		/// <summary>
		/// Formats a numeric result using sequential numerical traditional ideographs. Corresponds to an ST_NumberFormat enumeration value of ideographTraditional.
		/// </summary>
		public const string ZODIAC1 = "ZODIAC1";

		/// <summary>
		/// Formats a numeric result using sequential zodiac ideographs. Corresponds to an ST_NumberFormat enumeration value of ideographZodiac.
		/// </summary>
		public const string ZODIAC2 = "ZODIAC2";

		/// <summary>
		/// Formats a numeric result using sequential traditional zodiac ideographs. Corresponds to an ST_NumberFormat enumeration value of ideographZodiacTraditional.
		/// </summary>
		public const string ZODIAC3 = "ZODIAC3";
	}
}