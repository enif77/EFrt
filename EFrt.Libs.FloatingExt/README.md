# FLOATING-EXT

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
| F**   | no   | IC   | **f3 = Pow(f1, f2)**<br>(f1 f2 -- f3)<br>The second floating point value on the stack is taken to the power of the top floating point stack value and the result is left on the top of the stack. |
| F.    | no   | IC   | **Print floating point**<br>(f -- )<br>A floating point value on the top of the stack is printed. |
| FABS  | no   | IC   | **f2 = Abs(f1)**<br>(f1 -- f2)<br>Absolute value of f1. |
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

## Words (Extra)

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| F=    | no   | IC   | **Floating equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 is equal to f2, 0 otherwise. |
| F<>   | no   | IC   | **Floating not equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 is not equal to f2, 0 otherwise. |
| F<=   | no   | IC   | **Floating less than or equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 <= f2, 0 otherwise. |
| F>    | no   | IC   | **Floating greater than**<br>(f1 f2 -- flag)<br>Returns -1 if f1 > f2, 0 otherwise. |
| F>=   | no   | IC   | **Floating greater than or equal**<br>(f1 f2 -- flag)<br>Returns -1 if f1 >= f2, 0 otherwise. |
| F>S   | no   | IC   | **Floating to single cell integer**<br>(f -- n)<br>Converts a float number on the top of the stack to a single cell integer and stores it on the top of the stack. |
| F0<>  | no   | IC   | **Nonzero**<br>(f -- flag)<br>Returns -1 if f is not equal to 0, 0 otherwise. |
| F0>   | no   | IC   | **Greater than zero**<br>(f -- flag)<br>Returns -1 if f is greater than 0, 0 otherwise. |
| F1+   | no   | IC   | **Add one**<br>(f1 -- f2)<br>Adds one to the top of the stack. |
| F1-   | no   | IC   | **Subtract one**<br>(f1 -- f2)<br>Substracts one from the top of the stack. |
| F2+   | no   | IC   | **Add two**<br>(f1 -- f2)<br>Adds two to the top of the stack. |
| F2-   | no   | IC   | **Subtract two**<br>(f1 -- f2)<br>Substracts two from the top of the stack. |
| F2*   | no   | IC   | **Times two**<br>(f1 -- f2)<br>Substracts two from the top of the stack. |
| F2/   | no   | IC   | **Divide by two**<br>(f1 -- f2)<br>Divides the top of the stack by two. |
| S>F   | no   | IC   | **Single cell integer to floating**<br>(n -- f)<br>Converts a single cell integer on the top of the stack to a floationg point number and stores it on the top of the stack. |

## TODO

Words: `DF! DF@ DFALIGN DFALIGNED DFLOAT+ DFLOATS FACOSH FALOG FASINH FATANH FCOSH FE. FEXPM1 FLN
  FLNP1 FS. FSINCOS FSINH FTANH F~ PRECISION SET-PRECISION SF! SF@ SFALIGN SFALIGNED SFLOAT+
  SFLOATS
  (LIT)`
