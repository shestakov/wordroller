using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class FieldCharacter : RunContentElementBase
	{
		public FieldCharacter(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "fldChar") throw new ArgumentException($"XML element must be fldChar but was {xml.Name}", nameof(xml));
		}

		public FieldCharacterType CharacterType
		{
			get => Xml.GetOwnAttributeEnum<FieldCharacterType>("fldCharType");
			set => Xml.SetOwnAttributeEnum("fldCharType", value);
		}

		public static FieldCharacter Create(FieldCharacterType alignment)
		{
			var fldChar = new XElement
			(
				XName.Get("fldChar", Namespaces.w.NamespaceName),
				new XAttribute(XName.Get("fldCharType", Namespaces.w.NamespaceName), alignment.ToCamelCase())
			);

			return new FieldCharacter(fldChar);
		}

		public override string ToText() => "";
	}
}