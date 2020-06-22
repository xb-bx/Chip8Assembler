using System;
using System.Collections.Generic;
using System.Text;

namespace Chip8AssemblyCompiler.Lexing
{
    public enum TokenType
    {
        Operation,
        Comma,
        Num,
        Label,
    }
}
