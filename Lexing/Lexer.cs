using System;
using System.Collections.Generic;
using System.Text;

namespace Chip8AssemblyCompiler.Lexing
{
    public class Lexer
    {
        private string code;
        private int pos;

        public Lexer(string code)
        {
            this.code = code;
        }
        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();
            while (pos < code.Length)
            {
                if (char.IsLetter(code[pos]))
                {
                    tokens.Add(TokenizeOperation());
                }
                else if (char.IsDigit(code[pos]) || Uri.IsHexDigit(code[pos]))
                {
                    tokens.Add(TokenizeNumber());
                }
                else if (code[pos] == ',')
                {
                    tokens.Add(new Token(TokenType.Comma, ""));
                    pos++;
                }
                else if (code[pos]=='#')
                {
                    pos++;
                    while (code[pos] != '\n') 
                    {
                        pos++;
                    }
                    pos++;
                }
                else
                {
                    pos++;
                }
            }
            return tokens;
        }
        private Token TokenizeOperation()
        {
            var res = "";
            while (pos < code.Length && (char.IsLetter(code[pos]) || code[pos] == ':' || code[pos] == '_'))
            {
                res += code[pos];
                pos++;
            }
            if (res.EndsWith(":")) {
                return new Token(TokenType.Label, res.TrimEnd(':'));
            }
            return new Token(TokenType.Operation, res);
        }

        private Token TokenizeNumber()
        {
            var res = "";
            while (pos < code.Length && (char.IsDigit(code[pos]) || Uri.IsHexDigit(code[pos]) || code[pos] == 'x' || code[pos] == 'b')) 
            {
                res += code[pos];
                pos++;
            }
            return new Token(TokenType.Num, res);
        }
    }
}
