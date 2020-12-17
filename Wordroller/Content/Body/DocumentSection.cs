using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.HeadersAndFooters;
using Wordroller.Content.Properties.Sections;
using Wordroller.Content.Tables;
using Wordroller.Content.Text;
using Wordroller.Content.Text.Search;
using Wordroller.Packages;
using Wordroller.Styles;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Body
{
	public class DocumentSection : DocumentContentContainer
	{
		private readonly WordDocument document;
		private readonly List<Paragraph> sectionParagraphs;
		private readonly List<Table> sectionTables;

		private Footer? defaultFooter;
		private Header? defaultHeader;
		private Footer? evenFooter;
		private Header? evenHeader;
		private Footer? firstFooter;
		private Header? firstHeader;
		private Paragraph? hostingParagraph;

		internal DocumentSection(WordDocument document, PackagePart packagePart, XElement sectPr, XElement? hostingParagraphXml, IEnumerable<XElement> sectionParagraphElements,
			IEnumerable<XElement> sectionTableElements) : base(sectPr, packagePart, document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));
			this.document = document ?? throw new ArgumentNullException(nameof(document));
			var xName = Namespaces.w + "sectPr";
			if (sectPr.Name != xName) throw new ArgumentException($"XML element must be sectPr but was {sectPr.Name}", nameof(sectPr));

			sectionParagraphs = sectionParagraphElements
				.Select(p => new Paragraph(p, this))
				.ToList();

			sectionTables = sectionTableElements
				.Select(t => new Table(t, this))
				.ToList();

			if (hostingParagraphXml == null)
			{
				hostingParagraph = null;
			}
			else
			{
				var lastParagraph = sectionParagraphs.Last();

				if (lastParagraph.Properties.Xml?.Element(xName) == null)
					throw new Exception("Error constructing Section's paragraph list: the last section paragraph does not contain sectPr");

				hostingParagraph = lastParagraph;
			}
		}

		public SectionProperties Properties => new SectionProperties(Xml);

		public override IEnumerable<Paragraph> AllParagraphsRecursive => sectionParagraphs
			.Union(Tables.SelectMany(t => t.Paragraphs));

		public override IEnumerable<Paragraph> Paragraphs => sectionParagraphs.AsReadOnly();

		public override IEnumerable<Table> Tables => sectionTables.ToArray();

		public override Paragraph AppendParagraph()
		{
			var pPr = new XElement(XName.Get("pPr", Namespaces.w.NamespaceName));
			var p = new XElement(XName.Get("p", Namespaces.w.NamespaceName), pPr);

			var paragraph = new Paragraph(p, this);

			if (hostingParagraph == null) // i.e. this is the last document section
			{
				var lastParagraph = sectionParagraphs.LastOrDefault();

				if (lastParagraph == null)
					Xml.AddBeforeSelf(p);
				else
					lastParagraph.Xml.AddAfterSelf(p);
			}
			else
			{
				Xml.Remove();
				pPr.Add(Xml);
				hostingParagraph.Xml.AddAfterSelf(p);
				hostingParagraph = paragraph;
			}

			sectionParagraphs.Add(paragraph);

			return paragraph;
		}

		public override Table AppendTable(CreateTableParameters parameters)
		{
			Document.Styles.EnsureStyle(parameters.Style, StyleType.Table);
			var table = Table.Create(parameters, this);

			if (hostingParagraph == null) // i.e. this is the last document section
			{
				var lastParagraph = sectionParagraphs.LastOrDefault();

				if (lastParagraph == null)
					Xml.AddBeforeSelf(table.Xml);
				else
					lastParagraph.Xml.AddAfterSelf(table.Xml);
			}
			else
			{
				Xml.Remove();

				var newSectionLastParagraphXml = new XElement(
					XName.Get("p", Namespaces.w.NamespaceName),
					new XElement(XName.Get("pPr", Namespaces.w.NamespaceName), Xml));

				hostingParagraph.Xml.AddAfterSelf(newSectionLastParagraphXml);
				hostingParagraph.Xml.AddAfterSelf(table.Xml);
				var newSectionLastParagraph = new Paragraph(newSectionLastParagraphXml, this);
				hostingParagraph = newSectionLastParagraph;
				sectionParagraphs.Add(newSectionLastParagraph);
			}

			return table;
		}

		public override IEnumerable<ParagraphSearchResult> FindText(string stringToSeek, StringComparison stringComparison)
		{
			return AllParagraphsRecursive
				.Union(GetAllFooders().SelectMany(f => f.AllParagraphsRecursive))
				.SelectMany(p => p.FindText(stringToSeek, stringComparison));
		}

		#region Headers and Footers

		public Header? DefaultHeader => defaultHeader ??= FindHeader(FooderDesignation.Default);

		public Header? FirstHeader => firstHeader ??= FindHeader(FooderDesignation.First);

		public Header? EvenHeader => evenHeader ??= FindHeader(FooderDesignation.Even);

		public Footer? DefaultFooter => defaultFooter ??= GetFooter(FooderDesignation.Default);

		public Footer? FirstFooter => firstFooter ??= GetFooter(FooderDesignation.First);

		public Footer? EvenFooter => evenFooter ??= GetFooter(FooderDesignation.Even);

		private Header? FindHeader(FooderDesignation fooderDesignation)
		{
			return FindFooderOfType<Header>(FooderType.Header, fooderDesignation);
		}

		private Footer? GetFooter(FooderDesignation fooderDesignation)
		{
			return FindFooderOfType<Footer>(FooderType.Footer, fooderDesignation);
		}

		public Header CreateHeader(FooderDesignation fooderDesignation)
		{
			if (FindHeader(fooderDesignation) != null) throw new InvalidOperationException($"Header of type {fooderDesignation} already exists");
			return CreateFooder<Header>(FooderType.Header, fooderDesignation);
		}

		public Footer CreateFooter(FooderDesignation fooderDesignation)
		{
			if (GetFooter(fooderDesignation) != null) throw new InvalidOperationException($"Footer of type {fooderDesignation} already exists");
			return CreateFooder<Footer>(FooderType.Footer, fooderDesignation);
		}

		public void DeleteHeader(Header header)
		{
			if (header == null) throw new ArgumentNullException(nameof(header));
			DeleteFooder(header);
			if (header.Designation == FooderDesignation.Default) defaultHeader = null;
			if (header.Designation == FooderDesignation.First) firstHeader = null;
			if (header.Designation == FooderDesignation.Even) evenHeader = null;
		}

		public void DeleteFooter(Footer footer)
		{
			if (footer == null) throw new ArgumentNullException(nameof(footer));
			DeleteFooder(footer);
			if (footer.Designation == FooderDesignation.Default) defaultFooter = null;
			if (footer.Designation == FooderDesignation.First) firstFooter = null;
			if (footer.Designation == FooderDesignation.Even) evenFooter = null;
		}

		private T CreateFooder<T>(FooderType fooderType, FooderDesignation fooderDesignation) where T : Fooder
		{
			var referenceElementName = fooderType == FooderType.Header ? "headerReference" : "footerReference";

			var typeAttributeValue = fooderDesignation switch
			{
				FooderDesignation.Default => "default",
				FooderDesignation.First => "first",
				FooderDesignation.Even => "even",
				_ => throw new ArgumentOutOfRangeException(nameof(fooderDesignation), $"Unsupported header or footer type {fooderDesignation}")
			};

			var fooder = document.FooderCollection.Create(fooderType, fooderDesignation);
			var relationshipId = fooder.Relationship.Id;
			Xml.Add(new XElement(Namespaces.w + referenceElementName, new XAttribute(Namespaces.w + "type", typeAttributeValue),
				new XAttribute(Namespaces.r + "id", relationshipId)));

			return (T) fooder;
		}

		private T? FindFooderOfType<T>(FooderType fooderType, FooderDesignation fooderDesignation) where T : Fooder
		{
			var keyword = fooderType == FooderType.Header ? "header" : "footer";
			var referenceElement = FindFooderReferenceElement(keyword, fooderDesignation);
			if (referenceElement == null) return null;

			var relationshipId = GetRelationshipId(keyword, fooderDesignation, referenceElement);
			var relationship = GetFooderRelationship(fooderType, relationshipId);
			var fooder = document.FooderCollection.Get(relationship, fooderType, fooderDesignation);
			return fooder as T;
		}

		private void DeleteFooder(Fooder fooder)
		{
			string keyword = fooder.FooderType == FooderType.Header ? "header" : "footer";
			var referenceElement = FindFooderReferenceElement(keyword, fooder.Designation);
			if (referenceElement == null) throw new Exception("Fooder reference not found");
			referenceElement.Remove();
			document.FooderCollection.Delete(fooder);
		}

		private static string GetRelationshipId(string keyword, FooderDesignation fooderDesignation, XElement referenceElement)
		{
			return referenceElement.Attribute(Namespaces.r + "id")?.Value ?? throw new Exception($"Section does not have {keyword} of type {fooderDesignation}");
		}

		private PackageRelationship GetFooderRelationship(FooderType fooderType, string relationshipId)
		{
			var relationshipType = fooderType == FooderType.Header ? RelationshipTypes.Header : RelationshipTypes.Footer;
			var relationship = PackagePart.GetRelationship(relationshipId);

			if (relationship.RelationshipType != relationshipType)
				throw new Exception($"Relationship {relationshipId} type does not match {fooderType}");

			return relationship;
		}

		private XElement? FindFooderReferenceElement(string keyword, FooderDesignation fooderDesignation)
		{
			var referenceElementName = keyword + "Reference";

			var typeAttributeValue = fooderDesignation switch
			{
				FooderDesignation.Default => "default",
				FooderDesignation.First => "first",
				FooderDesignation.Even => "even",
				_ => throw new ArgumentOutOfRangeException(nameof(fooderDesignation), $"Unsupported header or footer type {fooderDesignation}")
			};

			var referenceElement = Xml.Elements(Namespaces.w + referenceElementName)
				.FirstOrDefault(e => e.Attribute(Namespaces.w + "type")?.Value == typeAttributeValue);
			// ?? throw new Exception("Fooder reference not found");

			return referenceElement;
		}

		private IEnumerable<Fooder> GetAllFooders()
		{
			return new Fooder?[] { DefaultHeader, FirstHeader, EvenHeader, DefaultFooter, FirstFooter, EvenFooter }
				.Where(f => f != null)!;
		}

		#endregion
	}
}