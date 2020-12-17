using System.Xml.Linq;

namespace Wordroller.Content.Properties.Sections.PageSizes
{
	internal interface IPageSizeContainer
	{
		internal XElement GetOrCreatePageSizeXmlElement();
	}
}