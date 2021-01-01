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
| !        | no   | IC   | **Store into address**<br>(n addr -- )<br>Stores the value n into the address addr (a variables stack index). |
| ' x      | no   | IC   | **Obtain execution token**<br>( -- xt)<br>Places the execution token of the following word on the stack. |
| (        | yes  | IC   | **Comment**<br>Skips all source characters till the closing ) character. |
| *        | no   | IC   | **n3 = n1 * n2**<br>(n1 n2 -- n3)<br>Multiplies n1 and n2 and leaves the product on the stack. |
| +        | no   | IC   | **n3 = n1 + n2**<br>(n1 n2 -- n3)<br>Adds n1 and n2 and leaves the sum on the stack. |
| +LOOP    | yes  | C    | **Add to loop index**<br>(n -- )<br>Adds n to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| ,        | no   | IC   | **Store in heap**<br>Reserves a single cell of data heap, initialising it to n. |
| -        | no   | IC   | **n3 = n1 - n2**<br>(n1 n2 -- n3)<br>Substracts n2 from n1 and leaves the difference on the stack. |
| .        | no   | IC   | **Print top of stack**<br>(n -- )<br>Prints the integer number on the top of the stack. |
| ."       | yes  | C    | **Print immediate string**<br>Prints the string that follows in the input stream. |
| /        | no   | IC   | **n3 = n1 / n2**<br>(n1 n2 -- n3)<br>Divides n1 by n2 and leaves the quotient on the stack. |
| /MOD     | no   | IC   | **n3 = n1 mod n2, n4 = n1 / n2**<br>(n1 n2 -- n3 n4)<br>Divides n1 by n2 and leaves quotient on top of stack, remainder as next on stack. |
| 0<       | no   | IC   | **Less than zero**<br>(n -- flag)<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0=       | no   | IC   | **Equal to zero**<br>(n -- flag)<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| 1+       | no   | IC   | **Add one**<br>(n1 -- n2)<br>Adds one to the top of the stack. |
| 1-       | no   | IC   | **Subtract one**<br>(n1 -- n2)<br>Substracts one from the top of the stack. |
| 2!       | no   | IC   | **Store two words**<br>(n1 n2 addr -- )<br>Stores the two words n1 and n2 at addresses addr and addr + 1. |
| 2*       | no   | IC   | **Times two**<br>(n1 -- n2)<br>Substracts two from the top of the stack. |
| 2/       | no   | IC   | **Divide by two**<br>(n1 -- n2)<br>Divides the top of the stack by two. |
| 2@       | no   | IC   | **Load two words**<br>(addr -- n1 n2)<br>Places the two words starting at addr on the top of the stack. |
| 2DROP    | no   | IC   | **Double drop**<br>(n1 n2 -- )<br>Discards two topmost items on the stack. |
| 2DUP     | no   | IC   | **Duplicate two**<br>(n1 n2 -- n1 n2 n1 n2)<br>Duplicates two topmost items on the stack. |
| 2OVER    | no   | IC   | **Double over**<br>(n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2)<br>Copies the second pair of items on the stack to the top of the stack. |
| 2SWAP    | no   | IC   | **Double swap**<br>(n1 n2 n3 n4 -- n3 n4 n1 n2)<br>Swaps the first and the second pair on the stack. |
| : w      | no   | I    | **Begin definition**<br>Begins compilation of a word named "w". |
| ;        | yes  | C    | **End definition**<br>Ends compilation of a word. |
| <        | no   | IC   | **Less than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 < n2, 0 otherwise. |
| =        | no   | IC   | **Equal**<br>(n1 n2 -- flag)<br>Returns -1 if n1 is equal to n2, 0 otherwise. |
| >        | no   | IC   | **Greater than**<br>(n1 n2 -- flag)<br>Returns -1 if n1 > n2, 0 otherwise. |
| >R       | no   | IC   | **To return stack**<br>(n -- ) [ - n]<br>Removes the top item from the stack and pushes it onto the return stack. |
| ?DUP     | no   | IC   | **Conditional duplicate**<br>(n -- 0 / n n)<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| @        | no   | IC   | **Fetch**<br>(addr -- n)<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack. |
| ABORT    | no   | IC   | **Abort**<br>Clears the stack and the object and performs a QUIT. |
| ABS      | no   | IC   | **n2 = Abs(n1)**<br>(n1 -- n2)<br>Replaces the top of stack with its absolute value. |
| ALLOT    | no   | IC   | **Allocate heap**<br>(n -- )<br>Allocates n cells of heap space. |
| AND      | no   | IC   | **Bitwise and**<br>(n1 n2 -- n3)<br>Stores the bitwise AND of n1 and n2 on the stack. |
| BEGIN    | yes  | C    | **Begin loop**<br><br>Begins a loop. The end of the loop is marked by the matching AGAIN, REPEAT, or UNTIL. |
| BL       | no   | IC   | **Blank**<br>( -- n)<br>Constant that leaves 32 (the ASCII code of the SPACE char) on the top of the stack. |
| CELLS    | no   | IC   | **Cells to bytes**<br>(n1 -- n2)<br>Converts n1 cells to n2 memory address units (bytes). Does actually nothing, since the heap is addressed in cells and not in bytes. |
| CHAR ccc | no   | IC   | **Char**<br>( -- c)<br>Skip leading spaces. Parse the string ccc and return c, the display code representing the first character of ccc. |
| CONSTANT x | no   | I    | **Declare constant**<br>(n --)<br>Declares a constant named x. When x is executed, the value n will be left on the stack. |
| CR       | no   | IC   | **Carriage return**<br>The folowing output will start at the new line. |
| CREATE   | no   | IC   | **Create object**<br>Create an object, given the name which appears next in the input stream, with a default action of pushing the parameter field address of the object when executed. No storage is allocated; normally the parameter field will be allocated and initialised by the defining word code that follows the CREATE. |
| DEPTH    | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | **Definite loop**<br>(limit index -- ) [ - limit index ]<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit−1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| DOES>    | no   | IC   | **Run-time action**<br>Sets the run-time action of a word created by the last CREATE to the code that follows. When the word is executed, its body is copied to the CREATEd word, and then the code that follows the DOES> is be executed. |
| DROP     | no   | IC   | **Discard top of stack**<br>(n --)<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | **Duplicate**<br>(n -- n n)<br>Duplicates the value at the top of the stack. |
| ELSE     | yes  | C    | **Else**<br><br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| EMIT     | no   | IC   | **Print char**<br>(n -- )<br>Prints out a character represented by a number on the top of the stack. |
| EXECUTE  | no   | IC   | **Execute word**<br>(xt -- )<br>Executes the word with execution token xt. |
| HERE     | no   | IC   | **Heap address**<br>( -- addr)<br>The current heap allocation address is placed on the top of the stack. addr + 1 is the first free heap cell. |
| I        | yes  | C    | **Inner loop index**<br>( -- n) [n -- n]<br>The index of the innermost DO—LOOP is placed on the stack. |
| IF       | yes  | C    | **Conditional statement**<br>(flag --)<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| IMMEDIATE | no   | IC   | **Mark immediate**<br><br>The most recently defined word is marked for immediate execution; it will be executed even if entered in compile state. |
| INVERT   | no   | IC   | **Bitwise not**<br>(n1 -- n2)<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| J        | yes  | C    | **Outer loop index**<br>( -- n) [J lim I -- J lim I]<br>The loop index of the next to innermost DO—LOOP is placed on the stack. |
| LEAVE    | yes  | C    | **Exit DO—LOOP**<br>The innermost DO—LOOP is immediately exited. Execution resumes after the LOOP statement marking the end of the loop. |
| LITERAL  | yes  | C    | **Compile literal**<br>(n -- )<br>Compiles the value on the top of the stack into the current definition. When the definition is executed, that value will be pushed onto the top of the stack. |
| LOOP     | yes  | C    | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| MAX      | no   | IC   | **Maximum**<br>(n1 n2 -- n3)<br>The greater of n1 and n2 is left on the top of the stack. |
| MIN      | no   | IC   | **Minimum**<br>(n1 n2 -- n3)<br>The lesser of n1 and n2 is left on the top of the stack. |
| MOD      | no   | IC   | **Modulus (remainder)**<br>(n1 n2 -- n3)<br>The remainder when n1 is divided by n2 is left on the top of the stack. |
| NEGATE   | no   | IC   | **n2 = -n1**<br>(n1 -- n2)<br>Negates the value the top of the stack. |
| OR       | no   | IC   | **Bitwise or**<br>(n1 n2 -- n3)<br>Stores the bitwise or of n1 and n2 on the stack. |
| OVER     | no   | IC   | **Duplicate second item**<br>(n1 n2 -- n1 n2 n1)<br>The second item on the stack is copied to the top. |
| QUIT     | no   | IC   | **Quit execution**<br>The return stack is cleared and control is returned to the interpreter. The stack and the object stack are not disturbed. |
| R>       | no   | IC   | **From return stack**<br>( -- n) [n - ]<br>The top value is removed from the return stack and pushed onto the stack. |
| @R       | no   | IC   | **Fetch return stack**<br>( -- n) [n - n]<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack. |
| RECURSE  | yes  | C    | **Recurse**<br><br>Appends a call of the current word definition to the current word definition. The same thing can be done simple by using the current words definition name. |
| REPEAT   | yes  | C    | **Close BEGIN—WHILE—REPEAT loop**<br>( -- n)<br>Another iteration of the current BEGIN—WHILE—REPEAT loop having been completed, execution continues after the matching BEGIN. |
| ROT      | no   | IC   | **Rotate 3 items**<br>(n1 n2 n3 -- n2 n3 n1)<br>The third item on the stack is placed on the top of the stack and the second and first items are moved down. |
| S"       | no   | IC   | **String literal**<br>{ -- s}<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |
| S>D      | no   | IC   | **Single cell number to double cell number**<br>(n -- d)<br>Converts a single cell number (32bit, int) to a double cell number (64bit, long). |
| SPACE    | no   | IC   | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES   | no   | IC   | **Print spaces**<br>(n -- )<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| SWAP     | no   | IC   | **Swap top two items**<br>(n1 n2 -- n2 n1)<br>The top two stack items are interchanged. |
| THEN     | yes  | C    | **End if**<br>( -- flag)<br>Used in an IF—ELSE—THEN sequence, marks the end of the conditional statement. |
| UNTIL    | yes  | C    | **End BEGIN—UNTIL loop**<br>(flag -- )<br>If flag is zero, the loop continues execution at the word following the matching BEGIN. If flag is nonzero, the loop is exited and the word following the UNTIL is executed. |
| VARIABLE x | no   | I    | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |
| WHILE    | yes  | C    | **Decide BEGIN—WHILE—REPEAT loop**<br>(flag -- )<br>If flag is nonzero, execution continues after the WHILE. If flag is zero, the loop is exited and execution resumed after the REPEAT that marks the end of the loop. |
| XOR      | no   | IC   | **Bitwise exclusive or**<br>(n1 n2 -- n3)<br>Stores the bitwise exclusive or of n1 and n2 on the stack. |
| [CHAR] ccc | yes  | C    | **Bracket char**<br>( -- c)<br>Compilation: Skip leading spaces. Parse the string ccc. Run-time: Return c, the display code representing the first character of ccc. Interpretation semantics for this word are undefined. |

Note: The `."` word works like `S" str" S.` words together.


## TODO

Words: `*/ */MOD +! >NUMBER ABORT" str" ACCEPT ALIGNALIGNED CELL+ COUNT DECIMAL 
  ENVIRONMENT? EVALUATE EXIT FILL FIND FM/MOD LSHIFT M* MOVE POSTPONE RSHIFT SM/REM
  TYPE U. U< UM* UM/MOD UNLOOP WORD [ ['] x ]`

Words (?): `>BODY`

Words (opt): `SYSTEM INCLUDE ARRAY x TRACE
  (XDO) (X?DO) (XLOOP) (+XLOOP) WORDSD  INT STRING 
  S! BRANCH x ?BRANCH x`

Variables: `BASE STATE`

## CREATE or BUILDS?

http://amforth.sourceforge.net/TG/recipes/Builds.html
http://www.forth.org/svfig/Len/definwds.htm