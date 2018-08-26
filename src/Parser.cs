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
        readonly Regex commonVariableDeclarationPattern = new Regex(@"(?<name>(a|an|the|my|your) [a-z]+) (is|were|are) (?<value>.+)", RegexOptions.Compiled);
       
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
            }
            return ast;
        }
    }
}