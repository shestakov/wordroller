using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Wordroller.Utility.Xml
{
	public static class MainXmlNamespaceOwnAttributesHelper
	{
		private static readonly Regex validColor = new Regex("^[0-9a-fA-F]{6}$");
		private static readonly Regex doubleHex = new Regex("^[0-9a-fA-F]{2}$");
		private static readonly Regex quadrupleHex = new Regex("^[0-9a-fA-F]{2}$");

		internal static string? GetOwnAttribute(this XElement? element, string attribute)
		{
			return element?.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName))?.Value;
		}

		internal static int? GetOwnAttributeIntNullable(this XElement? element, string attribute)
		{
			var value = element.GetOwnAttribute(attribute);
			if (value == null) return null;
			return int.TryParse(value, out var result) ? result : (int?) null;
		}

		internal static int GetOwnAttributeInt(this XElement? element, string attribute)
		{
			var value = element.GetOwnAttribute(attribute);
			return int.TryParse(value, out var result) ? result : throw new Exception($"XML element must exist and attribute {attribute} must be integer");
		}

		internal static bool GetOwnAttributeBool(this XElement? element, string attribute, bool defaultValue)
		{
			return element.GetOwnAttributeBoolNullable(attribute) ?? defaultValue;
		}

		internal static bool? GetOwnAttributeBoolNullable(this XElement? element, string attribute)
		{
			var value = element.GetOwnAttribute(attribute);

			if (value == null) return null;

			return XmlAttributeValueHelper.ParseOnOffValue(value);
		}

		internal static T? GetOwnAttributeEnumNullable<T>(this XElement? element, string attribute) where T : struct, Enum
		{
			var value = element.GetOwnAttribute(attribute);
			return Enum.TryParse<T>(value, true, out var result) ? result : (T?) null;
		}

		internal static T GetOwnAttributeEnum<T>(this XElement? element, string attribute) where T : struct, Enum
		{
			var value = element.GetOwnAttribute(attribute);
			return Enum.TryParse<T>(value, true, out var result) ? result : throw new Exception($"Element does not have an attribute named {attribute}");
		}

		internal static void SetOwnAttributeString(this XElement element, string attribute, string? value)
		{
			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value);
		}

		internal static void SetOwnAttributeEnumNullable<T>(this XElement element, string attribute, T? value) where T : struct, Enum
		{
			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value?.ToCamelCase());
		}

		internal static void SetOwnAttributeEnum<T>(this XElement element, string attribute, T value) where T : Enum
		{
			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value.ToCamelCase());
		}

		internal static void SetOwnAttributeIntNullable(this XElement element, string attribute, int? value)
		{
			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value?.ToString("D"));
		}

		internal static void SetOwnAttributeInt(this XElement element, string attribute, int value)
		{
			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value.ToString("D"));
		}

		internal static void SetOwnAttributeOnOffNullable(this XElement element, string attribute, bool? value)
		{
			var attributeValue = value switch
			{
				null => null,
				true => "1",
				false => "0"
			};

			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), attributeValue);
		}

		internal static void SetOwnAttributeOnOffNullIsOff(this XElement element, string attribute, bool value)
		{
			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value ? "1" : null);
		}

		internal static void SetOwnAttributeColor(this XElement element, string attribute, string? value)
		{
			if (value != null
				&& !value.Equals("auto", StringComparison.InvariantCultureIgnoreCase)
				&& !validColor.IsMatch(value))
				throw new ArgumentOutOfRangeException(nameof(value), value,
					"Value must be a color in hexadecimal format RRGGBB, or auto, or null");

			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value);
		}

		internal static void SetOwnAttributeDoubleHex(this XElement element, string attribute, string? value)
		{
			if (value != null && !doubleHex.IsMatch(value))
				throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be a 2 digit hexadecimal number like 0F, or null");

			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value);
		}

		internal static void SetOwnAttributeQuadrupleHex(this XElement element, string attribute, string? value)
		{
			if (value != null && !quadrupleHex.IsMatch(value))
				throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be a 4 digit hexadecimal number like 0FFF, or null");

			element.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value);
		}
	}
}