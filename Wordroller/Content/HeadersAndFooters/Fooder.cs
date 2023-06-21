using System;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Packages;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.HeadersAndFooters
{
	public abstract class Fooder : DocumentContentContainer
	{
		private readonly XDocument xDocument;

		protected Fooder(FooderType fooderType, PackagePart packagePart, XDocument xDocument, PackageRelationship relationship, FooderDesignation designation,
			WordDocument document)
			: base(xDocument.Root ?? throw new Exception("Header or footer root must not be null"), packagePart, document)
		{
			this.xDocument = xDocument;
			FooderType = fooderType;
			Relationship = relationship;
			Designation = designation;
		}

		public FooderType FooderType { get; }
		public FooderDesignation Designation { get; }
		public PackageRelationship Relationship { get; }

		internal void SavePackagePart()
		{
			PackagePartHelper.SavePackagePart(PackagePart, xDocument);
		}

		public int GetMaxDrawingId()
		{
			return Xml.Descendants(Namespaces.wp + "docPr")
				.Select(docPr => int.TryParse(docPr.Attribute("id")?.Value, out var id) ? id : 0)
				.DefaultIfEmpty()
				.Max();
		}
	}
}