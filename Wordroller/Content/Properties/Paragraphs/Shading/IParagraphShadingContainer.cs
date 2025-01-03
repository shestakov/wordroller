using System.Xml.Linq;

namespace Wordroller.Content.Properties.Paragraphs.Shading
{
	internal interface IParagraphShadingContainer
    {
		internal XElement GetOrCreateShadingXmlElement();
	}
}
