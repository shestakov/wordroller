using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Tables.Widths
{
	internal static class TableWidthElementXmlExtensions
	{
		internal static int? GetTableWidthTwValue(this XElement? xml, string name)
		{
			var (hasValue, unit, value) = GetTableWidthAttributeValue(xml, name);
			if (hasValue == false) return null;

			return unit switch
			{
				TableWidthUnit.Auto => null,
				TableWidthUnit.Pct => null,
				TableWidthUnit.Nil => 0,
				_ => value
			};
		}

		internal static int? GetTableWidthFpValue(this XElement? xml, string name)
		{
			var (hasValue, unit, value) = GetTableWidthAttributeValue(xml, name);
			if (hasValue == false) return null;

			return unit switch
			{
				TableWidthUnit.Auto => null,
				TableWidthUnit.Dxa => null,
				TableWidthUnit.Nil => 0,
				_ => value
			};
		}

		private static (bool, TableWidthUnit, int?) GetTableWidthAttributeValue(this XElement? xml, string name)
		{
			var element = xml?.Element(XName.Get(name, Namespaces.w.NamespaceName));
			if (element == null) return (false, TableWidthUnit.Dxa, null);
			var width = int.TryParse(element.Attribute(XName.Get("w", Namespaces.w.NamespaceName))?.Value, out var widthValue)
				? widthValue
				: (int?) null;
			var unit = Enum.TryParse<TableWidthUnit>(element.Attribute(XName.Get("type", Namespaces.w.NamespaceName))?.Value,
				out var widthType)
				? widthType
				: TableWidthUnit.Dxa;
			return (true, unit, width);
		}

		internal static void SetTableWidthTwValue(this XElement xml, string name, int? value)
		{
			if (value == null)
				SetTableWidthAttributes(xml, name, null, TableWidthUnit.Auto);
			else if (value == 0)
				SetTableWidthAttributes(xml, name, 0, TableWidthUnit.Nil);
			else
				SetTableWidthAttributes(xml, name, value, TableWidthUnit.Dxa);
		}

		internal static void SetTableWidthFpValue(this XElement xml, string name, int? value)
		{
			if (value == null)
				SetTableWidthAttributes(xml, name, null, TableWidthUnit.Auto);
			else if (value == 0)
				SetTableWidthAttributes(xml, name, 0, TableWidthUnit.Nil);
			else
				SetTableWidthAttributes(xml, name, value, TableWidthUnit.Pct);
		}

		private static void SetTableWidthAttributes(this XElement xml, string name, int? value, TableWidthUnit unit)
		{
			var xName = XName.Get(name, Namespaces.w.NamespaceName);
			var element = xml.Element(xName);

			if (element == null)
			{
				element = new XElement(xName);
				xml.Add(element);
			}

			element.SetAttributeValue(XName.Get("w", Namespaces.w.NamespaceName), value);
			element.SetAttributeValue(XName.Get("type", Namespaces.w.NamespaceName), unit.ToCamelCase());
		}
	}
}