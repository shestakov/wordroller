using System.IO;
using System.Linq;
using Wordroller.Content.Images;
using Wordroller.Content.Text.RunContent;
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
			var section = document.Body.Sections.First();

			using var imageStream1 = TestHelper.GetResourceStream("Wordroller.Tests.Resources.Image_200x100.png");
			var image1 = document.AddImage(imageStream1, KnownImageContentTypes.Png);
			var picture1 = section.WrapImageIntoInlinePicture(image1, "wordroller", "This is a sample image", 200, 100);
			var paragraph1 = section.AppendParagraph();
			paragraph1.AppendPicture(picture1);

			using var imageStream2 = TestHelper.GetResourceStream("Wordroller.Tests.Resources.Image_200x100_inverse.png");
			var image2 = document.AddImage(imageStream2, KnownImageContentTypes.Png);
			var picture2 = section.WrapImageIntoInlinePicture(image2, "wordroller", "This is a sample image", 200, 100);
			var paragraph2 = section.AppendParagraph();
			paragraph2.AppendPicture(picture2);

			SaveTempDocument(document, "Image_Append.docx");
		}

		[Fact]
		public void ReplaceImage()
		{
			using var documentWithoutImage = TestHelper.CreateNewDocument();
			using var imageStream = TestHelper.GetResourceStream("Wordroller.Tests.Resources.Image_200x100.png");
			var image = documentWithoutImage.AddImage(imageStream, KnownImageContentTypes.Png);

			var section = documentWithoutImage.Body.Sections.First();
			var picture = section.WrapImageIntoInlinePicture(image, "wordroller", "This is a sample image", 200, 100);
			section.AppendParagraph().AppendPicture(picture);

			using var writeStream = new MemoryStream();
			documentWithoutImage.Save(writeStream);
			var documentBytes = writeStream.GetBuffer();

			using var readStream = new MemoryStream(documentBytes);
			using var documentWithImage = new WordDocument(readStream);
			var drawings = documentWithImage.Body.Content.AllParagraphsRecursive.SelectMany(p =>
				p.Content.SelectMany(r => r.Content.OfType<RunDrawing>()));
			var drawing = drawings.Single();
			Assert.NotNull(drawing.InlinePicture);
			var existingImage = drawing.InlinePicture!.Image;
			using var newImageReadStream = TestHelper.GetResourceStream("Wordroller.Tests.Resources.Image_200x100_inverse.png");
			using var newImageWriteStream = documentWithImage.GetImageStream(existingImage, FileMode.Create, FileAccess.Write);
			newImageReadStream.CopyTo(newImageWriteStream);
			SaveTempDocument(documentWithImage, "Image_Replace.docx");
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
			picture.WidthEmu = (int)(picture.WidthEmu * 1.5);
			picture.HeightEmu = (int)(picture.HeightEmu * 1.5);

			SaveTempDocument(document, "Image_AppendAndModify.docx");
		}
	}
}