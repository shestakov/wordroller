namespace Wordroller.Content.Lists
{
	/// <summary>
	/// ST_NumberFormat
	/// </summary>
	public enum NumberFormat
	{
		/// <summary>
		/// No Numbering
		/// </summary>
		None,

		/// <summary>
		/// Bullet
		/// </summary>
		Bullet,

		/// <summary>
		/// Hexadecimal Numbering
		/// </summary>
		Hex,

		/// <summary>
		/// Decimal Numbers (1, 2, 3, 4, etc.)
		/// </summary>
		Decimal,

		/// <summary>
		/// Initial Zero Arabic Numerals (a zero added to numbers 1 through 9)
		/// </summary>
		DecimalZero,

		/// <summary>
		/// Lowercase Letter (based on the run language (e.g., a, b, c, etc.). Letters repeat for values greater than the size of the alphabet)
		/// </summary>
		LowerLetter,

		/// <summary>
		/// Uppercase Letter (based on the run language (e.g., a, b, c, etc.). Letters repeat for values greater than the size of the alphabet)
		/// </summary>
		UpperLetter,

		/// <summary>
		/// Lowercase Roman Numerals (i, ii, iii, iv, etc.)
		/// </summary>
		LowerRoman,

		/// <summary>
		/// Uppercase Roman Numerals (I, II, III, IV, etc.)
		/// </summary>
		UpperRoman,

		/// <summary>
		/// Cardinal Text (of the run language, in English: One, Two, Three, etc.)
		/// </summary>
		CardinalText,

		/// <summary>
		/// Ordinal Text (of the run language, in English: One, Two, Three, etc.)
		/// </summary>
		OrdinalText,


		/// <summary>
		/// AIUEO Order Hiragana
		/// </summary>
		Aiueo,

		/// <summary>
		/// Full-Width AIUEO Order Hiragana
		/// </summary>
		AiueoFullWidth,

		/// <summary>
		/// Arabic Abjad Numerals
		/// </summary>
		ArabicAbjad,

		/// <summary>
		/// Arabic Alphabet
		/// </summary>
		ArabicAlpha,

		/// <summary>
		/// Chicago Manual of Style (e.g., *, †, ‡, §)
		/// </summary>
		Chicago,

		/// <summary>
		/// Chinese Counting System
		/// </summary>
		ChineseCounting,

		/// <summary>
		/// Chinese Counting Thousand System
		/// </summary>
		ChineseCountingThousand,

		/// <summary>
		/// Chinese Legal Simplified Format
		/// </summary>
		ChineseLegalSimplified,

		/// <summary>
		/// Korean Chosung Numbering
		/// </summary>
		Chosung,

		/// <summary>
		/// Decimal Numbers Enclosed in a Circle
		/// </summary>
		DecimalEnclosedCircle,

		/// <summary>
		/// Decimal Numbers Enclosed in a Circle
		/// </summary>
		DecimalEnclosedCircleChinese,

		/// <summary>
		/// Decimal Numbers Followed by a Period
		/// </summary>
		DecimalEnclosedFullstop,

		/// <summary>
		/// Decimal Numbers Enclosed in Parenthesis
		/// </summary>
		DecimalEnclosedParen,

		/// <summary>
		/// Double Byte Arabic Numerals
		/// </summary>
		DecimalFullWidth,

		/// <summary>
		/// Double Byte Arabic Numerals Alternate
		/// </summary>
		DecimalFullWidth2,

		/// <summary>
		/// Single Byte Arabic Numerals
		/// </summary>
		DecimalHalfWidth,

		/// <summary>
		/// Korean Ganada Numbering
		/// </summary>
		Ganada,

		/// <summary>
		/// Hebrew Numerals
		/// </summary>
		Hebrew1,

		/// <summary>
		/// Hebrew Alphabet
		/// </summary>
		Hebrew2,

		/// <summary>
		/// Hindi Consonants
		/// </summary>
		HindiConsonants,

		/// <summary>
		/// Hindi Counting System
		/// </summary>
		HindiCounting,

		/// <summary>
		/// Hindi Numbers
		/// </summary>
		HindiNumbers,

		/// <summary>
		/// Hindi Vowels
		/// </summary>
		HindiVowels,

		/// <summary>
		/// Ideographs
		/// </summary>
		IdeographDigital,

		/// <summary>
		/// Ideographs Enclosed in a Circle
		/// </summary>
		IdeographEnclosedCircle,

		/// <summary>
		/// Traditional Legal Ideograph Format
		/// </summary>
		IdeographLegalTraditional,

		/// <summary>
		/// Traditional Ideograph Format
		/// </summary>
		IdeographTraditional,

		/// <summary>
		/// Zodiac Ideograph Format
		/// </summary>
		IdeographZodiac,

		/// <summary>
		/// Traditional Zodiac Ideograph Format
		/// </summary>
		IdeographZodiacTraditional,

		/// <summary>
		/// Iroha Ordered Katakana
		/// </summary>
		Iroha,

		/// <summary>
		/// Full-Width Iroha Ordered Katakana
		/// </summary>
		IrohaFullWidth,

		/// <summary>
		/// Japanese Counting System
		/// </summary>
		JapaneseCounting,

		/// <summary>
		/// Japanese Digital Ten Thousand Counting System
		/// </summary>
		JapaneseDigitalTenThousand,

		/// <summary>
		/// Japanese Legal Numbering
		/// </summary>
		JapaneseLegal,

		/// <summary>
		/// Korean Counting System
		/// </summary>
		KoreanCounting,

		/// <summary>
		/// Korean Digital Counting System
		/// </summary>
		KoreanDigital,

		/// <summary>
		/// Korean Digital Counting System Alternate
		/// </summary>
		KoreanDigital2,

		/// <summary>
		/// Korean Legal Numbering
		/// </summary>
		KoreanLegal,

		/// <summary>
		/// Number With Dashes
		/// </summary>
		NumberInDash,

		/// <summary>
		/// Ordinal
		/// </summary>
		Ordinal,

		/// <summary>
		/// Lowercase Russian Alphabet
		/// </summary>
		RussianLower,

		/// <summary>
		/// Uppercase Russian Alphabet
		/// </summary>
		RussianUpper,

		/// <summary>
		/// Taiwanese Counting System
		/// </summary>
		TaiwaneseCounting,

		/// <summary>
		/// Taiwanese Counting Thousand System
		/// </summary>
		TaiwaneseCountingThousand,

		/// <summary>
		/// Taiwanese Digital Counting System
		/// </summary>
		TaiwaneseDigital,

		/// <summary>
		/// Thai Counting System
		/// </summary>
		ThaiCounting,

		/// <summary>
		/// Thai Letters
		/// </summary>
		ThaiLetters,

		/// <summary>
		/// Thai Numerals
		/// </summary>
		ThaiNumbers,

		/// <summary>
		/// Vietnamese Numerals
		/// </summary>
		VietnameseCounting
	}
}