using Wordroller.Content.Lists;
using Wordroller.Styles;

public class ListDefinitionModel
{
	public ListDefinitionModel(ListDefinition listDefinition, NumberingStyle? style, NumberingStyle? numberingStyle, int? actualAbstractNumId)
	{
		ListDefinition = listDefinition;
		Style = style;
		NumberingStyle = numberingStyle;
		ActualAbstractNumId = actualAbstractNumId;
		AbstractNumId = listDefinition.AbstractNumId;
	}

	public int AbstractNumId { get; }
	public ListDefinition ListDefinition { get; }
	public NumberingStyle? Style { get; }
	public NumberingStyle? NumberingStyle { get; }
	public int? ActualAbstractNumId { get; }
}

public class ListModel
{
	public ListModel(List list)
	{
		List = list;
		NumId = list.NumId;
	}

	public int NumId { get; }
	public List List { get; }
}