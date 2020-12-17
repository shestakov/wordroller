using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Tables.Widths;
using Wordroller.Utility;

namespace Wordroller.Content.Properties.Tables.Margins
{
	public abstract class TableMarginsBase : OptionalXmlElementWrapper
	{
		protected TableMarginsBase(XElement? xml) : base(xml)
		{
		}

		public double? LeftCm { get => LeftTw / UnitHelper.TwipsPerCm; set => LeftTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? LeftInch { get => LeftTw / UnitHelper.TwipsPerInch; set => LeftTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? LeftTw
		{
			get => Xml.GetTableWidthTwValue("left");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthTwValue("left", value);
			}
		}

		public double? TopCm { get => TopTw / UnitHelper.TwipsPerCm; set => TopTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? TopInch { get => TopTw / UnitHelper.TwipsPerInch; set => TopTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? TopTw
		{
			get => Xml.GetTableWidthTwValue("top");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthTwValue("top", value);
			}
		}

		public double? RightCm { get => RightTw / UnitHelper.TwipsPerCm; set => RightTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? RightInch { get => RightTw / UnitHelper.TwipsPerInch; set => RightTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? RightTw
		{
			get => Xml.GetTableWidthTwValue("right");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthTwValue("right", value);
			}
		}

		public double? BottomCm { get => BottomTw / UnitHelper.TwipsPerCm; set => BottomTw = (int?) (value * UnitHelper.TwipsPerCm); }
		public double? BottomInch { get => BottomTw / UnitHelper.TwipsPerInch; set => BottomTw = (int?) (value * UnitHelper.TwipsPerInch); }
		public int? BottomTw
		{
			get => Xml.GetTableWidthTwValue("bottom");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthTwValue("bottom", value);
			}
		}

		public double? LeftPc { get => LeftFp / UnitHelper.FipcsPerPc; set => LeftFp = (int?) (value * UnitHelper.FipcsPerPc); }
		public int? LeftFp
		{
			get => Xml.GetTableWidthFpValue("left");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthFpValue("left", value);
			}
		}

		public double? TopPc { get => TopFp / UnitHelper.FipcsPerPc; set => TopFp = (int?) (value * UnitHelper.FipcsPerPc); }
		public int? TopFp
		{
			get => Xml.GetTableWidthFpValue("top");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthFpValue("top", value);
			}
		}

		public double? RightPc { get => RightFp / UnitHelper.FipcsPerPc; set => RightFp = (int?) (value * UnitHelper.FipcsPerPc); }
		public int? RightFp
		{
			get => Xml.GetTableWidthFpValue("right");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthFpValue("right", value);
			}
		}

		public double? BottomPc { get => BottomFp / UnitHelper.FipcsPerPc; set => BottomFp = (int?) (value * UnitHelper.FipcsPerPc); }
		public int? BottomFp
		{
			get => Xml.GetTableWidthFpValue("bottom");

			set
			{
				Xml ??= CreateRootElement();
				Xml.SetTableWidthFpValue("bottom", value);
			}
		}
	}
}