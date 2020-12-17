using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Tables.Cells
{
	internal interface ICellPropertiesContainer
	{
		internal XElement GetOrCreateCellPropertiesXmlElement();
	}
}