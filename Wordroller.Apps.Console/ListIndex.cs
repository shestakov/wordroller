using Wordroller.Content.Text;

namespace Wordroller.Apps.Console;

public class ListIndex
{
	public int NumId { get; }
	private readonly Stack<ListItem> stack = new();

	public ListItem AddParagraph(int level, Paragraph paragraph)
	{
		var lastItem = stack.TryPop(out var item) ? item : null;
		ListItem newItem;

		if (lastItem == null)
		{
			newItem = new ListItem(paragraph, 0, new[] { 1 }, new List<ListItem>());
			Items.Add(newItem);
			stack.Push(newItem);
		}
		else if (level == lastItem.Level)
		{
			newItem = new ListItem(paragraph, level, IncrementIndex(lastItem.Index), new List<ListItem>());
			Items.Add(newItem);
			stack.Push(newItem);
		}
		else if (level > lastItem.Level)
		{
			var levelIncrement = level - lastItem.Level;
			var levelError = levelIncrement > 1;
			var index = IncrementIndexLevel(lastItem.Index, levelIncrement);
			newItem = new ListItem(paragraph, level, index, new List<ListItem>(), levelError);
			lastItem.Children.Add(newItem);
			stack.Push(lastItem);
			stack.Push(newItem);
		}
		else
		{
			var lastIndex = lastItem.Index;
			var levelDecrement = lastItem.Level - level;

			for (var i = 0; i < levelDecrement; i++)
			{
				lastItem = stack.TryPop(out item) ? item : null;
			}

			if (lastItem != null) lastIndex = lastItem.Index;
			newItem = new ListItem(paragraph, level, IncrementIndex(lastIndex), new List<ListItem>());

			if (lastItem != null) lastItem.Children.Add(newItem);
			else Items.Add(newItem);

			stack.Push(newItem);
		}

		return newItem;
	}

	private int[] IncrementIndexLevel(int[] index, int levelIncrement)
	{
		var result = new int[index.Length + levelIncrement];
		Array.Copy(index, result, index.Length);
		result[^1] = 1;

		for (var i = 1; i < levelIncrement; i++)
			result[^(1 + i)] = -1;

		return result;
	}

	public ListIndex(int numId)
	{
		NumId = numId;
		Items = new List<ListItem>(0);
	}

	public List<ListItem> Items { get; }

	private int[] IncrementIndex(int[] index)
	{
		var result = new int[index.Length];
		Array.Copy(index, result, index.Length);
		result[^1] += 1;
		return result;
	}
}

public class ListItem
{
	public ListItem(Paragraph paragraph, int level, int[] index, List<ListItem> children, bool levelError = false)
	{
		Paragraph = paragraph;
		Index = index;
		Children = children;
		LevelError = levelError;
		Level = level;
	}

	public Paragraph Paragraph { get; }
	public int[] Index { get; }
	public List<ListItem> Children { get; }
	public bool LevelError { get; }
	public int Level { get; }

	public string IndexText => string.Join(".", Index.Select(i => i.ToString()));
}