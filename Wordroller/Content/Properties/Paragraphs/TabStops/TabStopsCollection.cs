using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Paragraphs.TabStops
{
	public class TabStopsCollection : OptionalXmlElementWrapper
	{
		private readonly ITabStopsContainer container;

		internal TabStopsCollection(ITabStopsContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "tabs") throw new ArgumentException($"XML element must be tabs but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public IEnumerable<TabStop> TabStops => Xml?.Elements(Namespaces.w + "tab").Select(e => new TabStop(e)) ?? Enumerable.Empty<TabStop>();

		public TabStop AddCm(TabStopType type, int position, TabStopLeaderCharacter? leaderCharacter = null)
		{
			return AddTw(type, (int) (position * UnitHelper.TwipsPerCm), leaderCharacter);
		}

		public TabStop AddInch(TabStopType type, int position, TabStopLeaderCharacter? leaderCharacter = null)
		{
			return AddTw(type, (int) (position * UnitHelper.TwipsPerInch), leaderCharacter);
		}

		public TabStop AddTw(TabStopType type, int position, TabStopLeaderCharacter? leaderCharacter = null)
		{
			Xml ??= CreateRootElement();
			var tabStop = TabStop.Create(type, position, leaderCharacter);
			Xml.Add(tabStop.Xml);
			return tabStop;
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateTabStopsXmlElement();
		}
	}
}