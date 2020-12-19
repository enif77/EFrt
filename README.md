# EFrt

EFrt is a embeddable FORTH language implementation.

## Data types

  - cell: A 32 bit data unit (int).
  - single cell integer: 32 bit signed integer number (int, 1 cell). Ex.: 123
  - double cell integer: 64 bit signed integer number (long, 2 cells). Ex.: 123L
  - floating point: 64 bit float number (double, 2 cells). Ex.: 123.0
  - string: A double quote terminated strings, stored on the objects stack. Ex.: "Hello!"
  - object: Any user data reference (object).

## Stacks

  - Data stack: Main stack for user data. Holds all 32bit and 64bit integers and 64 bit floats (as two 32 bit cells).
  - Return stack: Stack for interpreter internal use. Holds 32 bit signed integers.
  - Object stack: Can hold any object and strings.

## Heaps

  - Data heap: Main heap for user data. Holds all 32bit and 64bit integers and 64 bit floats (as two 32 bit cells).
  - Object heap: Hold any objects and strings.


## Examples

```

( Hello world! )
: hello S" Hello, world!" S. CR ;

( A simplified hello-world! )
: GREET   ." Hello, I speak Forth " ;

( Large letter F )
: STAR 42 EMIT ;
: STARS 0 DO  STAR  LOOP ;
: MARGIN CR 30 SPACES ;
: BLIP MARGIN STAR ;
: BAR MARGIN 5 STARS ;
: F BAR BLIP BAR BLIP BLIP CR ;

( do-loop, that runs 5 times )
: doit 5 0 DO ." hello" CR LOOP ;

( do-loop, that runs 5 times and shows the current I value ) 
: doit 5 0 DO ." hello" 1 SPACES I . CR LOOP 

( do-loop, that breaks after I > 4 ) 
: doit 10 0 DO ." hello" 1 SPACES I DUP . CR 4 > IF LEAVE THEN LOOP ;
  
( begin-until loop, that prins even numbers from 10 to 0 )
: doit 10 BEGIN DUP . CR 2 - DUP 0< UNTIL ;

( infinite loop )
: doit BEGIN ." hello" CR AGAIN ;

( loop writes "hello" 10 times )
: doit 10 BEGIN DUP 0> WHILE ." hello " 1 - DUP . CR REPEAT ;

( Messing with programmers... )
: 1 2 ;
1 1 + . CR  \ What result are you expecting here? :-)

( Constants )
123 CONSTANT C1  \ Defines a constant C1.
C1 . CR          \ Prints the value of the constant - 123.

( Variables )
VARIABLE A   \ Defines a single cell variable A.
123 A !      \ Stores 123 into the variable A.
A @ . CR     \ Fetches and prints out the value of the variable A.
A ? CR       \ The same thing - "?" is a shortcut for "@ .". 

2VARIABLE B  \ Defines a double cell variable B.
1.5 B 2!     \ Stores 1.5 float (a double cell value) into the variable B.
B 2@ F.      \ Fetches and prints out the double cell (float) value of the variable B.

```


## Words

Here is a list of implemented words.

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, {} = object stack.
- Description: A description of the word.


### CORE (CoreLib)

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

Words: `SYSTEM STATE MARKER name CHAR [ ] INCLUDE ABORT" str ARRAY x EXIT IMMEDIATE LITERAL TRACE
  (XDO) (X?DO) (XLOOP) (+XLOOP) WORDSD ' EXECUTE INT STRING EVALUATE UNLOOP +!
  NIP TUCK 2NIP 2TUCK -ROLL S! BRANCH x ?BRANCH x`

Variables: `BASE STATE`

---

### INT (IntegerLib)

