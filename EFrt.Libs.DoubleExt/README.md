# DOUBLE-EXT

Double cell integer manipulating extra words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

## Words (DOUBLE-EXT)

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| 2ROT     | no   | IC   | **Double rotate**<br>(n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)<br>Rotate the third pair on the stack to the top of the stack, moving down the first and the second pair. |

## Words (Extra)

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| D*       | no   | IC   | **d3 = d1 * d2**<br>(d1 d2 -- d3)<br>Multiplies d1 and d2 and leaves the product on the stack. |
| D/       | no   | IC   | **d3 = d1 / d2**<br>(d1 d2 -- d3)<br>Divides d1 by d2 and leaves the quotient on the stack. |
| D<=      | no   | IC   | **Less than or equal**<br>(d1 d2 -- flag)<br>Returns -1 if d1 <= d2, 0 otherwise. |
| D<>      | no   | IC   | **Not equal**<br>(d1 d2 -- flag)<br>Returns -1 if d1 is not equal to d2, 0 otherwise. |
| D>       | no   | IC   | **Greater than**<br>(d1 d2 -- flag)<br>Returns -1 if d1 > d2, 0 otherwise. |
| D>=      | no   | IC   | **Greater than or equal**<br>(d1 d2 -- flag)<br>Returns -1 if d1 >= d2, 0 otherwise. |
| D0<>     | no   | IC   | **Nonzero**<br>(d1 -- flag)<br>Returns -1 if d1 is not equal to 0, 0 otherwise. |
| D0>      | no   | IC   | **Greater than zero**<br>(d1 -- flag)<br>Returns -1 if d1 is greater than 0, 0 otherwise. |
| D1+      | no   | IC   | **Add one**<br>(d1 -- d2)<br>Adds one to the top of the stack. |
| D1-      | no   | IC   | **Subtract one**<br>(d1 -- d2)<br>Substracts one from the top of the stack. |
| D2+      | no   | IC   | **Add two**<br>(d1 -- d2)<br>Adds two to the top of the stack. |
| D2-      | no   | IC   | **Subtract two**<br>(d1 -- d2)<br>Substracts two from the top of the stack. |
| DAND     | no   | IC   | **Bitwise and**<br>(d1 d2 -- d3)<br>Stores the bitwise AND of d1 and n2 on the stack. |
| DMOD     | no   | IC   | **Modulus (remainder)**<br>(d1 d2 -- d3)<br>The remainder when d1 is divided by d2 is left on the top of the stack. |
| D/MOD    | no   | IC   | **d3 = d1 mod d2, d4 = d1 / d2**<br>(d1 d2 -- d3 d4)<br>Divides d1 by d2 and leaves quotient on top of stack, remainder as next on stack. |
| DNOT     | no   | IC   | **Bitwise not**<br>(d1 -- d2)<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| DOR      | no   | IC   | **Bitwise or**<br>(d1 d2 -- d3)<br>Stores the bitwise or of d1 and d2 on the stack. |
| DXOR     | no   | IC   | **Bitwise exclusive or**<br>(d1 d2 -- d3)<br>Stores the bitwise exclusive or of d1 and d2 on the stack. |

#### TODO

Words: `DU<`
