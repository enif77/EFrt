# TOOLS

TOOLS library.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

## Words

| Name  | Imm. | Mode | Description |
| ---   | ---  | ---  | --- |
| .S    | no   | IC   | **Print stack**<br>Prints entire contents of stack. TOS is the right-most item. |
| .O    | no   | IC   | **Print object stack**<br>Prints entire contents of the object stack. TOS is the top-most item. |
| ?     | no   | IC   | **Print indirect**<br>(addr -- )<br>Prints the value at the address (a variables stack index) at the top of the stack. |
| WORDS | no   | IC   | **List words defined**<br>Defined words are listed, from the most recently defined to the first defined. |

## TODO

Words: `SEE`