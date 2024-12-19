using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Lists;
using Wordroller.Content.Properties.Borders;
using Wordroller.Content.Properties.Paragraphs.Indentation;
using Wordroller.Content.Properties.Paragraphs.Spacing;
using Wordroller.Content.Properties.Paragraphs.TabStops;
using Wordroller.Content.Properties.Runs;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Paragraphs
{
	public class ParagraphProperties : OptionalXmlElementWrapper, IRunPropertiesContainer, IParagraphBordersContainer, IParagraphSpacingContainer, IParagraphShadingContainer, IParagraphIndentationContainer,
		ITabStopsContainer
	{
		private readonly IParagraphPropertiesContainer container;

		internal ParagraphProperties(IParagraphPropertiesContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "pPr") throw new ArgumentException($"XML element must be pPr but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public string? StyleId
		{
			get
			{
				var styleElement = Xml?.Element(XName.Get("pStyle", Namespaces.w.NamespaceName));
				var attr = styleElement?.Attribute(XName.Get("val", Namespaces.w.NamespaceName));
				return attr?.Value;
			}
			set
			{
				if (value?.Trim() == string.Empty) throw new ArgumentOutOfRangeException(nameof(value), value, "Style ID cannot be an empty or whitespace string");

				Xml ??= CreateRootElement();

				var xName = Namespaces.w.GetName("pStyle");
				var pStyle = Xml.Element(xName);

				if (value == null)
				{
					pStyle?.Remove();
				}
				else
				{
					if (pStyle == null)
					{
						pStyle = new XElement(xName);
						Xml.Add(pStyle);
					}

					pStyle.SetAttributeValue(Namespaces.w.GetName("val"), value);
				}
			}
		}

		public RunProperties ParagraphMarkProperties => new RunProperties(this, Xml?.Element(Namespaces.w + "rPr"));

		public ParagraphBorders Borders => new ParagraphBorders(this, Xml?.Element(Namespaces.w + "pBdr"));

		public ParagraphShading Shading => new ParagraphShading(this, Xml?.Element(Namespaces.w + "shd"));

		public ParagraphSpacing Spacing => new ParagraphSpacing(this, Xml?.Element(Namespaces.w + "spacing"));

		public ParagraphIndentation ParagraphIndentation => new ParagraphIndentation(this, Xml?.Element(Namespaces.w + "ind"));

		public TabStopsCollection TabStops => new TabStopsCollection(this, Xml?.Element(Namespaces.w + "tabs"));

		public TextDirection TextDirection
		{
			get
			{
				var bidi = Xml?.Element(Namespaces.w + "bidi");
				return bidi == null ? TextDirection.LeftToRight : TextDirection.RightToLeft;
			}

			set
			{
				Xml ??= CreateRootElement();

				var bidi = Xml.Element(Namespaces.w + "bidi");

				if (value == TextDirection.RightToLeft)
				{
					if (bidi == null)
						Xml.Add(new XElement(Namespaces.w + "bidi"));
				}
				else
				{
					bidi?.Remove();
				}
			}
		}

		public bool? KeepNext
		{
			get => Xml?.GetSingleElementOnOffNullable("keepNext", "val", true);

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementOnOffNullable("keepNext", "val", value);
			}
		}

		public bool? KeepLines
		{
			get => Xml?.GetSingleElementOnOffNullable("keepLines", "val", true);

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementOnOffNullable("keepLines", "val", value);
			}
		}

		public Alignment? Alignment
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<Alignment>("js", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElementEnum("jc", "val", value);
			}
		}

		public ListBinding? ListBinding
		{
			get
			{
				var numPr = Xml?.Element(XName.Get("numPr", Namespaces.w.NamespaceName));
				return numPr != null ? new ListBinding(numPr) : null;
			}
		}

		internal void SetSectionProperties(XElement element)
		{
			var xName = Namespaces.w + "sectPr";
			if (element.Name != xName) throw new ArgumentException("Element must be a sectPr", nameof(element));

			Xml ??= CreateRootElement();
			Xml.Element(xName)?.Remove();

			Xml.Add(element);
		}

		internal ListBinding BindList(List list, int level)
		{
			Xml ??= CreateRootElement();

			if (level < 0 || level > 8) throw new ArgumentOutOfRangeException(nameof(level), level, "List level must be between 0 and 8");

			var numPr = Xml.Element(XName.Get("numPr", Namespaces.w.NamespaceName));
			if (numPr == null)
			{
				numPr = new XElement(XName.Get("numPr", Namespaces.w.NamespaceName));
				Xml.Add(numPr);
			}

			var numId = numPr.Element(XName.Get("numId", Namespaces.w.NamespaceName));
			if (numId == null)
				numPr.Add(new XElement(XName.Get("numId", Namespaces.w.NamespaceName), new XAttribute(Namespaces.w + "val", list.NumId)));
			else
				numId.SetAttributeValue(Namespaces.w + "val", list.NumId);

			var ilvl = numPr.Element(XName.Get("ilvl", Namespaces.w.NamespaceName));
			if (ilvl == null)
				numPr.Add(new XElement(XName.Get("ilvl", Namespaces.w.NamespaceName), new XAttribute(Namespaces.w + "val", level)));
			else
				ilvl.SetAttributeValue(Namespaces.w + "val", level);

			return new ListBinding(numPr);
		}

		internal void ExcludeFromList()
		{
			var numPr = Xml?.Element(XName.Get("numPr", Namespaces.w.NamespaceName));
			if (numPr == null) throw new Exception("Paragraph is not bound to any list");
			numPr.Remove();
		}

		XElement IParagraphBordersContainer.GetOrCreateBordersXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("pBdr", Namespaces.w.NamespaceName);
			var pBdr = Xml.Element(xName);
			if (pBdr != null) return pBdr;
			pBdr = new XElement(xName);
			Xml.Add(pBdr);
			return pBdr;
		}

		XElement IRunPropertiesContainer.GetOrCreateRunPropertiesXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("rPr", Namespaces.w.NamespaceName);
			var rPr = Xml.Element(xName);
			if (rPr != null) return rPr;
			rPr = new XElement(xName);
			Xml.AddFirst(rPr);
			return rPr;
		}

		XElement IParagraphIndentationContainer.GetOrCreateIndentationXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("ind", Namespaces.w.NamespaceName);
			var ind = Xml.Element(xName);
			if (ind != null) return ind;
			ind = new XElement(xName);
			Xml.Add(ind);
			return ind;
		}

		XElement IParagraphSpacingContainer.GetOrCreateSpacingXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("spacing", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;
		}

		XElement IParagraphShadingContainer.GetOrCreateShadingXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("shd", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;
		}

		XElement ITabStopsContainer.GetOrCreateTabStopsXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("tabs", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;

		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateParagraphPropertiesXmlElement();
		}
	}
}