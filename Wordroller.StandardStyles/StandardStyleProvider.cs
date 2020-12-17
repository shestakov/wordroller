using System.Linq;
using System.Xml.Linq;
using Wordroller.Styles;
using Wordroller.Utility.Xml;

namespace Wordroller.StandardStyles
{
	public class StandardStyleProvider : IStyleProvider
	{
		public XElement? FindStyleElement(string styleId)
		{
			var document = XmlResourceHelper.GetXmlResource("Wordroller.StandardStyles.Resources.StandardTableStyles.xml");

			return document.Root?.Elements(Namespaces.w + "style")
				.SingleOrDefault(e => e.Attribute(Namespaces.w + "styleId")?.Value == styleId);
		}
	}
}