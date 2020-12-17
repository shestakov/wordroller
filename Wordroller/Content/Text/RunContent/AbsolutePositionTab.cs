using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class AbsolutePositionTab : RunContentElementBase
	{
		public AbsolutePositionTab(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "ptab") throw new ArgumentException($"XML element must be ptab but was {xml.Name}", nameof(xml));
		}

		public AbsolutePositionTabAlignment Alignment
		{
			get => Xml.GetOwnAttributeEnum<AbsolutePositionTabAlignment>("alignment");
			set => Xml.SetOwnAttributeEnum("alignment", value);
		}

		public AbsolutePositionTabLeaderCharacter LeaderCharacter
		{
			get => Xml.GetOwnAttributeEnum<AbsolutePositionTabLeaderCharacter>("leader");
			set => Xml.SetOwnAttributeEnum("leader", value);
		}

		public AbsolutePositionTabRelativeTo RelativeTo
		{
			get => Xml.GetOwnAttributeEnum<AbsolutePositionTabRelativeTo>("relativeTo");
			set => Xml.SetOwnAttributeEnum("relativeTo", value);
		}

		public static AbsolutePositionTab Create(AbsolutePositionTabAlignment alignment,
			AbsolutePositionTabLeaderCharacter leaderCharacter, AbsolutePositionTabRelativeTo relativeTo)
		{
			var ptab = new XElement
			(
				XName.Get("ptab", Namespaces.w.NamespaceName),
				new XAttribute(XName.Get("alignment", Namespaces.w.NamespaceName), alignment.ToCamelCase()),
				new XAttribute(XName.Get("leaderCharacter", Namespaces.w.NamespaceName), leaderCharacter.ToCamelCase()),
				new XAttribute(XName.Get("relativeTo", Namespaces.w.NamespaceName), relativeTo.ToCamelCase())
			);

			return new AbsolutePositionTab(ptab);
		}

		public override string ToText() => "\t";
	}
}