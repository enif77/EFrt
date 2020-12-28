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
| S.    | no   | IC   | **Print string**<br>{s -- }<br>A string on the top of the object stack is printed. |

## Words (FLOAT)

| Name  | Imm. | Mode | Description |
| ---   | ---  | ---  | --- |
| F.    | no   | IC   | **Print floating point**<br>(f -- )<br>A floating point value on the top of the stack is printed. |

## Words (TOOLS)

| Name  | Imm. | Mode | Description |
| ---   | ---  | ---  | --- |
| .O    | no   | IC   | **Print object stack**<br>Prints entire contents of the object stack. TOS is the top-most item. |
| .S    | no   | IC   | **Print stack**<br>Prints entire contents of stack. TOS is the right-most item. |
| ?     | no   | IC   | **Print indirect**<br>(addr -- )<br>Prints the value at the address (a variables stack index) at the top of the stack. |
| WORDS | no   | IC   | **List words defined**<br>Defined words are listed, from the most recently defined to the first defined. |

## Todo

Words: `.R`