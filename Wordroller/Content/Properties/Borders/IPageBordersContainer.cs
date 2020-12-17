using System.Xml.Linq;

namespace Wordroller.Content.Properties.Borders
{
	internal interface IPageBordersContainer
	{
		internal XElement GetOrCreateBordersXmlElement();
	}
}