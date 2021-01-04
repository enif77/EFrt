# DOUBLE (Double Cell Integer)

Double cell integer manipulating words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| 2CONSTANT x | no   | I   | **Double word constant**<br>(n1 n2 -- )<br>Declares a double word constant x. When x is executed, n1 and n2 are placed on the stack. |
| 2LITERAL | yes  | C    | **Compile literal**<br>(d -- )<br>Compiles the value on the top of the stack into the current definition. When the definition is executed, that value will be pushed onto the top of the stack. |
| 2VARIABLE x | no   | I   | **Double variable**<br>Creates a two cell (8 byte) variable named x. When x is executed, the address of the 8 byte area is placed on the stack. |
| D+       | no   | IC   | **d3 = d1 + d2**<br>(d1 d2 -- d3)<br>Adds d1 and d2 and leaves the sum on the stack. |
| D-       | no   | IC   | **d3 = d1 - d2**<br>(d1 d2 -- d3)<br>Substracts d2 from d1 and leaves the difference on the stack. |
| D.       | no   | IC   | **Print double cell integer**<br>(d -- )<br>A double cell integer value on the top of the stack is printed. |
| D0<      | no   | IC   | **Less than zero**<br>(d1 -- flag)<br>Returns -1 if d1 is less than 0, 0 otherwise. |
| D0=      | no   | IC   | **Equal to zero**<br>(d1 -- flag)<br>Returns -1 if d1 is equal to 0, 0 otherwise. |
| D2*      | no   | IC   | **Times two**<br>(d1 -- d2)<br>Substracts two from the top of the stack. |
| D2/      | no   | IC   | **Divide by two**<br>(d1 -- d2)<br>Divides the top of the stack by two. |
| D<       | no   | IC   | **Less than**<br>(d1 d2 -- flag)<br>Returns -1 if d1 < d2, 0 otherwise. |
| D=       | no   | IC   | **Equal**<br>(d1 d2 -- flag)<br>Returns -1 if d1 is equal to d2, 0 otherwise. |
| D>S      | no   | IC   | **Double cell number to single cell number**<br>(d -- n)<br>Converts a double cell number (64bit, long) to a single cell number (32bit, int). |
| DABS     | no   | IC   | **n2 = Abs(n1)**<br>(d1 -- d2)<br>Replaces the top of stack with its absolute value. |
| DMAX     | no   | IC   | **Maximum**<br>(d1 d2 -- d3)<br>The greater of d1 and d2 is left on the top of the stack. |
| DMIN     | no   | IC   | **Minimum**<br>(d1 d2 -- d3)<br>The lesser of d1 and d2 is left on the top of the stack. |
| DNEGATE  | no   | IC   | **n2 = -n1**<br>(d1 -- d2)<br>Negates the value the top of the stack. |

## TODO

Words: `D.R M*/ M+`

