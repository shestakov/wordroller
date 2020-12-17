using System;
using System.Xml.Linq;
using Wordroller.Content.Lists;
using Wordroller.Utility.Xml;

namespace Wordroller.Styles
{
	public class NumberingStyle : Style
	{
		public NumberingStyle(XElement xml) : base(xml, StyleType.Numbering)
		{
			GetNumId();
		}

		public int NumId
		{
			get
			{
				var numId = GetNumId();
				var val = numId.Attribute(Namespaces.w + "val") ?? throw new Exception("Element numId does not contain attribute val");
				return int.TryParse(val.Value, out var result) ? result : throw new Exception("Attribute val of numId is not a number");
			}
		}

		public NumberingStyle SetList(List list)
		{
			var numId = GetNumId();
			numId.SetAttributeValue(Namespaces.w + "val", list.NumId);
			return this;
		}

		private XElement GetNumId()
		{
			var pPr = Xml.Element(Namespaces.w + "pPr") ?? throw new Exception("Element pPr does not exist");
			var numPr = pPr.Element(Namespaces.w + "numPr") ?? throw new Exception("Element pPr does not contain element numPr");
			var numId = numPr.Element(Namespaces.w + "numId") ?? throw new Exception("Element numPr does not contain element numId");
			return numId;
		}
	}
}