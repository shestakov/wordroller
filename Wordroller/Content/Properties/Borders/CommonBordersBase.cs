using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public abstract class CommonBordersBase : BordersBase
	{
		internal CommonBordersBase(XElement? xml) : base(xml)
		{
		}

		public BorderElement Left => new BorderElement("left", this, Xml?.Element(Namespaces.w + "left"));
		public BorderElement Top => new BorderElement("top", this, Xml?.Element(Namespaces.w + "top"));
		public BorderElement Right => new BorderElement("right", this, Xml?.Element(Namespaces.w + "right"));
		public BorderElement Bottom => new BorderElement("bottom", this, Xml?.Element(Namespaces.w + "bottom"));
	}
}