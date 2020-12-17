using System;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Utility.Xml;

namespace Wordroller.Packages
{
	public class WordDocumentPackage : IDisposable
	{
		private readonly MemoryStream memoryStream;
		private readonly Package package;

		internal WordDocumentPackage(CultureInfo culture, bool isTemplate = false)
		{
			memoryStream = new MemoryStream();
			package = Package.Open(memoryStream, FileMode.Create, FileAccess.ReadWrite);
			var documentPart = CreateDefaultDocumentPart(isTemplate);
			DocumentPart = documentPart;
			SettingsPart = CreateDefaultSettingsPart(culture, documentPart);
			StylesPart = CreateDefaultStylesPart(culture, documentPart);
		}

		internal WordDocumentPackage(Stream stream)
		{
			memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			package = Package.Open(memoryStream, FileMode.Open, FileAccess.ReadWrite);

			var officeDocumentRelationship = package.GetRelationshipsByType(RelationshipTypes.OfficeDocument).Single();

			DocumentPart = package.GetPart(PackagePartHelper.EnsureCorrectUri(officeDocumentRelationship.TargetUri));
			SettingsPart = GetSinglePackagePart(RelationshipTypes.Settings);
			StylesPart = GetSinglePackagePart(RelationshipTypes.Styles);
			NumberingPart = FindSinglePackagePart(RelationshipTypes.Numbering);
		}

		public PackagePart DocumentPart { get; }
		public PackagePart SettingsPart { get; }
		public PackagePart StylesPart { get; }
		public PackagePart? NumberingPart { get; }

		public PackagePart GetPart(PackageRelationship relationship)
		{
			var packagePartUri = PackagePartHelper.EnsureCorrectUri(relationship.TargetUri);
			if (!package.PartExists(packagePartUri)) throw new Exception($"Package part {packagePartUri} does not exist");
			return package.GetPart(packagePartUri);
		}

		public void DeletePart(PackageRelationship relationship)
		{
			var packagePartUri = PackagePartHelper.EnsureCorrectUri(relationship.TargetUri);
			if (!package.PartExists(packagePartUri)) throw new Exception($"Package part {packagePartUri} does not exist");
			package.DeletePart(packagePartUri);
		}

		public (PackagePart, PackageRelationship) AddHeaderPart()
		{
			var safePartIndex = package.GetParts().Count() + 1;
			var partUri = new Uri($"/word/header{safePartIndex}.xml", UriKind.Relative);
			var part = package.CreatePart(partUri, ContentTypes.Header, CompressionOption.Normal);
			var content = XmlResourceHelper.GetXmlResource("Wordroller.Packages.Defaults.header.xml");
			PackagePartHelper.SavePackagePart(part, content);
			var relationship = DocumentPart.CreateRelationship(part.Uri, TargetMode.Internal, RelationshipTypes.Header);
			return (part, relationship);
		}

		public (PackagePart, PackageRelationship) AddFooterPart()
		{
			var safePartIndex = package.GetParts().Count() + 1;
			var partUri = new Uri($"/word/footer{safePartIndex}.xml", UriKind.Relative);
			var part = package.CreatePart(partUri, ContentTypes.Footer, CompressionOption.Normal);
			var content = XmlResourceHelper.GetXmlResource("Wordroller.Packages.Defaults.footer.xml");
			PackagePartHelper.SavePackagePart(part, content);
			var relationship = DocumentPart.CreateRelationship(part.Uri, TargetMode.Internal, RelationshipTypes.Footer);
			return (part, relationship);
		}

		public PackagePart AddImagePart(Stream imageStream, string contentType)
		{
			var extension = contentType.Substring(contentType.LastIndexOf("/", StringComparison.Ordinal) + 1);
			var imgPartUriPath = $"/word/media/{Guid.NewGuid()}.{extension}";
			var part = package.CreatePart(new Uri(imgPartUriPath, UriKind.Relative), contentType, CompressionOption.Normal);
			using var partStream = new PackagePartStream(part.GetStream(FileMode.Create, FileAccess.Write));
			imageStream.CopyTo(partStream);
			return part;
		}

