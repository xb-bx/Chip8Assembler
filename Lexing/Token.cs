using System;
using System.Collections.Generic;
using System.Text;

namespace Chip8AssemblyCompiler.Lexing
{
    public class Token
    {
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
        public override string ToString()
        {
            return $"[{Type.ToString()}, {Value}]";
        }
        public TokenType Type { get; set; }
        public string Value { get; set; }
    }
}
