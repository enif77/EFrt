# FLOAT

Floating point (64 bit, double, 2 cells) numbers handling words. The CORE library has words for double cell stack operations.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, \{} = object stack.
- Description: A description of the word.

Words: `F1+ F1- F2+ F2- F2* F2/ F0= F0<> F0< F0>`

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| F+    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 + f2**<br>Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack. |
| F-    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 - f2**<br>Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack. |
| F*    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 * f2**<br>Multiplies two floating point numbers on the top of the stack and leaves the product on the stack. |
| F/    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 / f2**<br>Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack. |
| F=    | no   | IC   | (f1 f2 -- flag) | **Floating equal**<br>Returns -1 if f1 is equal to f2, 0 otherwise. |
| F<>   | no   | IC   | (f1 f2 -- flag) | **Floating not equal**<br>Returns -1 if f1 is not equal to f2, 0 otherwise. |
| F<    | no   | IC   | (f1 f2 -- flag) | **Floating less than**<br>Returns -1 if f1 < f2, 0 otherwise. |
| F<=   | no   | IC   | (f1 f2 -- flag) | **Floating less than or equal**<br>Returns -1 if f1 <= f2, 0 otherwise. |
| F>    | no   | IC   | (f1 f2 -- flag) | **Floating greater than**<br>Returns -1 if f1 > f2, 0 otherwise. |
| F>=   | no   | IC   | (f1 f2 -- flag) | **Floating greater than or equal**<br>Returns -1 if f1 >= f2, 0 otherwise. |
| FABS  | no   | IC   | (f1 -- f2) | **f2 = Abs(f1)**<br>. |
| FIX   | no   | IC   | (f -- n) | **Floating to integer**<br>Converts a float number on the top of the floating poit stack to integer and stores it on the top of the data stack. |
| FLOAT | no   | IC   | (n -- f) | **Integer to floating**<br>Converts an integer on the top of the data stack to a floationg point number and stores it on the top of the floating point stack. |
| FMAX  | no   | IC   | (f1 f2 -- f3) | **Floating point maximum**<br>The greater of the two floating point values on the top of the stack is placed on the top of the stack. |
| FMIN  | no   | IC   | (f1 f2 -- f3) | **Floating point minimum**<br>The lesser of the two floating point values on the top of the stack is placed on the top of the stack. |

## TODO

Words: ACOS ASIN ATAN ATAN2 COS EXP n NEGATE FNEGATE (LIT) LOG POW SIN SQRT TAN \>FLOAT FLOOR FLITERAL (FLIT)
