using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Sections
{
	public class PageMargins : XmlElementWrapper
	{
		public PageMargins(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "pgMar") throw new ArgumentException("XML element must be pgMar", nameof(xml));
		}

		public double FooterCm { get => FooterTw / UnitHelper.TwipsPerCm; set => FooterTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double FooterInch  { get => FooterTw / UnitHelper.TwipsPerInch; set => FooterTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int FooterTw
		{
			get => GetAttribute("footer");
			set => SetAttribute("footer", value);
		}

		public double GutterCm { get => GutterTw / UnitHelper.TwipsPerCm; set => GutterTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double GutterInch  { get => GutterTw / UnitHelper.TwipsPerInch; set => GutterTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int GutterTw
		{
			get => GetAttribute("gutter");
			set => SetAttribute("gutter", value);
		}

		public double HeaderCm { get => HeaderTw / UnitHelper.TwipsPerCm; set => HeaderTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double HeaderInch  { get => HeaderTw / UnitHelper.TwipsPerInch; set => HeaderTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int HeaderTw
		{
			get => GetAttribute("header");
			set => SetAttribute("header", value);
		}

		public double LeftCm { get => LeftTw / UnitHelper.TwipsPerCm; set => LeftTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double LeftInch  { get => LeftTw / UnitHelper.TwipsPerInch; set => LeftTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int LeftTw
		{
			get => GetAttribute("left");
			set => SetAttribute("left", value);
		}

		public double TopCm { get => TopTw / UnitHelper.TwipsPerCm; set => TopTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double TopInch  { get => TopTw / UnitHelper.TwipsPerInch; set => TopTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int TopTw
		{
			get => GetAttribute("top");
			set => SetAttribute("top", value);
		}

		public double RightCm { get => RightTw / UnitHelper.TwipsPerCm; set => RightTw = (int) (value * UnitHelper.TwipsPerCm); }
		public double RightInch  { get => RightTw / UnitHelper.TwipsPerInch; set => RightTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int RightTw
		{
			get => GetAttribute("right");
			set => SetAttribute("right", value);
		}

		public double BottomCm { get => BottomTw / UnitHelper.TwipsPerCm; set => BottomTw =  (int) (value * UnitHelper.TwipsPerCm); }
		public double BottomInch  { get => BottomTw / UnitHelper.TwipsPerInch; set => BottomTw = (int) (value * UnitHelper.TwipsPerInch); }
		public int BottomTw
		{
			get => GetAttribute("bottom");
			set => SetAttribute("bottom", value);
		}

		private int GetAttribute(string name)
		{
			return int.TryParse(Xml.Attribute(XName.Get(name, Namespaces.w.NamespaceName))?.Value, out var value)
				? value
				: throw new Exception($"Page margins attribute {name} has incorrect value");
		}

		private void SetAttribute(string name, int value)
		{
			Xml.SetAttributeValue(XName.Get(name, Namespaces.w.NamespaceName), value);
		}
	}
}