using System.Xml.Linq;

namespace Wordroller.Content.Properties.Borders
{
	internal interface IBorderElementContainer
	{
		internal XElement GetOrCreateBorderXmlElement(string elementName);
	}
}