using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Paragraphs;
using Wordroller.Content.Properties.Runs;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.DocumentDefaults
{
	public class DocumentDefaults : XmlElementWrapper, IParagraphPropertiesContainer, IRunPropertiesContainer
	{
		public DocumentDefaults(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "docDefaults") throw new ArgumentException($"XML element must be docDefaults but was {xml.Name}", nameof(xml));
		}

		public ParagraphProperties ParagraphProperties => new ParagraphProperties(this, FindParagraphPropertiesXmlElement());

		public RunProperties RunProperties => new RunProperties(this, FindRunPropertiesXmlElement());

		XElement IParagraphPropertiesContainer.GetOrCreateParagraphPropertiesXmlElement()
		{
			var pPrDefault = GetOrCreateDefaultParagraphPropertiesXmlElement();
			var xName = XName.Get("pPr", Namespaces.w.NamespaceName);
			var pPr = pPrDefault.Element(xName);
			if (pPr != null) return pPr;
			pPr = new XElement(xName);
			pPrDefault.AddFirst(pPr);
			return pPr;
		}

		XElement IRunPropertiesContainer.GetOrCreateRunPropertiesXmlElement()
		{
			var rPrDefault = GetOrCreateDefaultRunPropertiesXmlElement();
			var xName = XName.Get("rPr", Namespaces.w.NamespaceName);
			var rPr = rPrDefault.Element(xName);
			if (rPr != null) return rPr;
			rPr = new XElement(xName);
			rPrDefault.AddFirst(rPr);
			return rPr;
		}

		private XElement? FindParagraphPropertiesXmlElement()
		{
			var pPrDefault = Xml.Element(XName.Get("pPrDefault", Namespaces.w.NamespaceName));
			return pPrDefault?.Element(XName.Get("pPr", Namespaces.w.NamespaceName));
		}

		private XElement? FindRunPropertiesXmlElement()
		{
			var pPrDefault = Xml.Element(XName.Get("rPrDefault", Namespaces.w.NamespaceName));
			return pPrDefault?.Element(XName.Get("rPr", Namespaces.w.NamespaceName));
		}

		private XElement GetOrCreateDefaultParagraphPropertiesXmlElement()
		{
			var xName = XName.Get("pPrDefault", Namespaces.w.NamespaceName);
			var pPrDefault = Xml.Element(xName);
			if (pPrDefault != null) return pPrDefault;
			pPrDefault = new XElement(xName);
			Xml.AddFirst(pPrDefault);
			return pPrDefault;
		}

		private XElement GetOrCreateDefaultRunPropertiesXmlElement()
		{
			var xName = XName.Get("rPrDefault", Namespaces.w.NamespaceName);
			var rPrDefault = Xml.Element(xName);
			if (rPrDefault != null) return rPrDefault;
			rPrDefault = new XElement(xName);
			Xml.AddFirst(rPrDefault);
			return rPrDefault;
		}
	}
}