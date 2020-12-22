# IO

IO library.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name  | Imm. | Mode | Description |
| ---   | ---  | ---  | --- |
| .     | no   | IC   | **Print top of stack**<br>(n -- )<br>Prints the integer number on the top of the stack. |
| ?     | no   | IC   | **Print indirect**<br>(addr -- )<br>Prints the value at the address (a variables stack index) at the top of the stack. |
| .(    | yes  | IC   | **Print constant string**<br>Immediatelly prints the string that follows in the input stream. |
| ."    | yes  | C    | **Print immediate string**<br>Prints the string that follows in the input stream. |
| .O    | no   | IC   | **Print object stack**<br>Prints entire contents of the object stack. TOS is the top-most item. |
| .S    | no   | IC   | **Print stack**<br>Prints entire contents of stack. TOS is the right-most item. |
| CR    | no   | IC   | **Carriage return**<br>The folowing output will start at the new line. |
| EMIT  | no   | IC   | **Print char**<br>(n -- )<br>Prints out a character represented by a number on the top of the stack. |
| D.    | no   | IC   | **Print double cell integer**<br>(d -- )<br>A double cell integer value on the top of the stack is printed. |
| F.    | no   | IC   | **Print floating point**<br>(f -- )<br>A floating point value on the top of the stack is printed. |
| S.    | no   | IC   | **Print string**<br>{s -- }<br>A string on the top of the object stack is printed. |
| SPACE | no   | IC   | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES | no  | IC   | **Print spaces**<br>(n -- )<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| WORDS | no   | IC   | **List words defined**<br>Defined words are listed, from the most recently defined to the first defined. |

Note: The `."` word works like `S" str" S.` words together.

#### Todo

Words: `.R`