# FLOATING

Floating point (64 bit, double, 2 cells) numbers handling words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name    | Imm. | Mode | Stack op. | Description |
| ---     | ---  | ---  | ---       | --- |
| >FLOAT  | no   | IC   | **String to number**<br>( -- f true \| false) {s -- }<br>Parses a string into a floating point number. Leaves true and the number on the stack, if the conversion was successfull. Leaves just false, if the conversion failed. |
| D>F     | no   | IC   | **Double cell integer to floating**<br>(d -- f)<br>Converts a double cell integer on the top of the stack to a floationg point number and stores it on the top of the stack. |
| F!      | no   | IC   | **Store into address**<br>(f addr -- )<br>Stores the floating point number f into the address addr (a variables stack index). |
| F*      | no   | IC   | **f3 = f1 * f2**<br>(f1 f2 -- f3)<br>Multiplies two floating point numbers on the top of the stack and leaves the product on the stack. |
| F+      | no   | IC   | **f3 = f1 + f2**<br>(f1 f2 -- f3)<br>Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack. |
| F-      | no   | IC   | **f3 = f1 - f2**<br>(f1 f2 -- f3)<br>Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack. |
| F/      | no   | IC   | **f3 = f1 / f2**<br>(f1 f2 -- f3)<br>Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack. |
| F0<     | no   | IC   | **Less than zero**<br>(f -- flag)<br>Returns -1 if f is less than 0, 0 otherwise. |
| F0=     | no   | IC   | **Equal to zero**<br>(f -- flag)<br>Returns -1 if f is equal to 0, 0 otherwise. |
| F<      | no   | IC   | **Floating less than**<br>(f1 f2 -- flag)<br>Returns -1 if f1 < f2, 0 otherwise. |
| F>D     | no   | IC   | **Floating to double cell integer**<br>(f -- d)<br>Converts a float number on the top of the stack to a double cell integer and stores it on the top of the stack. |
| F@      | no   | IC   | **Fetch**<br>(addr -- f)<br>Loads the floating point number at addr (a variables stack index) and leaves it at the top of the stack. |
| FCONSTANT x | no   | I    | **Declare constant**<br>(f --)<br>Declares a constant named x. When x is executed, the value f will be left on the stack. |
| FDEPTH  | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| FLOOR   | no   | IC   | **f2 = Floor(f1)**<br>(f1 -- f2)<br>Returns the largest integral value less than or equal to the specified number. |
| FMAX    | no   | IC   | **Floating point maximum**<br>(f1 f2 -- f3)<br>The greater of the two floating point values on the top of the stack is placed on the top of the stack. |
| FMIN    | no   | IC   | **Floating point minimum**<br>(f1 f2 -- f3)<br>The lesser of the two floating point values on the top of the stack is placed on the top of the stack. |
| FNEGATE | no   | IC   | **f2 = -f1**<br>(f1 -- f2)<br>The negative of the floating point value on the top of the stack replaces the floating point value there. |
| FVARIABLE x | no   | I    | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |

## TODO

Words: `FALIGN FALIGNED FDROP FDUP FLITERAL FLOAT+ FLOATS FOVER FROT FROUND FSWAP REPRESENT
  DF! DF@ DFALIGN DFALIGNED DFLOAT+ DFLOATS FACOSH FALOG FASINH FATANH FCOSH FE. FEXPM1 FLN
  FLNP1 FS. FSINCOS FSINH FTANH F~ PRECISION SET-PRECISION SF! SF@ SFALIGN SFALIGNED SFLOAT+
  SFLOATS
  (LIT)`
