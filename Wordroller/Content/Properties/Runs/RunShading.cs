using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties
{
	public class RunShading : OptionalXmlElementWrapper
	{
		private readonly IRunShadingContainer container;

		internal RunShading(IRunShadingContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "shd")
				throw new ArgumentException($"XML element must be shd but was {xml.Name}", nameof(xml));
			this.container = container;
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

		public string? Fill
		{
			get => Xml.GetOwnAttribute("fill");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeColor("fill", value);
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

		public ThemeColor? ThemeFill
		{
			get => Xml.GetOwnAttributeEnumNullable<ThemeColor>("themeFill");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("themeFill", value);
			}
		}

		public string? ThemeFillShade
		{
			get => Xml.GetOwnAttribute("themeFillShade");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeDoubleHex("themeFillShade", value);
			}
		}

		public string? ThemeFillTint
		{
			get => Xml.GetOwnAttribute("themeFillTint");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeDoubleHex("themeFillTint", value);
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

		public string? ThemeTint
		{
			get => Xml.GetOwnAttribute("themeTint");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeDoubleHex("themeTint", value);
			}
		}

		public ShadingPattern? ShadingPattern
		{
			get => Xml.GetOwnAttributeEnumNullable<ShadingPattern>("val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnumNullable("val", value);
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateShadingXmlElement();
		}
	}
}