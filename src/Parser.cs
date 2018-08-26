namespace Sellout
{
    public interface IParser
    {
        AbstractSyntaxTree BuildAst(string[] codeLines);
    }

    public class Parser : IParser
    {
        public AbstractSyntaxTree BuildAst(string[] codeLines)
        {
            var ast = new AbstractSyntaxTree();
            ast.DeclareVariable("a guitar", 6);
            return ast;
        }
    }
}