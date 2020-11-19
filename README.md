# EFrt

EFrt is a embeddable FORTH language implementation.

## Data types

  - integer: 32  bit signed integer number,
  - float: 32 bit float number,
  - string: A double quote terminated strings. Stored on the object stack.
  - other types will be available later - uint, short, ushort, byte etc.

Data stack holds 32 bit structures, that are implemented as a union type. Any .NET type,, 
that fits into 32 bits will be supported. 64bit types (long, double) will be implemented as
double stack items in a special library.

## Stacks

  - Data stack: Main stack for user data. Holds all 32 bit data types.
  - Return stack: Stack for interpreter internal use. Holds 32 bit signed integers.
  - Object stack: Can hold any object. Available for user data, strings etc. Not used by the interpreter.

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
  (not available) during implementation, IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, {} = object stack.
- Description: A description of the word.


### CORE (CoreLib)

Words: ( \ : ; 2DROP 2DUP 2OVER 2ROT 2SWAP AGAIN BEGIN BYE CLEAR DEPTH DO ?DO DROP DUP ?DUP ELSE
  FALSE FORGET IF OVER ROT -ROT >R R> R@ TRUE THEN REPEAT LEAVE I J LOOP +LOOP SWAP UNTIL WHILE

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| (     | yes  | IC   |           | **Comment**<br>Skips all source characters till the closing ) character. |
| \     | yes  | IC   |           | **Line comment**<br>Skips all source characters till the closing EOLN character. |
| : w   | no   | I    |           | **Begin definition**<br>Begins compilation of a word named "w". |
| ;     | yes  | C    |           | **End definition**<br>Ends compilation of a word. |
| 2DROP | no   | IC   | (n1 n2 --) | **Double drop**<br>Discards two topmost items on the stack. |
| 2DUP  | no   | IC   | (n1 n2 -- n1 n2 n1 n2) | **Duplicate two**<br>Duplicates two topmost items on the stack. |
| 2OVER | no   | IC   | (n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2) | **Double over**<br>Copies the second pair of items on the stack to the top of the stack. |
| 2ROT  | no   | IC   | (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2) | **Double rotate**<br>Rotate the third pair on the stack to the top of the stack, moving down the first and the second pair. |
| 2SWAP | no   | IC   | (n1 n2 n3 n4 -- n3 n4 n1 n2) | **Double swap**<br>Swaps the first and the second pair on the stack. |
| AGAIN | yes  | C    |           | **Indefinite loop**<br>Marks the end of an idefinite loop opened ba the matchin BEGIN. |

---

### INT (IntegerLib)

Words: + - * / 1+ 1- 2+ 2- 2* 2/ MOD /MOD NOT AND OR XOR MAX MIN ABS FLOAT = <> < <= > >= 0= 0<> 0< 0> "

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

---

### FLOAT (FloatLib)

Words: F+ F- F* F/ FMAX FMIN FABS FIX F= F<> F< F<= F> F>= 

#### F+ (f1 f2 - f3)

Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack.

#### F- (f1 f2 - f3)

Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack.

#### F* (f1 f2 - f3)

Multiplies two floating point numbers on the top of the stack and leaves the product on the stack.

#### F/ (f1 f2 - f3)

Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack.

#### F< (f1 f2 - flag)

Returns -1 if f1 < f2, 0 otherwise.

#### F<= (f1 f2 - flag)

Returns -1 if f1 <= f2, 0 otherwise.

#### F> (f1 f2 - flag)

Returns -1 if f1 > f2, 0 otherwise.

#### F>= (f1 f2 - flag)

Returns -1 if f1 >= f2, 0 otherwise.

#### F<> (f1 f2 - flag)

Returns -1 if f1 is not equal to f2, 0 otherwise.

#### F= (f1 f2 - flag)

Returns -1 if f1 is equal to f2, 0 otherwise.

---

### STR (StringLib)

Words: S+ S"

| Name  | Imm. | Mode | Stack op. | Description |
| ---   | ---  | ---  | ---       | --- |
| S+    | no   | IC   | {s1 s2 -- s3} | **String concatenate**<br>The string s1 is concatenated with the string s2 and the resulting s1 + s2 string is stored at the top of the object stack. |
| S"    | no   | C    | { -- s}   | **String literal**<br>Consume all source characters till the closing " character, creating a string from them and storing the result on the top of the object stack. |

---

### OBJ (ObjectLib)

Words: ODUP ODROP OSWAP OOVER OROT O-ROT ODEPTH OCLEAR

---

### IO (IoLib)

Words: .( ." . F. S. CR EMIT SPACES SPACE WORDS

#### . (n - )

Prints the integer number on the top of the stack.

#### .( str

Immediatelly prints the string that follows in the input stream.

#### ." str

Prints the string that follows in the input stream. Available in compilation only.

#### EMIT

Prints out a character represented by a number on the top of the stack.


## TODO

- String words: STRCPY, STRINT, STRLEN, STRREAL, SUBSTR, STRFORM, STRCAT, STRCHAR, STRCMP, STRCMPI, COMPARE, (STRLIT), TYPE
- Words: SYSTEM, STATE, PICK, ROLL, MARKER name, CHAR, [, ], INCLUDE, .S, ABORT, ABORT" str, ARRAY x, EXIT, IMMEDIATE, LITERAL, QUIT, TRACE,
    VARIABLE name, (XDO), (X?DO), (XLOOP), (+XLOOP), CONSTANT, !, @, WORDSD, ', EXECUTE, INT, FLOAT, STRING
- Math words: ACOS, ASIN, ATAN, ATAN2, COS, EXP, FABS, (FLIT) n, NEGATE, FNEGATE, (LIT), LOG, POW, SHIFT, SIN, SQRT, TAN, 
 