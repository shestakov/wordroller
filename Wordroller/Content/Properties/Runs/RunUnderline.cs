using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Runs
{
	/// <summary>
	/// CT_Color
	/// </summary>
	public class RunUnderline : OptionalXmlElementWrapper
	{
		private readonly IRunUnderlineContainer container;

		internal RunUnderline(IRunUnderlineContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "u") throw new ArgumentException($"XML element must be color but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public UnderlinePattern? Pattern
		{
			get => Xml.GetOwnAttributeEnumNullable<UnderlinePattern>("val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("val", value);
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

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateRunUnderlineXmlElement();
		}
	}
}