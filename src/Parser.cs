using System.Text.RegularExpressions;

namespace Sellout
{
    public interface IParser
    {
        AbstractSyntaxTree BuildAst(string[] codeLines);
    }

    public class Parser : IParser
    {
        readonly Regex commonVariableDeclarationPattern =
            new Regex(@"(?<name>(a|an|the|my|your) [a-z]+) (is|are|was|were) (?<value>.+)", RegexOptions.Compiled);

        readonly Regex properVariableDeclarationPattern =
            new Regex(@"(?<name>[A-Z][a-z]+( [A-Z][a-z]+)*) (is|are|was|were) (?<value>.+)", RegexOptions.Compiled);

        public AbstractSyntaxTree BuildAst(string[] codeLines)
        {
            var ast = new AbstractSyntaxTree();
            foreach (var codeLine in codeLines)
            {
                if (commonVariableDeclarationPattern.IsMatch(codeLine))
                {
                    var match = commonVariableDeclarationPattern.Match(codeLine);
                    ast.DeclareVariable(match.Groups["name"].Value, ParseValue(match.Groups["value"].Value));
                }
                else if (properVariableDeclarationPattern.IsMatch(codeLine))
                {
                    var match = properVariableDeclarationPattern.Match(codeLine);
                    ast.DeclareVariable(match.Groups["name"].Value, ParseValue(match.Groups["value"].Value));
                }
            }

            return ast;
        }

        static dynamic ParseValue(string value)
        {
            if (value.StartsWith('"') && value.EndsWith('"')) return value.Replace("\"", "");
            if (decimal.TryParse(value, out var decimalValue)) return decimalValue;

            return null;
        }
    }
}