using System.Xml.Linq;

namespace Wordroller.Content.Properties
{
	internal interface IParagraphShadingContainer
    {
		internal XElement GetOrCreateShadingXmlElement();
	}
}