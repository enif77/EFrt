﻿# FLOATING-EXT

Floating point (64 bit, double, 2 cells) numbers handling words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name      | Imm. | Mode | Description |
| ---       | ---  | ---  | --- |
| DF!       | no   | IC   | **Store into address**<br>(f addr -- )<br>Stores the floating point number f into the address addr (a heap array index). |
| DF@       | no   | IC   | **Fetch**<br>(addr -- ) (F: -- f)<br>Loads the floating point number at addr a heap array index) and leaves it at the top of the stack. |
| DFALIGN   | no   | IC   | **Align data pointer**<br>( -- )<br> If the data-space pointer is not float aligned, reserve enough data space to make it so. |
| DFALIGNED | no   | IC   | **Get aligned address**<br>(addr1 -- addr2)<br> The addr2 address is the first float-aligned address greater than or equal to addr1. |
| DFLOAT+   | no   | IC   | **Add floating point size**<br>(addr1 -- addr2)<br>Add the size in address units of a floating-point number to addr1, giving addr2. |
| DFLOATS   | no   | IC   | **Get floating point size**<br>(n1 -- n2)<br>n2 is the size in address units of n1 floating point numbers. |
| F**       | no   | IC   | **f3 = Pow(f1, f2)**<br>(F: f1 f2 -- f3)<br>The second floating point value on the stack is taken to the power of the top floating point stack value and the result is left on the top of the stack. |
| F.        | no   | IC   | **Print floating point**<br>(F: f -- )<br>A floating point value on the top of the stack is printed. |
| F>S       | no   | IC   | **Floating to single cell integer**<br>( -- n) (F: f -- )<br>Converts a floating point number on the top of the floating point stack to a single cell integer and stores it on the top of the stack. |
| FABS      | no   | IC   | **f2 = Abs(f1)**<br>(F: f1 -- f2)<br>Absolute value of f1. |
| FACOS     | no   | IC   | **f2 = Acos(f1)**<br>(F: f1 -- f2)<br>Replaces floating point top of stack with its arc cosine. |
| FACOSH    | no   | IC   | **f2 = Acosh(f1)**<br>(F: f1 -- f2)<br>Replaces floating point top of stack with its hyperbolic arc cosine. |
| FALOG     | no   | IC   | **f2 = Pow(10, f1)**<br>(F: f1 -- f2)<br>Raise ten to the power of f1, giving f2. |
| FASIN     | no   | IC   | **f2 = Asin(f1)**<br>(F: f1 -- f2)<br>Replaces floating point top of stack with its arc sine. |
| FASINH    | no   | IC   | **f2 = Asinh(f1)**<br>(F: f1 -- f2)<br>Replaces floating point top of stack with its hyperbolic arc sine. |
| FATAN     | no   | IC   | **f2 = Atan(f1)**<br>(F: f1 -- f2)<br>Replaces floating point top of stack with its arc tangent. |
| FATAN2    | no   | IC   | **f3 = Atan(f1 / f2)**<br>(F: f1 f2 -- f3)<br>Replaces the two floating point numbers on the top of the stack with the arc tangent of their quotient, properly handling zero denominators. |
| FATANH    | no   | IC   | **f2 = Atanh(f1)**<br>(F: f1 -- f2)<br>Replaces floating point top of stack with its hyperbolic arc tangent. |
| FCOS      | no   | IC   | **f2 = Cos(f1)**<br>(F: f1 -- f2)<br>f2 is the cosine of the radian angle f1. | 
| FCOSH     | no   | IC   | **f2 = Cosh(f1)**<br>(F: f1 -- f2)<br>f2 is the hyperbolic cosine of f1. | 
| FEXP      | no   | IC   | **f2 = Exp(f1)**<br>(F: f1 -- f2)<br>Raise e to the power f1, giving f2. | 
| FEXPM1    | no   | IC   | **f2 = Exp(f1) - 1**<br>(F: f1 -- f2)<br>Raise e to the power f1 and subtract 1, giving f2. | 
| FLN       | no   | IC   | **f2 = Log(f1)**<br>(F: f1 -- f2)<br>f2 is the natural logarithm of f1. | 
| FLNP1     | no   | IC   | **f2 = Log(f1)**<br>(F: f1 -- f2)<br>f2 is the natural logarithm of quantity f1 plus one. | 
| FLOG      | no   | IC   | **f2 = Log10(f1)**<br>(F: f1 -- f2)<br>f2 is the base-ten logarithm of f1. | 
| FSIN      | no   | IC   | **f2 = Sin(f1)**<br>(F: f1 -- f2)<br>f2 is the sine of the radian angle f1. | 
| FSINCOS   | no   | IC   | **f2 = Sin(f1), f3 = Cos(f1)**<br>(F: f1 -- f2 f3)<br>f2 is the sine of the radian angle f1. f3 is the cosine of the radian angle f1. | 
| FSINH     | no   | IC   | **f2 = Sinh(f1)**<br>(F: f1 -- f2)<br>f2 is the hyperbolic sine of f1. | 
| FSQRT     | no   | IC   | **f2 = Sqrt(f1)**<br>(F: f1 -- f2)<br>f2 is the square root of f1. |
| FTAN      | no   | IC   | **f2 = Tan(f1)**<br>(F: f1 -- f2)<br>f2 is the tangent of the radian angle f1. | 
| FTANH     | no   | IC   | **f2 = Tanh(f1)**<br>(F: f1 -- f2)<br>f2 is the hyperbolic tangent of f1. | 
| FTRUNC    | no   | IC   | **f2 = Truncate(f1)**<br>(F: f1 -- f2)<br>Round r1 to an integral value using the “round towards zero” rule, giving r2. | 
| FVALUE x  | no   | IC   | **Named value**<br>(n -- )<br>Like a CONSTANT, but the value can be changed using the word TO. |
| S>F       | no   | IC   | **Single cell integer to floating**<br>(n -- ) (F: -- f)<br>Converts a single cell integer on the top of the stack to a floating point number and stores it on the top of the floating point stack. |
| SF!       | no   | IC   | **Store into address**<br>(addr -- ) (F: f -- )<br>Stores the floating point number f as a 32 bit (single cell) into the address addr (a heap array index). |
| SF@       | no   | IC   | **Fetch**<br>(addr -- ) (F: -- f)<br>Loads the single cell (32 bit) floating point number at addr a heap array index) and leaves it at the top of the floating point stack. |
| SFALIGN   | no   | IC   | **Align data pointer**<br>( -- )<br> If the data-space pointer is not single cell float aligned, reserve enough data space to make it so. |
| SFALIGNED | no   | IC   | **Get aligned address**<br>(addr1 -- addr2)<br> The addr2 address is the first single cell float-aligned address greater than or equal to addr1. |
| SFLOAT+   | no   | IC   | **Add floating point size**<br>(addr1 -- addr2)<br>Add the size in address units of a single cell floating-point number to addr1, giving addr2. |
| SFLOATS   | no   | IC   | **Get floating point size**<br>(n1 -- n2)<br>n2 is the size in address units of n1 single cell floating point numbers. |


