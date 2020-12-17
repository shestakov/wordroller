using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Lists
{
	public class ListDefinitionLevel : XmlElementWrapper
	{
		public ListDefinitionLevel(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "lvl") throw new ArgumentException($"XML element must be lvl but was {xml.Name}", nameof(xml));
		}

		public int Level => Xml.GetOwnAttributeIntNullable("ilvl") ?? throw new Exception("Attribute ilvl must be an integer");
		public bool DeclareNotUsed => Xml.GetOwnAttributeBool("tentative", false);
		public ListNumberAlignment? NumberAlignment => Xml.GetSingleElementAttributeEnumNullable<ListNumberAlignment>("lvlJc", "val");
		public int Start => Xml.GetSingleElementAttributeInt("start", "val");
		public bool ForceNumericNumbering => Xml.GetSingleElementOnOff("isLgl", "val");

		/// <summary>
		/// 0 for no restart, null for restarting every time we dive in
		/// </summary>
		public int? RestartAtLevel => Xml.GetSingleElementAttributeIntNullable("lvlRestart", "val");

		public string? LevelText
		{
			get => Xml.GetSingleElementAttribute("lvlText", "val");
			set => Xml.SetSingleElementAttributeOrRemoveElement("lvlText", "val", value);
		}

		public NumberFormat? Format => Xml.GetSingleElementAttributeEnumNullable<NumberFormat>("numFmt", "val");
		public ListLevelTextSuffix Suffix => Xml.GetSingleElementAttributeEnumNullable<ListLevelTextSuffix>("suff", "val") ?? ListLevelTextSuffix.Tab;

		public string? StyleId => Xml.GetSingleElementAttribute("pStyle", "val");
	}
}