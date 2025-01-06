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
        var sourceHolders =
            context
                .AdditionalTextsProvider
                .Where(static file => file.Path.EndsWith(".enum.txt"))
                .Select((at, token) =>
                {
                    var body = at.GetText(token);
                    if (body is null) throw new InvalidOperationException($"{at.Path} is not a valid text file");
                    var typeName = at.Path.Split(".").First();
                    var enumValues =
                        body
                            .Lines
                            .Select(l => l.ToString().Trim())
                            .Where(s => !string.IsNullOrWhiteSpace(s))
                            .Distinct();
                    return SourceGenerator.Generate(typeName, enumValues.ToArray());
                });
        context.RegisterSourceOutput(sourceHolders, (spc, holder) => spc.AddSource(holder.hintName, holder.body));
    }
}