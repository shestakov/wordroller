using System.Linq;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Styles
{
	public class InternalDefaultStyleProvider : IStyleProvider
	{
		public XElement? FindStyleElement(string styleId)
		{
			var document = XmlResourceHelper.GetXmlResource("Wordroller.Styles.Resources.DefaultOnDemandStyles.xml");

			return document.Root?.Elements(Namespaces.w + "style")
				.SingleOrDefault(e => e.Attribute(Namespaces.w + "styleId")?.Value == styleId);
		}
	}
}