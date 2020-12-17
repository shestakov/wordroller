using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Tables.Rows
{
	internal interface IRowPropertiesContainer
	{
		internal XElement GetOrCreateRowPropertiesXmlElement();
	}
}