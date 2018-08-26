using System.IO;

namespace Sellout
{
    public class Transpiler
    {
        readonly IParser parser;
        readonly ICSharpWriter writer;

        public Transpiler(IParser parser, ICSharpWriter writer)
        {
            this.parser = parser;
            this.writer = writer;
        }

        public void Go(string sourcePath, string outputPath)
        {
            var abstractSyntaxTree = parser.BuildAst(File.ReadAllLines(sourcePath));
            foreach (var statement in abstractSyntaxTree.Statements)
            {
                writer.AppendStatement(statement.ToCSharp()); 
            }
            writer.Write(outputPath);
        }
    }
}