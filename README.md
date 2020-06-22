# Chip8 Assembler
Simple assembler for chip8 writen in C# and .NET Core

## Usage:
`Chip8AssemblyCompiler.exe -asm <assembly file> -out <output file>`
## Syntax:
| Command    | Arguments                 | Action                                                                             | Machine code | Example                                                       |
|------------|---------------------------|------------------------------------------------------------------------------------|--------------|---------------------------------------------------------------|
| CLEAR      | none                      | Clears display                                                                     | 00E0         | CLEAR                                                         |
| RETURN     | none                      | Returns from the subroutine                                                        | 00EE         | RETURN                                                        |
| JUMP       | address: ushort           | Jumps to the address                                                               | 1nnn         | JUMP 512 or JUMP 0x200 or JUMP 0b1000000000 or JUMP LABELNAME |
| CALL       | address: ushort           | Calls the subroutine at the address                                                | 2nnn         | CALL 512                                                      |
| SEREG      | x: byte, y: byte          | Skip next instruction if register x is equal to register y                         | 5xy0         | SEREG 0, 1                                                    |
| SNE        | x: byte, kk: byte         | Skip next instruction if register x is __NOT__ equal to kk             | 4xkk         | SNE 0, 232                                                    |
| LD         | x: byte, kk: byte         | Loads kk to register x                                                             | 6xkk         | LD 0, 0xFF                                                    |
| ADD        | x: byte, kk: byte         | Adds kk to register x                                                              | 7xkk         | ADD 0, 0b10101001                                             |
| LDREG      | x: byte, y: byte          | Loads value of register y to register x                                            | 8xy0         | LDREG 0, 1                                                    |
| OR         | x: byte, y: byte          | Loads register x \| register y to register x                                       | 8xy1         | OR 0, 1                                                       |
| AND        | x: byte, y: byte          | Loads register x & register y to register x                                        | 8xy2         | AND 0, 1                                                      |
| XOR        | x: byte, y: byte          | Loads register x ^ register y to register x                                        | 8xy3         | XOR 0, 1                                                      |
| SUB        | x: byte, y: byte          | Substructs register y from register x                                              | 8xy5         | SUB 0, 1                                                      |
| SHR        | x: byte, y: byte          | Loads register y >> 1 to register x                                                | 8xy6         | SHR 0, 1                                                      |
| SUBN       | x: byte, y: byte          | Loads register y \- register x to register x                                       | 8xy7         | SUBN 0, 1                                                     |
| SHL        | x: byte, y: byte          | Loads register y << 1 to register x                                                | 8xyE         | SHL 0, 1                                                      |
| SNEREG     | x: byte, y: byte          | Skip next instruction if register x is NOT equal to register y                     | 9xy0         | SNEREG 0, 1                                                   |
| LDI        | address: ushort           | Loads address to I                                                                 | Annn         | LDI 512                                                       |
| JUMPREG    | address: ushort           | Jumps to address \+ register 0                                                     | Bnnn         | JUMPREG 512                                                   |
| RND        | x: byte, kk: byte         | Loads random value & kk to register x                                              | Cxkk         | RND 255                                                       |
| DRAW       | x: byte, y: byte, n: byte | Draw n\-byte sprite at memory location I at coordinates \(register x, register y\) | Dxyn         | DRAW 0, 1, 5                                                  |
| SKNP       | x: byte                   | Skip next instruction if key register x NOT pressed                                | ExA1         | SKNP 0                                                        |
| LDDT       | x: byte                   | Loads value of delay timer to register x                                           | Fx07         | LDDT 0                                                        |
| LDK        | x: byte                   | Wait for key press and then store value of key to register x                       | Fx0A         | LDK 0                                                         |
| STREG      | x: byte                   | Loads value of register x to sound timer                                           | Fx18         | STREG 0                                                       |
| ADDI       | x: byte                   | Add value of register x to register I                                              | Fx1E         | ADDI 0                                                        |
| LDISP      | x: byte                   | Loads value of sprite location for digit register x                                | Fx29         | LDISP 0                                                       |
| STORE      | x: byte                   | Strore registrers from 0 to x in memory                                            | Fx55         | STORE 0                                                       |
| FILL       | x: byte                   | Fill register 0 to register x from memory at I                                     | Fx65         | FILL 0                                                        |
| LABELNAME: | none                      | Creates a label                                                                    | none         | MyLabel:                                                      |


