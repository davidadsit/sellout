namespace Sellout
{
    public class VariableDeclaration : Statement
    {
        public VariableDeclaration(string name, dynamic value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public dynamic Value { get; }

        public override string ToString() => $"{Name} => {Value}";
    }
}