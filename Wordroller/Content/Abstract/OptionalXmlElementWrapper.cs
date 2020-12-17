using System.Xml.Linq;

namespace Wordroller.Content.Abstract
{
	public abstract class OptionalXmlElementWrapper
	{
		protected OptionalXmlElementWrapper(XElement? xml)
		{
			Xml = xml;
		}

		public XElement? Xml { get; protected set; }

		protected abstract XElement CreateRootElement();
	}
}