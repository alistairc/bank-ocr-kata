using BankOcr.Console;

namespace BankOcr.Tests.TextRectangles;

class SelectingBlocks
{
    [Test]
    public void ShouldBeAbleToSelectEmpty()
    {
        var source = TextRectangle.FromString("one\ntwo\nthree");
        var selection = source.Select(0, 0, 0, 0);
        selection.ShouldBe(TextRectangle.Empty);
    }

    [Test]
    public void ShouldSelectTheBlockIfItExists()
    {
        var source = TextRectangle.FromString(
            "111111111111\n" +
            "22222 sel 22\n" +
            "33333 ect 33\n" +
            "444444444444"
        );
        var selection = source.Select(6, 1, 3, 2);
        selection.ShouldBe(TextRectangle.FromString("sel\nect"));
    }

    [Test]
    public void ShouldPadWithSpacesWhenSelectionGoesOutsideTheSource()
    {
        var source = TextRectangle.FromString(
            "111111111\n" +
            "222222222\n" +
            "333333cor\n" +
            "444444ner"
        );
        var selection = source.Select(6, 2, 5, 4);
        selection.MultiLineText.ShouldBe("cor  \nner  \n     \n     ");
    }

    [Test]
    public void ShouldAllowSelectionCompletelyOutsideOfSource()
    {
        var source = TextRectangle.FromString(
            "111\n" +
            "222\n" +
            "333"
        );
        var selection = source.Select(6, 10, 5, 4);
        selection.MultiLineText.ShouldBe("     \n     \n     \n     ");
    }
}