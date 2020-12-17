using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Tables.Margins
{
	internal interface ICellMarginsContainer
	{
		internal XElement GetOrCreateCellMarginsXmlElement();
	}
}