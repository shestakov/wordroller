using System.Xml.Linq;

namespace Wordroller.Content.Properties.Paragraphs
{
	internal interface IParagraphPropertiesContainer
	{
		internal XElement GetOrCreateParagraphPropertiesXmlElement();
	}
}