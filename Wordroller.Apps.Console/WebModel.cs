// using Wordroller.Content.Properties.Fonts;
// using Wordroller.Content.Properties.Runs;
// using Wordroller.Content.Text.RunContent;
//
// namespace Wordroller.Apps.Console;
//
// // WebModel is a representation of Hardroller.WordDocument that is suitable for web applications.
// // It is a snapshot of the document that can be serialized to JSON and sent to the client.
//
// public class WebModel
// {
// 	public DocumentBodyModel Body { get; set; }
// 	public HeaderModel[] Headers { get; set; }
// 	public FooterModel[] Footers { get; set; }
// 	public StyleModel[] Styles { get; set; }
// }
//
// public interface IFontSettings
// {
// 	string? Ascii { get; set; }
// 	string? HighAnsi { get; set; }
// 	string? EastAsia { get; set; }
// 	string? ComplexScript { get; set; }
// 	ThemeFont? AsciiThemeFont { get; set; }
// 	ThemeFont? HighAnsiThemeFont { get; set; }
// 	ThemeFont? EastAsiaThemeFont { get; set; }
// 	ThemeFont? ComplexScriptThemeFont { get; set; }
// 	FontHint? FontHint { get; set; }
// }
//
// public interface IRunProperties
// {
// 	bool? Bold { get; set; }
// 	bool? Italic { get; set; }
// 	TextCapitalization? Capitalization { get; set; }
// 	TextStrikethrough? Strikethrough { get; set; }
// 	RunVerticalAlignment? VerticalAlignment { get; set; }
// 	IFontSettings Font { get; }
// 	RunColor Color { get; }
// 	RunLanguage RunLanguage { get; }
// 	RunUnderline Underline { get; }
// 	HighlightColor? HighlightColor { get; set; }
// 	double? FontSize { get; set; }
// 	double? Kerning { get; set; }
// 	double? SpacingPt { get; set; }
// 	int? ScalePc { get; set; }
// 	double? Position { get; set; }
// 	bool? IsComplexScript { get; set; }
// 	bool? ComplexScriptBold { get; set; }
// 	bool? ComplexScriptItalic { get; set; }
// 	double? ComplexScriptFontSize { get; set; }
// }
//
// public interface IRunElementModel
// {
// }
//
// public interface IRunText: IRunElementModel
// {
// 	string Text { get; set; }
// }
//
// public interface IAbsolutePositionTab: IRunElementModel
// {
// 	AbsolutePositionTabAlignment Alignment { get; set; }
// 	AbsolutePositionTabLeaderCharacter LeaderCharacter { get; set; }
// 	AbsolutePositionTabRelativeTo RelativeTo { get; set; }
// }
//
// public interface IRunTab: IRunElementModel
// {
// }
//
// public interface IRunNoBreakHyphen: IRunElementModel
// {
// }
//
// public interface IRunSoftHyphen: IRunElementModel
// {
// }
//
