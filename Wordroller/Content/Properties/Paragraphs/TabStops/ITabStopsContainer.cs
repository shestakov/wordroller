using System.Xml.Linq;

namespace Wordroller.Content.Properties.Paragraphs.TabStops
{
	internal interface ITabStopsContainer
	{
		internal XElement GetOrCreateTabStopsXmlElement();
	}
}