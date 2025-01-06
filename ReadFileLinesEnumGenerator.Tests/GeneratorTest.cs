using ReadFileLinesEnumGenerator.Generated;

namespace ReadFileLinesEnumGenerator.Test;

public class GeneratorTest
{
    [Fact]
    public void GeneratedEnumValuesLengthIsCorrect()
    {
        var values = Enum.GetValues<valid>();
        Assert.Equal(4, values.Length);
    }

    [Fact]
    public void GeneratedEnumValuesStringValuesAreCorrect()
    {
        var stringValues = Enum.GetValues<valid>().Select(v => v.ToStringValue());
        Assert.Equal([
            "FOO",
            "bar",
            "baz.p",
            "42.x"
        ], stringValues);
    }
}