# FLOATING

Floating point (64 bit, double, 2 cells) numbers handling words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| D>F   | no   | IC   | **Double cell integer to floating**<br>(d -- f)<br>Converts a double cell integer on the top of the stack to a floationg point number and stores it on the top of the stack. |
| F!    | no   | IC   | **Store into address**<br>(f addr -- )<br>Stores the floating point number f into the address addr (a variables stack index). |
| F@    | no   | IC   | **Fetch**<br>(addr -- f)<br>Loads the floating point number at addr (a variables stack index) and leaves it at the top of the stack. |
| F+    | no   | IC   | **f3 = f1 + f2**<br>(f1 f2 -- f3)<br>Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack. |
| F-    | no   | IC   | **f3 = f1 - f2**<br>(f1 f2 -- f3)<br>Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack. |
| F*    | no   | IC   | **f3 = f1 * f2**<br>(f1 f2 -- f3)<br>Multiplies two floating point numbers on the top of the stack and leaves the product on the stack. |
| F/    | no   | IC   | **f3 = f1 / f2**<br>(f1 f2 -- f3)<br>Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack. |
| F=    | no   | IC   | **Floating equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 is equal to f2, 0 otherwise. |
| F<>   | no   | IC   | **Floating not equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 is not equal to f2, 0 otherwise. |
| F<    | no   | IC   | **Floating less than**<br>(f1 f2 -- flag)<br>Returns -1 if f1 < f2, 0 otherwise. |
| F<=   | no   | IC   | **Floating less than or equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 <= f2, 0 otherwise. |
| F>    | no   | IC   | **Floating greater than**<br>(f1 f2 -- flag)<br>Returns -1 if f1 > f2, 0 otherwise. |
| F>=   | no   | IC   | **Floating greater than or equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 >= f2, 0 otherwise. |
| F>D   | no   | IC   | **Floating to double cell integer**<br>(f -- d)<br>Converts a float number on the top of the stack to a double cell integer and stores it on the top of the stack. |
| F>S   | no   | IC   | **Floating to single cell integer**<br>(f -- n)<br>Converts a float number on the top of the stack to a single cell integer and stores it on the top of the stack. |
| F0<   | no   | IC   | **Less than zero**<br>(f -- flag)<br>Returns -1 if f is less than 0, 0 otherwise. |
| F0<>  | no   | IC   | **Nonzero**<br>(f -- flag)<br>Returns -1 if f is not equal to 0, 0 otherwise. |
| F0=   | no   | IC   | **Equal to zero**<br>(f -- flag)<br>Returns -1 if f is equal to 0, 0 otherwise. |
| F0>   | no   | IC   | **Greater than zero**<br>(f -- flag)<br>Returns -1 if f is greater than 0, 0 otherwise. |
| F1+   | no   | IC   | **Add one**<br>(f1 -- f2)<br>Adds one to the top of the stack. |
| F1-   | no   | IC   | **Subtract one**<br>(f1 -- f2)<br>Substracts one from the top of the stack. |
| F2+   | no   | IC   | **Add two**<br>(f1 -- f2)<br>Adds two to the top of the stack. |
| F2-   | no   | IC   | **Subtract two**<br>(f1 -- f2)<br>Substracts two from the top of the stack. |
| F2*   | no   | IC   | **Times two**<br>(f1 -- f2)<br>Substracts two from the top of the stack. |
| F2/   | no   | IC   | **Divide by two**<br>(f1 -- f2)<br>Divides the top of the stack by two. |
| FABS  | no   | IC   | **f2 = Abs(f1)**<br>(f1 -- f2)<br>Absolute value of f1. |
| FNEGATE | no   | IC   | **f2 = -f1**<br>(f1 -- f2)<br>The negative of the floating point value on the top of the stack replaces the floating point value there. |
| FACOS | no   | IC   | **f2 = arccos f1**<br>(f1 -- f2)<br>Replaces floating point top of stack with its arc cosine. |
| FASIN | no   | IC   | **f2 = arcsin f1**<br>(f1 -- f2)<br>Replaces floating point top of stack with its arc sine. |
| FATAN | no   | IC   | **f2 = arctan f1**<br>(f1 -- f2)<br>Replaces floating point top of stack with its arc tangent. |
| FATAN2 | no   | IC   | **f3 = arctan f1 / f2**<br>(f1 f2 -- f3)<br>Replaces the two floating point numbers on the top of the stack with the arc tangent of their quotient, properly handling zero denominators. |
| FCOS  | no   | IC   | **f2 = Cos(f1)**<br>(f1 -- f2)<br>The floating point value on the top of the stack is replaced by its cosine. | 
| FEXP  | no   | IC   | **f2 = Exp(f1)**<br>(f1 -- f2)<br>The floating point value on the top of the stack is replaced by its natural antilogarithm. | 
| FLOG  | no   | IC   | **f2 = Log(f1)**<br>(f1 -- f2)<br>The floating point value on the top of the stack is replaced by its natural logarithm. | 
| FSIN  | no   | IC   | **f2 = Sin(f1)**<br>(f1 -- f2)<br>The floating point value on the top of the stack is replaced by its sine. | 
| FSQRT | no   | IC   | **f2 = Sqrt(f1)**<br>(f1 -- f2)<br>The floating point value on the top of the stack is replaced by its square root. |
| FTAN  | no   | IC   | **f2 = Tan(f1)**<br>(f1 -- f2)<br>The floating point value on the top of the stack is replaced by its tangent. | 
| FLOOR | no   | IC   | **f2 = Floor(f1)**<br>(f1 -- f2)<br>Returns the largest integral value less than or equal to the specified number. |
| F**   | no   | IC   | **f3 = Pow(f1, f2)**<br>(f1 f2 -- f3)<br>The second floating point value on the stack is taken to the power of the top floating point stack value and the result is left on the top of the stack. |
| FCONSTANT x | no   | I    | **Declare constant**<br>(f --)<br>Declares a constant named x. When x is executed, the value f will be left on the stack. |
| FDEPTH | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| FMAX  | no   | IC   | **Floating point maximum**<br>(f1 f2 -- f3)<br>The greater of the two floating point values on the top of the stack is placed on the top of the stack. |
| FMIN  | no   | IC   | **Floating point minimum**<br>(f1 f2 -- f3)<br>The lesser of the two floating point values on the top of the stack is placed on the top of the stack. |
| FVARIABLE x | no   | I    | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |
| S>F   | no   | IC   | **Single cell integer to floating**<br>(n -- f)<br>Converts a single cell integer on the top of the stack to a floationg point number and stores it on the top of the stack. |

## TODO

Words: `(LIT) \>FLOAT`
