using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class SoftHyphen : RunContentElementBase
	{
		public SoftHyphen(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "softHyphen") throw new ArgumentException($"XML element must be softHyphen but was {xml.Name}", nameof(xml));
		}

		public static SoftHyphen Create()
		{
			var softHyphen = new XElement(XName.Get("softHyphen", Namespaces.w.NamespaceName));
			return new SoftHyphen(softHyphen);
		}

		public override string ToText() => "";
	}
}