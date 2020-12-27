﻿# CORE-EXT

Core extension words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| .(    | yes  | IC   | **Print constant string**<br>Immediatelly prints the string that follows in the input stream. |
| 0<>      | no   | IC   | **Nonzero**<br>(n -- flag)<br>Returns -1 if n1 is not equal to 0, 0 otherwise. |
| 0>       | no   | IC   | **Greater than zero**<br>(n -- flag)<br>Returns -1 if n1 is greater than 0, 0 otherwise. |
| <>       | no   | IC   | **Not equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 is not equal to n2, 0 otherwise. |
| ?DO      | yes  | C    | **Conditional loop**<br>(limit index -- ) [ - limit index ]<br>If n equals limit, skip immediately to the matching LOOP or +LOOP. Otherwise, enter the loop, which is thenceforth treated as a normal DO loop. |
| AGAIN    | yes  | C    | **Indefinite loop**<br>Marks the end of an idefinite loop opened by the matching BEGIN. |
| FALSE    | no   | IC   | **False**<br>( -- flag)<br>Constant that leaves the 0 (false) on the top of the stack. |
| PICK     | no   | IC   | **Pick item from stack**<br>(index -- n)<br>The index is removed from the stack and then the indexth stack item is copied to the top of the stack. The top of stack has index 0, the second item index 1, and so on. |
| ROLL     | no   | IC   | **Rotate indexth item to top**<br>(index -- n)<br>The index is removed from the stack and then the stack item selected by index, with 0 designating the top of stack, 1 the second item, and so on, is moved to the top of the stack. The intervening stack items are moved down one item. |
| TRUE     | no   | IC   | **True**<br>( -- flag)<br>Constant that leaves the -1 (true) on the top of the stack. |


#### TODO

Words: `.R 2>R 2R> 2R@ :NONAME CASE CONVERT ENDCASE ENDOF ERASE HEX MARKER NIP OF TO TUCK UNUSED WITHIN 2NIP 2TUCK -ROLL`

Variables: `...`