| Name     | Imm. | Mode | Stack op.    | Description |
| ---      | ---  | ---  | ---          | --- |
| +        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 + n2**<br>Adds n1 and n2 and leaves the sum on the stack. |
| -        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 - n2**<br>Substracts n2 from n1 and leaves the difference on the stack. |
| *        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 * n2**<br>Multiplies n1 and n2 and leaves the product on the stack. |
| /        | no   | IC   | (n1 n2 -- n3) | **n3 = n1 / n2**<br>Divides n1 by n2 and leaves the quotient on the stack. |
| <        | no   | IC   | (n1 n2 -- flag) | **Less than**<br>Returns -1 if n1 < n2, 0 otherwise. |
| <=       | no   | IC   | (n1 n2 -- flag) | **Less than or equal**<br>Returns -1 if n1 <= n2, 0 otherwise. |
| <>       | no   | IC   | (n1 n2 -- flag) | **Not equal**<br>Returns -1 if n1 is not equal to n2, 0 otherwise. |
| =        | no   | IC   | (n1 n2 -- flag) | **Equal**<br>Returns -1 if n1 is equal to n2, 0 otherwise. |
| >        | no   | IC   | (n1 n2 -- flag) | **Greater than**<br>Returns -1 if n1 > n2, 0 otherwise. |
| >=       | no   | IC   | (n1 n2 -- flag) | **Greater than or equal**<br>Returns -1 if n1 >= n2, 0 otherwise. |
| 0<       | no   | IC   | (n1 -- flag)  | **Less than zero**<br>Returns -1 if n1 is less than 0, 0 otherwise. |
| 0<>      | no   | IC   | (n1 -- flag)  | **Nonzero**<br>Returns -1 if n1 is not equal to 0, 0 otherwise. |
| 0=       | no   | IC   | (n1 -- flag)  | **Equal to zero**<br>Returns -1 if n1 is equal to 0, 0 otherwise. |
| 0>       | no   | IC   | (n1 -- flag)  | **Greater than zero**<br>Returns -1 if n1 is greater than 0, 0 otherwise. |
| 1+       | no   | IC   | (n1 -- n2)    | **Add one**<br>Adds one to the top of the stack. |
| 1-       | no   | IC   | (n1 -- n2)    | **Subtract one**<br>Substracts one from the top of the stack. |
| 2+       | no   | IC   | (n1 -- n2)    | **Add two**<br>Adds two to the top of the stack. |
| 2-       | no   | IC   | (n1 -- n2)    | **Subtract two**<br>Substracts two from the top of the stack. |
| 2*       | no   | IC   | (n1 -- n2)    | **Times two**<br>Substracts two from the top of the stack. |
| 2/       | no   | IC   | (n1 -- n2)    | **Divide by two**<br>Divides the top of the stack by two. |
| ABS      | no   | IC   | (n1 -- n2)    | **n2 = Abs(n1)**<br>Replaces the top of stack with its absolute value. |
| AND      | no   | IC   | (n1 n2 -- n3) | **Bitwise and**<br>Stores the bitwise AND of n1 and n2 on the stack. |
| NEGATE   | no   | IC   | (n1 -- n2)    | **n2 = -n1**<br>Negates the value the top of the stack. |
| MAX      | no   | IC   | (n1 n2 -- n3) | **Maximum**<br>The greater of n1 and n2 is left on the top of the stack. |
| MIN      | no   | IC   | (n1 n2 -- n3) | **Minimum**<br>The lesser of n1 and n2 is left on the top of the stack. |
| MOD      | no   | IC   | (n1 n2 -- n3) | **Modulus (remainder)**<br>The remainder when n1 is divided by n2 is left on the top of the stack. |
| /MOD     | no   | IC   | (n1 n2 -- n3 n4) | **n3 = n1 mod n2, n4 = n1 / n2**<br>Divides n1 by n2 and leaves quotient on top of stack, remainder as next on stack. |
| NOT      | no   | IC   | (n1 -- n2)    | **Bitwise not**<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| OR       | no   | IC   | (n1 n2 -- n3) | **Bitwise or**<br>Stores the bitwise or of n1 and n2 on the stack. |
| XOR      | no   | IC   | (n1 n2 -- n3) | **Bitwise exclusive or**<br>Stores the bitwise exclusive or of n1 and n2 on the stack. |


#### TODO

