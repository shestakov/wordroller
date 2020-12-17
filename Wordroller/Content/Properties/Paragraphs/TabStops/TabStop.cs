using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Paragraphs.TabStops
{
	public class TabStop : XmlElementWrapper
	{
		public TabStop(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "tab") throw new ArgumentException($"XML element must be tab but was {xml.Name}", nameof(xml));
		}

		public static TabStop Create(TabStopType type, int position, TabStopLeaderCharacter? leaderCharacter)
		{
			var tab = new XElement(XName.Get("tab", Namespaces.w.NamespaceName));
			tab.SetOwnAttributeEnum("val", type);
			tab.SetOwnAttributeInt("pos", position);
			tab.SetOwnAttributeEnumNullable("leader", leaderCharacter);
			return new TabStop(tab);
		}

		public TabStopType Type
		{
			get => Xml.GetOwnAttributeEnum<TabStopType>("val");
			set => Xml.SetOwnAttributeEnum("val", value);
		}

		public double PositionCm { get => PositionTw / UnitHelper.TwipsPerCm; set => PositionTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double PositionInch { get => PositionTw / UnitHelper.TwipsPerInch; set => PositionTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int PositionTw
		{
			get => Xml.GetOwnAttributeInt("pos");
			set => Xml.SetOwnAttributeInt("pos", value);
		}

		public TabStopLeaderCharacter? LeaderCharacter
		{
			get => Xml.GetOwnAttributeEnumNullable<TabStopLeaderCharacter>("leader");
			set => Xml.SetOwnAttributeEnumNullable("leader", value);
		}
	}
}