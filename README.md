# EFrt

EFrt is a embeddable FORTH language implementation.

## Data types

  - integer: 32 bit signed integer number (int).
  - floating point: 64 bit float number (double), that is 2 times more bits, than the integer type (or 2 data stack items).
  - string: A double quote terminated strings, stored on the objects stack.
  - other types will be available later - uint, short, ushort, byte etc.


## Stacks

  - Data stack: Main stack for user data. Holds all 32 bit integers and 64 bit floats (as 2 32 bit items).
  - Return stack: Stack for interpreter internal use. Holds 32 bit signed integers.
  - Object stack: Can hold any object and strings.


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
| 2DROP    | no   | IC   | (n1 n2 --) | **Double drop**<br>Discards two topmost items on the stack. |
| 2DUP     | no   | IC   | (n1 n2 -- n1 n2 n1 n2) | **Duplicate two**<br>Duplicates two topmost items on the stack. |
| 2OVER    | no   | IC   | (n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2) | **Double over**<br>Copies the second pair of items on the stack to the top of the stack. |
| 2ROT     | no   | IC   | (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2) | **Double rotate**<br>Rotate the third pair on the stack to the top of the stack, moving down the first and the second pair. |
| 2SWAP    | no   | IC   | (n1 n2 n3 n4 -- n3 n4 n1 n2) | **Double swap**<br>Swaps the first and the second pair on the stack. |
| AGAIN    | yes  | C    |           | **Indefinite loop**<br>Marks the end of an idefinite loop opened by the matching BEGIN. |
| BEGIN    | yes  | C    |           | **Begin loop**<br>Begins a loop. The end of the loop is marked by the matching AGAIN, REPEAT, or UNTIL. |
| BYE      | no   | IC   |           | **Terminate execuition**<br>Asks the interpreter to terminate execution. It ends the EFrt program. |
| CLEAR    | no   | IC   |           | **Clear stack**<br>All items on the data stack are discarded. |
| DEPTH    | no   | IC   | ( -- n)   | **Stack depth**<br>Returns the number of items on the stack before DEPTH was executed. |
| DO       | yes  | C    | (limit index -- ) [ - limit index ] | **Definite loop**<br>Executes the loop from the following word to the matching LOOP or +LOOP until n increments past the boundary between limit−1 and limit. Note that the loop is always executed at least once (see ?DO for an alternative to this). |
| ?DO      | yes  | C    | (limit index -- ) [ - limit index ] | **Conditional loop**<br>If n equals limit, skip immediately to the matching LOOP or +LOOP. Otherwise, enter the loop, which is thenceforth treated as a normal DO loop. |
| DROP     | no   | IC   | (n --)    | **Discard top of stack**<br>Discards the value at the top of the stack. |
| DUP      | no   | IC   | (n -- n n) | **Duplicate**<br>Duplicates the value at the top of the stack. |
| ?DUP     | no   | IC   | (n -- 0 / n n) | **Conditional duplicate**<br>If top of stack is nonzero, duplicate it. Otherwise leave zero on top of stack. |
| ELSE     | yes  | C    | (n -- n n) | **Else**<br>Used in an IF—ELSE—THEN sequence, delimits the code to be executed if the if-condition was false. |
| FALSE    | no   | IC   | ( -- flag) | **False**<br>Constant that leaves the 0 (false) on the top of the stack. |
| FORGET w | no   | IC   |           | **Forget word**<br>The most recent definition of word w is deleted, along with all words declared more recently than the named word. |
| I        | yes  | C    | ( -- n) [n -- n] | **Inner loop index**<br>The index of the innermost DO—LOOP is placed on the stack. |
| IF       | yes  | C    | (flag --) | **Conditional statement**<br>If flag is nonzero, the following statements are executed. Otherwise, execution resumes after the matching ELSE clause, if any, or after the matching THEN. |
| J        | yes  | C    | ( -- n) [J lim I -- J lim I] | **Outer loop index**<br>The loop index of the next to innermost DO—LOOP is placed on the stack. |
| LEAVE    | yes  | C    |           | **Exit DO—LOOP**<br>The innermost DO—LOOP is immediately exited. Execution resumes after the LOOP statement marking the end of the loop. |
| LOOP     | yes  | C    |           | **Increment loop index**<br>Adds one to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| +LOOP    | yes  | C    | (n -- )   | **Add to loop index**<br>Adds n to the index of the active loop. If the limit is reached, the loop is exited. Otherwise, another iteration is begun. |
| OCLEAR   | no   | IC   |           | **Clear object point stack**<br>All items on the object stack are discarded. |
| ODEPTH   | no   | IC   | ( -- n)   | **Object stack depth**<br>Returns the number of items on the object stack. |
| OVER     | no   | IC   | (n1 n2 -- n1 n2 n1) | **Duplicate second item**<br>The second item on the stack is copied to the top. |
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
| WHILE    | yes  | C    | (flag -- ) | **Decide BEGIN—WHILE—REPEAT loop**<br>If flag is nonzero, execution continues after the WHILE. If flag is zero, the loop is exited and execution resumed after the REPEAT that marks the end of the loop. |


