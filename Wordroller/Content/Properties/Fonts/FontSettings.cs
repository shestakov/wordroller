using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Fonts
{
	/// <summary>
	/// CT_Fonts
	/// </summary>
	public class FontSettings : OptionalXmlElementWrapper
	{
		private readonly IFontSettingsContainer container;

		internal FontSettings(IFontSettingsContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "rFonts") throw new ArgumentException($"XML element must be rFonts but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public string? Ascii
		{
			get => Xml.GetOwnAttribute("ascii");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("ascii", value);
				Xml.SetOwnAttributeEnumNullable<ThemeFont>("asciiTheme", null);
			}
		}

		public string? HighAnsi
		{
			get => Xml.GetOwnAttribute("hAnsi");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("hAnsi", value);
				Xml.SetOwnAttributeEnumNullable<ThemeFont>("hAnsiTheme", null);
			}
		}

		public string? EastAsia
		{
			get => Xml.GetOwnAttribute("eastAsia");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("eastAsia", value);
				Xml.SetOwnAttributeEnumNullable<ThemeFont>("eastAsiaTheme", null);
			}
		}

		public string? ComplexScript
		{
			get => Xml.GetOwnAttribute("cs");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("cs", value);
				Xml.SetOwnAttributeEnumNullable<ThemeFont>("cstheme", null);
			}
		}

		/// <summary>
		/// This property overrides Ascii property
		/// </summary>
		public ThemeFont? AsciiThemeFont
		{
			get => Xml.GetOwnAttributeEnumNullable<ThemeFont>("asciiTheme");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("asciiTheme", value);
			}
		}

		/// <summary>
		/// This property overrides HighAnsi property
		/// </summary>
		public ThemeFont? HighAnsiThemeFont
		{
			get => Xml.GetOwnAttributeEnumNullable<ThemeFont>("hAnsiTheme");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("hAnsiTheme", value);
			}
		}

		/// <summary>
		/// This property overrides EastAsia property
		/// </summary>
		public ThemeFont? EastAsiaThemeFont
		{
			get => Xml.GetOwnAttributeEnumNullable<ThemeFont>("eastAsiaTheme");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("eastAsiaTheme", value);
			}
		}

		/// <summary>
		/// This property overrides ComplexScript property
		/// </summary>
		public ThemeFont? ComplexScriptThemeFont
		{
			get => Xml.GetOwnAttributeEnumNullable<ThemeFont>("cstheme");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("cstheme", value);
			}
		}

		public FontHint? FontHint
		{
			get => Xml.GetOwnAttributeEnumNullable<FontHint>("hint");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("hint", value);
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateFontSettingsXmlElement();
		}
	}
}