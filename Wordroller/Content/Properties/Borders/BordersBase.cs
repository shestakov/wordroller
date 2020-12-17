using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public abstract class BordersBase : OptionalXmlElementWrapper, IBorderElementContainer
	{
		internal BordersBase(XElement? xml) : base(xml)
		{
		}

		XElement IBorderElementContainer.GetOrCreateBorderXmlElement(string elementName)
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get(elementName, Namespaces.w.NamespaceName);
			var element = Xml.Element(xName);
			if (element != null) return element;
			element = new XElement(xName);
			Xml.Add(element);
			return element;
		}
	}
}