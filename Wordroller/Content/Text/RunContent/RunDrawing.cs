using System;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Content.Drawings;
using Wordroller.Content.Images;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Text.RunContent
{
	public class RunDrawing : RunContentElementBase
	{
		private readonly DocumentContentContainer documentContentContainer;

		public RunDrawing(XElement xml, DocumentContentContainer documentContentContainer) : base(xml)
		{
			if (xml.Name != Namespaces.w + "drawing") throw new ArgumentException($"XML element must be drawing but was {xml.Name}", nameof(xml));
			this.documentContentContainer = documentContentContainer;
		}

		public InlinePicture? InlinePicture
		{
			get
			{
				var imageRelationshipId = Xml
					.Element(Namespaces.wp + "inline")
					?.Element(Namespaces.a + "graphic")
					?.Element(Namespaces.a + "graphicData")
					?.Element(Namespaces.pic + "pic")
					?.Element(Namespaces.pic + "blipFill")
					?.Element(Namespaces.a + "blip")
					?.Attribute(Namespaces.r + "embed")
					?.Value;

				if (imageRelationshipId == null) return null;

				var image = new Image(documentContentContainer.GetImageRelationship(imageRelationshipId).TargetUri);

				return new InlinePicture(Xml, image);
			}
		}

		public override string ToText() => "";
	}
}