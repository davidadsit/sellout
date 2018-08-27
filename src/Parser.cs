using System.Linq;
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
            new Regex(@"(?<name>(a|an|the|my|your|A|An|The|My|Your) [a-z]+) (is|are|was|were) (?<value>.+)", RegexOptions.Compiled);

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
            if (new[] {"true", "right", "yes", "ok"}.Any(x => x == value)) return true;
            if (new[] {"false", "wrong", "no", "lies"}.Any(x => x == value)) return false;
            if (decimal.TryParse(value, out var decimalValue)) return decimalValue;

            value = Regex.Replace(value, "[^a-zA-Z\\. ]", "");
            if (value.Contains("."))
            {
                var strings = value.Split(".");
                var wholes = strings[0].Trim().Split(" ").Select(x => x.Length % 10);
                var fractions = string.Join(" ", strings.Skip(1)).Trim().Split(" ").Select(x => x.Length % 10);
                var stringRepresentation = $"{string.Join("", wholes)}.{string.Join("", fractions)}";
                if (decimal.TryParse(stringRepresentation, out var poeticDecimalValue)) return poeticDecimalValue;
            }
            else
            {
                var lengths = value.Split(" ").Select(x => x.Length % 10);
                if (decimal.TryParse(string.Join("", lengths), out var poeticDecimalValue)) return poeticDecimalValue;
            }

            return null;
        }
    }
}