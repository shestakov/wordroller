using System.Xml.Linq;
using Wordroller.Content.Properties.Paragraphs;
using Wordroller.Utility.Xml;

namespace Wordroller.Styles
{
	public class ParagraphStyle : StyleWithRunProperties, IParagraphPropertiesContainer
	{
		public ParagraphStyle(XElement xml) : base(xml, StyleType.Paragraph)
		{
		}

		public ParagraphProperties ParagraphProperties => new ParagraphProperties(this, Xml.Element(XName.Get("pPr", Namespaces.w.NamespaceName)));

		XElement IParagraphPropertiesContainer.GetOrCreateParagraphPropertiesXmlElement()
		{
			var xName = XName.Get("pPr", Namespaces.w.NamespaceName);
			var pPr = Xml.Element(xName);
			if (pPr != null) return pPr;
			pPr = new XElement(xName);
			Xml.AddFirst(pPr);
			return pPr;
		}
	}
}