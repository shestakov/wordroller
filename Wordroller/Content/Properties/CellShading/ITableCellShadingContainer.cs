using System.Xml.Linq;

namespace Wordroller.Content.Properties.CellShading
{
	internal interface ITableCellShadingContainer
	{
		internal XElement GetOrCreateCellShadingXmlElement();
	}
}