Words: SHIFT INVERT 

---

### DOUBLE (LongIntegerLib)

| Name     | Imm. | Mode | Stack op.    | Description |
| ---      | ---  | ---  | ---          | --- |
| D+       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 + d2**<br>Adds d1 and d2 and leaves the sum on the stack. |
| D-       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 - d2**<br>Substracts d2 from d1 and leaves the difference on the stack. |
| D*       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 * d2**<br>Multiplies d1 and d2 and leaves the product on the stack. |
| D/       | no   | IC   | (d1 d2 -- d3) | **d3 = d1 / d2**<br>Divides d1 by d2 and leaves the quotient on the stack. |
| D<       | no   | IC   | (d1 d2 -- flag) | **Less than**<br>Returns -1 if d1 < d2, 0 otherwise. |
| D<=      | no   | IC   | (d1 d2 -- flag) | **Less than or equal**<br>Returns -1 if d1 <= d2, 0 otherwise. |
| D<>      | no   | IC   | (d1 d2 -- flag) | **Not equal**<br>Returns -1 if d1 is not equal to d2, 0 otherwise. |
| D=       | no   | IC   | (d1 d2 -- flag) | **Equal**<br>Returns -1 if d1 is equal to d2, 0 otherwise. |
| D>       | no   | IC   | (d1 d2 -- flag) | **Greater than**<br>Returns -1 if d1 > d2, 0 otherwise. |
| D>S      | no   | IC   | (d -- n) | **Double cell number to single cell number**<br>Converts a double cell number (64bit, long) to a single cell number (32bit, int). |
| D>=      | no   | IC   | (d1 d2 -- flag) | **Greater than or equal**<br>Returns -1 if d1 >= d2, 0 otherwise. |
| D0<      | no   | IC   | (d1 -- flag)  | **Less than zero**<br>Returns -1 if d1 is less than 0, 0 otherwise. |
| D0<>     | no   | IC   | (d1 -- flag)  | **Nonzero**<br>Returns -1 if d1 is not equal to 0, 0 otherwise. |
| D0=      | no   | IC   | (d1 -- flag)  | **Equal to zero**<br>Returns -1 if d1 is equal to 0, 0 otherwise. |
| D0>      | no   | IC   | (d1 -- flag)  | **Greater than zero**<br>Returns -1 if d1 is greater than 0, 0 otherwise. |
| D1+      | no   | IC   | (d1 -- d2)    | **Add one**<br>Adds one to the top of the stack. |
| D1-      | no   | IC   | (d1 -- d2)    | **Subtract one**<br>Substracts one from the top of the stack. |
| D2+      | no   | IC   | (d1 -- d2)    | **Add two**<br>Adds two to the top of the stack. |
| D2-      | no   | IC   | (d1 -- d2)    | **Subtract two**<br>Substracts two from the top of the stack. |
| D2*      | no   | IC   | (d1 -- d2)    | **Times two**<br>Substracts two from the top of the stack. |
| D2/      | no   | IC   | (d1 -- d2)    | **Divide by two**<br>Divides the top of the stack by two. |
| DABS     | no   | IC   | (d1 -- d2)    | **n2 = Abs(n1)**<br>Replaces the top of stack with its absolute value. |
| DAND     | no   | IC   | (d1 d2 -- d3) | **Bitwise and**<br>Stores the bitwise AND of d1 and n2 on the stack. |
| DNEGATE  | no   | IC   | (d1 -- d2)    | **n2 = -n1**<br>Negates the value the top of the stack. |
| DMAX     | no   | IC   | (d1 d2 -- d3) | **Maximum**<br>The greater of d1 and d2 is left on the top of the stack. |
| DMIN     | no   | IC   | (d1 d2 -- d3) | **Minimum**<br>The lesser of d1 and d2 is left on the top of the stack. |
| DMOD     | no   | IC   | (d1 d2 -- d3) | **Modulus (remainder)**<br>The remainder when d1 is divided by d2 is left on the top of the stack. |
| D/MOD    | no   | IC   | (d1 d2 -- d3 d4) | **d3 = d1 mod d2, d4 = d1 / d2**<br>Divides d1 by d2 and leaves quotient on top of stack, remainder as next on stack. |
| DNOT     | no   | IC   | (d1 -- d2)    | **Bitwise not**<br>Inverts the bits in the value on the top of the stack. This performs logical negation for truth values of −1 (True) and 0 (False). |
| DOR      | no   | IC   | (d1 d2 -- d3) | **Bitwise or**<br>Stores the bitwise or of d1 and d2 on the stack. |
| DXOR     | no   | IC   | (d1 d2 -- d3) | **Bitwise exclusive or**<br>Stores the bitwise exclusive or of d1 and d2 on the stack. |
| S>D      | no   | IC   | (n -- d)      | **Single cell number to double cell number**<br>Converts a single cell number (32bit, int) to a double cell number (64bit, long). |


