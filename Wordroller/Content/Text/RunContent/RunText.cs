using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class RunText : RunContentElementBase
	{
		public RunText(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "t") throw new ArgumentException($"XML element must be t but was {xml.Name}", nameof(xml));
		}

		public string Text
		{
			get => Xml.Value;
			set
			{
				Xml.SetValue(value);
				if (value != value.Trim(' '))
					Xml.SetAttributeValue(XName.Get("space", XNamespace.Xml.NamespaceName), "preserve");
			}
		}

		/// <summary>
		/// This property is set to true automatically when the Text property value has leading or trailing spaces
		/// </summary>
		public bool PreserveSpaces => Xml.Attribute(XName.Get("space", XNamespace.Xml.NamespaceName))?.Value == "preserve";

		public static RunText Create(string text)
		{
			var t = new XElement(XName.Get("t", Namespaces.w.NamespaceName), text);

			if (text != text.Trim(' '))
				t.SetAttributeValue(XName.Get("space", XNamespace.Xml.NamespaceName), "preserve");

			return new RunText(t);
		}

		public override string ToText()
		{
			return Text;
		}

		public void RemoveSubstring(int startIndex, int length)
		{
			var value = Xml.Value.Remove(startIndex, length);
			Xml.Value = value;

			Xml.SetAttributeValue(XName.Get("space", XNamespace.Xml.NamespaceName),
				value != value.Trim(' ') ? "preserve" : null);
		}
	}
}