# STRING

String manipulation words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name  | Imm. | Mode | Description |
| ---   | ---  | ---  | --- |
| S+    | no   | IC   | **String concatenate**<br>{s1 s2 -- s3}<br>The string s1 is concatenated with the string s2 and the resulting s1 + s2 string is stored at the top of the object stack. |
| S"    | no   | IC   | **String literal**<br>{ -- s}<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |

## Todo

Words: `STRCPY STRINT STRLEN STRREAL SUBSTR STRFORM STRCAT STRCHAR STRCMP STRCMPI COMPARE (STRLIT) TYPE S!`
