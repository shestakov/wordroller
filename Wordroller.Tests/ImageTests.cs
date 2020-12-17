using System.Linq;
using Wordroller.Content.Images;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class ImageTests : TestsBase
	{
		public ImageTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void AppendImage()
		{
			using var document = TestHelper.CreateNewDocument();
			using var imageStream = TestHelper.GetResourceStream("Wordroller.Tests.Resources.Image_200x100.png");
			var image = document.AddImage(imageStream, KnownImageContentTypes.Png);

			var section = document.Body.Sections.First();
			var picture = section.WrapImageIntoInlinePicture(image, "wordroller", "This is a sample image", 200, 100);

			var paragraph = section.AppendParagraph();
			paragraph.AppendPicture(picture);

			SaveTempDocument(document, "Image_Append.docx");
		}

		[Fact]
		public void AppendAndModifyImage()
		{
			using var document = TestHelper.CreateNewDocument();
			using var imageStream = TestHelper.GetResourceStream("Wordroller.Tests.Resources.Image_200x100.png");
			var image = document.AddImage(imageStream, KnownImageContentTypes.Png);

			var section = document.Body.Sections.First();
			var picture = section.WrapImageIntoInlinePicture(image, "wordroller", "This is a sample image", 200, 100);

			var paragraph = section.AppendParagraph();
			paragraph.AppendPicture(picture);

			picture.Name = "WORDROLLER";
			picture.Description = "This is an updated description";
			picture.WidthEmu = (int) (picture.WidthEmu * 1.5);
			picture.HeightEmu = (int) (picture.HeightEmu * 1.5);

			SaveTempDocument(document, "Image_AppendAndModify.docx");
		}
	}
}