---

### FLOAT (FloatLib)

Words: `F1+ F1- F2+ F2- F2* F2/ F0= F0<> F0< F0>`

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| F+    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 + f2**<br>Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack. |
| F-    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 - f2**<br>Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack. |
| F*    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 * f2**<br>Multiplies two floating point numbers on the top of the stack and leaves the product on the stack. |
| F/    | no   | IC   | (f1 f2 -- f3) | **f3 = f1 / f2**<br>Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack. |
| F=    | no   | IC   | (f1 f2 -- flag) | **Floating equal**<br>Returns -1 if f1 is equal to f2, 0 otherwise. |
| F<>   | no   | IC   | (f1 f2 -- flag) | **Floating not equal**<br>Returns -1 if f1 is not equal to f2, 0 otherwise. |
| F<    | no   | IC   | (f1 f2 -- flag) | **Floating less than**<br>Returns -1 if f1 < f2, 0 otherwise. |
| F<=   | no   | IC   | (f1 f2 -- flag) | **Floating less than or equal**<br>Returns -1 if f1 <= f2, 0 otherwise. |
| F>    | no   | IC   | (f1 f2 -- flag) | **Floating greater than**<br>Returns -1 if f1 > f2, 0 otherwise. |
| F>=   | no   | IC   | (f1 f2 -- flag) | **Floating greater than or equal**<br>Returns -1 if f1 >= f2, 0 otherwise. |
| FABS  | no   | IC   | (f1 -- f2) | **f2 = Abs(f1)**<br>. |
| FIX   | no   | IC   | (f -- n) | **Floating to integer**<br>Converts a float number on the top of the floating poit stack to integer and stores it on the top of the data stack. |
| FLOAT | no   | IC   | (n -- f) | **Integer to floating**<br>Converts an integer on the top of the data stack to a floationg point number and stores it on the top of the floating point stack. |
| FMAX  | no   | IC   | (f1 f2 -- f3) | **Floating point maximum**<br>The greater of the two floating point values on the top of the stack is placed on the top of the stack. |
| FMIN  | no   | IC   | (f1 f2 -- f3) | **Floating point minimum**<br>The lesser of the two floating point values on the top of the stack is placed on the top of the stack. |

#### TODO

Words: ACOS ASIN ATAN ATAN2 COS EXP n NEGATE FNEGATE (LIT) LOG POW SIN SQRT TAN \>FLOAT FLOOR FLITERAL (FLIT)

---

### STR (StringLib)

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| S+    | no   | IC   | {s1 s2 -- s3} | **String concatenate**<br>The string s1 is concatenated with the string s2 and the resulting s1 + s2 string is stored at the top of the object stack. |
| S"    | no   | C    | { -- s}   | **String literal**<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |

#### Todo

Words: `STRCPY, STRINT, STRLEN, STRREAL, SUBSTR, STRFORM, STRCAT, STRCHAR, STRCMP, STRCMPI, COMPARE, (STRLIT), TYPE`

---

