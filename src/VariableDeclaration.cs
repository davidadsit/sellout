namespace Sellout
{
    public struct VariableDeclaration : Statement
    {
        public VariableDeclaration(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public object Value { get; }
    }
}