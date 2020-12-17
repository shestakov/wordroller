using System.Xml.Linq;

namespace Wordroller.Content.Properties.Paragraphs.Spacing
{
	internal interface IParagraphSpacingContainer
	{
		internal XElement GetOrCreateSpacingXmlElement();
	}
}