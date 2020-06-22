using Chip8AssemblyCompiler.Lexing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chip8AssemblyCompiler.Parsing
{
    public class Parser
    {
        private List<Token> tokens; 
        private int pos;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }
        public byte[] Parse()
        {
            Dictionary<string, ushort> labels = new Dictionary<string, ushort>();
            var bytes = new List<byte>();
            while (pos < tokens.Count)
            {
                if (tokens[pos].Type == TokenType.Operation)
                {
                    switch (tokens[pos].Value)
                    {
                        case "CLEAR":
                            bytes.AddRange(GetOpcode(0, 0x0E0));
                            pos += 1;
                            break;
                        case "RETURN":
                            bytes.AddRange(GetOpcode(0, 0x0EE));
                            pos += 1;
                            break;
                        case "JUMPREG":
                        case "JUMP":
                            {
                                byte first = 0;
                                if (tokens[pos].Value == "JUMP")
                                {
                                    first = 1;
                                }
                                else
                                {
                                    first = 11;
                                }
                                pos += 1;
                                if (tokens[pos].Type == TokenType.Operation)
                                {
                                    var success = labels.TryGetValue(tokens[pos].Value, out ushort addr);
                                    if (!success)
                                    {
                                        throw new Exception($"Label {tokens[pos].Value} not found");
                                    }
                                    bytes.AddRange(GetOpcode(first, addr));
                                    pos += 1;
                                }
                                else
                                {
                                    //                     Console.WriteLine($"value {tokens[pos].Value}");
                                    var addr = ParseNum(tokens[pos].Value);
                                    bytes.AddRange(GetOpcode(first, (ushort)addr));
                                    pos += 1;
                                }
                            }
                            break;
                        case "LDI":
                        case "CALL": 
                            {
                                byte first = 0;
                                switch (tokens[pos].Value)
                                { 
                                    case "CALL": first = 2; break;
                                    case "LDI": first = 10; break; 
                                    default:
                                        break;
                                }
                                pos += 1;
                                var num = ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(first, (ushort)num));
                                pos += 1;
                            }
                            break;
                        case "RND":
                        case "ADD":
                        case "LD":
                        case "SNE":
                        case "SE":
                            {
                                byte first = 0;
                                switch (tokens[pos].Value)
                                {
                                    case "SE": first = 3; break;
                                    case "SNE": first = 4; break;
                                    case "LD": first = 6; break;
                                    case "ADD": first = 7; break;
                                    case "RND": first = 12; break;

                                    default:
                                        break;
                                }
                                pos += 1;
                                byte x = (byte)ParseNum(tokens[pos].Value);
                                pos += 2;
                                byte kk = (byte)ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(first, x, kk));
                                pos += 1;
                            }
                            break;
                        case "SEREG":
                            {
                                pos += 1;
                                byte x = (byte)ParseNum(tokens[pos].Value);
                                pos += 2;
                                byte y = (byte)ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(5, x, y, 0));
                                pos += 1;
                            }
                            break;
                        case "OR":
                        case "XOR":
                        case "AND":
                        case "ADDREG":
                        case "SUB":
                        case "SHR":
                        case "SUBN":
                        case "SHL":
                        case "LDREG":
                            {
                                byte last = 0;
                                switch (tokens[pos].Value)
                                {
                                    case "LDREG": last = 0; break;
                                    case "OR": last = 1; break;
                                    case "AND": last = 2; break;
                                    case "XOR": last = 3; break;
                                    case "ADDREG": last = 4; break;
                                    case "SUB": last = 5; break;
                                    case "SHR": last = 6; break;
                                    case "SUBN": last = 7; break;
                                    case "SHL": last = 0xE; break;
                                    default:
                                        break;
                                }
                                pos += 1;
                                byte x = (byte)ParseNum(tokens[pos].Value);
                                pos += 2;
                                byte y = (byte)ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(8, x, y, last));
                                pos += 1;
                            }
                            break;
                        case "SNEREG":
                            {
                                pos += 1;
                                byte x = (byte)ParseNum(tokens[pos].Value);
                                pos += 2;
                                byte y = (byte)ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(9, x, y, 0));
                                pos += 1;
                            }
                            break;
                        case "DRAW":
                            {
                                pos += 1;
                                byte x = (byte)ParseNum(tokens[pos].Value);
                                pos += 2;
                                byte y = (byte)ParseNum(tokens[pos].Value);
                                pos += 2;
                                byte n = (byte)ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(0xD, x, y, n));
                                pos += 1;
                            }
                            break;
                        case "SKP":
                        case "SKNP":
                        case "LDDT":
                        case "LDK":
                        case "DTREG":
                        case "STREG":
                        case "ADDI":
                        case "LDISP":
                        case "LDIBCD":
                        case "STORE":
                        case "FILL":
                            {
                                byte first = 0;
                                byte kk = 0;
                                switch (tokens[pos].Value)
                                {
                                    case "SKP":
                                        first = 0xE;
                                        kk = 0x9E;
                                        break;
                                    case "SKNP":
                                        first = 0xE;
                                        kk = 0xA1;
                                        break;
                                    case "LDDT":
                                        first = 0xF;
                                        kk = 0x07;
                                        break;
                                    case "LDK":
                                        first = 0xF;
                                        kk = 0x0A;
                                        break;
                                    case "DTREG":
                                        first = 0xF;
                                        kk = 0x15;
                                        break;
                                    case "STREG":
                                        first = 0xF;
                                        kk = 0x18;
                                        break;
                                    case "ADDI":
                                        first = 0xF;
                                        kk = 0x1E;
                                        break;
                                    case "LDISP":
                                        first = 0xF;
                                        kk = 0x29;
                                        break;
                                    case "LDIBCD":
                                        first = 0xF;
                                        kk = 0x33;
                                        break;
                                    case "STORE":
                                        first = 0xF;
                                        kk = 0x55;
                                        break;
                                    case "FILL":
                                        first = 0xF;
                                        kk = 0x65;
                                        break;
                                    default:
                                        break;
                                }
                                pos += 1;
                                byte x = (byte)ParseNum(tokens[pos].Value);
                                bytes.AddRange(GetOpcode(first, x, kk));
                            }
                            pos += 1;
                            break;
                        default:
                            pos += 1;
                            break;
                    }
                }
                else if (tokens[pos].Type == TokenType.Label)
                {
                    labels.Add(tokens[pos].Value, (ushort)(pos * 2 + 512));
                    bytes.Add(0x80);
                    bytes.Add(0x0F);
                    pos += 1;
                }
                else
                {
                    throw new Exception("Invalid syntax");
                }
            } 
            return bytes.ToArray();
        }

        public byte[] GetOpcode(byte first, ushort nnn)
        {
            var bytes = new byte[2];
            byte fst = (byte)((first << 4) | ((nnn & 0x0F00) >> 8));
            byte snd = ((byte)(nnn & 0x00FF));
            bytes[0] = fst;
            bytes[1] = snd;
            return bytes;
        }
        public byte[] GetOpcode(byte first, byte x, ushort y, byte last)
        {
            var bytes = new byte[2];
            byte fst = (byte)((first << 4) | ((x & 0x0F)));
            byte snd = ((byte)((y << 4) | (last & 0x0F)));
            bytes[0] = fst;
            bytes[1] = snd;
            return bytes;
        }
        public byte[] GetOpcode(byte first, byte x, byte kk)
        {
            var bytes = new byte[2];
            byte fst = (byte)((first << 4) | ((x & 0x0F)));
            bytes[0] = fst;
            bytes[1] = kk;
            return bytes;
        }

        public int ParseNum(string num)
        {
            if (num.StartsWith("0x"))
            {
                num = num.TrimStart('0', 'x');
                return Convert.ToInt32(num, 16);
            }
            else if (num.StartsWith("0b"))
            {
                num = num.TrimStart('0', 'b');
                return Convert.ToInt32(num, 2);
            }
            else
            {
                return int.Parse(num);
            }
        }

    }
}
