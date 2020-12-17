using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class NoBreakHyphen : RunContentElementBase
	{
		public NoBreakHyphen(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "noBreakHyphen") throw new ArgumentException($"XML element must be noBreakHyphen but was {xml.Name}", nameof(xml));
		}

		public static NoBreakHyphen Create()
		{
			var noBreakHyphen = new XElement(XName.Get("noBreakHyphen", Namespaces.w.NamespaceName));
			return new NoBreakHyphen(noBreakHyphen);
		}

		public override string ToText() => "\u2010";
	}
}