#### TODO

Words: SYSTEM STATE PICK ROLL MARKER name CHAR [ ] INCLUDE .S ABORT ABORT" str ARRAY x EXIT IMMEDIATE LITERAL QUIT TRACE
  VARIABLE name (XDO) (X?DO) (XLOOP) (+XLOOP) CONSTANT ! @ WORDSD ' EXECUTE INT STRING EVALUATE UNLOOP EXIT

---

### INT (IntegerLib)

Words: `+ - * / 1+ 1- 2+ 2- 2* 2/ MOD /MOD NOT AND OR XOR MAX MIN ABS = <> < <= > >= 0= 0<> 0< 0> "`

#### + (n1 n2 - n3)

Adds n1 and n2 and leaves the sum on the stack.

#### - (n1 n2 - n3)

Substracts n2 from n1 and leaves the difference on the stack.

#### * (n1 n2 - n3)

Multiplies n1 and n2 and leaves the product on the stack.

#### / (n1 n2 - n3)

Divides n1 by n2 and leaves the quotient on the stack.

#### < (n1 n2 - flag)

Returns -1 if n1 < n2, 0 otherwise.

#### <= (n1 n2 - flag)

Returns -1 if n1 <= n2, 0 otherwise.

#### > (n1 n2 - flag)

Returns -1 if n1 > n2, 0 otherwise.

#### >= (n1 n2 - flag)

Returns -1 if n1 >= n2, 0 otherwise.

#### <> (n1 n2 - flag)

Returns -1 if n1 is not equal to n2, 0 otherwise.

#### = (n1 n2 - flag)

Returns -1 if n1 is equal to n2, 0 otherwise.

#### 0< (n1 - flag)

Returns -1 if n1 is less than 0, 0 otherwise.

#### 0> (n1 - flag)

Returns -1 if n1 is greater than 0, 0 otherwise.

#### 0<> (n1 - flag)

Returns -1 if n1 is not equal to 0, 0 otherwise.

#### 0= (n1 - flag)

Returns -1 if n1 is equal to 0, 0 otherwise.

#### 1+ (n1 - n2)

Adds one to the top of the stack.

#### 1- (n1 - n2)

Substracts one from the top of the stack.

#### 2+ (n1 - n2)

Adds two to the top of the stack.

#### 2- (n1 - n2)

Substracts two from the top of the stack.

#### 2* (n1 - n2)

Multiplies the top of the stack by two.

#### 2/ (n1 - n2)

Divides the top of the stack by two.

#### TODO

Words: SHIFT INVERT AND OR


---

### FLOAT (FloatLib)

