using System;
using System.IO.Packaging;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.HeadersAndFooters
{
	public class Header : Fooder
	{
		internal Header(PackagePart packagePart, XDocument xDocument, PackageRelationship relationship, FooderDesignation designation, WordDocument document)
			: base(FooderType.Header, packagePart, xDocument, relationship, designation, document)
		{
			if (Xml.Name != Namespaces.w + "hdr") throw new ArgumentException($"The footer root must be hdr but was {Xml.Name}", nameof(xDocument));
		}
	}
}