using System;

namespace Sellout
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Detroit!");
            var transpiler = new Transpiler(new Parser(), new CSharpWriter());
            transpiler.Go(args[0], args[1]);
        }
    }
}
