# EFrt

EFrt is a embeddable FORTH language implementation.

## Data types

  - cell: A 32 bit data unit (int).
  - single cell integer: 32 bit signed integer number (int, 1 cell). Ex.: 123
  - double cell integer: 64 bit signed integer number (long, 2 cells). Ex.: 123L
  - floating point: 64 bit float number (double, 2 cells). Ex.: 123.0
  - string: A double quote terminated strings, stored on the objects stack. Ex.: S" Hello!"
  - object: Any user data reference (object).


## Stacks

  - Data stack: Main stack for user data. Holds all 32bit and 64bit integers and 64 bit floats (as two 32 bit cells).
  - Return stack: Stack for interpreter internal use. Holds 32 bit signed integers.
  - Object stack: Can hold any object and strings.


## Heaps

  - Data heap: Main heap for user data. Holds all 32bit and 64bit integers and 64 bit floats (as two 32 bit cells).
  - Object heap: Hold any objects and strings.


## Libraries

 * [CORE](EFrt.Libs.Core/README.md)
 * [CORE-EXT](EFrt.Libs.CoreExt/README.md)
 * [DOUBLE](EFrt.Libs.Double/README.md)
 * [DOUBLE-EXT](EFrt.Libs.DoubleExt/README.md)
 * [FLOATING](EFrt.Libs.Floating/README.md)
 * [OBJECT](EFrt.Libs.Object/README.md)
 * [STRING](EFrt.Libs.String/README.md)
 * [TOOLS](EFrt.Libs.Tools/README.md)
 * [TOOLS-EXT](EFrt.Libs.ToolsExt/README.md)


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

( 100 cells long array )
VARIABLE arr    \ Variable for storring the "address" of the first cell of the new array.
HERE 1+ arr !   \ Getting the first cell address.
100 ALLOT       \ Allocation of the array.
HERE . CR       \ Will print out the index ("address") of the last cell of the new array.
123 arr @ !     \ Stores 123 to the first cell of the array arr.
456 arr @ 1+ !  \ Stores 456 to the second cell of the array arr.
arr @ @ . CR    \ Gets and prints out the contents of the first cell of the array arr (123).
arr @ ? CR      \ Shorter version of the previous example.
arr @ 1+ ? CR   \ Gets and prints out the contents of the second cell of the array arr (456).

( Storing a number on the heap and printing it out )
123 , HERE @ . CR

( Factorial of N - without RECURSE )
: factorial DUP 0= IF DROP 1 ELSE DUP 1- FACTORIAL * THEN ;

( Factorial of N - with RECURSE )
: factorial DUP 0= IF DROP 1 ELSE DUP 1- RECURSE * THEN ;

5 fatorial . CR  \ 120

```

