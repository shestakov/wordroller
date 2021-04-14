using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Drawings;
using Wordroller.Content.Lists;
using Wordroller.Content.Properties.Paragraphs;
using Wordroller.Content.Properties.Sections;
using Wordroller.Content.Tables;
using Wordroller.Content.Text.RunContent;
using Wordroller.Content.Text.Search;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text
{
	public class Paragraph : DocumentContentElement, IParagraphPropertiesContainer
	{
		internal Paragraph(XElement xml, DocumentContentContainer parent) : base(xml, parent)
		{
		}

		internal IndexedRun[] IndexedContent => Content
			.Aggregate(new { Elements = new List<IndexedRun>(), Index = 0 }, (acc, e) =>
			{
				var index = acc.Index;
				var length = e.Text.Length;
				var element = new IndexedRun(e, index, length);
				acc.Elements.Add(element);
				return new { acc.Elements, Index = acc.Index + length };
			})
			.Elements
			.ToArray();


		public IEnumerable<Run> Content => Xml.Elements(XName.Get("r", Namespaces.w.NamespaceName)).Select(e => new Run(e, ParentContainer));

		public ParagraphProperties Properties => new ParagraphProperties(this, Xml.Element(XName.Get("pPr", Namespaces.w.NamespaceName)));

		XElement IParagraphPropertiesContainer.GetOrCreateParagraphPropertiesXmlElement()
		{
			var xName = XName.Get("pPr", Namespaces.w.NamespaceName);
			var pPr = Xml.Element(xName);
			if (pPr != null) return pPr;
			pPr = new XElement(xName);
			Xml.AddFirst(pPr);
			return pPr;
		}

		public Run AppendText(string text)
		{
			var run = Run.Create(text, ParentContainer);
			Xml.Add(run.Xml);
			return run;
		}

		public Run AppendPicture(InlinePicture inlinePicture)
		{
			var r = new XElement(Namespaces.w + "r", inlinePicture.Xml);
			Xml.Add(r);
			var run = new Run(r, ParentContainer);
			return run;
		}

		public IEnumerable<ParagraphSearchResult> FindText(string stringToSeek, StringComparison stringComparison)
		{
			var length = stringToSeek.Length;
			var indexedContent = IndexedContent;
			var text = indexedContent.Aggregate(new StringBuilder(), (sb, e) => sb.Append(e.Run.Text)).ToString();
			var indices = text.AllIndexesOf(stringToSeek, stringComparison);

			var results = indices.Select(r =>
			{
				var items = GetContentRange(r, length);
				return new ParagraphSearchResult(r, length, items.ToArray(), ParentContainer);
			});

			return results;
		}

		private List<RunSearchResult> GetContentRange(int startIndex, int length)
		{
			if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, "Start index must not be less than zero");
			if (length < 0) throw new ArgumentOutOfRangeException(nameof(length), length, "Start index must be greater than zero");
			var indexedContent = IndexedContent;
			if (indexedContent.Length < 1) throw new Exception("This paragraph content is empty");
			var start = indexedContent.BinarySearch(startIndex, 0, indexedContent.Length);
			var end = indexedContent.BinarySearch(startIndex + length - 1, start, indexedContent.Length);

			var result = new List<RunSearchResult>(end - start + 1);
			var remainingLength = length;

			for (var i = start; i <= end; i++)
			{
				var run = indexedContent[i];
				var startWithinRun = run.StartIndex >= startIndex ? 0 : startIndex - run.StartIndex;
				var lengthWithinRun = run.Length - startWithinRun <= remainingLength ? run.Length - startWithinRun : remainingLength;
				result.Add(new RunSearchResult(startWithinRun, lengthWithinRun, run.Run));
				remainingLength -= lengthWithinRun;
			}

			return result;
		}

		public void AppendTextBreak(TextBreakType type = TextBreakType.TextWrapping, TextBreakClear clear = TextBreakClear.None)
		{
			var br = TextBreak.Create(type, clear);
			var run = new XElement(XName.Get("r", Namespaces.w.NamespaceName), br.Xml); // TODO: Should append to the last run here
			Xml.Add(run);
		}

		public void InsertSectionBreak(SectionBreakType breakType)
		{
			var xName = XName.Get("sectPr", Namespaces.w.NamespaceName);

			var pPr = Xml.Element(XName.Get("pPr", Namespaces.w.NamespaceName));

			if (pPr?.Element(xName) != null)
				throw new InvalidOperationException("Paragraph already defines a section");

			pPr = ((IParagraphPropertiesContainer) this).GetOrCreateParagraphPropertiesXmlElement();
			var sectPr = new XElement(xName);

			if (breakType != SectionBreakType.NextPage)
				sectPr.SetAttributeValue(Namespaces.w + "type", breakType.ToCamelCase());

			pPr.Add(sectPr);
		}

		public override void Remove()
		{
			if (ParentContainer is Cell cell && cell.Paragraphs.Count() < 2) throw new InvalidOperationException("The last paragraph in a table cell cannot be deleted");
			base.Remove();
		}

		public Paragraph IncludeIntoList(List list, int level = 0)
		{
			Properties.BindList(list, level);
			return this;
		}

		public Paragraph ExcludeFromList()
		{
			Properties.ExcludeFromList();
			return this;
		}
	}
}