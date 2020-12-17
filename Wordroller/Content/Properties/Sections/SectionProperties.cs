using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Borders;
using Wordroller.Content.Properties.Sections.PageSizes;
using Wordroller.Utility.Xml;
using static System.Boolean;

namespace Wordroller.Content.Properties.Sections
{
	public class SectionProperties : XmlElementWrapper, IPageBordersContainer, IPageSizeContainer
	{
		public SectionProperties(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "sectPr") throw new ArgumentException("XML element must be sectPr", nameof(xml));
		}

		public SectionBreakType BreakType
		{
			get
			{
				var type = Xml.Attribute(Namespaces.w + "type")?.Value;
				return Enum.TryParse(type, true, out SectionBreakType breakType) ? breakType : SectionBreakType.NextPage;
			}
		}

		public bool MirrorMargins
		{
			get
			{
				var mirrorMargins = Xml.Element(XName.Get("mirrorMargins", Namespaces.w.NamespaceName));
				var attribute = mirrorMargins?.Attribute(XName.Get("val", Namespaces.w.NamespaceName));
				if (attribute == null) return true;
				return !TryParse(attribute.Value, out var value) || value;
			}

			set
			{
				var mirrorMargins = Xml.Element(XName.Get("mirrorMargins", Namespaces.w.NamespaceName));

				if (value)
				{
					if (mirrorMargins != null)
						mirrorMargins.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), null);
					else
						Xml.Add(new XElement(Namespaces.w + "mirrorMargins", string.Empty));
				}
				else
				{
					mirrorMargins?.Remove();
				}
			}
		}

		public bool CustomFirstPageHeaderAndFooter
		{
			get
			{
				var titlePg = Xml.Element(XName.Get("titlePg", Namespaces.w.NamespaceName));
				var attribute = titlePg?.Attribute(XName.Get("val", Namespaces.w.NamespaceName));
				if (attribute == null) return true;
				return !TryParse(attribute.Value, out var value) || value;
			}

			set
			{
				var titlePg = Xml.Element(XName.Get("titlePg", Namespaces.w.NamespaceName));

				if (value)
				{
					if (titlePg != null)
						titlePg.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), null);
					else
						Xml.Add(new XElement(Namespaces.w + "titlePg", string.Empty));
				}
				else
				{
					titlePg?.Remove();
				}
			}
		}

		public bool OddAndEvenPageHeadersAndFooters
		{
			get
			{
				var evenAndOddHeaders = Xml.Element(XName.Get("evenAndOddHeaders", Namespaces.w.NamespaceName));
				var attribute = evenAndOddHeaders?.Attribute(XName.Get("val", Namespaces.w.NamespaceName));
				if (attribute == null) return true;
				return !TryParse(attribute.Value, out var value) || value;
			}

			set
			{
				var evenAndOddHeaders = Xml.Element(XName.Get("evenAndOddHeaders", Namespaces.w.NamespaceName));

				if (value)
				{
					if (evenAndOddHeaders != null)
						evenAndOddHeaders.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), null);
					else
						Xml.Add(new XElement(Namespaces.w + "titlePg", string.Empty));
				}
				else
				{
					evenAndOddHeaders?.Remove();
				}
			}
		}

		public PageMargins Margins =>
			new PageMargins(Xml.Element(XName.Get("pgMar", Namespaces.w.NamespaceName)) ?? throw new Exception("pgMar element is obligatory within sectPr"));

		public PageBorders Borders => new PageBorders(this, Xml.Element(XName.Get("pgBorders", Namespaces.w.NamespaceName)));

		public PageSize Size => new PageSize(this, Xml.Element(XName.Get("pgSz", Namespaces.w.NamespaceName)));

		XElement IPageBordersContainer.GetOrCreateBordersXmlElement()
		{
			var xName = XName.Get("pgBorders", Namespaces.w.NamespaceName);
			var pgBorders = Xml.Element(xName);
			if (pgBorders != null) return pgBorders;
			pgBorders = new XElement(xName);
			Xml.Add(pgBorders);
			return pgBorders;
		}

		XElement IPageSizeContainer.GetOrCreatePageSizeXmlElement()
		{
			var xName = XName.Get("pgSz", Namespaces.w.NamespaceName);
			var paSz = Xml.Element(xName);
			if (paSz != null) return paSz;
			paSz = new XElement(xName);
			Xml.Add(paSz);
			return paSz;
		}
	}
}