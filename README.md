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

### BaseLib

Words: ( \ : ; CLEAR DUP 2DUP ?DUP DROP 2DROP SWAP 2SWAP OVER 2OVER ROT 2ROT -ROT DEPTH >R R> R@ FALSE
  TRUE IF ELSE THEN BEGIN WHILE REPEAT AGAIN LEAVE I J BYE DO ?DO LOOP +LOOP FORGET UNTIL

#### : w

Begins compilation of a word named w.

#### ;

Ends compilation of a word.

---

### IntegerLib

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

### FloatLib

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

### StringLib

Words: S+ S"

---

### ObjectLib

Words: ODUP ODROP OSWAP OOVER OROT O-ROT ODEPTH OCLEAR

---

### IOLib

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
 