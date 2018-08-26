using System;
using System.IO;
using NUnit.Framework;
using Sellout;

namespace SelloutTests.CSharpWriterTests
{
    public class BoilerplateTests
    {
        CSharpWriter writer;

        [SetUp]
        public void Setup()
        {
            writer = new CSharpWriter();
            writer.Write("test-project");
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete("test-project", true);
        }

        [Test]
        public void Writes_a_console_app_project_file()
        {
            var programText = File.ReadAllText("test-project/rockstar.csproj");
            Assert.That(programText.StartsWith(@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <RootNamespace>Rockstar</RootNamespace>
  </PropertyGroup>

</Project>"));
        }

        [Test]
        public void Writes_a_console_app_header()
        {
            var programText = File.ReadAllText("test-project/Program.cs");
            Assert.That(programText.StartsWith(@"﻿using System;

namespace console_template
{
    class Program
    {
        static void Main(string[] args)
        {
            // C# transpiled from the source Rockstar by sellout"));
        }

        [Test]
        public void Writes_a_console_app_footer()
        {
            var programText = File.ReadAllText("test-project/Program.cs");
            Assert.That(programText.EndsWith(@"        }
    }
}"));
        }
    }
}