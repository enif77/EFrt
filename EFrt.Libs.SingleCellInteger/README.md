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
| <=       | no   | IC   | **Less than or equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 <= n2, 0 otherwise. |
| >=       | no   | IC   | **Greater than or equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 >= n2, 0 otherwise. |
| 2+       | no   | IC   | **Add two**<br>(n1 -- n2)<br>Adds two to the top of the stack. |
| 2-       | no   | IC   | **Subtract two**<br>(n1 -- n2)<br>Substracts two from the top of the stack. |

