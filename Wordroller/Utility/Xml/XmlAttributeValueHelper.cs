using System;

namespace Wordroller.Utility.Xml
{
	internal static class XmlAttributeValueHelper
	{
		public static string ToCamelCase(this Enum value)
		{
			var str = value.ToString();
			return char.ToLowerInvariant(str[0]) + str.Substring(1);
		}

		public static bool ParseOnOffValue(string attributeValue)
		{
			var normalizedOnOffValue = attributeValue
				.Replace("on", "true")
				.Replace("1", "true")
				.Replace("off", "false")
				.Replace("0", "false");

			return bool.TryParse(normalizedOnOffValue, out var result)
				? result
				: throw new ArgumentOutOfRangeException(nameof(attributeValue), attributeValue, "Unsupported OnOff attribute value");
		}
	}
}