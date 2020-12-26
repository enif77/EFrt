# INTEGER

Single cell integer manipulating words. The CORE library has words for single cell stack operations.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| +        | no   | IC   | **n3 = n1 + n2**<br>(n1 n2 -- n3)<br>Adds n1 and n2 and leaves the sum on the stack. |
| -        | no   | IC   | **n3 = n1 - n2**<br>(n1 n2 -- n3)<br>Substracts n2 from n1 and leaves the difference on the stack. |
| *        | no   | IC   | **n3 = n1 * n2**<br>(n1 n2 -- n3)<br>Multiplies n1 and n2 and leaves the product on the stack. |
| /        | no   | IC   | **n3 = n1 / n2**<br>(n1 n2 -- n3)<br>Divides n1 by n2 and leaves the quotient on the stack. |
| <        | no   | IC   | **Less than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 < n2, 0 otherwise. |
| <=       | no   | IC   | **Less than or equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 <= n2, 0 otherwise. |
| =        | no   | IC   | **Equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 is equal to n2, 0 otherwise. |
| >        | no   | IC   | **Greater than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 > n2, 0 otherwise. |
| >=       | no   | IC   | **Greater than or equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 >= n2, 0 otherwise. |
| 0<       | no   | IC   | **Less than zero**<br>(n -- flag)<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0=       | no   | IC   | **Equal to zero**<br>(n -- flag)<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| 1+       | no   | IC   | **Add one**<br>(n1 -- n2)<br>Adds one to the top of the stack. |
| 1-       | no   | IC   | **Subtract one**<br>(n1 -- n2)<br>Substracts one from the top of the stack. |
| 2+       | no   | IC   | **Add two**<br>(n1 -- n2)<br>Adds two to the top of the stack. |
| 2-       | no   | IC   | **Subtract two**<br>(n1 -- n2)<br>Substracts two from the top of the stack. |
| 2*       | no   | IC   | **Times two**<br>(n1 -- n2)<br>Substracts two from the top of the stack. |
| 2/       | no   | IC   | **Divide by two**<br>(n1 -- n2)<br>Divides the top of the stack by two. |
| ABS      | no   | IC   | **n2 = Abs(n1)**<br>(n1 -- n2)<br>Replaces the top of stack with its absolute value. |
| AND      | no   | IC   | **Bitwise and**<br>(n1 n2 -- n3)<br>Stores the bitwise AND of n1 and n2 on the stack. |
| NEGATE   | no   | IC   | **n2 = -n1**<br>(n1 -- n2)<br>Negates the value the top of the stack. |
| MAX      | no   | IC   | **Maximum**<br>(n1 n2 -- n3)<br>The greater of n1 and n2 is left on the top of the stack. |
| MIN      | no   | IC   | **Minimum**<br>(n1 n2 -- n3)<br>The lesser of n1 and n2 is left on the top of the stack. |
| MOD      | no   | IC   | **Modulus (remainder)**<br>(n1 n2 -- n3)<br>The remainder when n1 is divided by n2 is left on the top of the stack. |
| /MOD     | no   | IC   | **n3 = n1 mod n2, n4 = n1 / n2**<br>(n1 n2 -- n3 n4)<br>Divides n1 by n2 and leaves quotient on top of stack, remainder as next on stack. |
| NOT      | no   | IC   | **Bitwise not**<br>(n1 -- n2)<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| OR       | no   | IC   | **Bitwise or**<br>(n1 n2 -- n3)<br>Stores the bitwise or of n1 and n2 on the stack. |
| XOR      | no   | IC   | **Bitwise exclusive or**<br>(n1 n2 -- n3)<br>Stores the bitwise exclusive or of n1 and n2 on the stack. |

## TODO

Words: `SHIFT RSHIFT INVERT +!`
