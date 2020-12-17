using System.IO;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public abstract class TestsBase
	{
		protected readonly ITestOutputHelper TestOutputHelper;

		protected TestsBase(ITestOutputHelper testOutputHelper)
		{
			TestOutputHelper = testOutputHelper;
		}

		protected void SaveTempDocument(WordDocument document, string fileName)
		{
			var path = Path.Combine(TestHelper.GetTempDirectory(), fileName);
			TestOutputHelper.WriteLine(path);
			using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
			document.Save(fileStream);
		}
	}
}