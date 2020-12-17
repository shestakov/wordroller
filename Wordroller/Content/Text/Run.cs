using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Drawings;
using Wordroller.Content.Properties.Runs;
using Wordroller.Content.Text.RunContent;
using Wordroller.Content.Text.Search;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text
{
	public class Run : XmlElementWrapper, IRunPropertiesContainer
	{
		private static readonly Regex splitRegex = new Regex("(\n|\t|\u00AD|\u2011)");
		private readonly DocumentContentContainer contentContainer;

		internal Run(XElement xml, DocumentContentContainer contentContainer) : base(xml)
		{
			this.contentContainer = contentContainer;
		}

		public RunProperties Properties => new RunProperties(this, Xml.Element(XName.Get("rPr", Namespaces.w.NamespaceName)));

		public RunText AddText(string text)
		{
			var runText = RunText.Create(text);
			Xml.Add(runText.Xml);
			return runText;
		}

		public TextBreak AddBreak(TextBreakType textBreakType = TextBreakType.TextWrapping, TextBreakClear textBreakClear = TextBreakClear.None)
		{
			var br = TextBreak.Create(textBreakType, textBreakClear);
			Xml.Add(br.Xml);
			return br;
		}

		public Tab AddTab()
		{
			var tab = Tab.Create();
			Xml.Add(tab.Xml);
			return tab;
		}

		public SoftHyphen AddSoftHyphen()
		{
			var softHyphen = SoftHyphen.Create();
			Xml.Add(softHyphen.Xml);
			return softHyphen;
		}

		public NoBreakHyphen AddNoBreakHyphen()
		{
			var noBreakHyphen = NoBreakHyphen.Create();
			Xml.Add(noBreakHyphen.Xml);
			return noBreakHyphen;
		}

		public InlinePicture AddPicture(InlinePicture inlinePicture)
		{
			Xml.Add(inlinePicture.Xml);
			return inlinePicture;
		}

		public IEnumerable<RunContentElementBase> Content
		{
			get
			{
				var elements = Xml.Elements();
				foreach (var e in elements)
				{
					var name = e.Name.LocalName;

					RunContentElementBase? item = name switch
					{
						"t" => new RunText(e),
						"br" => new TextBreak(e),
						"cr" => new CarriageReturn(e),
						"lastRenderedPageBreak" => new LastRenderedPageBreak(e),
						"noBreakHyphen" => new NoBreakHyphen(e),
						"ptab" => new AbsolutePositionTab(e),
						"softHyphen" => new SoftHyphen(e),
						"sym" => new Symbol(e),
						"tab" => new Tab(e),
						"drawing" => new RunDrawing(e, contentContainer),
						"delText" => null, // TODO: maybe some day...
						_ => null
					};

					if (item == null) continue;

					yield return item;
				}
			}
		}

		internal static Run Create(string text, DocumentContentContainer contentContainer)
		{
			var xml = new XElement(Namespaces.w + "r", BuildContent(text).Select(e => e.Xml));
			return new Run(xml, contentContainer);
		}

		private static IEnumerable<RunContentElementBase> BuildContent(string text)
		{
			if (text == null) throw new ArgumentNullException(nameof(text));
			var parts = splitRegex.Split(text);
			return parts.Select<string, RunContentElementBase>(part =>
			{
				return part switch
				{
					"\n" => TextBreak.Create(),
					"\t" => Tab.Create(),
					"\u00AD" => SoftHyphen.Create(),
					"\u2011" => NoBreakHyphen.Create(),
					_ => RunText.Create(part)
				};
			});
		}

		internal IndexedRunContentElement[] IndexedContent => Content
			.Aggregate(new {Elements = new List<IndexedRunContentElement>(), Index = 0}, (acc, e) =>
			{
				var index = acc.Index;
				var length = e.ToText().Length;
				var element = new IndexedRunContentElement(e, index, length);
				acc.Elements.Add(element);
				return new {acc.Elements, Index = acc.Index + length};
			})
			.Elements
			.ToArray();

		internal List<IndexedRunContentElement> GetContentRange(int startIndex, int length)
		{
			if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, "Start index must not be less than zero");
			if (length < 0) throw new ArgumentOutOfRangeException(nameof(length), length, "Start index must be greater than zero");
			var indexedContent = IndexedContent;
			if (indexedContent.Length < 1) throw new Exception("This run content is empty");
			var start = indexedContent.BinarySearch(startIndex, 0, indexedContent.Length);
			var end = indexedContent.BinarySearch(startIndex + length - 1, start, indexedContent.Length);

			var result = new List<IndexedRunContentElement>(end - start + 1);
			var remainingLength = length;

			for (var i = start; i <= end; i++)
			{
				var indexedElement = indexedContent[i];
				var startWithinElement = indexedElement.StartIndex >= startIndex ? 0 : startIndex - indexedElement.StartIndex;
				var lengthWithinElement = indexedElement.Length - startWithinElement <= remainingLength ? indexedElement.Length - startWithinElement : remainingLength;
				var item = new IndexedRunContentElement(indexedElement.ElementBase, startWithinElement, lengthWithinElement);
				result.Add(item);
				remainingLength -= lengthWithinElement;
			}

			return result;
		}

		public string Text => Content.Aggregate(new StringBuilder(), (sb, e) => sb.Append(e.ToText())).ToString();

		XElement IRunPropertiesContainer.GetOrCreateRunPropertiesXmlElement()
		{
			var xName = XName.Get("rPr", Namespaces.w.NamespaceName);
			var rPr = Xml.Element(xName);
			if (rPr != null) return rPr;
			rPr = new XElement(xName);
			Xml.AddFirst(rPr);
			return rPr;
		}

		internal Run Cut(int startIndex, int endIndex)
		{
			var secondRun = new Run(new XElement(Xml), contentContainer);

			RemoveContentStartingFrom(startIndex);
			secondRun.RemoveContentFromBeginning(endIndex);
			return secondRun;
		}

		internal void RemoveContentStartingFrom(int startIndex)
		{
			var position = 0;

			foreach (var element in Content)
			{
				var textLength = element.ToText().Length;

				if (position >= startIndex)
				{
					element.Remove();
				}
				else if (position < startIndex && position + textLength >= startIndex)
				{
					if (element is RunText elementText)
					{
						elementText.Text = elementText.Text.Substring(0, startIndex - position);
					}
				}
				else // if (position + element.ToText().Length < startIndex)
				{
					// Do nothing
				}

				position += textLength;
			}
		}

		internal void RemoveContentFromBeginning(int endIndex)
		{
			var position = 0;

			foreach (var element in Content)
			{
				var textLength = element.ToText().Length;

				if (position + textLength < endIndex)
				{
					element.Remove();
				}
				else if (position < endIndex && position + textLength >= endIndex)
				{
					if (element is RunText elementText)
					{
						elementText.Text = elementText.Text.Substring(endIndex - position);
					}
				}
				else
				{
					// Do nothing
				}

				position += textLength;
			}
		}
	}
}