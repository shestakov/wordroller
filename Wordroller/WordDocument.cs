using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Wordroller.Content.Body;
using Wordroller.Content.HeadersAndFooters;
using Wordroller.Content.Images;
using Wordroller.Content.Lists;
using Wordroller.Packages;
using Wordroller.Styles;

namespace Wordroller
{
	public class WordDocument : IDisposable
	{
		private readonly WordDocumentPackage package;
		private StyleCollection? styleCollection;
		private readonly IStyleProvider? externalStyleProvider = null;

		internal FooderCollection FooderCollection { get; }

		public WordDocument(CultureInfo culture, bool isTemplate = false)
		{
			package = new WordDocumentPackage(culture, isTemplate);
			Body = new DocumentBody(this, package.DocumentPart);
			FooderCollection = new FooderCollection(package, this);
			NumberingCollection = new NumberingCollection(null, package);
		}

		public WordDocument(Stream stream)
		{
			package = new WordDocumentPackage(stream);
			Body = new DocumentBody(this, package.DocumentPart);
			FooderCollection = new FooderCollection(package, this);
			NumberingCollection = new NumberingCollection(package.NumberingPart, package);
		}

		public DocumentBody Body { get; }
		public StyleCollection Styles => styleCollection ??= new StyleCollection(package.StylesPart, externalStyleProvider);
		public NumberingCollection NumberingCollection { get; }

		public void Save(Stream stream)
		{
			Body.SavePackagePart();
			FooderCollection.SavePackageParts();
			Styles.SavePackagePart();
			NumberingCollection.SavePackagePart();
			package.Save(stream);
		}

		public Image AddImage(Stream imageStream, string contentType)
		{
			var part = package.AddImagePart(imageStream, contentType);
			return new Image(part.Uri);
		}

		public Stream GetImageStream(Image image, FileMode mode, FileAccess access)
		{
			return package.GetPackagePartStream(image.PackagePartUri, mode, access);
		}

		public void Dispose()
		{
			package.Dispose();
		}

		public int GetNewDrawingId()
		{
			return new[]
				{
					1,
					Body.GetMaxDrawingId(),
					FooderCollection.GetMaxDrawingId()
					// TODO: Here go other document parts that may contain drawings
				}
				.Max();
		}
	}
}