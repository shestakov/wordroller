using Wordroller;
using Wordroller.Apps.Console;
using Wordroller.Content.Lists;
using Wordroller.Content.Text;
using Wordroller.Styles;

if (args.Length < 1)
{
	PrintHelp();
	return;
}

var fileNameArgument = args[0];
var fileName = Path.IsPathRooted(fileNameArgument) ? fileNameArgument : Path.Combine(AppContext.BaseDirectory, fileNameArgument);
Log($"Processing file {fileNameArgument}");

using var readStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
using var document = new WordDocument(readStream);
var lists = document.NumberingCollection.Lists.ToDictionary(d => d.NumId, d => new ListModel(d));
var listDefinitions = document.NumberingCollection.ListDefinitions.ToDictionary(d => d.AbstractNumId, d => MakeListDefinitionModel(d, document.Styles, lists));
var utilizedListDefinitions = new HashSet<int>();
var utilizedStyles = new HashSet<string>();
var utilizedNumStyles = new HashSet<string>();
var utilizedTableStyles = new HashSet<string>();
var listIndexes = new Dictionary<int, ListIndex>();

Log($"Lists found: {lists.Count}");
foreach (var list in lists.Values)
{
	var abstractNumId = list.List.AbstractNumId;
	listIndexes.Add(list.NumId, new ListIndex(list.NumId));
	Log($"List {list.NumId} on {abstractNumId}");
	if (list.NumId is 3) Log(list.List.Xml.ToString());
	utilizedListDefinitions.Add(abstractNumId);
}

Log($"List definitions found: {listDefinitions.Count}");
foreach (var definition in listDefinitions.Values)
{
	var abstractNumId = definition.AbstractNumId;
	var listDefinition = definition.ListDefinition;
	Log(
		$"{(utilizedListDefinitions.Contains(abstractNumId) ? "Used" : "Unused")} List Definition {abstractNumId} named \"{abstractNumId}\" {listDefinition.MultiLevelType} StyleLink \"{listDefinition.StyleLink}\" NumStyleLink \"{listDefinition.NumStyleLink}\" {(definition.ActualAbstractNumId != null ? "ActualAbstractNumId " + definition.ActualAbstractNumId : "")}");
	// Log(definition.Xml.ToString());
	if (!string.IsNullOrEmpty(listDefinition.StyleLink)) utilizedStyles.Add(listDefinition.StyleLink!);
	if (!string.IsNullOrEmpty(listDefinition.NumStyleLink)) utilizedNumStyles.Add(listDefinition.NumStyleLink!);
}

foreach (var paragraph in document.Body.Content.AllParagraphsRecursive)
{
	var paragraphListBinding = paragraph.Properties.ListBinding;

	var paragraphStyleId = paragraph.Properties.StyleId;
	var style = paragraphStyleId != null ? document.Styles.GetParagraphStyle(paragraphStyleId) : null;
	var styleListBinding = style?.ParagraphProperties.ListBinding;

	if (paragraphListBinding != null && styleListBinding != null)
		Log($"Duplicate list binging {paragraphListBinding.NumId} and {styleListBinding.NumId}:" + ParagraphText(paragraph));

	var effectiveListBinding = paragraphListBinding ?? styleListBinding;

	int? numId;
	int? directNumId;
	int level;
	ListDefinitionLevel? levelDefinition;

	if (styleListBinding != null)
	{
		numId = styleListBinding.NumId;
		directNumId = numId;
		var referencedList = lists[numId.Value];
		var referencedListDefinition = listDefinitions[referencedList.List.AbstractNumId];
		levelDefinition = referencedListDefinition.ListDefinition.Levels.Values.FirstOrDefault(l => l.StyleId == paragraphStyleId);
		level = levelDefinition?.Level ?? -1;
		var numberingStyleId = referencedListDefinition.ListDefinition.StyleLink!;
		var numberingStyle = document.Styles.GetNumberingStyle(numberingStyleId);
		var referencedNumId = numberingStyle.NumId;
		numId = referencedNumId;
	}
	else
	{
		numId = effectiveListBinding?.NumId;
		directNumId = numId;
		level = effectiveListBinding?.Level ?? -1;

		if (numId.HasValue)
		{
			var referencedList = lists[numId.Value];
			var referencedListDefinition = listDefinitions[referencedList.List.AbstractNumId];
			levelDefinition = referencedListDefinition.ListDefinition.Levels[level];
			var levelStart = levelDefinition.Start;
			var levelStyleId = levelDefinition.StyleId;
			var numberingStyleId = referencedListDefinition.ListDefinition.StyleLink;

			if (numberingStyleId != null)
			{
				var numberingStyle = document.Styles.GetNumberingStyle(numberingStyleId);
				var referencedNumId = numberingStyle.NumId;
				numId = referencedNumId;
			}
		}
	}

	if (numId != null)
	{
		var abstractNumId = lists[numId.Value].List.AbstractNumId;

		var index = listIndexes[numId.Value];
		var listItem = index.AddParagraph(level, paragraph);

		var run = paragraph.AppendText($" {listItem.IndexText} [{directNumId}>{numId}|L{level}]");
		run.Properties.Color.Color = "ff0000";

		if (numId != 6)
		{
			Log($"{numId}: " + ParagraphText(paragraph));
		}
	}
}

foreach (var styleId in utilizedStyles)
{
	var style = document.Styles.GetNumberingStyle(styleId);
	Log(style.Xml.ToString());
}

var outputFileName = Path.Combine(Path.GetDirectoryName(fileName)!, Path.GetFileNameWithoutExtension(fileName) + ".wl" + Path.GetExtension(fileName));
using var writeStream = new FileStream(outputFileName, FileMode.OpenOrCreate, FileAccess.Write);
document.Save(writeStream);

string ParagraphText(Paragraph paragraph1)
{
	return string.Concat(paragraph1.Content.Select(r => r.Text));
}

void Log(string text)
{
	Console.WriteLine(text);
}

void PrintHelp()
{
	Log(@"Usage: dotnet wordroller file_name.docx");
}

ListDefinitionModel MakeListDefinitionModel(ListDefinition listDefinition, StyleCollection styleCollection, Dictionary<int, ListModel> listModels)
{
	var style = string.IsNullOrEmpty(listDefinition.StyleLink) ? null : styleCollection.GetNumberingStyle(listDefinition.StyleLink);
	var numStyle = string.IsNullOrEmpty(listDefinition.NumStyleLink) ? null : styleCollection.GetNumberingStyle(listDefinition.NumStyleLink);
	var styleNumId = style?.NumId ?? numStyle?.NumId;
	var referencedList = styleNumId != null ? listModels[styleNumId.Value] : null;
	var actualAbstractNumId = referencedList?.List.AbstractNumId;
	var listDefinitionModel = new ListDefinitionModel(listDefinition, style, numStyle, actualAbstractNumId);
	return listDefinitionModel;
}