### OBJ (ObjectLib)

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| O!     | no   | IC   | (addr -- ) {o -- } | **Store into address**<br>Stores the objct o into the address addr (a object variables stack index). |
| O@     | no   | IC   | (addr -- ) { -- o} | **Fetch**<br>Loads the object at addr (a object variables stack index) and leaves it at the top of the object stack. |
| OALLOT | no   | IC   | (n -- addr) | **Allocate object heap**<br>Allocates n cells of object heap space. |
| OCLEAR | no   | IC   |           | **Clear object point stack**<br>All items on the object stack are discarded. |
| ODEPTH | no   | IC   | ( -- n)   | **Object stack depth**<br>Returns the number of items on the object stack. |
| ODROP  | no   | IC   | {o -- }   | **Discard top of the object stack**<br>Discards the value at the top of the object stack. |
| ODUP   | no   | IC   | {o -- o o} | **Duplicate object**<br>Duplicates the value at the top of the object stack. |
| OHERE  | no   | IC   | ( -- addr) | **Object heap address**<br>The current object heap allocation address is placed on the top of the stack. addr + 1 is the first free object heap cell. |
| OOVER  | no   | IC   | {o1 o2 -- o1 o2 o1} | **Duplicate the second object stack item**<br>The second item on the object stack is copied to the top. |
| OPICK  | no   | IC   | (n -- ) { -- o} | **Pick item from the object stack**<br>The index is removed from the stack and then the indexth object stack item is copied to the top of the object stack. The top of object stack has index 0, the second item of the object stack index 1, and so on. |
| OROLL  | no   | IC   | (n -- )   | **Rotate indexth item to top**<br>The index is removed from the stack and then the object stack item selected by the index, with 0 designating the top of the object stack, 1 the second item, and so on, is moved to the top of the objects stack. The intervening objects stack items are moved down one item. |
| OROT   | no   | IC   | {o1 o2 o3 -- o2 o3 o1} | **Rotate 3 object stack items**<br>The third item on the object stack is placed on the top of the object stack and the second and first items are moved down. |
| -OROT  | no   | IC   | {o1 o2 o3 -- o2 o3 o1} | **Reverse object stack rotate**<br>Moves the top of the object stack to the third item, moving the third and second items up. |
| OSWAP  | no   | IC   | {o1 o2 -- o2 o1} | **Swap top two object stack items**<br>The top two object stack items are interchanged. |
| OVARIABLE x | no   | I    |            | **Declare object variable**<br>An object type variable named x is declared and its value is set to null. When x is executed, its address will be placed on the stack. An object reference is reserved on the object variables stack for the variable's value. |

---

### IO (IoLib)

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| .     | no   | IC   | (n -- )   | **Print top of stack**<br>Prints the integer number on the top of the stack. |
| ?     | no   | IC   | (addr -- ) | **Print indirect**<br>Prints the value at the address (a variables stack index) at the top of the stack. |
| .(    | yes  | IC   |           | **Print constant string**<br>Immediatelly prints the string that follows in the input stream. |
| ."    | yes  | C    |           | **Print immediate string**<br>Prints the string that follows in the input stream. |
| .O    | no   | IC   |           | **Print object stack**<br>Prints entire contents of the object stack. TOS is the top-most item. |
| .S    | no   | IC   |           | **Print stack**<br>Prints entire contents of stack. TOS is the right-most item. |
| CR    | no   | IC   |           | **Carriage return**<br>The folowing output will start at the new line. |
| EMIT  | no   | IC   | (n -- )   | **Print char**<br>Prints out a character represented by a number on the top of the stack. |
| F.    | no   | IC   | (f -- )   | **Print floating point**<br>A floating point value on the top of the stack is printed. |
| S.    | no   | IC   | {s -- }   | **Print string**<br>A string on the top of the object stack is printed. |
| SPACE | no   | IC   |           | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES | no  | IC   | (n -- )   | **Print spaces**<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| WORDS | no   | IC   |           | **List words defined**<br>Defined words are listed, from the most recently defined to the first defined. |

Note: The `."` word works like `S" str" S.` words together.

#### Todo

Words: `.R`
