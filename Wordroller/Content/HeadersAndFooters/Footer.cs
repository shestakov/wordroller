using System;
using System.IO.Packaging;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.HeadersAndFooters
{
	public class Footer : Fooder
	{
		internal Footer(PackagePart packagePart, XDocument xDocument, PackageRelationship relationship, FooderDesignation fooderDesignation, WordDocument document)
			: base(FooderType.Footer, packagePart, xDocument, relationship, fooderDesignation, document)
		{
			if (Xml.Name != Namespaces.w + "ftr") throw new ArgumentException($"The footer root must be ftr but was {Xml.Name}", nameof(xDocument));
		}
	}
}