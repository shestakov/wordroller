using System.Xml.Linq;

namespace Wordroller.Content.Properties.Borders
{
	internal interface ITableBordersContainer
	{
		internal XElement GetOrCreateBordersXmlElement();
	}
}