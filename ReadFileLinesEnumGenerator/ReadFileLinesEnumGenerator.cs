using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ReadFileLinesEnumGenerator;

[Generator(LanguageNames.CSharp)]
public class ReadFileLinesEnumGenerator : IIncrementalGenerator
{
    private static readonly IdentifierRegularizer IdentifierRegularizer = new()
    {
        Rules = new Func<string, string>[]
        {
            id => id,
            id => new string(id.Select(c => SyntaxFacts.IsIdentifierPartCharacter(c) ? c : '_').ToArray()),
            id => $"_{id}",
            id => $"@{id}"
        }
    };

    private static readonly SourceGenerator SourceGenerator = new()
    {
        Namespace = "ReadFileLinesEnumGenerator.Generated",
        IdentifierRegularizer = IdentifierRegularizer
    };

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var enumValues =
            context
                .AdditionalTextsProvider
                .Select((additionalText, token) =>
                {
                    var sourcePath = additionalText.Path;
                    if (!sourcePath.EndsWith(".enum.txt")) return null;
                    var body = additionalText.GetText(token);
                    if (body is null) throw new InvalidOperationException($"{sourcePath} is not a valid text file");
                    var typeName = sourcePath.Split(".").First();
                    var enumValues =
                        body
                            .Lines
                            .Select(l => l.ToString())
                            .Where(s => !string.IsNullOrWhiteSpace(s));
                    return SourceGenerator.Generate(typeName, enumValues.ToArray());
                });
        context.RegisterSourceOutput(enumValues, (spc, source) =>
        {
            if (source is null) return;
            var (hintName, body) = source.Value;
            spc.AddSource(hintName, body);
        });
    }
}