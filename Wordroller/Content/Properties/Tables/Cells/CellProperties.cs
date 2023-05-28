using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Borders;
using Wordroller.Content.Properties.CellShading;
using Wordroller.Content.Properties.Tables.Margins;
using Wordroller.Content.Properties.Tables.Widths;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Tables.Cells
{
	public class CellProperties : OptionalXmlElementWrapper, ICellMarginsContainer, ITableCellBordersContainer, ITableCellShadingContainer
	{
		private readonly ICellPropertiesContainer container;

		internal CellProperties(ICellPropertiesContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "tcPr")
				throw new ArgumentException($"XML element must be tcPr but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public CellMargins Margins => new CellMargins(this, Xml?.Element(XName.Get("tcMar", Namespaces.w.NamespaceName)));

		public TableCellBorders Borders => new TableCellBorders(this, Xml?.Element(XName.Get("tcBorders", Namespaces.w.NamespaceName)));

		public TableCellShading Shading => new TableCellShading(this, Xml?.Element(XName.Get("shd", Namespaces.w.NamespaceName)));

		public int GridSpan
		{
			get => Xml?.GetSingleElementAttributeIntNullable("gridSpan", "val") ?? 1;
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("gridSpan", "val", value != 1 ? value.ToString() : null);
			}
		}

		public CellVerticalMerge? VerticalMerge
		{
			// It is vMerge, not vmerge despite the specification
			get => Xml?.GetSingleElementAttributeEnumNullable<CellVerticalMerge>("vMerge", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("vMerge", "val", value?.ToCamelCase());
			}
		}

		public CellVerticalAlignment? VerticalAlignment
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<CellVerticalAlignment>("vAlign", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("vAlign", "val", value?.ToCamelCase());
			}
		}

		public CellTextDirection? TextDirection
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<CellTextDirection>("textDirection", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("textDirection", "val", value?.ToCamelCase());
			}
		}

		public double? PreferredWidthCm
		{
			get => PreferredWidthTw / UnitHelper.TwipsPerCm;
			set => PreferredWidthTw = value.HasValue ? (int)(value * UnitHelper.TwipsPerCm)! : (int?)null;
		}

		public double? PreferredWidthInch
		{
			get => PreferredWidthTw / UnitHelper.TwipsPerInch;
			set => PreferredWidthTw = value.HasValue ? (int)(value * UnitHelper.TwipsPerInch)! : (int?)null;
		}

		public int? PreferredWidthTw
		{
			get => Xml.GetTableWidthTwValue("tcW");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthTwValue("tcW", value);
			}
		}

		public double? PreferredWidthPc
		{
			get => PreferredWidthFp / UnitHelper.FipcsPerPc;
			set => PreferredWidthFp = value.HasValue ? (int)(value * UnitHelper.FipcsPerPc)! : (int?)null;
		}

		public int? PreferredWidthFp
		{
			get => Xml.GetTableWidthFpValue("tcW");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthFpValue("tcW", value);
			}
		}

		XElement ICellMarginsContainer.GetOrCreateCellMarginsXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("tcMar", Namespaces.w.NamespaceName);
			var tcPr = Xml.Element(xName);
			if (tcPr != null) return tcPr;
			tcPr = new XElement(xName);
			Xml.Add(tcPr);
			return tcPr;
		}

		XElement ITableCellBordersContainer.GetOrCreateBordersXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("tcBorders", Namespaces.w.NamespaceName);
			var tcBorders = Xml.Element(xName);
			if (tcBorders != null) return tcBorders;
			tcBorders = new XElement(xName);
			Xml.Add(tcBorders);
			return tcBorders;
		}

		XElement ITableCellShadingContainer.GetOrCreateCellShadingXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("shd", Namespaces.w.NamespaceName);
			var shd = Xml.Element(xName);
			if (shd != null) return shd;
			shd = new XElement(xName);
			Xml.Add(shd);
			return shd;
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateCellPropertiesXmlElement();
		}
	}
}