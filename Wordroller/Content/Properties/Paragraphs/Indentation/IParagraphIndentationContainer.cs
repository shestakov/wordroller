using System.Xml.Linq;

namespace Wordroller.Content.Properties.Paragraphs.Indentation
{
	internal interface IParagraphIndentationContainer
	{
		internal XElement GetOrCreateIndentationXmlElement();
	}
}