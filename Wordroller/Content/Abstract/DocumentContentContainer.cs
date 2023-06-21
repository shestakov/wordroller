using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Drawings;
using Wordroller.Content.Images;
using Wordroller.Content.Tables;
using Wordroller.Content.Text;
using Wordroller.Content.Text.Search;
using Wordroller.Packages;
using Wordroller.Styles;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Abstract
{
	public abstract class DocumentContentContainer : XmlElementWrapper
	{
		internal DocumentContentContainer(XElement xml, PackagePart packagePart, WordDocument document) : base(xml)
		{
			Document = document ?? throw new ArgumentNullException(nameof(document));
			PackagePart = packagePart ?? throw new ArgumentNullException(nameof(packagePart));
		}

		public WordDocument Document { get; }
		internal PackagePart PackagePart { get; }

		public virtual IEnumerable<Paragraph> Paragraphs => Xml.Elements(Namespaces.w + "p")
			.Select(e => new Paragraph(e, this));

		public virtual IEnumerable<Paragraph> AllParagraphsRecursive => Xml.Descendants(Namespaces.w + "p")
			.Select(e => new Paragraph(e, this));

		public virtual IEnumerable<Table> Tables => Xml.Elements(Namespaces.w + "tbl")
			.Select(t => new Table(t, this));

		public virtual IEnumerable<Table> AllTablesRecursive => Xml.Descendants(Namespaces.w + "tbl")
			.Select(t => new Table(t, this));

		public PackageRelationship GetImageRelationship(string relationshipId)
		{
			if (!PackagePart.RelationshipExists(relationshipId))
				throw new ArgumentOutOfRangeException($"Package relationship ${relationshipId} does not exist", nameof(relationshipId));

			return PackagePart.GetRelationship(relationshipId);
		}

		private PackageRelationship EnsureImageRelationship(Image image)
		{
			PackagePart.EnsureRelationshipsPart();
			var imagePackagePartUri = image.PackagePartUri;

			return PackagePart.GetRelationshipsByType(RelationshipTypes.Image).SingleOrDefault(r => r.TargetUri.OriginalString == imagePackagePartUri.OriginalString)
				   ?? PackagePart.CreateRelationship(imagePackagePartUri, TargetMode.Internal, RelationshipTypes.Image);
		}

		public virtual Paragraph AppendParagraph()
		{
			var xml = new XElement(XName.Get("p", Namespaces.w.NamespaceName));
			Xml.Add(xml);
			return new Paragraph(xml, this);
		}

		public Paragraph AppendParagraph(string text)
		{
			var paragraph = AppendParagraph();
			paragraph.AppendText(text);
			return paragraph;
		}

		public virtual Table AppendTable(CreateTableParameters parameters)
		{
			Document.Styles.EnsureStyle(parameters.Style, StyleType.Table);
			var table = Table.Create(parameters, this);
			Xml.Add(table.Xml);
			return table;
		}

		public InlinePicture WrapImageIntoInlinePicture(Image image, string name, string description, int widthPx, int heightPx)
		{
			var imageRelationship = EnsureImageRelationship(image);
			var newDocPrId = Document.GetNewDrawingId();

			var widthEmu = widthPx * UnitHelper.EmusPerPixel;
			var heightEmu = heightPx * UnitHelper.EmusPerPixel;
			var imageRelationshipId = imageRelationship.Id;

			var drawing = InlinePicture.CreateInlineDrawing(newDocPrId, imageRelationshipId, name, description, widthEmu, heightEmu);

			return new InlinePicture(drawing, image);
		}

		public virtual IEnumerable<ParagraphSearchResult> FindText(string stringToSeek, StringComparison stringComparison)
		{
			return AllParagraphsRecursive.SelectMany(p => p.FindText(stringToSeek, stringComparison));
		}
	}
}