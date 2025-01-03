using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Fonts;
using Wordroller.Utility;
using Wordroller.Utility.Xml;
using Wordroller.Content.Properties.Borders;

namespace Wordroller.Content.Properties.Runs
{
	public class RunProperties : OptionalXmlElementWrapper, IRunColorContainer, IRunUnderlineContainer, IFontSettingsContainer, IRunLanguageContainer, IRunShadingContainer, IBorderElementContainer
    {
		private readonly IRunPropertiesContainer container;

		internal RunProperties(IRunPropertiesContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "rPr") throw new ArgumentException($"XML element must be rPr but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public bool? Bold
		{
			get => GetOnOffValue("b");
			set => SetOnOffValue("b", value);
		}

		public bool? Italic
		{
			get => GetOnOffValue("i");
			set => SetOnOffValue("i", value);
		}

		public TextCapitalization? Capitalization
		{
			get
			{
				var caps = GetOnOffValue("caps");
				var smallCaps = GetOnOffValue("smallCaps");

				if (caps == null && smallCaps == null) return null;
				if (caps == true) return TextCapitalization.Caps;
				if (smallCaps == true) return TextCapitalization.SmallCaps;
				return TextCapitalization.None;
			}

			set
			{
				Xml ??= CreateRootElement();

				if (value == null)
				{
					SetOnOffValue("caps", null);
					SetOnOffValue("smallCaps", null);
				} else if (value == TextCapitalization.None)
				{
					SetOnOffValue("caps", false);
					SetOnOffValue("smallCaps", false);
				} else if (value == TextCapitalization.Caps)
				{
					SetOnOffValue("caps", true);
					SetOnOffValue("smallCaps", null);
				} else if (value == TextCapitalization.SmallCaps)
				{
					SetOnOffValue("caps", null);
					SetOnOffValue("smallCaps", true);
				}
			}
		}

		public TextStrikethrough? Strikethrough
		{
			get
			{
				var singleStrike = GetOnOffValue("strike");
				var doubleStrike = GetOnOffValue("dstrike");

				if (singleStrike == null && doubleStrike == null) return null;
				if (singleStrike == true) return TextStrikethrough.Single;
				if (doubleStrike == true) return TextStrikethrough.Double;
				return TextStrikethrough.None;
			}
			set
			{
				Xml ??= CreateRootElement();

				if (value == null)
				{
					SetOnOffValue("strike", null);
					SetOnOffValue("dstrike", null);
				} else if (value == TextStrikethrough.None)
				{
					SetOnOffValue("strike", false);
					SetOnOffValue("dstrike", false);
				} else if (value == TextStrikethrough.Single)
				{
					SetOnOffValue("strike", true);
					SetOnOffValue("dstrike", null);
				} else if (value == TextStrikethrough.Double)
				{
					SetOnOffValue("strike", null);
					SetOnOffValue("dstrike", true);
				}
			}
		}

		public RunVerticalAlignment? VerticalAlignment
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<RunVerticalAlignment>("vertAlign", "val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("vertAlign", "val", value?.ToCamelCase());
			}
		}

		public FontSettings Font => new FontSettings(this, Xml?.Element(XName.Get("rFonts", Namespaces.w.NamespaceName)));

		public RunColor Color => new RunColor(this, Xml?.Element(XName.Get("color", Namespaces.w.NamespaceName)));

		public RunLanguage RunLanguage => new RunLanguage(this, Xml?.Element(XName.Get("lang", Namespaces.w.NamespaceName)));

		public RunUnderline Underline => new RunUnderline(this, Xml?.Element(XName.Get("u", Namespaces.w.NamespaceName)));

		public RunShading Shading => new RunShading(this, Xml?.Element(XName.Get("shd", Namespaces.w.NamespaceName)));

		public RunBorder Border => new RunBorder(this, Xml?.Element(Namespaces.w + "bdr"));
		
		public HighlightColor? HighlightColor
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<HighlightColor>("highlight", "val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("highlight", "val", value?.ToCamelCase());
			}
		}

