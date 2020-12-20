# DOUBLE (Double Cell Integer)

Double cell integer manipulating words. The CORE library has words for double cell stack operations.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, {} = object stack.
- Description: A description of the word.

| Name     | Imm. | Mode | Stack op.    | Description |
| ---      | ---  | ---  | ---          | --- |
| D+       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 + d2**<br>Adds d1 and d2 and leaves the sum on the stack. |
| D-       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 - d2**<br>Substracts d2 from d1 and leaves the difference on the stack. |
| D*       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 * d2**<br>Multiplies d1 and d2 and leaves the product on the stack. |
| D/       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 / d2**<br>Divides d1 by d2 and leaves the quotient on the stack. |
| D<       | no   | IC   | (d1 d2 -- flag) | **Less than**<br>Returns -1 if d1 < d2, 0 otherwise. |
| D<=      | no   | IC   | (d1 d2 -- flag) | **Less than or equal**<br>Returns -1 if d1 <= d2, 0 otherwise. |
| D<>      | no   | IC   | (d1 d2 -- flag) | **Not equal**<br>Returns -1 if d1 is not equal to d2, 0 otherwise. |
| D=       | no   | IC   | (d1 d2 -- flag) | **Equal**<br>Returns -1 if d1 is equal to d2, 0 otherwise. |
| D>       | no   | IC   | (d1 d2 -- flag) | **Greater than**<br>Returns -1 if d1 > d2, 0 otherwise. |
| D>S      | no   | IC   | (d -- n) | **Double cell number to single cell number**<br>Converts a double cell number (64bit, long) to a single cell number (32bit, int). |
| D>=      | no   | IC   | (d1 d2 -- flag) | **Greater than or equal**<br>Returns -1 if d1 >= d2, 0 otherwise. |
| D0<      | no   | IC   | (d1 -- flag)  | **Less than zero**<br>Returns -1 if d1 is less than 0, 0 otherwise. |
| D0<>     | no   | IC   | (d1 -- flag)  | **Nonzero**<br>Returns -1 if d1 is not equal to 0, 0 otherwise. |
| D0=      | no   | IC   | (d1 -- flag)  | **Equal to zero**<br>Returns -1 if d1 is equal to 0, 0 otherwise. |
| D0>      | no   | IC   | (d1 -- flag)  | **Greater than zero**<br>Returns -1 if d1 is greater than 0, 0 otherwise. |
| D1+      | no   | IC   | (d1 -- d2)    | **Add one**<br>Adds one to the top of the stack. |
| D1-      | no   | IC   | (d1 -- d2)    | **Subtract one**<br>Substracts one from the top of the stack. |
| D2+      | no   | IC   | (d1 -- d2)    | **Add two**<br>Adds two to the top of the stack. |
| D2-      | no   | IC   | (d1 -- d2)    | **Subtract two**<br>Substracts two from the top of the stack. |
| D2*      | no   | IC   | (d1 -- d2)    | **Times two**<br>Substracts two from the top of the stack. |
| D2/      | no   | IC   | (d1 -- d2)    | **Divide by two**<br>Divides the top of the stack by two. |
| DABS     | no   | IC   | (d1 -- d2)    | **n2 = Abs(n1)**<br>Replaces the top of stack with its absolute value. |
| DAND     | no   | IC   | (d1 d2 -- d3) | **Bitwise and**<br>Stores the bitwise AND of d1 and n2 on the stack. |
| DNEGATE  | no   | IC   | (d1 -- d2)    | **n2 = -n1**<br>Negates the value the top of the stack. |
| DMAX     | no   | IC   | (d1 d2 -- d3) | **Maximum**<br>The greater of d1 and d2 is left on the top of the stack. |
| DMIN     | no   | IC   | (d1 d2 -- d3) | **Minimum**<br>The lesser of d1 and d2 is left on the top of the stack. |
| DMOD     | no   | IC   | (d1 d2 -- d3) | **Modulus (remainder)**<br>The remainder when d1 is divided by d2 is left on the top of the stack. |
| D/MOD    | no   | IC   | (d1 d2 -- d3 d4) | **d3 = d1 mod d2, d4 = d1 / d2**<br>Divides d1 by d2 and leaves quotient on top of stack, remainder as next on stack. |
| DNOT     | no   | IC   | (d1 -- d2)    | **Bitwise not**<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| DOR      | no   | IC   | (d1 d2 -- d3) | **Bitwise or**<br>Stores the bitwise or of d1 and d2 on the stack. |
| DXOR     | no   | IC   | (d1 d2 -- d3) | **Bitwise exclusive or**<br>Stores the bitwise exclusive or of d1 and d2 on the stack. |
| S>D      | no   | IC   | (n -- d)      | **Single cell number to double cell number**<br>Converts a single cell number (32bit, int) to a double cell number (64bit, long). |
