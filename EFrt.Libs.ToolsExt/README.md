# TOOLS-EXT

Extra tools words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

## Words

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| BYE      | no   | IC   | **Terminate execution**<br>Asks the interpreter to terminate execution. It ends the EFrt program. |
| FORGET w | no   | IC   | **Forget word**<br>The most recent definition of word w is deleted, along with all words declared more recently than the named word. |


#### TODO

Words: `AHEAD `
