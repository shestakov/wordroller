using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class TextBreak : RunContentElementBase
	{
		public TextBreak(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "br") throw new ArgumentException($"XML element must be br but was {xml.Name}", nameof(xml));
		}

		public TextBreakType BreakType
		{
			get => Xml.GetOwnAttributeEnumNullable<TextBreakType>("type") ?? TextBreakType.TextWrapping;

			set
			{
				var actualValue = value != TextBreakType.TextWrapping ? value :  (TextBreakType?) null;
				Xml.SetOwnAttributeEnumNullable("type", actualValue);

				if (value != TextBreakType.TextWrapping)
					Xml.SetAttributeValue("clear", null);
			}
		}

		public TextBreakClear? Clear
		{
			get =>
				BreakType == TextBreakType.TextWrapping
					? Xml.GetOwnAttributeEnumNullable<TextBreakClear>("clear") ?? TextBreakClear.None
					: (TextBreakClear?) null;

			set
			{
				if (BreakType != TextBreakType.TextWrapping) throw new Exception("Clear type for a TextBreak can be set only when BreakType is TextWrapping");
				var actualValue = value != TextBreakClear.None ? value :  null;
				Xml.SetOwnAttributeEnumNullable("clear", actualValue);
			}
		}

		public static TextBreak Create(TextBreakType type = TextBreakType.TextWrapping, TextBreakClear clear = TextBreakClear.None)
		{
			var br = new XElement
			(
				XName.Get("br", Namespaces.w.NamespaceName),
				new XAttribute(XName.Get("type", Namespaces.w.NamespaceName), type.ToCamelCase())
			);

			if (type == TextBreakType.TextWrapping && clear != TextBreakClear.None)
				br.SetAttributeValue(XName.Get("clear", Namespaces.w.NamespaceName), clear.ToCamelCase());

			return new TextBreak(br);
		}

		public override string ToText() => BreakType == TextBreakType.TextWrapping ? "\n" : "\f";
	}
}