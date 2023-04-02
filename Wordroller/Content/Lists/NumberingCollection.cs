using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Xml.Linq;
using Wordroller.Content.Abstract;
using Wordroller.Packages;
using Wordroller.Utility.Xml;

namespace Wordroller.Content.Lists
{
	public class NumberingCollection : OptionalDocumentPartWrapper
	{
		public NumberingCollection(PackagePart? packagePart, WordDocumentPackage package) : base(packagePart, package)
		{
		}

		public IEnumerable<ListDefinition> ListDefinitions =>
			XmlDocument?.Root?.Elements(Namespaces.w + "abstractNum").Select(e => new ListDefinition(e)) ?? Enumerable.Empty<ListDefinition>();

		public IEnumerable<List> Lists => XmlDocument?.Root?.Elements(Namespaces.w + "num").Select(e => new List(e)) ?? Enumerable.Empty<List>();

		public ListDefinition CreateListDefinition(string name, MultiLevelType multiLevelType, string? nsid = null, string? tmpl = null, bool? restartNumberingAfterBreak = null)
		{
			var root = GetOrCreatePackagePartAndDocumentRoot();
			var abstractNumId = GetMaxAbstractNumId() + 1;

			var xml = new XElement(Namespaces.w + "abstractNum",
				new XAttribute(Namespaces.w + "abstractNumId", abstractNumId),
				new XElement(Namespaces.w + "name", new XAttribute(Namespaces.w + "val", name)),
				new XElement(Namespaces.w + "nsid", new XAttribute(Namespaces.w + "val", nsid ?? "")),
				new XElement(Namespaces.w + "tmpl", new XAttribute(Namespaces.w + "val", tmpl ?? "")),
				new XElement(Namespaces.w + "multiLevelType", new XAttribute(Namespaces.w + "val", multiLevelType.ToCamelCase()))
			);

			if (restartNumberingAfterBreak.HasValue)
				xml.SetAttributeValue(Namespaces.w + "restartNumberingAfterBreak", restartNumberingAfterBreak);

			root.Add(xml);
			return new ListDefinition(xml);
		}

		public List CreateList(ListDefinition listDefinition)
		{
			var root = GetOrCreatePackagePartAndDocumentRoot();
			var abstractNumId = listDefinition.AbstractNumId;
			var numId = GetMaxNumId() + 1;

			var xml = new XElement(Namespaces.w + "num",
				new XAttribute(Namespaces.w + "numId", numId),
				new XElement(Namespaces.w + "abstractNumId", new XAttribute(Namespaces.w + "val", abstractNumId))
			);

			root.Add(xml);
			return new List(xml);
		}

		public List GetList(int numId)
		{
			var element = XmlDocument?.Root?.Elements(XName.Get("num", Namespaces.w.NamespaceName))
							  .FirstOrDefault(d => numId == d.GetOwnAttributeInt("numId"))
						  ?? throw new ArgumentOutOfRangeException(nameof(numId), $"A list (num) with numId {numId} not found");

			return new List(element);
		}

		public ListDefinition GetListDefinition(List list)
		{
			var abstractNumId = list.AbstractNumId;
			var element = XmlDocument?.Descendants()
							  .FirstOrDefault(d => d.Name.LocalName == "abstractNum" && abstractNumId == d.GetOwnAttributeInt("abstractNumId"))
						  ?? throw new Exception($"A list definition (abstractNum) with abstractNumId {abstractNumId} not found");

			return new ListDefinition(element);
		}

		private int GetMaxAbstractNumId()
		{
			var abstractNums = XmlDocument?.Root?.Elements(Namespaces.w + "abstractNum").ToArray() ?? Array.Empty<XElement>();
			return abstractNums.Any() ? abstractNums.Attributes(Namespaces.w + "abstractNumId").Max(e => int.Parse(e.Value)) : 0;
		}

		private int GetMaxNumId()
		{
			var nums = XmlDocument?.Root?.Elements(Namespaces.w + "num").ToArray() ?? Array.Empty<XElement>();
			return nums.Any() ? nums.Attributes(Namespaces.w + "numId").Max(e => int.Parse(e.Value)) : 0;
		}

		protected override XElement GetOrCreatePackagePartAndDocumentRoot()
		{
			if (PackagePart == null)
			{
				PackagePart = Package.CreateDefaultNumberingPart();
				XmlDocument = PackagePartHelper.ReadPackagePart(PackagePart);
			}

			return XmlDocument?.Root ?? throw new Exception("The XML document must be initialized and have root");
		}
	}
}