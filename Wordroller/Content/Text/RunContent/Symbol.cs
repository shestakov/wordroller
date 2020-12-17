using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class Symbol : RunContentElementBase
	{
		public Symbol(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "sym") throw new ArgumentException($"XML element must be sym but was {xml.Name}", nameof(xml));
		}

		public string Font
		{
			get => Xml.GetOwnAttribute("font") ?? throw new Exception("Attribute font is required but not provided");
			set => Xml.SetOwnAttributeString("font", value);
		}

		public string Char
		{
			get => Xml.GetOwnAttribute("char") ?? throw new Exception("Attribute char is required but not provided");
			set => Xml.SetOwnAttributeDoubleHex("leader", value);
		}

		public static Symbol Create(string font, string charHex)
		{
			var sym = new XElement
			(
				XName.Get("sym", Namespaces.w.NamespaceName),
				new XAttribute(XName.Get("font", Namespaces.w.NamespaceName), font),
				new XAttribute(XName.Get("char", Namespaces.w.NamespaceName), charHex)
			);

			return new Symbol(sym);
		}

		public override string ToText()
		{
			var code = int.Parse(Char, System.Globalization.NumberStyles.HexNumber);
			return char.ConvertFromUtf32(code);
		}
	}
}