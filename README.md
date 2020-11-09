# EFrt

EFrt is a embeddable FORTH language implementation.

## Data types

  - integer: 32  bit signed integer number,
  - float: 32 bit float number,
  - string: Single or double quote terminated strings. Stored on the object stack.
  - other types will be available later - uint, short, ushort, byte, string etc.

Data stack holds 32 bit structures, that are implemented as a union type. Any .NET type,, 
that fits into 32 bits will be supported. 64bit types (long, double) will be implemented as
double stack items in a special library.

## Stacks

  - Data stack: Main stack for user data. Holds all 32 bit data types.
  - Return stack: Stack for interpreter internal use. Holds 32 bit signed integers.
  - Object stack: Can hold any object. Available for user data, strings etc. Not used by the interpreter.
  
## Words

Here is a list of implemented words.

### BaseLib

Words: ( \ : ; DUP 2DUP ?DUP DROP 2DROP SWAP 2SWAP OVER 2OVER ROT 2ROT -ROT DEPTH >R R> R@ FALSE
  TRUE IF ELSE THEN BEGIN REPEAT BYE DO ?DO LOOP +LOOP FORGET

#### : w

Begins compilation of a word named w.

#### ;

Ends compilation of a word.


### IntegerLib

Words: + - * / 1+ 1- 2+ 2- 2* 2/ MOD /MOD NOT AND OR XOR MAX MIN ABS FLOAT = <> < > 0= 0<> 0< 0> "

#### + (n1 n2 - n3)

Adds n1 and n2 and leaves the sum on the stack.

#### - (n1 n2 - n3)

Substracts n2 from n1 and leaves the difference on the stack.

#### * (n1 n2 - n3)

Multiplies n1 and n2 and leaves the product on the stack.

#### / (n1 n2 - n3)

Divides n1 by n2 and leaves the quotient on the stack.


### FloatLib

Words: F+ F- F* F/ FMAX FMIN FABS FIX F= F<> F< F> 

#### F+ (f1 f2 - f3)

Adds two floating point numbers on the top of the stack and leaves the sum on the top of the stack.

#### F- (f1 f2 - f3)

Substracts the floating value f2 from the floating value f1 and leaves the difference on the top of the stack.

#### F* (f1 f2 - f3)

Multiplies two floating point numbers on the top of the stack and leaves the product on the stack.

#### F/ (f1 f2 - f3)

Divides the floating number f1 by the floating number f2 and leaves the quotient on the top of the stack stack.


### StringLib

Words: S+


### IOLib

Words: .( . F. S. CR SPACES WORDS

#### . (n - )

Prints the integer number on the top of the stack.

#### .( str

Immediatelly prints the string that follows in the input stream.