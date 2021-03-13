# CORE

Common words for all base operations.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes, S = suspended compilation only (not available in I, C or IC).
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name     | Imm. | Mode | Description |
| ---      | ---  | ---  | --- |
| !        | no   | IC   | **Store into address**<br>(n a-addr -- )<br>Stores the value n into the address addr (a heap array index). |
| ' x      | no   | IC   | **Obtain execution token**<br>( -- xt)<br>Places the execution token of the following word on the stack. |
| (        | yes  | IC   | **Comment**<br>Skips all source characters till the closing ) character. |
| *        | no   | IC   | **n3 = n1 * n2**<br>(n1 n2 -- n3)<br>Multiplies n1 and n2 and leaves the product on the stack. |
| */       | no   | IC   | **n4 = (n1 * n2) / n3**<br>(n1 n2 n3 -- n4)<br>Multiplies n1 and n2 producing the double-cell result d. Divides d by n3 giving the single cell quotient n4. |
| */MOD    | no   | IC   | **n5 = (n1 * n2) / n3, n4 = (n1 * n2) % n3**<br>(n1 n2 n3 -- n4 n5)<br>Multiplies n1 and n2 producing the double-cell result d. Divides d by n3 giving the single cell remainder n4 and singlecell quotient n5. |
| +        | no   | IC   | **n3 = n1 + n2**<br>(n1 n2 -- n3)<br>Adds n1 and n2 and leaves the sum on the stack. |
| +!       | no   | IC   | **addr += n**<br>(n addr -- )<br>Adds n to the contents at the address addr. |
| +LOOP    | yes  | C    | **Add to loop index**<br>(n -- )<br>Adds n to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| ,        | no   | IC   | **Store in heap**<br>Reserves a single cell of data heap, initialising it to n. |
| -        | no   | IC   | **n3 = n1 - n2**<br>(n1 n2 -- n3)<br>Subtracts n2 from n1 and leaves the difference on the stack. |
| .        | no   | IC   | **Print top of stack**<br>(n -- )<br>Prints the integer number on the top of the stack. |
| ." str   | yes  | C    | **Print immediate string**<br>Prints the string that follows in the input stream. |
| /        | no   | IC   | **n3 = n1 / n2**<br>(n1 n2 -- n3)<br>Divides n1 by n2 and leaves the quotient on the stack. |
| /MOD     | no   | IC   | **n3 = n1 mod n2, n4 = n1 / n2**<br>(n1 n2 -- n3 n4)<br>Divides n1 by n2 and leaves quotient on top of stack, remainder as next on stack. |
| 0<       | no   | IC   | **Less than zero**<br>(n -- flag)<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0=       | no   | IC   | **Equal to zero**<br>(n -- flag)<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| 1+       | no   | IC   | **Add one**<br>(n1 -- n2)<br>Adds one to the top of the stack. |
| 1-       | no   | IC   | **Subtract one**<br>(n1 -- n2)<br>Subtracts one from the top of the stack. |
| 2!       | no   | IC   | **Store two words**<br>(n1 n2 addr -- )<br>Stores the two words n1 and n2 at addresses addr and addr + 1. |
| 2*       | no   | IC   | **Times two**<br>(n1 -- n2)<br>Multiplies the top of the stack by two. |
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
| >BODY    | no   | IC   | **To body**<br>(xt -- addr)<br>Gets the data-field address of a CREATEd word. |
| >NUMBER  | no   | IC   | **String to number**<br>( -- n true \| false) {s -- }<br>Parses a string into a single cell integer. Leaves true and the number on the stack, if the conversion was successfull. Leaves just false, if the conversion failed. |
| >R       | no   | IC   | **To return stack**<br>(n -- ) [ - n]<br>Removes the top item from the stack and pushes it onto the return stack. |
| ?DUP     | no   | IC   | **Conditional duplicate**<br>(n -- 0 / n n)<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| @        | no   | IC   | **Fetch**<br>(addr -- n)<br>Loads the value at addr (a variables stack index) and leaves it at the top of the stack. |
| ABORT    | no   | IC   | **Abort**<br>Clears the stack and the object stack and performs a QUIT. |
| ABORT" str | yes  | C    | **Abort with message**<br>(flag -- )<br>Prints the string literal that follows in line, then aborts, clearing all execution state to return to the interpreter. |
| ABS      | no   | IC   | **n2 = Abs(n1)**<br>(n1 -- n2)<br>Replaces the top of stack with its absolute value. |
| ALIGN    | no   | IC   | **Align data pointer**<br>( -- )<br> If the data-space pointer is not single cell integer aligned, reserve enough data space to make it so. |
| ALIGNED  | no   | IC   | **Get aligned address**<br>(addr1 -- addr2)<br> The addr2 address is the first single cell integer-aligned address greater than or equal to addr1. |
| ALLOT    | no   | IC   | **Allocate heap**<br>(n -- )<br>Allocates n cells of heap space. |
| AND      | no   | IC   | **Bitwise and**<br>(n1 n2 -- n3)<br>Stores the bitwise AND of n1 and n2 on the stack. |
| BASE     | no   | IC   | **Number conversion radix address**<br>( -- addr)<br>The addr is the address of a cell containing the current number-conversion radix {2...36}. |
| BEGIN    | yes  | C    | **Begin loop**<br><br>Begins a loop. The end of the loop is marked by the matching AGAIN, REPEAT, or UNTIL. |
| BL       | no   | IC   | **Blank**<br>( -- n)<br>Constant that leaves 32 (the ASCII code of the SPACE char) on the top of the stack. |
| C!       | no   | IC   | **Store char into address**<br>(char a-addr -- )<br>Stores the value char into the address addr (a heap array index). |
| C,       | no   | IC   | **Store char in heap**<br>Reserves a single char of data heap, initialising it to char. |
| C@       | no   | IC   | **Fetch char**<br>(c-addr -- char)<br>Loads the character at the c-addr and leaves it at the top of the stack. |
| CELL+    | no   | IC   | **Add single cell integer size**<br>(addr1 -- addr2)<br>Add the size in address units of a single cell integer number to addr1, giving addr2. |
| CELLS    | no   | IC   | **Cells to bytes**<br>(n1 -- n2)<br>Converts n1 cells to n2 memory address units (bytes). |
| CHAR ccc | no   | IC   | **Char**<br>( -- c)<br>Skip leading spaces. Parse the string ccc and return c, the display code representing the first character of ccc. |
| CHAR+    | no   | IC   | **Add char size**<br>(c-addr1 -- c-addr2)<br>Add the size in address units of a character to c-addr1, giving c-addr2. |
| CHARS    | no   | IC   | **Chars to bytes**<br>(n1 -- n2)<br>Converts n1 characters to n2 memory address units (bytes). |
| CONSTANT x | no   | I    | **Declare constant**<br>(n -- )<br>Declares a constant named x. When x is executed, the value n will be left on the stack. |
| COUNT    | no   | IC   | **String length**<br>( -- n) {o -- s} <br>Converts an object on the top of the object stack to a string and returns the number of characters (string.Length) in it. Leaves the string on the top of the object stack. |
| CR       | no   | IC   | **Carriage return**<br>( -- )<br>The folowing output will start at the new line. |
| CREATE   | no   | IC   | **Create object**<br>Create an object, given the name which appears next in the input stream, with a default action of pushing the parameter field address of the object when executed. No storage is allocated; normally the parameter field will be allocated and initialised by the defining word code that follows the CREATE. |
| DECIMAL  | no   | IC   | **Set number conversion radix to ten**<br>( -- )<br>Set the numeric conversion radix to ten (decimal). |
| DEPTH    | no   | IC   | **Stack depth**<br>( -- n)<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | **Definite loop**<br>(limit index -- ) [ - limit index ]<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit−1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| DOES>    | no   | IC   | **Run-time action**<br>Sets the run-time action of a word created by the last CREATE to the code that follows. When the word is executed, its body is copied to the CREATEd word, and then the code that follows the DOES> is be executed. |
| DROP     | no   | IC   | **Discard top of stack**<br>(n --)<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | **Duplicate**<br>(n -- n n)<br>Duplicates the value at the top of the stack. |
| ELSE     | yes  | C    | **Else**<br><br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| EMIT     | no   | IC   | **Print char**<br>(n -- )<br>Prints out a character represented by a number on the top of the stack. |
| EVALUATE | no   | IC   | **Evaluate string**<br>{s -- }<br>Evaluates a string the top of the object stack. |
| EXECUTE  | no   | IC   | **Execute word**<br>(xt -- )<br>Executes the word with execution token xt. |
| EXIT     | yes  | C    | **Exit definition**<br>Exit from the current definition immediately. Note that EXIT cannot be used within a DO—LOOP without UNLOOP. Use LEAVE instead. |
| FILL     | no   | IC   | **Fill memory**<br>(addr n1 n2 -- )<br>If n1 is greater than zero, fills n1 cells beginning at addr with n2. |
| FIND     | no   | IC   | **Find word**<br>( -- 0 \| xt 1 \| xt -1) {s -- \| s}<br>Find the definition named in the string s. If the definition is not found, return s and zero. If the definition is found, return its execution token xt. If the definition is immediate, also return one (1), otherwise also return minus-one (-1). |
| FM/MOD   | no   | IC   | **n2 = d % n1, n3 = d / n1**<br>(d n1 -- n2 n3)<br>Divide d by n1, giving the floored quotient n3 and the remainder n2. |
| HERE     | no   | IC   | **Heap address**<br>( -- addr)<br>The current heap allocation address is placed on the top of the stack. addr + 1 is the first free heap cell. |
| I        | yes  | C    | **Inner loop index**<br>( -- n) [n -- n]<br>The index of the innermost DO—LOOP is placed on the stack. |
| IF       | yes  | C    | **Conditional statement**<br>(flag --)<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| IMMEDIATE | no   | IC   | **Mark immediate**<br><br>The most recently defined word is marked for immediate execution; it will be executed even if entered in compile state. |
| INVERT   | no   | IC   | **Bitwise not**<br>(n1 -- n2)<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| J        | yes  | C    | **Outer loop index**<br>( -- n) [J lim I -- J lim I]<br>The loop index of the next to innermost DO—LOOP is placed on the stack. |
| LEAVE    | yes  | C    | **Exit DO—LOOP**<br>The innermost DO—LOOP is immediately exited. Execution resumes after the LOOP statement marking the end of the loop. |
| LITERAL  | yes  | C    | **Compile literal**<br>(n -- )<br>Compiles the value on the top of the stack into the current definition. When the definition is executed, that value will be pushed onto the top of the stack. |
| LOOP     | yes  | C    | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| LSHIFT   | no   | IC   | **n2 = n1 << u**<br>(n1 u -- n2)<br>Perform a logical left shift of u bit-places on x1, giving x2. |
| M*       | no   | IC   | **d = n1 * n2**<br>(n1 n2 -- d)<br>d is the signed product of n1 times n2. |
| MAX      | no   | IC   | **Maximum**<br>(n1 n2 -- n3)<br>The greater of n1 and n2 is left on the top of the stack. |
| MIN      | no   | IC   | **Minimum**<br>(n1 n2 -- n3)<br>The lesser of n1 and n2 is left on the top of the stack. |
| MOD      | no   | IC   | **Modulus (remainder)**<br>(n1 n2 -- n3)<br>The remainder when n1 is divided by n2 is left on the top of the stack. |
| MOVE     | no   | IC   | **Copy cells**<br>(addr1 addr2 u -- )<br>If u is greater than zero, copy the contents of u consecutive cells at addr1 to the u consecutive cells at addr2. After MOVE completes, the u consecutive cells at addr2 contain exactly what the u consecutive cells at addr1 contained before the move. |
| NEGATE   | no   | IC   | **n2 = -n1**<br>(n1 -- n2)<br>Negates the value the top of the stack. |
| OR       | no   | IC   | **Bitwise or**<br>(n1 n2 -- n3)<br>Stores the bitwise or of n1 and n2 on the stack. |
| OVER     | no   | IC   | **Duplicate second item**<br>(n1 n2 -- n1 n2 n1)<br>The second item on the stack is copied to the top. |
| POSTPONE x | yes  | C    | **Append word**<br>( -- )<br>During compilation finds a word with the name x and adds it to the new word definition. |
| QUIT     | no   | IC   | **Quit execution**<br>The return stack is cleared and control is returned to the interpreter. The stack and the object stack are not disturbed. |
| R>       | no   | IC   | **From return stack**<br>( -- n) [n - ]<br>The top value is removed from the return stack and pushed onto the stack. |
| @R       | no   | IC   | **Fetch return stack**<br>( -- n) [n - n]<br>The top value on the return stack is pushed onto the stack. The value is not removed from the return stack. |
| RECURSE  | yes  | C    | **Recurse**<br><br>Appends a call of the current word definition to the current word definition. The same thing can be done simple by using the current words definition name. |
| REPEAT   | yes  | C    | **Close BEGIN—WHILE—REPEAT loop**<br>( -- n)<br>Another iteration of the current BEGIN—WHILE—REPEAT loop having been completed, execution continues after the matching BEGIN. |
| ROT      | no   | IC   | **Rotate 3 items**<br>(n1 n2 n3 -- n2 n3 n1)<br>The third item on the stack is placed on the top of the stack and the second and first items are moved down. |
| RSHIFT   | no   | IC   | **n2 = n1 >> u**<br>(n1 u -- n2)<br>Perform a logical right shift of u bit-places on x1, giving x2. |
| S" str   | yes  | IC   | **String literal**<br>{ -- s}<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |
| S>D      | no   | IC   | **Single cell number to double cell number**<br>(n -- d)<br>Converts a single cell number (32bit, int) to a double cell number (64bit, long). |
| SM/REM   | no   | IC   | **n2 = d % n1, n3 = d / n1**<br>(d n1 -- n2 n3)<br>Divide d by n1, giving the symmetric quotient n3 and the remainder n2. |
| SPACE    | no   | IC   | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES   | no   | IC   | **Print spaces**<br>(n -- )<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| STATE    | no   | IC   | **Interpreter state address**<br>( -- addr)<br>The addr is the address of a cell containing the compilation-state flag. STATE is tru ewhen in compilation state, false otherwise. |
| SWAP     | no   | IC   | **Swap top two items**<br>(n1 n2 -- n2 n1)<br>The top two stack items are interchanged. |
| THEN     | yes  | C    | **End if**<br>( -- flag)<br>Used in an IF—ELSE—THEN sequence, marks the end of the conditional statement. |
| TYPE     | no   | IC   | **Print string**<br>{s -- }<br>Prints out a string on the top of the object stack. |
| U.       | no   | IC   | **Print top of stack**<br>(u -- )<br>Prints the unsigned integer number on the top of the stack. |
| U<       | no   | IC   | **Less than**<br>(u1 u2 -- flag)<br>Returns -1 if u1 < u2, 0 otherwise. |
| UM*      | no   | IC   | **ud = u1 * u2**<br>(u1 u2 -- ud)<br>ud is the unsigned product of u1 times u2. |
| UM/MOD   | no   | IC   | **u2 = ud % u1, u3 = ud / u1**<br>(ud u1 -- u2 u3)<br>Divide ud by u1, giving the quotient u3 and the remainder u2. |
| UNLOOP   | no   | C    | **Discard DO—LOOP control parameters**<br>[limit index -- ]<br>Loop control parameters are removed from the return stack. Use this before the EXITing a loop. |
| UNTIL    | yes  | C    | **End BEGIN—UNTIL loop**<br>(flag -- )<br>If flag is zero, the loop continues execution at the word following the matching BEGIN. If flag is nonzero, the loop is exited and the word following the UNTIL is executed. |
| VARIABLE x | no   | I    | **Declare variable**<br>A variable named x is declared and its value is set to zero. When x is executed, its address will be placed on the stack. Four bytes are reserved on the heap for the variable's value. |
| WHILE    | yes  | C    | **Decide BEGIN—WHILE—REPEAT loop**<br>(flag -- )<br>If flag is nonzero, execution continues after the WHILE. If flag is zero, the loop is exited and execution resumed after the REPEAT that marks the end of the loop. |
| WORD     | no   | C    | **Parse word**<br>(c -- ) { -- s}<br>Skip leading delimiters (c). Parse characters ccc delimited by the char c. |
| XOR      | no   | IC   | **Bitwise exclusive or**<br>(n1 n2 -- n3)<br>Stores the bitwise exclusive or of n1 and n2 on the stack. |
| [        | yes  | C    | **Suspend compilation**<br>Within a compilation, returns to the interpretive state. |
| [']      | yes  | C    | **Obtain execution token**<br>Places the execution token of the following word to the currently compiled word as a literal. |
| [CHAR] ccc | yes  | C    | **Bracket char**<br>( -- c)<br>Compilation: Skip leading spaces. Parse the string ccc. Run-time: Return c, the display code representing the first character of ccc. Interpretation semantics for this word are undefined. |
| ]        | no   | S    | **Resume compilation**<br>Resumes a new word compilation. |

Note: The `."` word works like `S" str" S.` words together.


## TODO

Words: `ENVIRONMENT?`

The FILL word should work with chars and not bytes.


## Skipped words

```
# #> #S <# >IN ACCEPT HOLD KEY SIGN SOURCE
```
