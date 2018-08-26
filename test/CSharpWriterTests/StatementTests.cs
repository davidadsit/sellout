using System.IO;
using NUnit.Framework;
using Sellout;

namespace SelloutTests.CSharpWriterTests
{
    public class StatementTests
    {
        CSharpWriter writer;

        [SetUp]
        public void Setup()
        {
            writer = new CSharpWriter();
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete("test-project", true);
        }

        [Test]
        public void Writes_writes_a_single_statement()
        {
            writer.AppendStatement("var testVariable = 0;");
            writer.Write("test-project");
            var programText = File.ReadAllText("test-project/Program.cs");
            Assert.That(programText.Contains("var testVariable = 0;"));
        }
    }
}