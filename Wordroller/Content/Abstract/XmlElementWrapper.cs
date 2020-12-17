using System;
using System.Xml.Linq;

namespace Wordroller.Content.Abstract
{
	public abstract class XmlElementWrapper
	{
		protected XmlElementWrapper(XElement xml)
		{
			Xml = xml ?? throw new ArgumentNullException(nameof(xml));
		}

		public XElement Xml { get; private set; }

		public virtual void Remove()
		{
			Xml.Remove();
		}
	}
}