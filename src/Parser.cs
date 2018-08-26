using System;
using System.Text.RegularExpressions;

namespace Sellout
{
    public interface IParser
    {
        AbstractSyntaxTree BuildAst(string[] codeLines);
    }

    public class Parser : IParser
    {
        readonly Regex commonVariableDeclarationPattern = new Regex(@"(?<name>(a|an|the|my|your) [a-z]+) (is|are|was|were) (?<value>.+)", RegexOptions.Compiled);
        readonly Regex properVariableDeclarationPattern = new Regex(@"(?<name>[A-Z][a-z]+( [A-Z][a-z]+)*) (is|are|was|were) (?<value>.+)", RegexOptions.Compiled);
       
        public AbstractSyntaxTree BuildAst(string[] codeLines)
        {
            var ast = new AbstractSyntaxTree();
            foreach (var codeLine in codeLines)
            {
                if (commonVariableDeclarationPattern.IsMatch(codeLine))
                {
                    var match = commonVariableDeclarationPattern.Match(codeLine);
                    var value = match.Groups["value"].Value;
                    if (decimal.TryParse(value, out decimal decimalValue)) ast.DeclareVariable(match.Groups["name"].Value, decimalValue);
                }
                else if (properVariableDeclarationPattern.IsMatch(codeLine))
                {
                    var match = properVariableDeclarationPattern.Match(codeLine);
                    var value = match.Groups["value"].Value;
                    if (decimal.TryParse(value, out decimal decimalValue)) ast.DeclareVariable(match.Groups["name"].Value, decimalValue);
                }
            }
            return ast;
        }
    }
}