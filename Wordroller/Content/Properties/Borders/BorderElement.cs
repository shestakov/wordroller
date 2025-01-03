using System;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	/// <summary>
	///     CT_Border
	/// </summary>
	public class BorderElement : OptionalXmlElementWrapper
	{
		private static readonly string[] legalElementNames =
		{
			"top",
			"left",
			"bottom",
			"right",
			"between",
			"bar",
			"bdr",
			"insideH",
			"insideV",
			"tl2br",
			"tr2bl"
		};

		private readonly IBorderElementContainer container;
		private readonly string elementName;

		internal BorderElement(string elementName, IBorderElementContainer container, XElement? xml) : base(xml)
		{
			if (legalElementNames.All(s => s != elementName))
				throw new ArgumentException("Element name must be one of the supported border elements", nameof(elementName));

			if (xml != null && xml.Name != XName.Get(elementName, Namespaces.w.NamespaceName))
				throw new ArgumentException($"XML element name {xml.Name} does not match the target element name {elementName}", nameof(xml));

			this.elementName = elementName;
			this.container = container;
		}

		public LineBorderStyle Style
		{
			get => Xml.GetOwnAttributeEnumNullable<LineBorderStyle>("val") ?? throw new Exception("Attribute val cannot be null");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnum("val", value);
			}
		}

		public string? Color
		{
			get => Xml.GetOwnAttribute("color");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeColor("color", value);
			}
		}

		public ThemeColor? ThemeColor
		{
			get => Xml.GetOwnAttributeEnumNullable<ThemeColor>("themeColor");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("themeColor", value);
			}
		}

		public string? ThemeTint
		{
			get => Xml.GetOwnAttribute("themeTint");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeDoubleHex("themeTint", value);
			}
		}

		public string? ThemeShade
		{
			get => Xml.GetOwnAttribute("themeShade");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeDoubleHex("themeShade", value);
			}
		}

		public int? WidthEp
		{
			get => Xml.GetOwnAttributeIntNullable("sz");
			set
			{
				Xml ??= CreateRootElement();

				if (value.HasValue && (value.Value < 2 || value.Value > 96))
					throw new ArgumentOutOfRangeException(nameof(value), value, "Width is specified in eighth of a point and must be between 2 and 96, or null");

				Xml.SetOwnAttributeIntNullable("sz", value);
			}
		}

		public double? SpaceCm
		{
			get => SpacePt / UnitHelper.PointsPerCm;
			set => SpacePt = (int?) (value * UnitHelper.PointsPerCm);
		}

		public double? SpaceInch
		{
			get => SpacePt / UnitHelper.PointsPerInch;
			set => SpacePt = (int?) (value * UnitHelper.PointsPerInch);
		}

		public int? SpacePt
		{
			get => Xml.GetOwnAttributeIntNullable("space");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("space", value);
			}
		}

		public bool Shadow
		{
			get => Xml.GetOwnAttributeBool("shadow", false);
			set
			{
				if (value == false && Xml == null) return;
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeOnOffNullIsOff("shadow", value);
			}
		}

		public bool Frame
		{
			get => Xml.GetOwnAttributeBool("frame", false);
			set
			{
				if (value == false && Xml == null) return;
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeOnOffNullIsOff("frame", value);
			}
		}

		protected override XElement CreateRootElement()
		{
			var element = container.GetOrCreateBorderXmlElement(elementName);

			if (element.Name != XName.Get(elementName, Namespaces.w.NamespaceName))
				throw new ArgumentException($"XML element name {element.Name} does not match the target element name {elementName}", nameof(element));

			if (element.GetOwnAttribute("val") == null)
			{
				element.SetOwnAttributeEnum("val", LineBorderStyle.Single);
				element.SetOwnAttributeColor("color", "000000");
				element.SetOwnAttributeIntNullable("sz", 2);
			}

			return element;
		}
	}
}