﻿using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Tables.Margins
{
	public class CellMargins : TableMarginsBase
	{
		private readonly ICellMarginsContainer container;

		internal CellMargins(ICellMarginsContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "tcMar") throw new ArgumentException($"XML element must be tcMar but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateCellMarginsXmlElement();
		}
	}
}