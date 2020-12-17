using System.Xml.Linq;

namespace Wordroller.Styles
{
	public class CharacterStyle : StyleWithRunProperties
	{
		public CharacterStyle(XElement xml) : base(xml, StyleType.Character)
		{
		}
	}
}