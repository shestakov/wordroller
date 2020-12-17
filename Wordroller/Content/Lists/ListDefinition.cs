using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Lists
{
	public class ListDefinition : XmlElementWrapper
	{
		internal ListDefinition(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "abstractNum") throw new ArgumentException($"XML element must be abstractNum but was {xml.Name}", nameof(xml));
		}

		public int AbstractNumId => Xml.GetOwnAttributeIntNullable("abstractNumId") ?? throw new Exception("Attribute abstractNumId must be an integer");
		public MultiLevelType MultiLevelType => Xml.GetSingleElementAttributeEnum<MultiLevelType>("multiLevelType", "val");
		public string? Name => Xml.GetSingleElementAttribute("name", "val");
		public string? NumStyleLink => Xml.GetSingleElementAttribute("numStyleLink", "val");
		public string? StyleLink => Xml.GetSingleElementAttribute("styleLink", "val");
		public string? Nsid => Xml.GetSingleElementAttribute("nsid", "val");

		public IReadOnlyDictionary<int, ListDefinitionLevel> Levels => new ReadOnlyDictionary<int, ListDefinitionLevel>(
			Xml.Elements(XName.Get("lvl", Namespaces.w.NamespaceName))
				.Select(e => new ListDefinitionLevel(e))
				.ToDictionary(l => l.Level)
		);

		public ListDefinitionLevel CreateLevel(int level, int start, NumberFormat numberFormat, ListNumberAlignment numberAlignment, int indentLeftTw, int hangingTw,
			string levelText, bool tentative = true, string? tplc = null)
		{
			var xml = new XElement(Namespaces.w + "lvl",
				new XAttribute(Namespaces.w + "ilvl", level),
				new XAttribute(Namespaces.w + "tentative", tentative ? 1 : 0),
				new XElement(Namespaces.w + "start", new XAttribute(Namespaces.w + "val", start)),
				new XElement(Namespaces.w + "numFmt", new XAttribute(Namespaces.w + "val", numberFormat.ToCamelCase())),
				new XElement(Namespaces.w + "lvlText", new XAttribute(Namespaces.w + "val", levelText)),
				new XElement(Namespaces.w + "lvlJc", new XAttribute(Namespaces.w + "val", numberAlignment.ToCamelCase())),
				new XElement(Namespaces.w + "pPr",
					new XElement(Namespaces.w + "ind",
						new XAttribute(Namespaces.w + "left", indentLeftTw),
						new XAttribute(Namespaces.w + "hanging", hangingTw)))
			);

			if (tplc != null)
				xml.SetAttributeValue(Namespaces.w + "tplc", tplc);

			Xml.Add(xml);
			return new ListDefinitionLevel(xml);
		}
	}
}