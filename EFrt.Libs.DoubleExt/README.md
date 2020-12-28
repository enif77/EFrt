# DOUBLE-EXT

Double cell integer manipulating extra words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

## Words (DOUBLE-EXT)

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| 2ROT     | no   | IC   | **Double rotate**<br>(n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)<br>Rotate the third pair on the stack to the top of the stack, moving down the first and the second pair. |


#### TODO

Words: `DU<`
