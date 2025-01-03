using System.Xml.Linq;

namespace Wordroller.Content.Properties.Runs.Shading
{
	internal interface IRunShadingContainer
	{
		internal XElement GetOrCreateShadingXmlElement();
	}
}
