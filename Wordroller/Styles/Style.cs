using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Styles
{
	public abstract class Style : XmlElementWrapper
	{
		protected Style(XElement xml, StyleType styleType) : base(xml)
		{
			if (xml.Name != Namespaces.w + "style") throw new ArgumentException($"XML element must be style but was {xml.Name}", nameof(xml));
			var actualType = xml.GetOwnAttributeEnum<StyleType>("type");
			if (styleType != actualType) throw new ArgumentException($"XML element must have attribute type equal to {styleType} but equals to {actualType}");
		}

		public StyleType Type => Xml.GetOwnAttributeEnum<StyleType>("type");
	}
}