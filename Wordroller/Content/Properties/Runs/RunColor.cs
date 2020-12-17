using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Runs
{
	/// <summary>
	///     CT_Color
	/// </summary>
	public class RunColor : OptionalXmlElementWrapper
	{
		private readonly IRunColorContainer container;

		internal RunColor(IRunColorContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "color") throw new ArgumentException($"XML element must be color but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public string? Color
		{
			get => Xml.GetOwnAttribute("val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeColor("val", value);
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
			return container.GetOrCreateRunColorXmlElement();
		}
	}
}