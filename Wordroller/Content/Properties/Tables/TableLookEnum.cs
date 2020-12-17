using System;

 namespace Wordroller.Content.Properties.Tables
{
	[Flags]
	public enum TableLookEnum : uint
	{
		None = 0x0000,
		FirstRow = 0x0020,
		LastRow = 0x0040,
		FirstColumn = 0x0080,
		LastColumn = 0x0100,
		NoHBand = 0x0200,
		NoVBand = 0x0400
	}
}