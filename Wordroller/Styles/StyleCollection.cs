using System;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Lists;
using Wordroller.Content.Properties.DocumentDefaults;
using Wordroller.Utility.Xml;

namespace Wordroller.Styles
{
	public class StyleCollection : DocumentPartWrapper
	{
		private readonly XElement root;
		private readonly IStyleProvider internalDefaultStyleProvider = new InternalDefaultStyleProvider();
		private readonly IStyleProvider? externalStyleProvider;

		internal StyleCollection(PackagePart packagePart, IStyleProvider? externalStyleProvider) : base(packagePart)
		{
			this.externalStyleProvider = externalStyleProvider;
			root = XmlDocument.Root ?? throw new Exception("The XML document does not have root");
		}

		public DocumentDefaults DocumentDefaults => new DocumentDefaults(GetOrCreateDocDefaultsXmlElement());

		public ParagraphStyle CreateParagraphStyle(string styleId, string name, bool? isPrimary)
		{
			return new ParagraphStyle(CreateStyleXml(StyleType.Paragraph, styleId, name, isPrimary));
		}

		public ParagraphStyle CreateCharacterStyle(string styleId, string name, bool? isPrimary)
		{
			return new ParagraphStyle(CreateStyleXml(StyleType.Character, styleId, name, isPrimary));
		}

		public NumberingStyle CreateNumberingStyle(string styleId, string name, List list)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));

			var xml = CreateStyleXml(StyleType.Numbering, styleId, name, false);

			xml.Add(
				new XElement(Namespaces.w + "pPr",
					new XElement(Namespaces.w + "numPr",
						new XElement(Namespaces.w + "numId",
							new XAttribute(Namespaces.w + "val", list.NumId)))));

			return new NumberingStyle(xml);
		}

		private XElement CreateStyleXml(StyleType styleType, string styleId, string name, bool? isPrimary)
		{
			if (string.IsNullOrWhiteSpace(styleId)) throw new ArgumentException("Parameter styleId cannot be null", nameof(styleId));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Parameter name cannot be null", nameof(name));

			var xml = new XElement(Namespaces.w + "style",
				new XAttribute(Namespaces.w + "type", styleType.ToCamelCase()),
				new XAttribute(Namespaces.w + "styleId", styleId),
				new XElement(Namespaces.w + "name", new XAttribute(Namespaces.w + "val", name))
			);

			if (isPrimary == true)
				xml.Add(Namespaces.w + "qFormat");

			root.Add(xml);
			return xml;
		}

		public ParagraphStyle GetParagraphStyle(string styleId)
		{
			var element = GetOrAddStyleElement(styleId, StyleType.Paragraph);
			return new ParagraphStyle(element);
		}

		public CharacterStyle GetCharacterStyle(string styleId)
		{
			var element = GetOrAddStyleElement(styleId, StyleType.Character);
			return new CharacterStyle(element);
		}

		public NumberingStyle GetNumberingStyle(string styleId)
		{
			var element = GetOrAddStyleElement(styleId, StyleType.Paragraph);
			return new NumberingStyle(element);
		}

		public void EnsureStyle(string styleId, StyleType styleType)
		{
			GetOrAddStyleElement(styleId, styleType);
		}

		private XElement GetOrAddStyleElement(string styleId, StyleType styleType)
		{
			var existingStyle = root.Elements(Namespaces.w + "style").FirstOrDefault(d => styleId.Equals(d.GetOwnAttribute("styleId")));

			if (existingStyle != null)
			{
				var existingStyleType = existingStyle.GetOwnAttribute("type");

				if (!styleType.ToCamelCase().Equals(existingStyleType))
				{
					throw new ArgumentException($"Style {styleId} exists in the document yet it is not of style {styleType} but of type {existingStyleType}", nameof(styleId));
				}

				return existingStyle;
			}

			var providedStyle = externalStyleProvider?.FindStyleElement(styleId) ?? internalDefaultStyleProvider.FindStyleElement(styleId);

			if (providedStyle != null)
			{
				var providedStyleType = providedStyle.GetOwnAttribute("type");

				if (!styleType.ToCamelCase().Equals(providedStyleType))
				{
					throw new Exception($"Style {styleId} provided by External Style Provider is not of type {styleType} but of type {providedStyleType}");
				}

				root.Add(providedStyle);
				return providedStyle;
			}

			throw new ArgumentException($"Style {styleId} does not exist in the document and was not provided by External Style Provider", nameof(styleId));
		}

		private bool HasStyle(string value, string type)
		{
			return XmlDocument.Descendants()
				.Any(x => x.Name.Equals(Namespaces.w + "style") &&
						  (x.Attribute(Namespaces.w + "type") == null || x.Attribute(Namespaces.w + "type").Value.Equals(type)) &&
						  x.Attribute(Namespaces.w + "styleId") != null && x.Attribute(Namespaces.w + "styleId").Value.Equals(value));
		}

		internal XDocument AddStylesForList()
		{
			var listStyleExists = root
					.Elements()
					.Select(s => new { s, styleId = s.Attribute(XName.Get("styleId", Namespaces.w.NamespaceName)) })
					.Where(t => t.styleId != null && t.styleId.Value == "ListParagraph")
					.Select(t => t.s)
					.Any();

			if (!listStyleExists)
			{
				var style = new XElement
				(
					Namespaces.w + "style",
					new XAttribute(Namespaces.w + "type", "paragraph"),
					new XAttribute(Namespaces.w + "styleId", "ListParagraph"),
					new XElement(Namespaces.w + "name", new XAttribute(Namespaces.w + "val", "List Paragraph")),
					new XElement(Namespaces.w + "basedOn", new XAttribute(Namespaces.w + "val", "Normal")),
					new XElement(Namespaces.w + "uiPriority", new XAttribute(Namespaces.w + "val", "34")),
					new XElement(Namespaces.w + "qformat"),
					new XElement(Namespaces.w + "rsid", new XAttribute(Namespaces.w + "val", "00832EE1")),
					new XElement
					(
						Namespaces.w + "rPr",
						new XElement(Namespaces.w + "ind", new XAttribute(Namespaces.w + "left", "720")),
						new XElement
						(
							Namespaces.w + "contextualSpacing"
						)
					)
				);

				root.Add(style);
			}

			return XmlDocument;
		}

		private XElement GetOrCreateDocDefaultsXmlElement()
		{
			var xName = XName.Get("docDefaults", Namespaces.w.NamespaceName);
			var docDefaults = root.Element(xName);
			if (docDefaults != null) return docDefaults;
			docDefaults = new XElement(xName);
			root.AddFirst(docDefaults);
			return docDefaults;
		}
	}
}