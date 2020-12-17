using System.Xml.Linq;

 namespace Wordroller.Content.Properties.Fonts
{
	internal interface IFontSettingsContainer
	{
		internal XElement GetOrCreateFontSettingsXmlElement();
	}
}