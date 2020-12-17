using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class CarriageReturn : RunContentElementBase
	{
		public CarriageReturn(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "cr") throw new ArgumentException($"XML element must be cr but was {xml.Name}", nameof(xml));
		}

		public static CarriageReturn Create()
		{
			return new CarriageReturn(new XElement(XName.Get("cr", Namespaces.w.NamespaceName)));
		}

		public override string ToText() => "\n";
	}
}