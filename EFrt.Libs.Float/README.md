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

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| F!    | no   | IC   | (f addr -- ) | **Store into address**<br>Stores the floating point number f into the address addr (a variables stack index). |
| F@    | no   | IC   | (addr -- f) | **Fetch**<br>Loads the floating point number at addr (a variables stack index) and leaves it at the top of the stack. |
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
| F0<   | no   | IC   | (f -- flag)  | **Less than zero**<br>Returns -1 if f is less than 0, 0 otherwise. |
| F0<>  | no   | IC   | (f -- flag)  | **Nonzero**<br>Returns -1 if f is not equal to 0, 0 otherwise. |
| F0=   | no   | IC   | (f -- flag)  | **Equal to zero**<br>Returns -1 if f is equal to 0, 0 otherwise. |
| F0>   | no   | IC   | (f -- flag)  | **Greater than zero**<br>Returns -1 if f is greater than 0, 0 otherwise. |
| F1+   | no   | IC   | (f1 -- f2) | **Add one**<br>Adds one to the top of the stack. |
| F1-   | no   | IC   | (f1 -- f2) | **Subtract one**<br>Substracts one from the top of the stack. |
| F2+   | no   | IC   | (f1 -- f2) | **Add two**<br>Adds two to the top of the stack. |
| F2-   | no   | IC   | (f1 -- f2) | **Subtract two**<br>Substracts two from the top of the stack. |
| F2*   | no   | IC   | (f1 -- f2) | **Times two**<br>Substracts two from the top of the stack. |
| F2/   | no   | IC   | (f1 -- f2) | **Divide by two**<br>Divides the top of the stack by two. |
| FABS  | no   | IC   | (f1 -- f2) | **f2 = Abs(f1)**<br>. |
| CONSTANT x | no   | I    | (f --)    | **Declare constant**<br>Declares a constant named x. When x is executed, the value f will be left on the stack. |
| DEPTH    | no   | IC   | ( -- n)   | **Stack depth**<br>Returns the number of items on the stack before DEPTH was executed. |
| FIX   | no   | IC   | (f -- n) | **Floating to integer**<br>Converts a float number on the top of the floating poit stack to integer and stores it on the top of the data stack. |
| FLOAT | no   | IC   | (n -- f) | **Integer to floating**<br>Converts an integer on the top of the data stack to a floationg point number and stores it on the top of the floating point stack. |
| FMAX  | no   | IC   | (f1 f2 -- f3) | **Floating point maximum**<br>The greater of the two floating point values on the top of the stack is placed on the top of the stack. |
| FMIN  | no   | IC   | (f1 f2 -- f3) | **Floating point minimum**<br>The lesser of the two floating point values on the top of the stack is placed on the top of the stack. |
| VARIABLE x | no   | I    |            | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |

## TODO

Words: `ACOS ASIN ATAN ATAN2 COS EXP NEGATE FNEGATE (LIT) LOG POW SIN SQRT TAN \>FLOAT FLOOR F** S>F F>S D>F F>D`
