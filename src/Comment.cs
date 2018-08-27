namespace Sellout
{
    public class Comment : Statement
    {
        public string Text { get; }

        public Comment(string text)
        {
            Text = text;
        }

        public override string ToCSharp() => $"/* {Text} */";
    }
}