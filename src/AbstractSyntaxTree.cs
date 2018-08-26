using System;
using System.Collections.Generic;

namespace Sellout
{
    public class AbstractSyntaxTree
    {
        readonly List<Statement> statements = new List<Statement>();

        public IEnumerable<Statement> Statements => statements.AsReadOnly();

        public void DeclareVariable(string name, object value)
        {
            statements.Add(new VariableDeclaration(name, value));
        }
    }
}