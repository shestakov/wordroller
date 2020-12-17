using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Lists
{
	public class List: XmlElementWrapper
	{
		internal List(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "num") throw new ArgumentException($"XML element must be num but was {xml.Name}", nameof(xml));
		}

		public int NumId => Xml.GetOwnAttributeIntNullable("numId") ?? throw new Exception("Attribute numId must be an integer");
		public int AbstractNumId => Xml.GetSingleElementAttributeInt("abstractNumId", "val");
	}
}