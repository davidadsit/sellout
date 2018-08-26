using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using Sellout;

namespace SelloutTests.TranspilerTests
{
    public class TranspilerTests
    {
        const string SourcePathRock = "source-path.rock";
        const string OutputPath = "output-path";
        Mock<IParser> parserMock;
        Mock<ICSharpWriter> writerMock;
        Transpiler transpiler;

        [SetUp]
        public void Setup()
        {
            parserMock = new Mock<IParser>();
            writerMock = new Mock<ICSharpWriter>();
            transpiler = new Transpiler(parserMock.Object, writerMock.Object);
            File.WriteAllText(SourcePathRock, "");
        }

        [Test]
        public void Writes_each_statement_from_the_ast()
        {
            var abstractSyntaxTree = new AbstractSyntaxTree();
            abstractSyntaxTree.DeclareVariable("var-1", 123);
            abstractSyntaxTree.DeclareVariable("var-2", 456);
            parserMock.Setup(x => x.BuildAst(It.IsAny<string[]>())).Returns(abstractSyntaxTree);
            transpiler.Go(SourcePathRock, OutputPath);
            foreach (var statement in abstractSyntaxTree.Statements)
            {
                writerMock.Verify(x => x.AppendStatement(statement.ToCSharp()));
            }
        }

        [Test]
        public void Does_not_declare_variables_multiple_times()
        {

            var abstractSyntaxTree = new AbstractSyntaxTree();
            abstractSyntaxTree.DeclareVariable("var-1", 123);
            abstractSyntaxTree.DeclareVariable("var-1", 456);
            parserMock.Setup(x => x.BuildAst(It.IsAny<string[]>())).Returns(abstractSyntaxTree);
            transpiler.Go(SourcePathRock, OutputPath);
            writerMock.Verify(x => x.AppendStatement(abstractSyntaxTree.Statements.ElementAt(0).ToCSharp()));
            writerMock.Verify(x => x.AppendStatement(abstractSyntaxTree.Statements.ElementAt(1).ToCSharp().Replace("var ", "")));
        }
    }
}