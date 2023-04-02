using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Lists
{
	public class ListBinding: XmlElementWrapper
	{
		internal ListBinding(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "numPr") throw new ArgumentException($"XML element must be numPr but was {xml.Name}", nameof(xml));
		}

		public static ListBinding Create(List list, int level)
		{
			var xml = new XElement(XName.Get("numPr", Namespaces.w.NamespaceName),
				new XElement(XName.Get("ilvl", Namespaces.w.NamespaceName), new XAttribute(Namespaces.w + "val", level)),
				new XElement(XName.Get("numId", Namespaces.w.NamespaceName), new XAttribute(Namespaces.w + "val", list.NumId)));

			return new ListBinding(xml);
		}

		public int NumId => Xml.GetSingleElementAttributeInt("numId", "val");
		public int? Level => Xml.GetSingleElementAttributeIntNullable("ilvl", "val");
	}
}