using NUnit.Framework;
using Sellout;

namespace SelloutTests.StatementTests
{
    public class CommentTests
    {
        [TestCase("casual fridays are canceled", "/* casual fridays are canceled */")]
        public void c(string comment, string cSharp)
        {
            Assert.That(new Comment(comment).ToCSharp(), Is.EqualTo(cSharp));
        }
    }
}