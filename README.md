# What is Wordroller?
Wordroller is a free and friendly library for creating and processing Microsoft Word documents often referred to as `docx` files.

This document format is defined in the [`Office Open XML`](https://en.wikipedia.org/wiki/Office_Open_XML) ([`OOXML`](https://en.wikipedia.org/wiki/Office_Open_XML)) standard, mostly its part named `WordprocessingML`.

## The library

- Does not require Word installed
- Wraps the complexity of Microsoft Office document's internals and provides a simple API
- Yet adds just a thin layer above nodes of internal XML documents, allowing direct access to XML if necessary
- Maintains compatibility with Microsoft Word 2007 and newer
- Targets .Net Standard 2.0 and supports both .Net and .Net Core
- Written in clean maintainable manner
- Only depends on System.IO.Packaging NuGet package

## Motivation

For about 5 years, my company relied upon the well-known DocX library by [Cathal Coffey](https://github.com/ccoffey) and later [Przemyslaw Klys](https://www.linkedin.com/in/pklys/). We needed precise rendering of Word documents yet did not have neither time nor resources to dig into low-level [Open XML SDK](https://github.com/OfficeDev/Open-XML-SDK) or [NPOI](https://github.com/nissl-lab/npoi) back in the day.

The DocX library had have friendly and comprehensible API and was of great help from the very start. Yet soon we had to fork it and patch heavily. It turned out that it is very hard to modify the original library, and virtually impossible to refactor.

Finally, in 2017 the original DocX library was acquired by some company and now known as [xceedsoftware/DocX](https://github.com/xceedsoftware/DocX) that is no longer free. The remaining [free 'classic' branch](https://github.com/xceedsoftware/DocX/tree/Classic) still exists but does no support .Net Core and is no longer maintained.

This is why I decided to create a new implementation in cleaner and more maintainable way. First, it was used for internal needs, but it looked like a good idea to share it with the community.

Wordroller is intended to support startups and small business automation teams that might have no resources to dive into complicated logic of low-level libraries or justify purchasing of commercial products. 

## Purposes

Wordroller is created for several primary purposes:

- To create and modify documents first and foremost for printing, not interactive use. That is why interactive features like hyperlinks are not implemented yet.
- To create documents from scratch. In this scenario, the entire document content is created from your code and appended paragraph by paragraph and table by table. This is the primary scenario in my company.
- To create documents from existing templates or documents provided by either developers or users. Find and Replace feature is implemented for this scenario.

Yet some possible purposes were not of high priority when creating Wordroller:

- Content extraction and document indexing. As of now, not all parts of a document are even loaded, and some of document content is ignored (like text insertions and deletions). However, if this scenario is crucial for you please let me know.
- Random document editing. The scenario of random content insertion or replacement is not currently supported. If this scenario is crucial for you please let me know.

## Quick Start and Samples

You can install Wordroller [from NuGet](https://www.nuget.org/packages/Wordroller/) or build from source code.

For samples, please refer to [Wordroller.Tests](https://github.com/shestakov/wordroller/tree/master/Wordroller.Tests) project.

### Basic Example

```cs
using (var document = new WordDocument(CultureInfo.GetCultureInfo("ru-ru")))
{
    document.Styles.DocumentDefaults.RunProperties.Font.Ascii = "Times New Roman";
    document.Styles.DocumentDefaults.RunProperties.Font.HighAnsi = "Arial";
    document.Styles.DocumentDefaults.ParagraphProperties.Spacing.BetweenLinesLn = 1.5;

    var section = document.Body.Sections.First();

    var paragraph1 = section.AppendParagraph();
    paragraph1.AppendText("This is the ASCII text in the default Times New Roman font");
    paragraph1.AppendText("\n");
    paragraph1.AppendText("А это кириллический текст другим шрифтом");

    var paragraph2 = section.AppendParagraph();
    var run2 = paragraph2.AppendText("This is a text with a font different form default");
    run2.Properties.Font.Ascii = "Helvetica";

    var path = Path.Combine(TestHelper.GetTempDirectory(), "TextDocument.docx");
    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
    {
        document.Save(fileStream);
    }
}
```

## Feature Comparison

Wordroller is a smaller library than the original DocX. While I initially intended to implement most of the DocX features, Wordroller still lacks some. Some because they did not match the primary purposes of the library, some because I found out that the original library itself did not really implement it properly (like tracking changes).

### Wordroller DOES NOT support

- Editing document settings and password protection
- Joining documents
- Charts
- Changes tracking (this feature was just _partially_ implemented in DocX but _proper_ implementation would require an enormous effort)
- Tables of Content
- Equations
- Bookmarks
- Hyperlinks
- ~~Fields~~ **Basic support for complex fields had been added**
- Editing core properties
- Editing custom properties

### Wordroller DOES support

- Document sections (a serious problem when using DocX)
- Paragraphs and Runs, and their properties
- Tables and their properties
- Numbering (list definitions, actual lists, including paragraphs into lists)
- All kinds of headers and footers
- Basic support for styles
- **Text search and replacement**
- **Basic support for complex fields, primarily PAGE and NUMPAGES.**

## Q&A

~~Q) Why version 0.1 not 1.0?~~

~~A) While Wordroller is already used in production environment, I suppose there is a field for improvement regarding the API to make it more friendly and comprehensible. With your feedback.~~

---

Q) Why so few tests?

A) It is expensive. There will be more later. Or I hope so.

---

Q) What is Wordroller.StandardStyles?

A) An empty Word document (created by either Word or Wordroller) does contain very few basic styles. The complete set containing all the possible styles (especially for all the versions of Word) would be very bloated.

That is why Wordroller utilizes IStyleProvider interface so user can implement it to provide required styles. Wordroller.StandardStyles contains an implementation of IStyleProvider that loads different standard styles form the assembly resources.

---

Q) I've found a bug! How do I get it fixed?

A) This is a fresh project and I would be grateful for feedback. Please use GitHub Issues. I will try to fix problems in timely manner.

---

Q) I would like a new feature to be added. What should I do?

A) It would be great if you start with a GitHub issue to initiate a discussion.

---

Q) _Fooders?_

A) I did not want it, but someone had to. Yes, `type Fooder = Header | Footer`. Sorry for that.
