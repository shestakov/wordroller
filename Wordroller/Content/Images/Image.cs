using System;

 namespace Wordroller.Content.Images
{
	public class Image
	{
		public Uri PackagePartUri { get; }

		internal Image(Uri packagePartUri)
		{
			PackagePartUri = packagePartUri;
		}
	}
}