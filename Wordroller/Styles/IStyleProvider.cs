using System.Xml.Linq;

namespace Wordroller.Styles
{
	public interface IStyleProvider
	{
		public XElement? FindStyleElement(string styleId);
	}
}