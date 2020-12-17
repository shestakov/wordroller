using System.Xml.Linq;

namespace Wordroller.Content.Properties.Borders
{
	internal interface ITableCellBordersContainer
	{
		internal XElement GetOrCreateBordersXmlElement();
	}
}