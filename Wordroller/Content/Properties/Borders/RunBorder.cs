using System;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Properties.Borders
{
	public class RunBorder : BorderElement
	{
		private readonly IBorderElementContainer container;

		internal RunBorder(IBorderElementContainer container, XElement? xml) : base("bdr", container, xml)
		{
			if (xml != null && xml.Name != Namespaces.w + "bdr") throw new ArgumentException($"XML element must be bdr but was {xml.Name}", nameof(xml));
			this.container = container;
		}
	}
}