Words: `F1+ F1- F2+ F2- F2* F2/ F0= F0<> F0< F0>`

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| F.    | no   | IC   | F:(f -- )   | **Print floating point**<br>A floating point value on the top of the stack is printed. |
| F+    | no   | IC   | F:(f1 f2 -- f3) | **f3 = f1 + f2**<br>Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack. |
| F-    | no   | IC   | F:(f1 f2 -- f3) | **f3 = f1 - f2**<br>Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack. |
| F*    | no   | IC   | F:(f1 f2 -- f3) | **f3 = f1 * f2**<br>Multiplies two floating point numbers on the top of the stack and leaves the product on the stack. |
| F/    | no   | IC   | F:(f1 f2 -- f3) | **f3 = f1 / f2**<br>Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack. |
| F=    | no   | IC   | F:(f1 f2 -- ) ( -- flag) | **Floating equal**<br>Returns -1 if f1 is equal to f2, 0 otherwise. |
| F<>   | no   | IC   | F:(f1 f2 -- ) ( -- flag) | **Floating not equal**<br>Returns -1 if f1 is not equal to f2, 0 otherwise. |
| F<    | no   | IC   | F:(f1 f2 -- ) ( -- flag) | **Floating less than**<br>Returns -1 if f1 < f2, 0 otherwise. |
| F<=   | no   | IC   | F:(f1 f2 -- ) ( -- flag) | **Floating less than or equal**<br>Returns -1 if f1 <= f2, 0 otherwise. |
| F>    | no   | IC   | F:(f1 f2 -- ) ( -- flag) | **Floating greater than**<br>Returns -1 if f1 > f2, 0 otherwise. |
| F>=   | no   | IC   | F:(f1 f2 -- ) ( -- flag) | **Floating greater than or equal**<br>Returns -1 if f1 >= f2, 0 otherwise. |
| FABS  | no   | IC   | F:(f1 -- f2) | **f2 = Abs(f1)**<br>. |
| FIX   | no   | IC   | F:(f -- ) ( -- n) | **Floating to integer**<br>Converts a float number on the top of the floating poit stack to integer and stores it on the top of the data stack. |
| FLOAT | no   | IC   | F:( -- f) (n -- ) | **Integer to floating**<br>Converts an integer on the top of the data stack to a floationg point number and stores it on the top of the floating point stack. |
| FMAX  | no   | IC   | (f1 f2 -- f3) | **Floating point maximum**<br>The greater of the two floating point values on the top of the stack is placed on the top of the stack. |
| FMIN  | no   | IC   | (f1 f2 -- f3) | **Floating point minimum**<br>The lesser of the two floating point values on the top of the stack is placed on the top of the stack. |

#### TODO

Words: ACOS ASIN ATAN ATAN2 COS EXP  n NEGATE FNEGATE (LIT) LOG POW SIN SQRT TAN \>FLOAT FLOOR FLITERAL (FLIT)

---

### STR (StringLib)

Words: `S+ S"`

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| S+    | no   | IC   | {s1 s2 -- s3} | **String concatenate**<br>The string s1 is concatenated with the string s2 and the resulting s1 + s2 string is stored at the top of the object stack. |
| S"    | no   | C    | { -- s}   | **String literal**<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |

#### Todo

Words: STRCPY, STRINT, STRLEN, STRREAL, SUBSTR, STRFORM, STRCAT, STRCHAR, STRCMP, STRCMPI, COMPARE, (STRLIT), TYPE

---

### OBJ (ObjectLib)

Words: `ODUP ODROP OSWAP OOVER OROT -OROT ODEPTH OCLEAR`

---

### IO (IoLib)

Words: `.( ." . S. CR EMIT SPACES SPACE WORDS`

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| .     | no   | IC   | (n -- )   | **Print top of stack**<br>Prints the integer number on the top of the stack. |
| .(    | yes  | IC   |           | **Print constant string**<br>Immediatelly prints the string that follows in the input stream. |
| ."    | yes  | C    |           | **Print immediate string**<br>Prints the string that follows in the input stream. |
| CR    | no   | IC   |           | **Carriage return**<br>The folowing output will start at the new line. |
| EMIT  | no   | IC   | (n -- )   | **Print char**<br>Prints out a character represented by a number on the top of the stack. |
| S.    | no   | IC   | {s -- }   | **Print string**<br>A string on the top of the object stack is printed. |
| SPACE | no   | IC   |           | **Print SPACE**<br>Prints out the SPACE character. |
| SPACES | no  | IC   | (n -- )   | **Print spaces**<br>Prints out N characters of SPACE, where N is a number on the top of the stack. |
| WORDS | no   | IC   |           | **List words defined**<br>Defined words are listed, from the most recently defined to the first defined. |

Note: The `."` word works like `S" str" S.` words together.
