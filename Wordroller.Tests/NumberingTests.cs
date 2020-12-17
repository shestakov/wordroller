using System.Linq;
using Wordroller.Content.Lists;
using Wordroller.Content.Properties.Sections.PageSizes;
using Xunit;
using Xunit.Abstractions;

namespace Wordroller.Tests
{
	public class NumberingTests : TestsBase
	{
		public NumberingTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
		{
		}

		[Fact]
		public void CreateList()
		{
			using var document = TestHelper.CreateNewDocument();
			var section = document.Body.Sections.First();
			var w = section.Properties.Size.WidthTw;
			var h = section.Properties.Size.HeightTw;
			section.Properties.Size.Orientation = PageOrientation.Landscape;
			section.Properties.Size.WidthTw = h;
			section.Properties.Size.HeightTw = w;

			var firstDefinition = document.NumberingCollection.CreateListDefinition("Multi-level List", MultiLevelType.HybridMultiLevel, "41A6377C", "BB30A3F6");

			firstDefinition.CreateLevel(0, 1, NumberFormat.Bullet, ListNumberAlignment.Left, 720, 360, "%1.", true, "0409000F");
			firstDefinition.CreateLevel(1, 1, NumberFormat.Chicago, ListNumberAlignment.Left, 1440, 360, "%1.%2.", true, "04090019");
			firstDefinition.CreateLevel(2, 1, NumberFormat.Decimal, ListNumberAlignment.Right, 2160, 180, "%1.%2.%3.", true, "0409001B");
			firstDefinition.CreateLevel(3, 1, NumberFormat.None, ListNumberAlignment.Left, 2880, 360, "%1.%2.%3.%4.", true, "0409000F");
			firstDefinition.CreateLevel(4, 1, NumberFormat.CardinalText, ListNumberAlignment.Left, 3600, 360, "%1.%2.%3.%4.%5.", true, "04090019");
			firstDefinition.CreateLevel(5, 1, NumberFormat.LowerRoman, ListNumberAlignment.Right, 4320, 180, "%1.%2.%3.%4.%5.%6.", true, "0409001B");
			firstDefinition.CreateLevel(6, 1, NumberFormat.DecimalZero, ListNumberAlignment.Left, 5040, 360, "%1.%2.%3.%4.%5.%6.%7.", true, "0409000F");
			firstDefinition.CreateLevel(7, 1, NumberFormat.OrdinalText, ListNumberAlignment.Left, 5762, 360, "%1.%2.%3.%4.%5.%6.%7.%8.", true, "04090019");
			firstDefinition.CreateLevel(8, 1, NumberFormat.DecimalEnclosedCircle, ListNumberAlignment.Right, 6480, 180, "%1.%2.%3.%4.%5.%6.%7.%8.%9.", true, "0409001B");

			var listDefinitions = document.NumberingCollection.ListDefinitions.ToArray();

			TestOutputHelper.WriteLine($"{listDefinitions.Length} list definitions total.");

			foreach (var definition in listDefinitions)
			{
				TestOutputHelper.WriteLine(definition.Name);

				foreach (var level in definition.Levels.Values)
				{
					TestOutputHelper.WriteLine($"{level.Level}: {level.Format}");
				}
			}

			var listDefinition = document.NumberingCollection.ListDefinitions.First();

			var list = document.NumberingCollection.CreateList(listDefinition);

			var p0 = section.AppendParagraph("Level 0").IncludeIntoList(list, 0);
			var p1 = section.AppendParagraph("Level 1").IncludeIntoList(list, 1);
			var p2 = section.AppendParagraph("Level 2").IncludeIntoList(list, 2);
			var p3 = section.AppendParagraph("Level 3").IncludeIntoList(list, 3);
			var p4 = section.AppendParagraph("Level 4").IncludeIntoList(list, 4);
			var p5 = section.AppendParagraph("Level 5").IncludeIntoList(list, 5);
			var p6 = section.AppendParagraph("Level 6").IncludeIntoList(list, 6);
			var p7 = section.AppendParagraph("Level 7").IncludeIntoList(list, 7);
			var p8 = section.AppendParagraph("Level 8").IncludeIntoList(list, 8);

			var p9 = section.AppendParagraph("Level 0").IncludeIntoList(list, 0);
			p9.ExcludeFromList();

			SaveTempDocument(document, "Lists_Multilevel.docx");
		}
	}
}