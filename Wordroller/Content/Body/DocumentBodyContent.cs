using System;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Properties.Sections;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Body
{
	public class DocumentBodyContent : DocumentContentContainer
	{
		internal DocumentBodyContent(WordDocument document, PackagePart packagePart, XElement xml) : base(xml, packagePart, document)
		{
			if (xml.Name != Namespaces.w + "body") throw new ArgumentException($"XML element must be body but was {xml.Name}", nameof(xml));
		}

		/// <summary>
		/// Starting a new Document Section after the last paragraph in the Document Body. Document Body must contain at least one paragraph.
		/// </summary>
		public void StartNewSection(SectionBreakType? sectionBreakType = null)
		{
			var lastParagraph = Paragraphs.LastOrDefault() ??
								throw new InvalidOperationException("Body must contain at least one paragraph to start a new section");

			var sectPr = new XElement(LastSectionProperties.Xml);
			lastParagraph.Properties.SetSectionProperties(sectPr);

			LastSectionProperties.Xml.SetSingleElementAttributeOrRemoveElement("type", "val", sectionBreakType?.ToCamelCase());
		}

		public SectionProperties LastSectionProperties =>
			new SectionProperties(Xml.Element(Namespaces.w + "sectPr") ?? throw new Exception("Document body does not have a sectPr element"));
	}
}