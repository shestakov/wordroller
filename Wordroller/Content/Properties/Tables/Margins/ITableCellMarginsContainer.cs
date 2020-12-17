using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Tables.Margins
{
	internal interface ITableCellMarginsContainer
	{
		internal XElement GetOrCreateTableCellMarginsXmlElement();
	}
}