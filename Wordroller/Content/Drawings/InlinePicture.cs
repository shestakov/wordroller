using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Images;
using Wordroller.Utility;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Drawings
{
	public class InlinePicture : XmlElementWrapper
	{
		internal InlinePicture(XElement xml, Image image) : base(xml)
		{
			if (xml.Name != Namespaces.w + "drawing") throw new ArgumentException($"XML element must be drawing but was {xml.Name}", nameof(xml));
			Image = image;
		}

		public Image Image { get; }

		public int Id => int.TryParse(FindDocPr()?.Attribute("id")?.Value, out var value)
			? value
			: throw new Exception("Incorrect attribute id in drawing > inline > extent > docPr");

		public string Name
		{
			get => FindDocPr()?.Attribute("name")?.Value ?? throw new Exception("Attribute name is missing in drawing > inline > extent > docPr");
			set => FindDocPr()?.SetAttributeValue("name", value);
		}

		public string? Description
		{
			get => FindDocPr()?.Attribute("descr")?.Value;
			set => FindDocPr()?.SetAttributeValue("descr", value);
		}

		public int WidthEmu
		{
			get => int.TryParse(FindExtent()?.Attribute("cx")?.Value, out var value) ? value : throw new Exception("Incorrect attribute cx in drawing > inline > extent");

			set
			{
				FindExtent()?.SetAttributeValue("cx", value);
				FindTransform()?.SetAttributeValue("cx", value); // does not actually affect the result
			}
		}

		public int HeightEmu
		{
			get => int.TryParse(FindExtent()?.Attribute("cy")?.Value, out var value) ? value : throw new Exception("Incorrect attribute cy in drawing > inline > extent");

			set
			{
				FindExtent()?.SetAttributeValue("cy", value);
				FindTransform()?.SetAttributeValue("cy", value); // does not actually affect the result
			}
		}

		public int WidthPx => WidthEmu / UnitHelper.EmusPerPixel;
		public int HeightPx => HeightEmu / UnitHelper.EmusPerPixel;

		private XElement? FindExtent()
		{
			return Xml
				.Element(Namespaces.wp + "inline")
				?.Element(Namespaces.wp + "extent");
		}

		private XElement? FindDocPr()
		{
			return Xml.Element(Namespaces.wp + "inline")
				?.Element(Namespaces.wp + "docPr");
		}

		private XElement? FindTransform()
		{
			return Xml.Element(Namespaces.wp + "inline")
				?.Element(Namespaces.a + "graphic")
				?.Element(Namespaces.a + "graphicData")
				?.Element(Namespaces.pic + "pic")
				?.Element(Namespaces.pic + "spPr")
				?.Element(Namespaces.a + "xfrm");
		}

		internal static XElement CreateInlineDrawing(int id, string imageRelationshipId, string name, string description, int widthEmu, int heightEmu)
		{
			var drawing = new XElement(Namespaces.w + "drawing",
				new XElement(Namespaces.wp + "inline", new XAttribute("distT", 0), new XAttribute("distB", 0), new XAttribute("distL", 0), new XAttribute("distR", 0),
					new XElement(Namespaces.wp + "extent", new XAttribute("cx", widthEmu * 2), new XAttribute("cy", heightEmu * 2)),
					new XElement(Namespaces.wp + "effectExtent", new XAttribute("l", 0), new XAttribute("t", 0), new XAttribute("r", 0), new XAttribute("b", 0)),
					new XElement(Namespaces.wp + "docPr", new XAttribute("id", id), new XAttribute("name", name), new XAttribute("descr", description)),
					new XElement(Namespaces.wp + "cNvGraphicFramePr",
						new XElement(Namespaces.a + "graphicFrameLocks", new XAttribute("noChangeAspect", "1"))),
					new XElement(Namespaces.a + "graphic",
						new XElement(Namespaces.a + "graphicData", new XAttribute(Namespaces.a + "uri", Namespaces.pic),
							new XElement(Namespaces.pic + "pic",
								new XElement(Namespaces.pic + "nvPicPr",
									new XElement(Namespaces.pic + "cNvPr", new XAttribute("id", id), new XAttribute("name", name)),
									new XElement(Namespaces.pic + "cNvPicPr")),
								new XElement(Namespaces.pic + "blipFill",
									new XElement(Namespaces.a + "blip", new XAttribute(Namespaces.r + "embed", imageRelationshipId)),
									new XElement(Namespaces.a + "stretch",
										new XElement(Namespaces.a + "fillRect"))),
								new XElement(Namespaces.pic + "spPr",
									new XElement(Namespaces.a + "xfrm",
										new XElement(Namespaces.a + "off", new XAttribute("x", 0), new XAttribute("y", 0)),
										new XElement(Namespaces.a + "ext", new XAttribute("cx", widthEmu), new XAttribute("cy", heightEmu))
									),
									new XElement(Namespaces.a + "prstGeom", new XAttribute("prst", "rect"),
										new XElement(Namespaces.a + "avLst")))))
					)
				)
			);

			return drawing;
		}
	}
}