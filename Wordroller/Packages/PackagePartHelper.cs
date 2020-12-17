using System;
using System.IO;
using System.IO.Packaging;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Packages
{
	public static class PackagePartHelper
	{
		public static void EnsureRelationshipsPart(this PackagePart packagePart)
		{
			var package = packagePart.Package;
			var path = packagePart.Uri.OriginalString.Replace("/word/", "");
			var relationshipsPartUri = new Uri("/word/_rels/" + path + ".rels", UriKind.Relative);

			if (package.PartExists(relationshipsPartUri)) return;

			var part = package.CreatePart(relationshipsPartUri, ContentTypes.Relationships, CompressionOption.Maximum);
			using TextWriter textWriter = new StreamWriter(new PackagePartStream(part.GetStream()));

			var document = new XDocument
			(
				new XDeclaration("1.0", "UTF-8", "yes"),
				new XElement(Namespaces.rel + "Relationships")
			);

			document.Save(textWriter);
		}

		public static Uri EnsureCorrectUri(Uri packagePartUri)
		{
			// Fixing possible URI issues, thanks Novacode.DocX and others
			return new Uri("/word/" + packagePartUri.OriginalString
				.Replace("/word/", "")
				.Replace("word/", "")
				.Replace("file://", ""), UriKind.Relative);
		}

		public static XDocument ReadPackagePart(PackagePart part)
		{
			using var partStream = part.GetStream();
			using TextReader textReader = new StreamReader(partStream);
			var xDocument = XDocument.Load(textReader);
			return xDocument;
		}

		public static void SavePackagePart(PackagePart part, XDocument document)
		{
			using var stream = new PackagePartStream(part.GetStream(FileMode.Create, FileAccess.Write));
			using TextWriter textWriter = new StreamWriter(stream);
			document.Save(textWriter, SaveOptions.None);
		}
	}
}