﻿# CORE-EXT

Core extension words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during interpretation), IC = available in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| .(       | yes  | IC   | **Print constant string**<br>Immediately prints the string that follows in the input stream. |
| 0<>      | no   | IC   | **Nonzero**<br>(n -- flag)<br>Returns -1 if n1 is not equal to 0, 0 otherwise. |
| 0>       | no   | IC   | **Greater than zero**<br>(n -- flag)<br>Returns -1 if n1 is greater than 0, 0 otherwise. |
| 2>R      | no   | IC   | **To return stack**<br>(n1 n2 -- ) [ -- n1 n2]<br>Removes top two item from the stack and pushes them onto the return stack. |
| 2R>      | no   | IC   | **From return stack**<br>( -- n1 n2) [n1 n2 -- ]<br>Removes top two item from the return stack and pushes them onto the data stack. |
| 2R@      | no   | IC   | **Fetch from return stack**<br>( -- n1 n2) [n1 n2 -- n1 n2]<br>Reads top two item from the return stack and pushes them onto the data stack. |
| :NONAME  | no   | IC   | **Begin definition without a name**<br>( -- xt)<br>Begins compilation of a word without a name. The ; word then leaves this new word execution token on the stack. |
| <>       | no   | IC   | **Not equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 is not equal to n2, 0 otherwise. |
| ?DO      | yes  | C    | **Conditional loop**<br>(limit index -- ) [ - limit index ]<br>If n equals limit, skip immediately to the matching LOOP or +LOOP. Otherwise, enter the loop, which is thenceforth treated as a normal DO loop. |
| AGAIN    | yes  | C    | **Indefinite loop**<br>Marks the end of an indefinite loop opened by the matching BEGIN. |
| FALSE    | no   | IC   | **False**<br>( -- flag)<br>Constant that leaves the 0 (false) on the top of the stack. |
| HEX      | no   | IC   | **Set number conversion radix to sixteen**<br>( -- )<br>Set the numeric conversion radix to sixteen. |
| NIP      | no   | IC   | **Drop item below stack top**<br>(n1 n2 -- n2)<br>Drop the first item below the top of the stack. |
| PICK     | no   | IC   | **Pick item from stack**<br>(index -- n)<br>The index is removed from the stack and then the indexth stack item is copied to the top of the stack. The top of stack has index 0, the second item index 1, and so on. |
| ROLL     | no   | IC   | **Rotate indexth item to top**<br>(index -- n)<br>The index is removed from the stack and then the stack item selected by index, with 0 designating the top of stack, 1 the second item, and so on, is moved to the top of the stack. The intervening stack items are moved down one item. |
| S\\" str | yes  | IC   | **String literal**<br>{ -- s}<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. Supports `\` escaped special characters. |
| TO       | no   | IC   | **Set value**<br>(n -- )<br>Sets a value of a by VALUE or FVALUE created word. |
| TRUE     | no   | IC   | **True**<br>( -- flag)<br>Constant that leaves the -1 (true) on the top of the stack. |
| TUCK     | no   | IC   | **Dup stack top**<br>(n1 n2 -- n2 n1 n2)<br>Copy the first (top) stack item below the second stack item. |
| VALUE x  | no   | IC   | **Named value**<br>(n -- )<br>Like a CONSTANT, but the value can be changed using the word TO. |
| WITHIN   | no   | IC   | **Is in interval**<br>(n1 n2 n3 -- flag)<br>Checks, if n1 is within the n2 .. n3 interval. |
| \        | yes  | IC   | **Line comment**<br>Skips all source characters till the closing EOLN character. |

## Words (Extra)

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| B!       | no   | IC   | **Store byte into address**<br>(byte addr -- )<br>Stores the value char into the address addr (a heap array index). |
| B,       | no   | IC   | **Store byte in heap**<br>Reserves a single char of data heap, initialising it to char. |
| B@       | no   | IC   | **Fetch byte**<br>(addr -- byte)<br>Loads the character at the c-addr and leaves it at the top of the stack. |
| BYTE+    | no   | IC   | **Add byte size**<br>(addr1 -- addr2)<br>Add the size in address units of a byte to addr1, giving addr2. |
| BYTES    | no   | IC   | **Bytes to bytes**<br>(n1 -- n2)<br>Converts n1 bytes to n2 memory address units (bytes). |
| -ROLL    | no   | IC   | **Rotate top to indexth item**<br>(index -- n)<br>The index is removed from the stack and then the top stack item is moved to the indexth stack position. The intervening stack items are moved up one item. |
| -ROT     | no   | IC   | **Reverse rotate**<br>(n1 n2 n3 -- n2 n3 n1)<br>Moves the top of stack to the third item, moving the third and second items up. |
| <=       | no   | IC   | **Less than or equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 <= n2, 0 otherwise. |
| >=       | no   | IC   | **Greater than or equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 >= n2, 0 otherwise. |
| 2+       | no   | IC   | **Add two**<br>(n1 -- n2)<br>Adds two to the top of the stack. |
| 2-       | no   | IC   | **Subtract two**<br>(n1 -- n2)<br>Subtracts two from the top of the stack. |
| 2NIP     | no   | IC   | **Drop two items below stack top**<br>(n1 n2 n3 n4 -- n3 n4)<br>Drop the fourth and the third item below the top of the stack. |
| 2TUCK    | no   | IC   | **Dup stack top**<br>(n1 n2 n3 n4 -- n3 n4 n1 n2 n3 n4)<br>Copy the first two stack items below the fourth stack item. |
| CLEAR    | no   | IC   | **Clear stack**<br>All items on the data stack are discarded. |


## TODO

Words: `.R ACTION-OF BUFFER: C" CASE COMPILE, DEFER DEFER! DEFER@ ENDCASE ENDOF ERASE
  IS OF MARKER OF PARSE PARSE-NAME U.R U> UNUSED`

Words (Extra): BFILL

## Skipped words

```
HOLDS PAD REFILL RESTORE-INPUT SAVE-INPUT SOURCE-ID [COMPILE]
```