## Words (Extra)

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| F=    | no   | IC   | **Floating equal**<br>( -- flag) (F: f1 f2 -- )<br>Returns -1 if f1 is equal to f2, 0 otherwise. |
| F<>   | no   | IC   | **Floating not equal**<br>( -- flag) (F: f1 f2 -- )<br>Returns -1 if f1 is not equal to f2, 0 otherwise. |
| F<=   | no   | IC   | **Floating less than or equal**<br>( -- flag) (F: f1 f2 -- )<br>Returns -1 if f1 <= f2, 0 otherwise. |
| F>    | no   | IC   | **Floating greater than**<br>( -- flag) (F: f1 f2 -- )<br>Returns -1 if f1 > f2, 0 otherwise. |
| F>=   | no   | IC   | **Floating greater than or equal**<br>( -- flag) (F: f1 f2 -- )<br>Returns -1 if f1 >= f2, 0 otherwise. |
| F0<>  | no   | IC   | **Nonzero**<br>( -- flag) (F: f -- )<br>Returns -1 if f is not equal to 0, 0 otherwise. |
| F0>   | no   | IC   | **Greater than zero**<br>( -- flag) (F: f -- )<br>Returns -1 if f is greater than 0, 0 otherwise. |
| F1+   | no   | IC   | **Add one**<br>(F: f1 -- f2)<br>Adds one to the top of the stack. |
| F1-   | no   | IC   | **Subtract one**<br>(F: f1 -- f2)<br>Subtracts one from the top of the stack. |
| F2+   | no   | IC   | **Add two**<br>(F: f1 -- f2)<br>Adds two to the top of the stack. |
| F2-   | no   | IC   | **Subtract two**<br>(F: f1 -- f2)<br>Subtracts two from the top of the stack. |
| F2*   | no   | IC   | **Times two**<br>(F: f1 -- f2)<br>Subtracts two from the top of the stack. |
| F2/   | no   | IC   | **Divide by two**<br>(F: f1 -- f2)<br>Divides the top of the stack by two. |

## TODO

Words: `DFFIELD: FE. FFIELD: FS. F~ PRECISION SET-PRECISION SFFIELD:`