		public double? FontSize { get => FontSizeInHalfPoints / 2; set => FontSizeInHalfPoints = (int?) (value * 2); }
		public int? FontSizeInHalfPoints
		{
			get => Xml?.GetSingleElementAttributeIntNullable("sz", "val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElementInt("sz", "val", value);
			}
		}

		public double? Kerning { get => KerningInHalfPoints / 2; set => KerningInHalfPoints = (int?) (value * 2); }
		public int? KerningInHalfPoints
		{
			get => Xml.GetOwnAttributeIntNullable("kern");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("kern", value);
			}
		}

		public double? SpacingPt { get => SpacingTw / UnitHelper.TwipsPerPt; set => SpacingTw = (int?) (value * UnitHelper.TwipsPerPt); }
		public int? SpacingTw
		{
			get => Xml.GetOwnAttributeIntNullable("spacing");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("spacing", value);
			}
		}

		public int? ScalePc
		{
			get => Xml.GetOwnAttributeIntNullable("w");
			set
			{
				if (value < 1 || value > 600) throw new ArgumentOutOfRangeException(nameof(value), value, "Scale must be between 1 and 600");
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("w", value);
			}
		}

		public double? Position { get => PositionInHalfPoints / 2; set => PositionInHalfPoints = (int?) (value * 2); }
		public int? PositionInHalfPoints
		{
			get => Xml.GetOwnAttributeIntNullable("position");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("position", value);
			}
		}

		#region Complex Script Properties

		public bool? IsComplexScript
		{
			get => GetOnOffValue("cs");
			set => SetOnOffValue("cs", value);
		}

		public bool? ComplexScriptBold
		{
			get => GetOnOffValue("bCs");
			set => SetOnOffValue("bCs", value);
		}

		public bool? ComplexScriptItalic
		{
			get => GetOnOffValue("iCs");
			set => SetOnOffValue("iCs", value);
		}

		public double? ComplexScriptFontSize { get => ComplexScriptFontSizeInHalfPoints / 2; set => ComplexScriptFontSizeInHalfPoints = (int?) (value * 2); }
		public int? ComplexScriptFontSizeInHalfPoints
		{
			get => Xml?.GetSingleElementAttributeIntNullable("szCs", "val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElementInt("szCs", "val", value);
			}
		}

		#endregion

		private bool? GetOnOffValue(string elementName)
		{
			var e = Xml?.Element(XName.Get(elementName, Namespaces.w.NamespaceName));
			if (e == null) return null;
			return e.GetOwnAttributeBoolNullable("val") != false;
		}

		private void SetOnOffValue(string elementName, bool? value)
		{
			Xml ??= CreateRootElement();

			var e = Xml.Element(XName.Get(elementName, Namespaces.w.NamespaceName));

			if (value.HasValue)
			{
				if (e == null)
				{
					e = new XElement(XName.Get(elementName, Namespaces.w.NamespaceName));
					Xml.Add(e);
				}

				e.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), value == false ? "0" : null);
			}
			else
			{
				e?.Remove();
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateRunPropertiesXmlElement();
		}

		XElement IRunColorContainer.GetOrCreateRunColorXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("color", Namespaces.w.NamespaceName);
			var tcBorders = Xml.Element(xName);
			if (tcBorders != null) return tcBorders;
			tcBorders = new XElement(xName);
			Xml.Add(tcBorders);
			return tcBorders;
		}

		XElement IRunUnderlineContainer.GetOrCreateRunUnderlineXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("u", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;

		}

		XElement IFontSettingsContainer.GetOrCreateFontSettingsXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("rFonts", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;
		}

		XElement IRunLanguageContainer.GetOrCreateRunLanguageXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("lang", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;
		}

		XElement IRunShadingContainer.GetOrCreateShadingXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("shd", Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;
		}

		XElement IBorderElementContainer.GetOrCreateBorderXmlElement(string elementName = "bdr")
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get(elementName, Namespaces.w.NamespaceName);
			var spacing = Xml.Element(xName);
			if (spacing != null) return spacing;
			spacing = new XElement(xName);
			Xml.Add(spacing);
			return spacing;
		}
	}
}