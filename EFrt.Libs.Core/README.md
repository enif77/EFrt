# CORE

Common words for all base operations.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| ,        | no   | IC   | **Store in heap**<br>Reserves a single cell of data heap, initialising it to n. |
| (        | yes  | IC   | **Comment**<br>Skips all source characters till the closing ) character. |
| \        | yes  | IC   | **Line comment**<br>Skips all source characters till the closing EOLN character. |
| : w      | no   | I    | **Begin definition**<br>Begins compilation of a word named "w". |
| ;        | yes  | C    | **End definition**<br>Ends compilation of a word. |
| !        | no   | IC   | **Store into address**<br>(n addr -- )<br>Stores the value n into the address addr (a variables stack index). |
| @        | no   | IC   | **Fetch**<br>(addr -- n)<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack. |
| 2!       | no   | IC   | **Store two words**<br>(n1 n2 addr -- )<br>Stores the two words n1 and n2 at addresses addr and addr + 1. |
| 2@       | no   | IC   | **Load two words**<br>(addr -- n1 n2)<br>Places the two words starting at addr on the top of the stack. |
| 2CONSTANT x | no   | I   | **Double word constant**<br>(n1 n2 -- )<br>Declares a double word constant x. When x is executed, n1 and n2 are placed on the stack. |
| 2DROP    | no   | IC   | **Double drop**<br>(n1 n2 -- )<br>Discards two topmost items on the stack. |
| 2DUP     | no   | IC   | **Duplicate two**<br>(n1 n2 -- n1 n2 n1 n2)<br>Duplicates two topmost items on the stack. |
| 2OVER    | no   | IC   | **Double over**<br>(n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2)<br>Copies the second pair of items on the stack to the top of the stack. |
| 2ROT     | no   | IC   | **Double rotate**<br>(n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)<br>Rotate the third pair on the stack to the top of the stack, moving down the first and the second pair. |
| 2SWAP    | no   | IC   | **Double swap**<br>(n1 n2 n3 n4 -- n3 n4 n1 n2)<br>Swaps the first and the second pair on the stack. |
| 2VARIABLE x | no   | I   | **Double variable**<br>Creates a two cell (8 byte) variable named x. When x is executed, the address of the 8 byte area is placed on the stack. |
| ABORT    | no   | IC   | **Abort**<br>Clears the stack and the object and performs a QUIT. |
| ALLOT    | no   | IC   | **Allocate heap**<br>(n -- )<br>Allocates n cells of heap space. |
| BEGIN    | yes  | C    | **Begin loop**<br><br>Begins a loop. The end of the loop is marked by the matching AGAIN, REPEAT, or UNTIL. |
| BL       | no   | IC   | **Blank**<br>( -- n)<br>Constant that leaves 32 (the ASCII code of the SPACE char) on the top of the stack. |
| BYE      | no   | IC   | **Terminate execuition**<br>Asks the interpreter to terminate execution. It ends the EFrt program. |
| CHAR ccc | no   | IC   | **Char**<br>( -- c)<br>Skip leading spaces. Parse the string ccc and return c, the display code representing the first character of ccc. |
| [CHAR] ccc | yes  | C    | **Bracket char**<br>( -- c)<br>Compilation: Skip leading spaces. Parse the string ccc. Run-time: Return c, the display code representing the first character of ccc. Interpretation semantics for this word are undefined. |
| CLEAR    | no   | IC   | **Clear stack**<b<br><br>r>All items on the data stack are discarded. |
| CONSTANT x | no   | I    | **Declare constant**<br>(n --)<br>Declares a constant named x. When x is executed, the value n will be left on the stack. |
| DEPTH    | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | **Definite loop**<br>(limit index -- ) [ - limit index ]<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit−1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| DROP     | no   | IC   | **Discard top of stack**<br>(n --)<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | **Duplicate**<br>(n -- n n)<br>Duplicates the value at the top of the stack. |
| ?DUP     | no   | IC   | **Conditional duplicate**<br>(n -- 0 / n n)<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| ELSE     | yes  | C    | **Else**<br>(n -- n n)<br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| FORGET w | no   | IC   | **Forget word**<br>The most recent definition of word w is deleted, along with all words declared more recently than the named word. |
| HERE     | no   | IC   | **Heap address**<br>( -- addr)<br>The current heap allocation address is placed on the top of the stack. addr + 1 is the first free heap cell. |
| I        | yes  | C    | **Inner loop index**<br>( -- n) [n -- n]<br>The index of the innermost DO—LOOP is placed on the stack. |
| IF       | yes  | C    | **Conditional statement**<br>(flag --)<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| IMMEDIATE | no   | IC   | **Mark immediate**<br><br>The most recently defined word is marked for immediate execution; it will be executed even if entered in compile state. |
| J        | yes  | C    | **Outer loop index**<br>( -- n) [J lim I -- J lim I]<br>The loop index of the next to innermost DO—LOOP is placed on the stack. |
| LEAVE    | yes  | C    | **Exit DO—LOOP**<br>The innermost DO—LOOP is immediately exited. Execution resumes after the LOOP statement marking the end of the loop. |
| LITERAL  | yes  | C    | **Compile literal**<br>(n -- )<br>Compiles the value on the top of the stack into the current definition. When the definition is executed, that value will be pushed onto the top of the stack. |
| LOOP     | yes  | C    | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| +LOOP    | yes  | C    | **Add to loop index**<br>(n -- )<br>Adds n to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| OVER     | no   | IC   | **Duplicate second item**<br>(n1 n2 -- n1 n2 n1)<br>The second item on the stack is copied to the top. |
| QUIT     | no   | IC   | **Quit execution**<br>The return stack is cleared and control is returned to the interpreter. The stack and the object stack are not disturbed. |
| >R       | no   | IC   | **To return stack**<br>(n -- ) [ - n]<br>Removes the top item from the stack and pushes it onto the return stack. |
| R>       | no   | IC   | **From return stack**<br>( -- n) [n - ]<br>The top value is removed from the return stack and pushed onto the stack. |
| @R       | no   | IC   | **Fetch return stack**<br>( -- n) [n - n]<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack. |
| RECURSE  | yes  | C    | **Recurse**<br><br>Appends a call of the current word definition to the current word definition. The same thing can be done simple by using the current words definition name. |
| REPEAT   | yes  | C    | **Close BEGIN—WHILE—REPEAT loop**<br>( -- n)<br>Another iteration of the current BEGIN—WHILE—REPEAT loop having been completed, execution continues after the matching BEGIN. |
| ROT      | no   | IC   | **Rotate 3 items**<br>(n1 n2 n3 -- n2 n3 n1)<br>The third item on the stack is placed on the top of the stack and the second and first items are moved down. |
| -ROT     | no   | IC   | **Reverse rotate**<br>(n1 n2 n3 -- n2 n3 n1)<br>Moves the top of stack to the third item, moving the third and second items up. |
| SWAP     | no   | IC   | **Swap top two items**<br>(n1 n2 -- n2 n1)<br>The top two stack items are interchanged. |
| THEN     | yes  | C    | **End if**<br>( -- flag)<br>Used in an IF—ELSE—THEN sequence, marks the end of the conditional statement. |
| UNTIL    | yes  | C    | **End BEGIN—UNTIL loop**<br>(flag -- )<br>If flag is zero, the loop continues execution at the word following the matching BEGIN. If flag is nonzero, the loop is exited and the word following the UNTIL is executed. |
| VARIABLE x | no   | I    | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |
| WHILE    | yes  | C    | **Decide BEGIN—WHILE—REPEAT loop**<br>(flag -- )<br>If flag is nonzero, execution continues after the WHILE. If flag is zero, the loop is exited and execution resumed after the REPEAT that marks the end of the loop. |


#### TODO

Words: `SYSTEM STATE [ ] INCLUDE ABORT" str ARRAY x EXIT TRACE
  (XDO) (X?DO) (XLOOP) (+XLOOP) WORDSD ' x ['] x EXECUTE INT STRING EVALUATE UNLOOP +!
  S! BRANCH x ?BRANCH x`

Variables: `BASE STATE`