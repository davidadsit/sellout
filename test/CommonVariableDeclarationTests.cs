using NUnit.Framework;
using System.Linq;
using Sellout;

namespace SelloutTests
{
    public class CommonVariableDeclarationTests
    {
        Parser parser;

        [SetUp]
        public void Setup()
        {
            parser = new Parser();
        }

        [TestCase("a","guitar", 6)]
        public void statement_include_variable_declaration(string article, string name, object initialValue)
        {
            var ast = parser.BuildAst(new []{$"{article} {name} is {initialValue}"});
            Assert.That(ast.Statements.Single(), Is.EqualTo(new VariableDeclaration($"{article} {name}", initialValue)));
        }
    }
}