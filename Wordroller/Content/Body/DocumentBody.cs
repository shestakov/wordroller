using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Text.Search;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Body
{
	public class DocumentBody: DocumentPartWrapper
	{
		private readonly WordDocument wordDocument;
		public XElement Xml { get; }

		internal DocumentBody(WordDocument wordDocument, PackagePart packagePart) : base(packagePart)
		{
			this.wordDocument = wordDocument;
			Xml = XmlDocument.Root?.Element(XName.Get("body", Namespaces.w.NamespaceName)) ?? throw new Exception("Document part root does not contain body");
		}

		public DocumentBodyContent Content => new DocumentBodyContent(wordDocument, PackagePart, Xml);

		public IEnumerable<DocumentSection> Sections
		{
			get
			{
				var xNameP = XName.Get("p", Namespaces.w.NamespaceName);
				var xNameTbl = XName.Get("tbl", Namespaces.w.NamespaceName);
				var allContentItems = Xml.Elements().Where(e => e.Name == xNameP || e.Name == xNameTbl);
				var currentSectionParagraphs = new List<XElement>();
				var currentSectionTables = new List<XElement>();
				var sections = new List<DocumentSection>();

				foreach (var element in allContentItems)
				{
					if (element.Name == xNameP)
					{
						var paragraphSectPr = element.Descendants().FirstOrDefault(s => s.Name.LocalName == "sectPr");
						currentSectionParagraphs.Add(element);

						if (paragraphSectPr != null)
						{
							var section = new DocumentSection(wordDocument, PackagePart, paragraphSectPr, element, currentSectionParagraphs, currentSectionTables);
							sections.Add(section);
							currentSectionParagraphs = new List<XElement>();
						}
					}
					else if (element.Name == xNameTbl)
					{
						currentSectionTables.Add(element);
					}
					else
					{
						throw new Exception($"Error creating Sections content: unknown element name {element.Name}");
					}
				}

				var body = Xml;
				var baseSectionXml = body.Element(XName.Get("sectPr", Namespaces.w.NamespaceName));
				if (baseSectionXml == null) throw new Exception("The document body does not have a default sectPr element");
				var baseSection = new DocumentSection(wordDocument, PackagePart, baseSectionXml, null, currentSectionParagraphs, currentSectionTables);
				sections.Add(baseSection);

				return sections;
			}
		}

		public IEnumerable<ParagraphSearchResult> FindText(string stringToSeek, StringComparison stringComparison)
		{
			return Sections.SelectMany(s => s.FindText(stringToSeek, stringComparison));

			// return Content.AllParagraphsRecursive
			// 	.Union(Sections.SelectMany(s => s.GetAllFooders().SelectMany(f => f.AllParagraphsRecursive)))
			// 	.SelectMany(p => p.FindText(stringToSeek, stringComparison));
		}

		public int GetMaxDrawingId() => Xml.Descendants(Namespaces.wp + "docPr")
			.Select(docPr => int.TryParse(docPr.Attribute(Namespaces.wp + "id")?.Value, out var id) ? id : 0)
			.DefaultIfEmpty()
			.Max();
	}
}