using System.Xml.Linq;

namespace Wordroller.Content.Properties.Runs
{
	internal interface IRunPropertiesContainer
	{
		internal XElement GetOrCreateRunPropertiesXmlElement();
	}
}