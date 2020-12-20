# IO

IO library.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, {} = object stack.
- Description: A description of the word.

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| .     | no   | IC   | (n -- )   | **Print top of stack**<br>Prints the integer number on the top of the stack. |
| ?     | no   | IC   | (addr -- ) | **Print indirect**<br>Prints the value at the address (a variables stack index) at the top of the stack. |
| .(    | yes  | IC   |           | **Print constant string**<br>Immediatelly prints the string that follows in the input stream. |
| ."    | yes  | C    |           | **Print immediate string**<br>Prints the string that follows in the input stream. |
| .O    | no   | IC   |           | **Print object stack**<br>Prints entire contents of the object stack. TOS is the top-most item. |
| .S    | no   | IC   |           | **Print stack**<br>Prints entire contents of stack. TOS is the right-most item. |
| CR    | no   | IC   |           | **Carriage return**<br>The folowing output will start at the new line. |
| EMIT  | no   | IC   | (n -- )   | **Print char**<br>Prints out a character represented by a number on the top of the stack. |
| F.    | no   | IC   | (f -- )   | **Print floating point**<br>A floating point value on the top of the stack is printed. |
| S.    | no   | IC   | {s -- }   | **Print string**<br>A string on the top of the object stack is printed. |
| SPACE | no   | IC   |           | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES | no  | IC   | (n -- )   | **Print spaces**<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| WORDS | no   | IC   |           | **List words defined**<br>Defined words are listed, from the most recently defined to the first defined. |

Note: The `."` word works like `S" str" S.` words together.

#### Todo

Words: `.R`