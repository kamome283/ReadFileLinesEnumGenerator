using Microsoft.CodeAnalysis.CSharp;

namespace ReadFileLinesEnumGenerator;

internal class IdentifierRegularizer
{
    public IEnumerable<Func<string, string>> Rules { get; init; } = null!;

    public string Regularize(string identifier)
    {
        var initialIdentifier = identifier;
        foreach (var rule in Rules)
        {
            identifier = rule(identifier);
            if (SyntaxFacts.IsValidIdentifier(identifier))
                return identifier;
        }

        throw new ArgumentException($"'{initialIdentifier}' cannot be regularized to a valid identifier");
    }
}