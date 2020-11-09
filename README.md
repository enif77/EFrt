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

### IntegerLib

Words: + - 1+ 1- 2+ 2- 2* 2/ * / MOD /MOD NOT AND OR XOR MAX MIN ABS FLOAT = <> < > 0= 0<> 0< 0> "

### FloatLib

Words: F+ F- F* F/ FMAX FMIN FABS FIX F= F<> F< F> 

### StringLib

Words: S+

### IOLib

Words: .( . F. S. CR SPACES WORDS

