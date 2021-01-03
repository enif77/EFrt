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

( Indexed array )
( Source: http://www.forth.org/svfig/Len/definwds.htm )
: indexed-array (n -- ) (i -- a)
     CREATE CELLS ALLOT
     DOES> SWAP CELLS + ;

20 indexed-array foo  \ Make a 1-dimensional array with 20 cells
 3 foo                \ Put addr of fourth element on the stack

( 80 cells long buffer/array )
CREATE buffer 80 CELLS ALLOT

( ' and EXECUTE )
: goodbye ." Goodbye" CR ;
: hello ." Hello" CR ;
VARIABLE a
: greet a @ execute ;   \ Expects an execution token (xt) of a word.
' hello a !             \ Set the variable a to the xt of the word hello.
greet                   \ greet will say "Hello".
' goodbye a !           \ Set the variable a to the xt of the word goodbye.
greet                   \ greet will say "Goodbye".

( ' ['] and EXECUTE )
( Source: https://www.forth.com/starting-forth/9-forth-execution/ ) 
( 1 ) : hello    ." Hello " ;
( 2 ) : goodbye  ." Goodbye " ;
( 3 ) VARIABLE 'aloha  ' hello 'aloha !
( 4 ) : aloha    'aloha @ EXECUTE ;

aloha                   \ Prints out "Hello".
' goodbye 'aloha !      \ Sets the 'aloha variable to xt of the goodbye word.
aloha                   \ Prints out "Goodbye".

: say  ' 'aloha ! ;

say hello
aloha                   \ Prints out "Hello".
say goodbye
aloha                   \ Prints out "Goodbye".

: coming   ['] hello   'aloha ! ;
: going    ['] goodbye 'aloha ! ;

coming                  \ Sets 'aloha to the xt of the word hello.
aloha                   \ Prints out "Hello".
going                   \ Sets 'aloha to the xt of the word goodbye.
aloha                   \ Prints out "Goodbye".

( Forvard declaration )
( Source: And so Forth..., J.L. Bezemer )
-1 VALUE (step2)            \ A place to store a reference (execution token) to the word step2.
: step2 (step2) EXECUTE ;   \ The step2 word. Without a body for now.
: step1 1+ DUP . CR step2 ; \ The step1 word, calling the step2 word.
:noname 1+ DUP . CR step1 ; \ The body of the step2 word. :NONAME leaves an execution token of the created word on the stack.
TO (step2)                  \ Sets the body of the word step2 (using the value of the VALUE (step2)).
1 step1                     \ Executes the step1 word, that in turn calls the step2 word, which calls the step1 word...

```

