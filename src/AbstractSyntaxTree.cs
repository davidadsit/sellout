using System;
using System.Collections.Generic;

namespace Sellout
{
    public class AbstractSyntaxTree
    {
        readonly List<Statement> statements = new List<Statement>();

        public IEnumerable<Statement> Statements => statements.AsReadOnly();

        public void DeclareVariable(string name, dynamic value)
        {
            statements.Add(new Variable(name, value));
        }
    }
}