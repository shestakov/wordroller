using System.Xml.Linq;

namespace Wordroller.Content.Properties.Borders
{
	internal interface IParagraphBordersContainer
	{
		internal XElement GetOrCreateBordersXmlElement();
	}
}