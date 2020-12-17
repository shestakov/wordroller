using System.Globalization;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Borders;
using Wordroller.Content.Properties.Tables.Margins;
using Wordroller.Content.Properties.Tables.Widths;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Tables
{
	public class TableProperties : OptionalXmlElementWrapper, ITableCellMarginsContainer, ITableBordersContainer
	{
		private readonly ITablePropertiesContainer container;

		internal TableProperties(ITablePropertiesContainer container, XElement? xml) : base(xml)
		{
			this.container = container;
		}

		public TableCellMargins DefaultCellMargins => new TableCellMargins(this, Xml?.Element(XName.Get("tblCellMar", Namespaces.w.NamespaceName)));

		public TableBorders Borders => new TableBorders(this, Xml?.Element(XName.Get("tblBorders", Namespaces.w.NamespaceName)));

		public Alignment? Alignment
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<Alignment>("jc", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("jc", "val", value?.ToCamelCase());
			}
		}

		public TableLayoutType? LayoutType
		{
			get => Xml?.GetSingleElementAttributeEnumNullable<TableLayoutType>("tblLayout", "type");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("tblLayout", "type", value?.ToCamelCase());
			}
		}

		public bool? VisuallyRightToLeft
		{
			get => Xml?.GetSingleElementOnOff("bidiVisual", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementOnOffNullable("bidiVisual", "val", value);
			}
		}

		public string? StyleId
		{
			get => Xml?.GetSingleElementAttribute("tblStyle", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("tblStyle", "val", value);
				// Document.StylesCollection.EnsureTableDesignStyle(styleId);
			}
		}

		public TableLookEnum? ConditionalFormatting
		{
			get
			{
				var tblLook = Xml?.Element(XName.Get("tblLook", Namespaces.w.NamespaceName));

				if (tblLook == null) return null;

				var val = tblLook.Attribute(XName.Get("val", Namespaces.w.NamespaceName));

				var word2007Value =
					uint.TryParse(val?.Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var hexValue)
						? (TableLookEnum) hexValue
						: TableLookEnum.None;

				var firstRow = tblLook.GetOwnAttributeBool("firstRow", false) ? TableLookEnum.FirstRow : TableLookEnum.None;
				var lastRow = tblLook.GetOwnAttributeBool("lastRow", false) ? TableLookEnum.LastRow : TableLookEnum.None;
				var firstColumn = tblLook.GetOwnAttributeBool("firstColumn", false) ? TableLookEnum.FirstColumn : TableLookEnum.None;
				var lastColumn = tblLook.GetOwnAttributeBool("lastColumn", false) ? TableLookEnum.LastColumn : TableLookEnum.None;
				var noHorizontalBand = tblLook.GetOwnAttributeBool("noHBand", false) ? TableLookEnum.NoHBand : TableLookEnum.None;
				var noVerticalBand = tblLook.GetOwnAttributeBool("noVBand", false) ? TableLookEnum.NoVBand : TableLookEnum.None;

				var modernValue = firstRow | lastRow | firstColumn | lastColumn | noHorizontalBand | noVerticalBand;

				return modernValue != TableLookEnum.None ? modernValue : word2007Value;
			}

			set
			{
				Xml ??= CreateRootElement();

				var tblLook = Xml.Element(XName.Get("tblLook", Namespaces.w.NamespaceName));

				if (value == null || value == TableLookEnum.None)
				{
					tblLook?.Remove();
					return;
				}

				if (tblLook == null)
				{
					tblLook = new XElement(XName.Get("tblLook", Namespaces.w.NamespaceName));
					Xml.Add(tblLook);
				}

				var word2007Value = ((uint) value).ToString("X8").Substring(4);
				tblLook.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), word2007Value);

				tblLook.SetAttributeValue(XName.Get(TableLookEnum.FirstRow.ToCamelCase(), Namespaces.w.NamespaceName), value.Value.HasFlag(TableLookEnum.FirstRow) ? "1" : "0");
				tblLook.SetAttributeValue(XName.Get(TableLookEnum.LastRow.ToCamelCase(), Namespaces.w.NamespaceName), value.Value.HasFlag(TableLookEnum.LastRow) ? "1" : "0");
				tblLook.SetAttributeValue(XName.Get(TableLookEnum.FirstColumn.ToCamelCase(), Namespaces.w.NamespaceName),
					value.Value.HasFlag(TableLookEnum.FirstColumn) ? "1" : "0");
				tblLook.SetAttributeValue(XName.Get(TableLookEnum.LastColumn.ToCamelCase(), Namespaces.w.NamespaceName), value.Value.HasFlag(TableLookEnum.LastColumn) ? "1" : "0");
				tblLook.SetAttributeValue(XName.Get(TableLookEnum.NoHBand.ToCamelCase(), Namespaces.w.NamespaceName), value.Value.HasFlag(TableLookEnum.NoHBand) ? "1" : "0");
				tblLook.SetAttributeValue(XName.Get(TableLookEnum.NoVBand.ToCamelCase(), Namespaces.w.NamespaceName), value.Value.HasFlag(TableLookEnum.NoVBand) ? "1" : "0");
			}
		}

		public string? TableCaption
		{
			get => Xml?.GetSingleElementAttribute("tblCaption", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("tblCaption", "val", value);
			}
		}

		public string? TableDescription
		{
			get => Xml?.GetSingleElementAttribute("tblDescription", "val");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("tblDescription", "val", value);
			}
		}

		public double? PreferredWidthCm
		{
			get => PreferredWidthTw / UnitHelper.TwipsPerCm;
			set => PreferredWidthTw = value.HasValue ? (int) (value * UnitHelper.TwipsPerCm)! : (int?) null;
		}

		public double? PreferredWidthInch
		{
			get => PreferredWidthTw / UnitHelper.TwipsPerInch;
			set => PreferredWidthTw = value.HasValue ? (int)(value * UnitHelper.TwipsPerInch)! : (int?) null;
		}

		public int? PreferredWidthTw
		{
			get => Xml.GetTableWidthTwValue("tblW");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthTwValue("tblW", value);
			}
		}

		public double? PreferredWidthPc
		{
			get => PreferredWidthFp / UnitHelper.FipcsPerPc;
			set => PreferredWidthFp = value.HasValue ? (int) (value * UnitHelper.FipcsPerPc)! : (int?) null;
		}

		public int? PreferredWidthFp
		{
			get => Xml.GetTableWidthFpValue("tblW");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthFpValue("tblW", value);
			}
		}

		XElement ITableBordersContainer.GetOrCreateBordersXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("tblBorders", Namespaces.w.NamespaceName);
			var tblBorders = Xml.Element(xName);
			if (tblBorders != null) return tblBorders;
			tblBorders = new XElement(xName);
			Xml.Add(tblBorders);
			return tblBorders;
		}

		XElement ITableCellMarginsContainer.GetOrCreateTableCellMarginsXmlElement()
		{
			Xml ??= CreateRootElement();
			var xName = XName.Get("tblCellMar", Namespaces.w.NamespaceName);
			var tcPr = Xml.Element(xName);
			if (tcPr != null) return tcPr;
			tcPr = new XElement(xName);
			Xml.AddFirst(tcPr);
			return tcPr;
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateTablePropertiesXmlElement();
		}
	}
}