		public Stream GetPackagePartStream(Uri uri, FileMode mode, FileAccess access)
		{
			return new PackagePartStream(package.GetPart(PackagePartHelper.EnsureCorrectUri(uri)).GetStream(mode, access));
		}

		public void Save(Stream stream)
		{
			package.Close();
			memoryStream.Position = 0;
			memoryStream.WriteTo(stream);
			memoryStream.Flush();
		}

		public void Dispose()
		{
			package.Close();
			memoryStream.Dispose();
		}

		internal PackagePart CreateDefaultNumberingPart()
		{
			var packagePartUri = new Uri("/word/numbering.xml", UriKind.Relative);
			var part = package.CreatePart(packagePartUri, ContentTypes.Numbering, CompressionOption.Normal);
			DocumentPart.CreateRelationship(packagePartUri, TargetMode.Internal, RelationshipTypes.Numbering);
			var content = XmlResourceHelper.GetXmlResource("Wordroller.Packages.Defaults.numbering.xml");
			PackagePartHelper.SavePackagePart(part, content);
			return part;
		}

		private PackagePart GetSinglePackagePart(string relationshipType)
		{
			return FindSinglePackagePart(relationshipType) ?? throw new Exception($"The package part does not contain a single relationship of type {relationshipType}");
		}

		private PackagePart? FindSinglePackagePart(string relationshipType)
		{
			var packageRelationship = DocumentPart.GetRelationships().SingleOrDefault(r => r.RelationshipType == relationshipType);
			return packageRelationship != null ? package.GetPart(PackagePartHelper.EnsureCorrectUri(packageRelationship.TargetUri)) : null;
		}

		private PackagePart CreateDefaultDocumentPart(bool isTemplate)
		{
			var contentType = isTemplate ? ContentTypes.Template : ContentTypes.Document;
			var documentPart = package.CreatePart(new Uri("/word/document.xml", UriKind.Relative), contentType, CompressionOption.Normal);
			package.CreateRelationship(documentPart.Uri, TargetMode.Internal, RelationshipTypes.OfficeDocument);
			var document = XmlResourceHelper.GetXmlResource("Wordroller.Packages.Defaults.document.xml");
			PackagePartHelper.SavePackagePart(documentPart, document);
			return documentPart;
		}

		private PackagePart CreateDefaultSettingsPart(CultureInfo culture, PackagePart documentPart)
		{
			var packagePartUri = new Uri("/word/settings.xml", UriKind.Relative);
			var part = package.CreatePart(packagePartUri, ContentTypes.Settings, CompressionOption.Normal);
			documentPart.CreateRelationship(packagePartUri, TargetMode.Internal, RelationshipTypes.Settings);
			var settings = XmlResourceHelper.GetXmlResource("Wordroller.Packages.Defaults.settings.xml");

			var themeFontLang = settings.Root?.Element(XName.Get("themeFontLang", Namespaces.w.NamespaceName));
			themeFontLang?.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), culture);
			PackagePartHelper.SavePackagePart(part, settings);
			return part;
		}

		private PackagePart CreateDefaultStylesPart(CultureInfo culture, PackagePart documentPart)
		{
			var packagePartUri = new Uri("/word/styles.xml", UriKind.Relative);
			var part = package.CreatePart(packagePartUri, ContentTypes.Styles, CompressionOption.Normal);
			documentPart.CreateRelationship(packagePartUri, TargetMode.Internal, RelationshipTypes.Styles);
			var content = XmlResourceHelper.GetXmlResource("Wordroller.Packages.Defaults.styles.xml");

			var lang = content.Root
				?.Element(XName.Get("docDefaults", Namespaces.w.NamespaceName))
				?.Element(XName.Get("rPrDefault", Namespaces.w.NamespaceName))
				?.Element(XName.Get("rPr", Namespaces.w.NamespaceName))
				?.Element(XName.Get("lang", Namespaces.w.NamespaceName))
				?? throw new Exception("The default styles part is corrupt");

			lang.SetAttributeValue(XName.Get("val", Namespaces.w.NamespaceName), culture.ToString());
			PackagePartHelper.SavePackagePart(part, content);
			return part;
		}
	}
}