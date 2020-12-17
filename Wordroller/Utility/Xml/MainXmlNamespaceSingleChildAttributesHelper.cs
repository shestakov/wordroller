using System;
using System.Xml;
using System.Xml.Linq;

namespace Wordroller.Utility.Xml
{
	internal static class MainXmlNamespaceSingleChildAttributesHelper
	{
		internal static void SetSingleElementAttributeOrRemoveElement(this XElement xml, string element, string attribute, string? value)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));

			if (value == null)
			{
				e?.Remove();
			}
			else
			{
				if (e == null)
				{
					e = new XElement(XName.Get(element, Namespaces.w.NamespaceName));
					xml.Add(e);
				}

				e.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value);
			}
		}

		internal static void SetSingleElementAttributeOrRemoveElementInt(this XElement xml, string element, string attribute, int? value)
		{
			xml.SetSingleElementAttributeOrRemoveElement(element, attribute, value.HasValue ? XmlConvert.ToString(value.Value) : null);
		}

		internal static void SetSingleElementAttributeOrRemoveElementEnum<T>(this XElement xml, string element, string attribute, T? value) where T : struct, Enum
		{
			xml.SetSingleElementAttributeOrRemoveElement(element, attribute, value?.ToCamelCase());
		}

		internal static void SetSingleElementAttributeElementMustExist(this XElement xml, string element, string attribute, string? value)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));

			if (e == null)
			{
				e = new XElement(XName.Get(element, Namespaces.w.NamespaceName));
				xml.Add(e);
			}

			e.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), value);
		}

		internal static void RemoveSingleElement(this XElement xml, string element)
		{
			xml.Element(XName.Get(element, Namespaces.w.NamespaceName))?.Remove();
		}

		internal static string? GetSingleElementAttribute(this XElement xml, string element, string attribute)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			return e?.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName))?.Value;
		}

		internal static int GetSingleElementAttributeInt(this XElement xml, string element, string attribute)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			var a = e?.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName));
			return int.TryParse(a?.Value, out var result) ? result : throw new Exception($"{attribute} of {xml.Name.LocalName} must be integer");
		}

		internal static int? GetSingleElementAttributeIntNullable(this XElement xml, string element, string attribute)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			var a = e?.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName));
			return int.TryParse(a?.Value, out var result) ? result : (int?) null;
		}

		internal static T GetSingleElementAttributeEnum<T>(this XElement xml, string element, string attribute) where T : struct
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			var a = e?.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName));
			return Enum.TryParse<T>(a?.Value, true, out var result)
				? result
				: throw new Exception($"Attribute {attribute} of element {element} is null");
		}

		internal static T? GetSingleElementAttributeEnumNullable<T>(this XElement xml, string element, string attribute) where T : struct
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			var a = e?.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName));
			if (a == null) return null;
			return Enum.TryParse<T>(a.Value, true, out var result) ? result : (T?) null; // TODO: Maybe throw here?
		}

		internal static bool GetSingleElementOnOff(this XElement xml, string element, string attribute)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			if (e == null) return false;

			var a = e.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName));
			if (a == null) return true;

			return XmlAttributeValueHelper.ParseOnOffValue(a.Value);
		}

		internal static bool? GetSingleElementOnOffNullable(this XElement xml, string element, string attribute, bool defaultValue)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			if (e == null) return null;

			var a = e.Attribute(XName.Get(attribute, Namespaces.w.NamespaceName));
			if (a == null) return defaultValue;

			return XmlAttributeValueHelper.ParseOnOffValue(a.Value);
		}

		internal static bool? GetSingleElementAttributeBoolNullable(this XElement xml, string element, string attribute, bool defaultValueIfAttributeIsAbsent)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));
			if (e == null) return null;
			var value = e.GetOwnAttributeBoolNullable(attribute) ?? defaultValueIfAttributeIsAbsent;
			return value;
		}

		internal static void SetSingleElementOnOffNullable(this XElement xml, string element, string attribute, bool? value)
		{
			var e = xml.Element(XName.Get(element, Namespaces.w.NamespaceName));

			if (value == null)
			{
				e?.Remove();
			}
			else
			{
				if (e == null)
				{
					e = new XElement(XName.Get(element, Namespaces.w.NamespaceName));
					xml.Add(e);
				}

				if (value == false)
				{
					e.SetAttributeValue(XName.Get(attribute, Namespaces.w.NamespaceName), "0");
				}
			}
		}
	}
}