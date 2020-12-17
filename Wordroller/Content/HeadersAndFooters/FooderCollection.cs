using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using Wordroller.Packages;
using Wordroller.Styles;

namespace Wordroller.Content.HeadersAndFooters
{
	internal class FooderCollection
	{
		private readonly Dictionary<Uri, Fooder> fooders = new Dictionary<Uri, Fooder>();
		private readonly WordDocumentPackage package;
		private readonly WordDocument wordDocument;

		public FooderCollection(WordDocumentPackage package, WordDocument wordDocument)
		{
			this.package = package;
			this.wordDocument = wordDocument;
		}

		public Fooder Create(FooderType fooderType, FooderDesignation fooderDesignation)
		{
			var (part, relationship) = fooderType switch
			{
				FooderType.Header => package.AddHeaderPart(),
				FooderType.Footer => package.AddFooterPart(),
				_ => throw new ArgumentOutOfRangeException(nameof(fooderType))
			};

			var document = PackagePartHelper.ReadPackagePart(part);

			Fooder fooder = fooderType switch
			{
				FooderType.Header => new Header(part, document, relationship, fooderDesignation, wordDocument),
				FooderType.Footer => new Footer(part, document, relationship, fooderDesignation, wordDocument),
				_ => throw new ArgumentOutOfRangeException(nameof(fooderType))
			};

			var packagePartUri = PackagePartHelper.EnsureCorrectUri(relationship.TargetUri);
			fooders[packagePartUri] = fooder;

			EnsureDefaultStyles(fooderType);

			return fooder;
		}

		public Fooder Get(PackageRelationship relationship, FooderType fooderType, FooderDesignation fooderDesignation)
		{
			var packagePartUri = PackagePartHelper.EnsureCorrectUri(relationship.TargetUri);

			if (fooders.ContainsKey(packagePartUri))
			{
				var cachedFooder = fooders[packagePartUri];

				return cachedFooder.FooderType == fooderType
					? cachedFooder
					: throw new Exception($"Requested fooder is not of type {fooderType}");
			}

			var packagePart = package.GetPart(relationship);

			var document = PackagePartHelper.ReadPackagePart(packagePart);

			var fooder = fooderType switch
			{
				FooderType.Header => (Fooder) new Header(packagePart, document, relationship, fooderDesignation, wordDocument),
				FooderType.Footer => new Footer(packagePart, document, relationship, fooderDesignation, wordDocument),
				_ => throw new ArgumentOutOfRangeException(nameof(fooderType))
			};

			fooders[packagePartUri] = fooder;
			return fooder;
		}

		public void Delete(Fooder fooder)
		{
			fooders.Remove(fooder.Relationship.TargetUri);
			package.DeletePart(fooder.Relationship);
		}

		public int GetMaxDrawingId() => fooders.Values
			.Select(e => e.GetMaxDrawingId())
			.DefaultIfEmpty()
			.Max();

		internal void SavePackageParts()
		{
			foreach (var fooder in fooders.Values) fooder.SavePackagePart();
		}

		private void EnsureDefaultStyles(FooderType fooderType)
		{
			switch (fooderType)
			{
				case FooderType.Header:
					wordDocument.Styles.EnsureStyle("Header", StyleType.Paragraph);
					wordDocument.Styles.EnsureStyle("HeaderChar", StyleType.Character);
					break;
				case FooderType.Footer:
					wordDocument.Styles.EnsureStyle("Footer", StyleType.Paragraph);
					wordDocument.Styles.EnsureStyle("FooterChar", StyleType.Character);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(fooderType));
			}
		}
	}
}