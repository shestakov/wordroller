using System.IO;

namespace Wordroller.Utility
{
	public static class WordDocumentHelper
	{
		public static WordDocument LoadFromFile(string filename)
		{
			using var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			return new WordDocument(fileStream);
		}
	}
}