using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Sections.PageSizes
{
	public class PageSize : OptionalXmlElementWrapper
	{
		private readonly IPageSizeContainer container;

		internal PageSize(IPageSizeContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "pgSz") throw new ArgumentException($"XML element must be pgSz but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public double? WidthCm { get => WidthTw / UnitHelper.TwipsPerCm; set => WidthTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? WidthInch { get => WidthTw / UnitHelper.TwipsPerInch; set => WidthTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? WidthTw
		{
			get => Xml.GetOwnAttributeIntNullable("w");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("w", value);
			}
		}

		public double? HeightCm { get => HeightTw / UnitHelper.TwipsPerCm; set => HeightTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? HeightInch { get => HeightTw / UnitHelper.TwipsPerInch; set => HeightTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? HeightTw
		{
			get => Xml.GetOwnAttributeIntNullable("h");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("h", value);
			}
		}

		public PageOrientation? Orientation
		{
			get => Xml?.GetOwnAttributeEnumNullable<PageOrientation>("orient");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("orient", value);
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreatePageSizeXmlElement();
		}
	}
}