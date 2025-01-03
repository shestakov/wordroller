using System.Xml.Linq;

namespace Wordroller.Content.Properties
{
	internal interface IRunShadingContainer
	{
		internal XElement GetOrCreateShadingXmlElement();
	}
}