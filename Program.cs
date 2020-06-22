using Chip8AssemblyCompiler.Lexing;
using Chip8AssemblyCompiler.Parsing;
using System;
using System.IO;

namespace Chip8AssemblyCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage:\n\t-asm <assembly file>\n\t-out <output file>");
                return;
            }
            var assemblyFilePath = args[1];
            if (!File.Exists(assemblyFilePath))
            {
                Console.WriteLine("File doesnt exists!!!");
                return;
            }
            Lexer lexer = new Lexer(File.ReadAllText(assemblyFilePath));
            var tokens = lexer.Tokenize();
            foreach (var item in tokens)
            {
                Console.WriteLine(item);
            }
            var parser = new Parser(tokens);
            var bytes = parser.Parse();
            File.WriteAllBytes(args[3], bytes);
        }
    }
}
