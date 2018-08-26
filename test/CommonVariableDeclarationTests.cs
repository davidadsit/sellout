using System.Linq;
using NUnit.Framework;
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

        [TestCase("a guitar", "is", 6)]
        [TestCase("an apple", "is", 3.14)]
        [TestCase("the bananas", "are", 5)]
        [TestCase("my guitar", "was", 1000)]
        [TestCase("your axe", "are", -12)]
        [TestCase("your fingers", "were", 10)]
        public void statement_including_numberic_variable_declaration(string name, string verb, decimal value)
        {
            var ast = parser.BuildAst(new[] {$"{name} {verb} {value}"});
            Assert.That(ast.Statements.Single().ToString(), Is.EqualTo(new VariableDeclaration(name, value).ToString()));
        }

        [TestCase("a guitar", "is", "red")]
        [TestCase("the bananas", "are", "molded")]
        [TestCase("my guitar", "was", "on fire")]
        [TestCase("your fingers", "were", "bloody")]
        public void statement_including_string_variable_declaration(string name, string verb, string value)
        {
            var ast = parser.BuildAst(new[] {$"{name} {verb} \"{value}\""});
            Assert.That(ast.Statements.Single().ToString(), Is.EqualTo(new VariableDeclaration(name, value).ToString()));
        }
    }
}