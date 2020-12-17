using System;
using System.Globalization;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Runs
{
	/// <summary>
	/// CT_Language
	/// </summary>
	public class RunLanguage : OptionalXmlElementWrapper
	{
		private readonly IRunLanguageContainer container;

		internal RunLanguage(IRunLanguageContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "lang") throw new ArgumentException($"XML element must be lang but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public void SetMainLanguage(CultureInfo culture) { MainLanguage = culture.Name; }
		public void SetEastAsiaLanguage(CultureInfo culture) { EastAsiaLanguage = culture.Name; }
		public void SetComplexScriptLanguage(CultureInfo culture) { ComplexScriptLanguage = culture.Name; }

		public string? MainLanguage
		{
			get => Xml.GetOwnAttribute("val");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("val", value);
			}
		}

		public string? EastAsiaLanguage
		{
			get => Xml.GetOwnAttribute("eastAsia");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("eastAsia", value);
			}
		}

		public string? ComplexScriptLanguage
		{
			get => Xml.GetOwnAttribute("bidi");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeString("bidi", value);
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateRunLanguageXmlElement();
		}
	}
}