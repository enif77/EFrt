# EXCEPTION

Exceptions handling words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| CATCH    | no   | C    | **Catch an exception**<br>(xt -- n)<br>Pushes the current execution state to the exception stack, executes xt, and returns 0 for no-error execution (dropping the exception frame) and non-zero, if a THROW was executed. |

#### TODO

Words: `THROW`

Words (EXT): `ABORT ABORT"`

Variables: `...`