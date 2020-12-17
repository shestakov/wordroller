using System.Xml.Linq;

namespace Wordroller.Content.Properties.Runs
{
	internal interface IRunUnderlineContainer
	{
		internal XElement GetOrCreateRunUnderlineXmlElement();
	}
}