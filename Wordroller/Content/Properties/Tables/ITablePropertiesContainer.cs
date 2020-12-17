using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Tables
{
	internal interface ITablePropertiesContainer
	{
		internal XElement GetOrCreateTablePropertiesXmlElement();
	}
}