using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Paragraphs.Indentation
{
	public class ParagraphIndentation : OptionalXmlElementWrapper
	{
		private readonly IParagraphIndentationContainer container;

		internal ParagraphIndentation(IParagraphIndentationContainer container, XElement? xml) : base(xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "ind") throw new ArgumentException($"XML element must be ind but was {xml.Name}", nameof(xml));
			this.container = container;
		}

		public double? LeftCm { get => LeftTw / UnitHelper.TwipsPerCm; set => LeftTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? LeftInch { get => LeftTw / UnitHelper.TwipsPerInch; set => LeftTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? LeftTw
		{
			get => Xml.GetOwnAttributeIntNullable("leftChars") == null
				? Xml.GetOwnAttributeIntNullable("left")
				: null;

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("left", value);
				Xml.SetOwnAttributeIntNullable("leftChars", null);
			}
		}

		public double? RightCm { get => RightTw / UnitHelper.TwipsPerCm; set => RightTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? RightInch { get => RightTw / UnitHelper.TwipsPerInch; set => RightTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? RightTw
		{
			get => Xml.GetOwnAttributeIntNullable("rightChars") == null
				? Xml.GetOwnAttributeIntNullable("right")
				: null;

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("right", value);
			}
		}

		public double? HangingCm { get => HangingTw / UnitHelper.TwipsPerCm; set => HangingTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? HangingInch { get => HangingTw / UnitHelper.TwipsPerInch; set => HangingTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? HangingTw
		{
			get => Xml.GetOwnAttributeIntNullable("hangingChars") == null
				? Xml.GetOwnAttributeIntNullable("hanging")
				: null;

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("hanging", value);
				Xml.SetOwnAttributeIntNullable("firstLine", null);
				Xml.SetOwnAttributeIntNullable("hangingChars", null);
			}
		}

		public double? FirstLineCm { get => FirstLineTw / UnitHelper.TwipsPerCm; set => FirstLineTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? FirstLineInch { get => FirstLineTw / UnitHelper.TwipsPerInch; set => FirstLineTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? FirstLineTw
		{
			get => Xml.GetOwnAttributeIntNullable("firstLineChars") == null || Xml.GetOwnAttributeIntNullable("hanging") == null
				? Xml.GetOwnAttributeIntNullable("firstLine")
				: null;

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("firstLine", value);
				Xml.SetOwnAttributeIntNullable("hanging", null);
				Xml.SetOwnAttributeIntNullable("firstLineChars", null);
			}
		}

		public double? LeftChars { get => LeftInHundredthsOfCharacter / 100; set => LeftInHundredthsOfCharacter = (int?) (value * 100); }

		/// <summary>
		///     Sets the left indent in hundredths of a character unit. When setting this property, properties LeftTw, LeftCm, LeftInch are reset to null.
		/// </summary>
		public int? LeftInHundredthsOfCharacter
		{
			get => Xml.GetOwnAttributeIntNullable("leftChars");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("leftChars", value);
				Xml.SetOwnAttributeIntNullable("left", null);
			}
		}

		public double? RightChars { get => RightInHundredthsOfCharacter / 100; set => RightInHundredthsOfCharacter = (int?) (value * 100); }

		/// <summary>
		///     Sets the left indent in hundredths of a character unit. When setting this property, properties RightTw, RightCm, RightInch are reset to null.
		/// </summary>
		public int? RightInHundredthsOfCharacter
		{
			get => Xml.GetOwnAttributeIntNullable("rightChars");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("rightChars", value);
				Xml.SetOwnAttributeIntNullable("right", null);
			}
		}

		public double? HangingChars { get => HangingInHundredthsOfCharacter / 100; set => HangingInHundredthsOfCharacter = (int?) (value * 100); }

		/// <summary>
		///     Sets the left indent in hundredths of a character unit. When setting this property, properties HangingTw, HangingCm, HangingInch are reset to null.
		/// </summary>
		public int? HangingInHundredthsOfCharacter
		{
			get => Xml.GetOwnAttributeIntNullable("hangingChars");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("hangingChars", value);
				Xml.SetOwnAttributeIntNullable("firstLineChars", null);
				Xml.SetOwnAttributeIntNullable("hanging", null);
			}
		}

		public double? FirstLineChars { get => FirstLineInHundredthsOfCharacter / 100; set => FirstLineInHundredthsOfCharacter = (int?) (value * 100); }

		/// <summary>
		///     Sets the first line indent in hundredths of a character unit. When setting this property, properties FirstLineTw, FirstLineCm, FirstLineInch are reset to null.
		/// </summary>
		public int? FirstLineInHundredthsOfCharacter
		{
			get => Xml.GetOwnAttributeIntNullable("hangingChars") == null
				? Xml.GetOwnAttributeIntNullable("firstLineChars")
				: null;

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetOwnAttributeIntNullable("firstLineChars", value);
				Xml.SetOwnAttributeIntNullable("hangingChars", null);
				Xml.SetOwnAttributeIntNullable("hanging", null);
			}
		}

		protected override XElement CreateRootElement()
		{
			return container.GetOrCreateIndentationXmlElement();
		}
	}
}