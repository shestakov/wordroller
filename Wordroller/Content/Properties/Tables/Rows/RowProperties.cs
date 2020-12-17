using System.Globalization;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Tables.Rows
{
	public class RowProperties : OptionalXmlElementWrapper
	{
		private readonly IRowPropertiesContainer container;

		internal RowProperties(IRowPropertiesContainer container, XElement? xml) : base(xml)
		{
			this.container = container;
		}

		public int GridAfter
		{
			get => Xml?.GetSingleElementAttributeIntNullable("gridAfter", "val") ?? 0;
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("gridAfter", "val", value != 0 ? value.ToString() : null);
			}
		}

		public int GridBefore
		{
			get => Xml?.GetSingleElementAttributeIntNullable("gridBefore", "val") ?? 0;
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetSingleElementAttributeOrRemoveElement("gridBefore", "val", value != 0 ? value.ToString() : null);
			}
		}

		public double? ExactHeightCm { get => ExactHeightTw / UnitHelper.TwipsPerCm; set => ExactHeightTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? ExactHeightInch { get => ExactHeightTw / UnitHelper.TwipsPerInch; set => ExactHeightTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? ExactHeightTw
		{
			get
			{
				var trHeight = Xml?.Element(XName.Get("trHeight", Namespaces.w.NamespaceName));
				return trHeight.GetOwnAttributeEnum<RowHeightRule>("hRule") == RowHeightRule.Exact ? trHeight.GetOwnAttributeInt("val") : (int?) null;
			}

			set => SetHeight(value, RowHeightRule.Exact);
		}

		public double? MinHeightCm { get => MinHeightTw / UnitHelper.TwipsPerCm; set => MinHeightTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? MinHeightInch { get => MinHeightTw / UnitHelper.TwipsPerInch; set => MinHeightTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? MinHeightTw
		{
			get
			{
				var trHeight = Xml?.Element(XName.Get("trHeight", Namespaces.w.NamespaceName));
				return trHeight.GetOwnAttributeEnum<RowHeightRule>("hRule") == RowHeightRule.AtLest ? trHeight.GetOwnAttributeInt("val") : (int?) null;
			}
			set => SetHeight(value, RowHeightRule.AtLest);
		}

		private void SetHeight(double? height, RowHeightRule rule)
		{
			Xml ??= CreateRootElement();

			var xName = XName.Get("trHeight", Namespaces.w.NamespaceName);

			var trHeight = Xml.Element(xName);

			if (height.HasValue && rule != RowHeightRule.Auto)
			{
				if (trHeight == null)
				{
					trHeight = new XElement(xName);
					Xml.Add(trHeight);
				}

				trHeight.SetAttributeValue(XName.Get("hRule", Namespaces.w.NamespaceName), rule.ToCamelCase());
				trHeight.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName),
					height.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				trHeight?.Remove();
			}
		}

		/// <summary>
		/// Table header is repeated on every page
		/// </summary>
		/// <remarks>
		/// If value is null, table style's property is applied. If table style is not provided, defaults to false
		/// </remarks>
		public bool? RepeatTableHeader
		{
			get => Xml?.GetSingleElementAttributeBoolNullable("tblHeader", "val", true);
			set
			{
				Xml ??= CreateRootElement();

				if (value == null)
				{
					Xml.RemoveSingleElement("tblHeader");
				}
				else
				{
					Xml.SetSingleElementAttributeElementMustExist("tblHeader", "val", value.Value ? null : "0");
				}
			}
		}

		/// <summary>
		/// Row can split across pages
		/// </summary>
		/// <remarks>
		/// If value is null, table style's property is applied. If table style is not provided, defaults to true
		/// </remarks>
		public bool? CanSplitAcrossPages
		{
			get => Xml?.GetSingleElementAttributeBoolNullable("cantSplit", "val", false);
			set
			{
				Xml ??= CreateRootElement();

				if (value == null)
				{
					Xml.RemoveSingleElement("cantSplit");
				}
				else
				{
					// NOTE: The property value is inverse to the stored value
					Xml.SetSingleElementAttributeElementMustExist("cantSplit", "val", value.Value ? "0" : null);
				}
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateRowPropertiesXmlElement();
		}
	}
}