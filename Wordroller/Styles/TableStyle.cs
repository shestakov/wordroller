using System.Xml.Linq;

namespace Wordroller.Styles
{
	public class TableStyle : StyleWithRunProperties
	{
		public TableStyle(XElement xml) : base(xml, StyleType.Table)
		{
		}
	}
}