using System.Linq;
using NUnit.Framework;
using Sellout;

namespace SelloutTests
{
    public class ProperVariableDeclarationTests
    {
        Parser parser;

        [SetUp]
        public void Setup()
        {
            parser = new Parser();
        }

        [TestCase("Tommy", "is", 6)]
        [TestCase("Gina", "are", 3.14)]
        [TestCase("Dr Feelgood", "was", 5)]
        [TestCase("Doctor Roxo The Rock And Roll Clown", "were", 1000)]
        public void statement_include_variable_declaration(string name, string verb, decimal value)
        {
            var ast = parser.BuildAst(new[] {$"{name} {verb} {value}"});
            Assert.That(ast.Statements.Single().ToString(), Is.EqualTo(new VariableDeclaration(name, value).ToString()));
        }
    }
}