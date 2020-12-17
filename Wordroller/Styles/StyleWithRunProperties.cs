using System.Xml.Linq;
using Wordroller.Content.Properties.Runs;
using Wordroller.Utility.Xml;

namespace Wordroller.Styles
{
	public abstract class StyleWithRunProperties : Style, IRunPropertiesContainer
	{
		protected StyleWithRunProperties(XElement xml, StyleType styleType) : base(xml, styleType)
		{
		}

		public RunProperties RunProperties => new RunProperties(this, Xml.Element(XName.Get("rPr", Namespaces.w.NamespaceName)));

		XElement IRunPropertiesContainer.GetOrCreateRunPropertiesXmlElement()
		{
			var xName = XName.Get("rPr", Namespaces.w.NamespaceName);
			var rPr = Xml.Element(xName);
			if (rPr != null) return rPr;
			rPr = new XElement(xName);
			Xml.AddFirst(rPr);
			return rPr;
		}
	}
}