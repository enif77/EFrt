# STRING

String manipulation words.

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
| S+    | no   | IC   | {s1 s2 -- s3} | **String concatenate**<br>The string s1 is concatenated with the string s2 and the resulting s1 + s2 string is stored at the top of the object stack. |
| S"    | no   | IC   | { -- s}   | **String literal**<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |

## Todo

Words: `STRCPY, STRINT, STRLEN, STRREAL, SUBSTR, STRFORM, STRCAT, STRCHAR, STRCMP, STRCMPI, COMPARE, (STRLIT), TYPE`
