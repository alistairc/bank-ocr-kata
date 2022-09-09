using BankOcr.Console;

namespace BankOcr.Tests;

class SelectingRectanglesOfText
{
    [Test]
    public void ShouldParseAnEmptyString()
    {
        var selection = TextRectangle.FromString("");

        selection.Height.ShouldBe(0);
        selection.Width.ShouldBe(0);
        selection.MultiLineText.ShouldBe(string.Empty);
    }

    [Test]
    public void ShouldHandleSingleLineText()
    {
        var selection = TextRectangle.FromString("hello world");

        selection.Height.ShouldBe(1);
        selection.Width.ShouldBe(11);
        selection.MultiLineText.ShouldBe("hello world");
    }

    [Test]
    public void ShouldHandleMultiLineRectangularText()
    {
        var selection = TextRectangle.FromString("hello\nworld");

        selection.Height.ShouldBe(2);
        selection.Width.ShouldBe(5);
        selection.MultiLineText.ShouldBe("hello\nworld");
    }

    [Test]
    public void ShouldNormalizeNewlines()
    {
        var selection1 = TextRectangle.FromString("aaa\nbbb\nccc");
        var selection2 = TextRectangle.FromString("aaa\rbbb\rccc");
        var selection3 = TextRectangle.FromString("aaa\r\nbbb\r\nccc");

        selection3.ShouldBe(selection1);
        selection2.ShouldBe(selection1);

        selection3.MultiLineText.ShouldNotContain('\r');
    }

    [Test]
    public void ShouldUseWidthOfLongestLine()
    {
        var selection = TextRectangle.FromString(
            "short\n" + 
            "longer line"
        );

        selection.Height.ShouldBe(2);
        selection.Width.ShouldBe(11);
    }

    [Test]
    public void ShouldPadLinesWithSpacesToEqualLength()
    {
        var selection = TextRectangle.FromString(
            "short\n" + 
            "longer line\n" +
            ""
        );

        selection.MultiLineText.ShouldBe(
            "short      \n" + 
            "longer line\n" +
            "           "
        );
    }
}