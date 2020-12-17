using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Paragraphs.Spacing
{
	public class ParagraphSpacing : OptionalXmlElementWrapper
	{
		private readonly IParagraphSpacingContainer container;

		internal ParagraphSpacing(IParagraphSpacingContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "spacing") throw new ArgumentException($"XML element must be spacing but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public double? BeforeCm
		{
			get => BeforeTw / UnitHelper.TwipsPerCm;
			set => BeforeTw = (int?) (value * UnitHelper.TwipsPerCm);
		}

		public double? BeforeInch
		{
			get => BeforeTw / UnitHelper.TwipsPerInch;
			set => BeforeTw = (int?) (value * UnitHelper.TwipsPerInch);
		}

		public int? BeforeTw
		{
			get => Xml.GetOwnAttributeIntNullable("before");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("before", value);
			}
		}

		public double? AfterCm
		{
			get => AfterTw / UnitHelper.TwipsPerCm;
			set => AfterTw = (int?) (value * UnitHelper.TwipsPerCm);
		}

		public double? AfterInch
		{
			get => AfterTw / UnitHelper.TwipsPerInch;
			set => AfterTw = (int?) (value * UnitHelper.TwipsPerInch);
		}

		public int? AfterTw
		{
			get => Xml.GetOwnAttributeIntNullable("after");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("after", value);
			}
		}

		public double? BetweenLinesCm
		{
			get => BetweenLinesTw / UnitHelper.TwipsPerCm;
			set => BetweenLinesTw = (int?) (value * UnitHelper.TwipsPerCm);
		}

		public double? BetweenLinesInch
		{
			get => BetweenLinesTw / UnitHelper.TwipsPerInch;
			set => BetweenLinesTw = (int?) (value * UnitHelper.TwipsPerInch);
		}

		/// <summary>
		///     Sets the <i>at least</i> <i>absolute</i> spacing between the Paragraph lines in twips.
		///     To set <i>exact</i> spacing set this value first and then set property ExactSpacingBetweenLines to true.
		/// </summary>
		public int? BetweenLinesTw
		{
			get
			{
				var lineRule = Xml.GetOwnAttributeEnumNullable<LineSpacingRule>("lineRule");
				if (lineRule != LineSpacingRule.Exact && lineRule != LineSpacingRule.AtLeast) return null;
				return Xml.GetOwnAttributeIntNullable("line");
			}

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("line", value);
				Xml.SetOwnAttributeEnum("lineRule", LineSpacingRule.AtLeast);
			}
		}

		public bool ExactSpacingBetweenLines
		{
			get => Xml.GetOwnAttributeEnumNullable<LineSpacingRule>("lineRule") == LineSpacingRule.Exact;

			set
			{
				if (BetweenLinesTw == null) throw new InvalidOperationException("Absolute spacing between lines must be set. Set the BetweenLinesTw property first.");
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeEnum("lineRule", value ? LineSpacingRule.Exact : LineSpacingRule.AtLeast);
			}
		}

		public double? BeforeLn
		{
			get => BeforeInHundredthsOfLine / 100;
			set => BeforeInHundredthsOfLine = (int?) (value * 100);
		}

		/// <summary>
		///     Sets the spacing before the Paragraph in hundredths of line. If this property is set, value of the property BeforeTw is ignored
		/// </summary>
		public int? BeforeInHundredthsOfLine
		{
			get => Xml.GetOwnAttributeIntNullable("beforeLines");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("beforeLines", value);
			}
		}

		public double? AfterLn
		{
			get => AfterInHundredthsOfLine / 100;
			set => AfterInHundredthsOfLine = (int?) (value * 100);
		}

		/// <summary>
		///     Sets the spacing before the Paragraph in hundredths of line. If this property is set, value of the property AfterTw is ignored
		/// </summary>
		public int? AfterInHundredthsOfLine
		{
			get => Xml.GetOwnAttributeIntNullable("afterLines");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("afterLines", value);
			}
		}

		public double? BetweenLinesLn
		{
			get => BetweenLinesIn240thsOfLine / 240;
			set => BetweenLinesIn240thsOfLine = (int?) (value * 240);
		}

		/// <summary>
		///     Sets the spacing between the Paragraph lines in 240th of the height of a line.
		/// </summary>
		[SuppressMessage("ReSharper", "InconsistentNaming")]
		public int? BetweenLinesIn240thsOfLine
		{
			get
			{
				var lineRule = Xml.GetOwnAttributeEnumNullable<LineSpacingRule>("lineRule");
				if (lineRule == LineSpacingRule.Exact || lineRule == LineSpacingRule.AtLeast) return null;
				return Xml.GetOwnAttributeIntNullable("line");
			}

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("line", value);
				Xml.SetOwnAttributeEnumNullable<LineSpacingRule>("lineRule", null);
			}
		}

		/// <summary>
		/// Application automatically determines spacing before paragraph
		/// </summary>
		/// <remarks>
		/// If value is null, the property value higher in the styling hierarchy is applied and can interfere with user specified spacing
		/// </remarks>
		public bool? BeforeAutospacing
		{
			get => Xml?.GetOwnAttributeBoolNullable("beforeAutospacing");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeOnOffNullable("beforeAutospacing", value);
			}
		}

		/// <summary>
		/// Application automatically determines spacing after paragraph
		/// </summary>
		/// <remarks>
		/// If value is null, the property value higher in the styling hierarchy is applied and can interfere with user specified spacing
		/// </remarks>
		public bool? AfterAutospacing
		{
			get => Xml?.GetOwnAttributeBoolNullable("afterAutospacing");
			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeOnOffNullable("afterAutospacing", value);
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateSpacingXmlElement();
		}
	}
}