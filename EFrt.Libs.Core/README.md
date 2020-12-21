﻿# CORE

Common words for all base operations.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, \{} = object stack.
- Description: A description of the word.

| Name     | Imm. | Mode | Stack op. | Description |
| ---      | ---  | ---  | ---       | --- |
| (        | yes  | IC   |           | **Comment**<br>Skips all source characters till the closing ) character. |
| \        | yes  | IC   |           | **Line comment**<br>Skips all source characters till the closing EOLN character. |
| : w      | no   | I    |           | **Begin definition**<br>Begins compilation of a word named "w". |
| ;        | yes  | C    |           | **End definition**<br>Ends compilation of a word. |
| !        | no   | IC   | (n addr -- ) | **Store into address**<br>Stores the value n into the address addr (a variables stack index). |
| @        | no   | IC   | (addr -- n) | **Fetch**<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack. |
| 2!       | no   | IC   | (n1 n2 addr -- ) | **Store two words**<br>Stores the two words n1 and n2 at addresses addr and addr + 1. |
| 2@       | no   | IC   | (addr -- n1 n2) | **Load two words**<br>Places the two words starting at addr on the top of the stack. |
| 2CONSTANT x | no   | I    | (n1 n2 -- )    | **Double word constant**<br>Declares a double word constant x. When x is executed, n1 and n2 are placed on the stack. |
| 2DROP    | no   | IC   | (n1 n2 -- ) | **Double drop**<br>Discards two topmost items on the stack. |
| 2DUP     | no   | IC   | (n1 n2 -- n1 n2 n1 n2) | **Duplicate two**<br>Duplicates two topmost items on the stack. |
| 2OVER    | no   | IC   | (n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2) | **Double over**<br>Copies the second pair of items on the stack to the top of the stack. |
| 2ROT     | no   | IC   | (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2) | **Double rotate**<br>Rotate the third pair on the stack to the top of the stack, moving down the first and the second pair. |
| 2SWAP    | no   | IC   | (n1 n2 n3 n4 -- n3 n4 n1 n2) | **Double swap**<br>Swaps the first and the second pair on the stack. |
| 2VARIABLE x | no   | I    |            | **Double variable**<br>Creates a two cell (8 byte) variable named x. When x is executed, the address of the 8 byte area is placed on the stack. |
| ABORT    | no   | IC   |           | **Abort**<br>Clears the stack and the object and performs a QUIT. |
| AGAIN    | yes  | C    |           | **Indefinite loop**<br>Marks the end of an idefinite loop opened by the matching BEGIN. |
| ALLOT    | no   | IC   | (n -- addr) | **Allocate heap**<br>Allocates n cells of heap space. |
| BEGIN    | yes  | C    |           | **Begin loop**<br>Begins a loop. The end of the loop is marked by the matching AGAIN, REPEAT, or UNTIL. |
| BL       | no   | IC   | ( -- n)   | **Blank**<br>Constant that leaves 32 (the ASCII code of the SPACE char) on the top of the stack. |
| BYE      | no   | IC   |           | **Terminate execuition**<br>Asks the interpreter to terminate execution. It ends the EFrt program. |
| CHAR ccc | no   | IC   | ( -- c)   | **Char**<br>Skip leading spaces. Parse the string ccc and return c, the display code representing the first character of ccc. |
| [CHAR] ccc | yes  | C    | ( -- c)   | **Bracket char**<br>Compilation: Skip leading spaces. Parse the string ccc. Run-time: Return c, the display code representing the first character of ccc. Interpretation semantics for this word are undefined. |
| CLEAR    | no   | IC   |           | **Clear stack**<br>All items on the data stack are discarded. |
| CONSTANT x | no   | I    | (n --)    | **Declare constant**<br>Declares a constant named x. When x is executed, the value n will be left on the stack. |
| DEPTH    | no   | IC   | ( -- n)   | **Stack depth**<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | (limit index -- ) [ - limit index ] | **Definite loop**<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit−1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| ?DO      | yes  | C    | (limit index -- ) [ - limit index ] | **Conditional loop**<br>If n equals limit, skip immediately to the matching LOOP or +LOOP. Otherwise, enter the loop, which is thenceforth treated as a normal DO loop. |
| DROP     | no   | IC   | (n --)    | **Discard top of stack**<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | (n -- n n) | **Duplicate**<br>Duplicates the value at the top of the stack. |
| ?DUP     | no   | IC   | (n -- 0 / n n) | **Conditional duplicate**<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| ELSE     | yes  | C    | (n -- n n) | **Else**<br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| FALSE    | no   | IC   | ( -- flag) | **False**<br>Constant that leaves the 0 (false) on the top of the stack. |
| FORGET w | no   | IC   |           | **Forget word**<br>The most recent definition of word w is deleted, along with all words declared more recently than the named word. |
| HERE     | no   | IC   | ( -- addr) | **Heap address**<br>The current heap allocation address is placed on the top of the stack. addr + 1 is the first free heap cell. |
| I        | yes  | C    | ( -- n) [n -- n] | **Inner loop index**<br>The index of the innermost DO—LOOP is placed on the stack. |
| IF       | yes  | C    | (flag --) | **Conditional statement**<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| J        | yes  | C    | ( -- n) [J lim I -- J lim I] | **Outer loop index**<br>The loop index of the next to innermost DO—LOOP is placed on the stack. |
| LEAVE    | yes  | C    |           | **Exit DO—LOOP**<br>The innermost DO—LOOP is immediately exited. Execution resumes after the LOOP statement marking the end of the loop. |
| LOOP     | yes  | C    |           | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| +LOOP    | yes  | C    | (n -- )   | **Add to loop index**<br>Adds n to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| OVER     | no   | IC   | (n1 n2 -- n1 n2 n1) | **Duplicate second item**<br>The second item on the stack is copied to the top. |
| PICK     | no   | IC   | (index -- n) | **Pick item from stack**<br>The index is removed from the stack and then the indexth stack item is copied to the top of the stack. The top of stack has index 0, the second item index 1, and so on. |
| QUIT     | no   | IC   |           | **Quit execution**<br>The return stack is cleared and control is returned to the interpreter. The stack and the object stack are not disturbed. |
| ROLL     | no   | IC   | (index -- n) | **Rotate indexth item to top**<br>The index is removed from the stack and then the stack item selected by index, with 0 designating the top of stack, 1 the second item, and so on, is moved to the top of the stack. The intervening stack items are moved down one item. |
| >R       | no   | IC   | (n -- ) [ - n] | **To return stack**<br>Removes the top item from the stack and pushes it onto the return stack. |
| R>       | no   | IC   | ( -- n) [n - ] | **From return stack**<br>The top value is removed from the return stack and pushed onto the stack. |
| @R       | no   | IC   | ( -- n) [n - n] | **Fetch return stack**<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack. |
| REPEAT   | yes  | C    | ( -- n)   | **Close BEGIN—WHILE—REPEAT loop**<br>Another iteration of the current BEGIN—WHILE—REPEAT loop having been completed, execution continues after the matching BEGIN. |
| ROT      | no   | IC   | (n1 n2 n3 -- n2 n3 n1) | **Rotate 3 items**<br>The third item on the stack is placed on the top of the stack and the second and first items are moved down. |
| -ROT     | no   | IC   | (n1 n2 n3 -- n2 n3 n1) | **Reverse rotate**<br>Moves the top of stack to the third item, moving the third and second items up. |
| SWAP     | no   | IC   | (n1 n2 -- n2 n1) | **Swap top two items**<br>The top two stack items are interchanged. |
| THEN     | yes  | C    | ( -- flag) | **End if**<br>Used in an IF—ELSE—THEN sequence, marks the end of the conditional statement. |
| TRUE     | no   | IC   | ( -- flag) | **True**<br>Constant that leaves the -1 (true) on the top of the stack. |
| UNTIL    | yes  | C    | (flag -- ) | **End BEGIN—UNTIL loop**<br>If flag is zero, the loop continues execution at the word following the matching BEGIN. If flag is nonzero, the loop is exited and the word following the UNTIL is executed. |
| VARIABLE x | no   | I    |            | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |
| WHILE    | yes  | C    | (flag -- ) | **Decide BEGIN—WHILE—REPEAT loop**<br>If flag is nonzero, execution continues after the WHILE. If flag is zero, the loop is exited and execution resumed after the REPEAT that marks the end of the loop. |


#### TODO

Words: `SYSTEM STATE MARKER name [ ] INCLUDE ABORT" str ARRAY x EXIT IMMEDIATE LITERAL TRACE
  (XDO) (X?DO) (XLOOP) (+XLOOP) WORDSD ' EXECUTE INT STRING EVALUATE UNLOOP +!
  NIP TUCK 2NIP 2TUCK -ROLL S! BRANCH x ?BRANCH x 2>R 2R> 2R@`

Variables: `BASE STATE`