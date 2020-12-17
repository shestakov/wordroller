using System.IO.Packaging;
using System.Xml.Linq;
using Wordroller.Packages;

namespace Wordroller.Content.Abstract
{
	public abstract class DocumentPartWrapper
	{
		protected readonly PackagePart PackagePart;
		protected readonly XDocument XmlDocument;

		protected DocumentPartWrapper(PackagePart packagePart)
		{
			PackagePart = packagePart;
			XmlDocument = PackagePartHelper.ReadPackagePart(PackagePart);
		}

		internal void SavePackagePart()
		{
			PackagePartHelper.SavePackagePart(PackagePart, XmlDocument);
		}
	}
}