using System.Xml.Linq;
using Wordroller.Content.Abstract;

namespace Wordroller.Content.Text.RunContent
{
	public abstract class RunContentElementBase: XmlElementWrapper
	{
		protected RunContentElementBase(XElement xml) : base(xml)
		{
		}

		public abstract string ToText();
	}
}