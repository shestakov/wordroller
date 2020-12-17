using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class Tab : RunContentElementBase
	{
		public Tab(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "tab") throw new ArgumentException($"XML element must be tab but was {xml.Name}", nameof(xml));
		}

		public static Tab Create()
		{
			var tab = new XElement(XName.Get("tab", Namespaces.w.NamespaceName));
			return new Tab(tab);
		}

		public override string ToText() => "\t";
	}
}