using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Wordroller.Utility.Xml
{
	public static class XmlResourceHelper
	{
		public static XDocument GetXmlResource(string resourceName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			using var stream = assembly.GetManifestResourceStream(resourceName);
			if (stream == null) throw new ArgumentException($"Embedded resource {resourceName} not found", nameof(resourceName));
			using TextReader sr = new StreamReader(stream);
			var document = XDocument.Load(sr);
			return document;
		}
	}
}