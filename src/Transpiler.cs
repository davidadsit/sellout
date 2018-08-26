using System.Collections.Generic;
using System.IO;

namespace Sellout
{
    public class Transpiler
    {
        readonly List<string> declaredVariables;
        readonly IParser parser;
        readonly ICSharpWriter writer;

        public Transpiler(IParser parser, ICSharpWriter writer)
        {
            this.parser = parser;
            this.writer = writer;
            declaredVariables = new List<string>();
        }

        public void Go(string sourcePath, string outputPath)
        {
            var abstractSyntaxTree = parser.BuildAst(File.ReadAllLines(sourcePath));
            foreach (var statement in abstractSyntaxTree.Statements)
            {
                var cSharp = statement.ToCSharp();
                cSharp = EliminateDuplicateVariableDeclaration(statement, cSharp);

                writer.AppendStatement(cSharp);
            }

            writer.Write(outputPath);
        }

        string EliminateDuplicateVariableDeclaration(Statement statement, string cSharp)
        {
            if (!(statement is Variable)) return cSharp;

            var variable = (Variable) statement;
            if (declaredVariables.Contains(variable.CleanName))
            {
                cSharp = cSharp.Replace("var ", "");
            }
            else
            {
                declaredVariables.Add(variable.CleanName);
            }

            return cSharp;
        }
    }
}