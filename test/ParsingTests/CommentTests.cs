using System.Linq;
using NUnit.Framework;
using Sellout;

namespace SelloutTests.ParsingTests
{
    public class CommentTests
    {
        Parser parser;

        [SetUp]
        public void Setup()
        {
            parser = new Parser();
        }

        [TestCase("(Doctor Roxo The Rock And Roll Clown)", "Doctor Roxo The Rock And Roll Clown")]
        [TestCase("A fire is burning. (Doctor Roxo The Rock And Roll Clown)", "Doctor Roxo The Rock And Roll Clown")]
        public void statement_include_variable_declaration(string line, string comment)
        {
            var ast = parser.BuildAst(new[] { line });
            Assert.That(ast.Statements.First().ToString(), Is.EqualTo(new Comment(comment).ToString()));
        }
    }
}