using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class FieldInstructionText : RunContentElementBase
	{
		public FieldInstructionText(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "instrText") throw new ArgumentException($"XML element must be instrText but was {xml.Name}", nameof(xml));
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

		public static FieldInstructionText Create(FieldType fieldType, string format)
		{
			var text = " " + fieldType.ToString().ToUpperInvariant() + "  " + (!string.IsNullOrEmpty(format) ? " " + format : "") + " ";
			var instrText = new XElement(XName.Get("instrText", Namespaces.w.NamespaceName), text);

			if (text != text.Trim(' '))
				instrText.SetAttributeValue(XName.Get("space", XNamespace.Xml.NamespaceName), "preserve");

			return new FieldInstructionText(instrText);
		}

		public override string ToText()
		{
			return "";
		}
	}
}