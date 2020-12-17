using System.IO.Packaging;
using System.Xml.Linq;
using Wordroller.Packages;

namespace Wordroller.Content.Abstract
{
	public abstract class OptionalDocumentPartWrapper
	{
		protected readonly WordDocumentPackage Package;
		protected PackagePart? PackagePart;
		protected XDocument? XmlDocument;

		protected OptionalDocumentPartWrapper(PackagePart? packagePart, WordDocumentPackage package)
		{
			PackagePart = packagePart;
			Package = package;

			if(PackagePart != null)
				XmlDocument = PackagePartHelper.ReadPackagePart(PackagePart);
		}

		internal void SavePackagePart()
		{
			if (PackagePart != null && XmlDocument != null)
				PackagePartHelper.SavePackagePart(PackagePart, XmlDocument);
		}

		protected abstract XElement GetOrCreatePackagePartAndDocumentRoot();
	}
}