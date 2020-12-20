# INTEGER

Single cell integer manipulating words. The CORE library has words for single cell stack operations.

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
| +        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 + n2**<br>Adds n1 and n2 and leaves the sum on the stack. |
| -        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 - n2**<br>Substracts n2 from n1 and leaves the difference on the stack. |
| *        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 * n2**<br>Multiplies n1 and n2 and leaves the product on the stack. |
| /        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 / n2**<br>Divides n1 by n2 and leaves the quotient on the stack. |
| <        | no   | IC   | (n1 n2 -- flag) | **Less than**<br>Returns -1 if n1 < n2, 0 otherwise. |
| <=       | no   | IC   | (n1 n2 -- flag) | **Less than or equal**<br>Returns -1 if n1 <= n2, 0 otherwise. |
| <>       | no   | IC   | (n1 n2 -- flag) | **Not equal**<br>Returns -1 if n1 is not equal to n2, 0 otherwise. |
| =        | no   | IC   | (n1 n2 -- flag) | **Equal**<br>Returns -1 if n1 is equal to n2, 0 otherwise. |
| >        | no   | IC   | (n1 n2 -- flag) | **Greater than**<br>Returns -1 if n1 > n2, 0 otherwise. |
| >=       | no   | IC   | (n1 n2 -- flag) | **Greater than or equal**<br>Returns -1 if n1 >= n2, 0 otherwise. |
| 0<       | no   | IC   | (n1 -- flag)  | **Less than zero**<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0<>      | no   | IC   | (n1 -- flag)  | **Nonzero**<br>Returns -1 if n1 is not equal to 0, 0 otherwise. |
| 0=       | no   | IC   | (n1 -- flag)  | **Equal to zero**<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| 0>       | no   | IC   | (n1 -- flag)  | **Greater than zero**<br>Returns -1 if n1 is greater than 0, 0 otherwise. |
| 1+       | no   | IC   | (n1 -- n2)    | **Add one**<br>Adds one to the top of the stack. |
| 1-       | no   | IC   | (n1 -- n2)    | **Subtract one**<br>Substracts one from the top of the stack. |
| 2+       | no   | IC   | (n1 -- n2)    | **Add two**<br>Adds two to the top of the stack. |
| 2-       | no   | IC   | (n1 -- n2)    | **Subtract two**<br>Substracts two from the top of the stack. |
| 2*       | no   | IC   | (n1 -- n2)    | **Times two**<br>Substracts two from the top of the stack. |
| 2/       | no   | IC   | (n1 -- n2)    | **Divide by two**<br>Divides the top of the stack by two. |
| ABS      | no   | IC   | (n1 -- n2)    | **n2 = Abs(n1)**<br>Replaces the top of stack with its absolute value. |
| AND      | no   | IC   | (n1 n2 -- n3) | **Bitwise and**<br>Stores the bitwise AND of n1 and n2 on the stack. |
| NEGATE   | no   | IC   | (n1 -- n2)    | **n2 = -n1**<br>Negates the value the top of the stack. |
| MAX      | no   | IC   | (n1 n2 -- n3) | **Maximum**<br>The greater of n1 and n2 is left on the top of the stack. |
| MIN      | no   | IC   | (n1 n2 -- n3) | **Minimum**<br>The lesser of n1 and n2 is left on the top of the stack. |
| MOD      | no   | IC   | (n1 n2 -- n3) | **Modulus (remainder)**<br>The remainder when n1 is divided by n2 is left on the top of the stack. |
| /MOD     | no   | IC   | (n1 n2 -- n3 n4) | **n3 = n1 mod n2, n4 = n1 / n2**<br>Divides n1 by n2 and leaves quotient on top of stack, remainder as next on stack. |
| NOT      | no   | IC   | (n1 -- n2)    | **Bitwise not**<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| OR       | no   | IC   | (n1 n2 -- n3) | **Bitwise or**<br>Stores the bitwise or of n1 and n2 on the stack. |
| XOR      | no   | IC   | (n1 n2 -- n3) | **Bitwise exclusive or**<br>Stores the bitwise exclusive or of n1 and n2 on the stack. |

## TODO

Words: `SHIFT RSHIFT INVERT +!`
