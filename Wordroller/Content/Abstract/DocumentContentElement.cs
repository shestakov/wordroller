using System.Xml.Linq;

namespace Wordroller.Content.Abstract
{
	public abstract class DocumentContentElement : XmlElementWrapper
	{
		protected DocumentContentElement(XElement xml, DocumentContentContainer parent) : base(xml)
		{
			ParentContainer = parent;
		}

		internal DocumentContentContainer ParentContainer { get; }
	}
}