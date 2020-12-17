using System.Xml.Linq;

namespace Wordroller.Content.Properties.Runs
{
	internal interface IRunLanguageContainer
	{
		internal XElement GetOrCreateRunLanguageXmlElement();
	}
}