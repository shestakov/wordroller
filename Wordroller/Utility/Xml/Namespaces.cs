using System.Xml.Linq;

namespace Wordroller.Utility.Xml
{
	public static class Namespaces
	{
		public static readonly XNamespace rel = "http://schemas.openxmlformats.org/package/2006/relationships";

		public static readonly XNamespace r = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
		public static readonly XNamespace m = "http://schemas.openxmlformats.org/officeDocument/2006/math";

		public static readonly XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

		public static readonly XNamespace wp = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing";
		public static readonly XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";
		public static readonly XNamespace pic = "http://schemas.openxmlformats.org/drawingml/2006/picture";
		public static readonly XNamespace c = "http://schemas.openxmlformats.org/drawingml/2006/chart";

		public static readonly XNamespace v = "urn:schemas-microsoft-com:vml";

		public static readonly XNamespace cp = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";
		public static readonly XNamespace vt = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes";

		public static readonly XNamespace customProperties = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties";
	}
}