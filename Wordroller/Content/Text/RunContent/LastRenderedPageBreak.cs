using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	/// <summary>
	/// This is a run element that is created by an editor and should not be inserted manually
	/// </summary>
	public class LastRenderedPageBreak : RunContentElementBase
	{
		public LastRenderedPageBreak(XElement xml) : base(xml)
		{
			if (xml.Name != Namespaces.w + "lastRenderedPageBreak") throw new ArgumentException($"XML element must be lastRenderedPageBreak but was {xml.Name}", nameof(xml));
		}

		public override string ToText()
		{
			return "";
		}
	}
}