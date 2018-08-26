using System.Globalization;

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

        public string CleanName
        {
            get
            {
                var strings = Name.ToLowerInvariant().Split(' ', '-');
                for (var i = 1; i < strings.Length; i++)
                {
                    strings[i] = new CultureInfo("en-US", false).TextInfo.ToTitleCase(strings[i]);
                }

                return string.Join("", strings);
            }
        }

        public override string ToString()
        {
            return $"{Name} => {Value}";
        }

        public override string ToCSharp()
        {
            if (Value is string)
            {
                return $"var {CleanName} = \"{Value}\";";
            }
            return $"var {CleanName} = {Value};";
        }
    }
}