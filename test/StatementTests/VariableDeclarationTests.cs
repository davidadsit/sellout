using NUnit.Framework;
using Sellout;

namespace SelloutTests.StatementTests
{
    public class VariableDeclarationTests
    {
        [TestCase("name", "value", "var name = \"value\";")]
        [TestCase("name", 123, "var name = 123;")]
        [TestCase("a name is a name", 123, "var aNameIsAName = 123;")]
        public void c(string name, dynamic value, string cSharp)
        {
            Assert.That(new VariableDeclaration(name, value).ToCSharp(), Is.EqualTo(cSharp));
        }
    }
}