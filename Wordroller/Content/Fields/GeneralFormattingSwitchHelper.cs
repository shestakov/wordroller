using System;
using Wordroller.Content.Lists;

namespace Wordroller.Content.Fields
{
	public static class GeneralFormattingSwitchHelper
	{
		public static string GetSwitch(CommonFieldGeneralFormatting formatting)
		{
			return formatting switch
			{
				CommonFieldGeneralFormatting.AlphabeticUppercase => GeneralFormattingSwitches.ALPHABETIC,
				CommonFieldGeneralFormatting.AlphabeticLowercase => GeneralFormattingSwitches.alphabetic,
				CommonFieldGeneralFormatting.Capitalized => GeneralFormattingSwitches.Caps,
				CommonFieldGeneralFormatting.FirstLetterCapitalized => GeneralFormattingSwitches.FirstCap,
				CommonFieldGeneralFormatting.Lowercase => GeneralFormattingSwitches.Lower,
				CommonFieldGeneralFormatting.Uppercase => GeneralFormattingSwitches.Upper,
				CommonFieldGeneralFormatting.DollarText => GeneralFormattingSwitches.DollarText,
				_ => throw new ArgumentOutOfRangeException(nameof(formatting), formatting, null)
			};
		}

		public static string GetSwitch(NumberFormat numberFormat)
		{
			return numberFormat switch
			{
				NumberFormat.Aiueo => GeneralFormattingSwitches.AIUEO,
				NumberFormat.Decimal => GeneralFormattingSwitches.Arabic,
				NumberFormat.ArabicAbjad => GeneralFormattingSwitches.ARABICABJAD,
				NumberFormat.ArabicAlpha => GeneralFormattingSwitches.ARABICALPHA,
				NumberFormat.NumberInDash => GeneralFormattingSwitches.ArabicDash,
				NumberFormat.CardinalText => GeneralFormattingSwitches.CardText,
				NumberFormat.ChineseCounting => GeneralFormattingSwitches.CHINESENUM1,
				NumberFormat.ChineseLegalSimplified => GeneralFormattingSwitches.CHINESENUM2,
				NumberFormat.ChineseCountingThousand => GeneralFormattingSwitches.CHINESENUM3,
				NumberFormat.Chosung => GeneralFormattingSwitches.CHOSUNG,
				NumberFormat.DecimalEnclosedCircle => GeneralFormattingSwitches.CIRCLENUM,
				NumberFormat.DecimalFullWidth => GeneralFormattingSwitches.DBCHAR,
				NumberFormat.IdeographDigital => GeneralFormattingSwitches.DBNUM1,
				NumberFormat.KoreanCounting => GeneralFormattingSwitches.DBNUM2,
				NumberFormat.JapaneseLegal => GeneralFormattingSwitches.DBNUM3,
				NumberFormat.JapaneseDigitalTenThousand => GeneralFormattingSwitches.DBNUM4,
				NumberFormat.Ganada => GeneralFormattingSwitches.GANADA,
				NumberFormat.DecimalEnclosedFullstop => GeneralFormattingSwitches.GB1,
				NumberFormat.DecimalEnclosedParen => GeneralFormattingSwitches.GB2,
				NumberFormat.DecimalEnclosedCircleChinese => GeneralFormattingSwitches.GB3,
				NumberFormat.IdeographEnclosedCircle => GeneralFormattingSwitches.GB4,
				NumberFormat.Hebrew1 => GeneralFormattingSwitches.HEBREW1,
				NumberFormat.Hebrew2 => GeneralFormattingSwitches.HEBREW2,
				NumberFormat.Hex => GeneralFormattingSwitches.Hex,
				NumberFormat.HindiNumbers => GeneralFormattingSwitches.HINDIARABIC,
				NumberFormat.HindiCounting => GeneralFormattingSwitches.HINDICARDTEXT,
				NumberFormat.HindiVowels => GeneralFormattingSwitches.HINDILETTER1,
				NumberFormat.HindiConsonants => GeneralFormattingSwitches.HINDILETTER2,
				NumberFormat.Iroha => GeneralFormattingSwitches.IROHA,
				NumberFormat.JapaneseCounting => GeneralFormattingSwitches.KANJINUM2,
				NumberFormat.Ordinal => GeneralFormattingSwitches.Ordinal,
				NumberFormat.OrdinalText => GeneralFormattingSwitches.OrdText,
				NumberFormat.UpperRoman => GeneralFormattingSwitches.Roman,
				NumberFormat.LowerRoman => GeneralFormattingSwitches.roman,
				NumberFormat.DecimalHalfWidth => GeneralFormattingSwitches.SBCHAR,
				NumberFormat.ThaiNumbers => GeneralFormattingSwitches.THAIARABIC,
				NumberFormat.ThaiCounting => GeneralFormattingSwitches.THAICARDTEXT,
				NumberFormat.ThaiLetters => GeneralFormattingSwitches.THAILETTER,
				NumberFormat.VietnameseCounting => GeneralFormattingSwitches.VIETCARDTEXT,
				NumberFormat.IdeographTraditional => GeneralFormattingSwitches.ZODIAC1,
				NumberFormat.IdeographZodiac => GeneralFormattingSwitches.ZODIAC2,
				NumberFormat.IdeographZodiacTraditional => GeneralFormattingSwitches.ZODIAC3,
				// NumberFormat.None => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.Bullet => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.DecimalZero => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.LowerLetter => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.UpperLetter => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.AiueoFullWidth => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.Chicago => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.DecimalFullWidth2 => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.IdeographLegalTraditional => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.IrohaFullWidth => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.KoreanDigital => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.KoreanDigital2 => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.KoreanLegal => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.RussianLower => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.RussianUpper => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.TaiwaneseCounting => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.TaiwaneseCountingThousand => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				// NumberFormat.TaiwaneseDigital => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null),
				_ => throw new ArgumentOutOfRangeException(nameof(numberFormat), numberFormat, null)
			};
		}

		public static string GetSwitch(SpecialEastAsiaFieldGeneralFormatting formatting)
		{
			return formatting switch
			{
				SpecialEastAsiaFieldGeneralFormatting.BAHTTEXT => GeneralFormattingSwitches.BAHTTEXT,
				SpecialEastAsiaFieldGeneralFormatting.KANJINUM1 => GeneralFormattingSwitches.KANJINUM1,
				SpecialEastAsiaFieldGeneralFormatting.KANJINUM2 => GeneralFormattingSwitches.KANJINUM2,
				SpecialEastAsiaFieldGeneralFormatting.KANJINUM3 => GeneralFormattingSwitches.KANJINUM3,
				_ => throw new ArgumentOutOfRangeException(nameof(formatting), formatting, null)
			};
		}
	}
}