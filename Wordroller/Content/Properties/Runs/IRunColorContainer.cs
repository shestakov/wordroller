using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Runs
{
	internal interface IRunColorContainer
	{
		internal XElement GetOrCreateRunColorXmlElement();
	}
}