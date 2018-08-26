using System;
using System.IO;
using System.Text;

namespace Sellout
{
    public interface ICSharpWriter
    {
        void Write(string path);
        void AppendStatement(string statement);
    }

    public class CSharpWriter : ICSharpWriter
    {
        readonly StringBuilder statements;

        public CSharpWriter()
        {
            statements = new StringBuilder();
        }

        public void Write(string path)
        {
            MakeProjectPath(path);
            WriteProjectFile(path);
            WriteProgramHeader(path);
            WriteProgramStatements(path);
            WriteProgramFooter(path);
        }

        void MakeProjectPath(string path)
        {
            Directory.CreateDirectory(path);
        }

        static void WriteProjectFile(string path)
        {
            File.WriteAllText($"{path}/rockstar.csproj", @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <RootNamespace>Rockstar</RootNamespace>
  </PropertyGroup>

</Project>");
        }

        void WriteProgramHeader(string path)
        {
            File.WriteAllText($"{path}/Program.cs", @"﻿using System;

namespace Rockstar
{
    class Program
    {
        static void Main(string[] args)
        {
            // C# transpiled from the source Rockstar by sellout
");
        }

        void WriteProgramStatements(string path)
        {
            File.AppendAllText($"{path}/Program.cs", statements.ToString());
        }

        void WriteProgramFooter(string path)
        {
            File.AppendAllText($"{path}/Program.cs", @"        }
    }
}");
        }

        public void AppendStatement(string statement)
        {
            statements.AppendLine($"            {statement}");
        }
    }
}