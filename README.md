﻿# EFrt

EFrt is a embeddable FORTH language implementation.

## Packages

| NuGet | Version | Downloads |
|-------|---------|-----------|
| [EFrt](https://www.nuget.org/packages/EFrt) | ![EFrt](https://img.shields.io/nuget/v/EFrt.svg) | ![EFrt](https://img.shields.io/nuget/dt/EFrt.svg) |
| [EFrt.Core](https://www.nuget.org/packages/EFrt.Core) | ![EFrt.Core](https://img.shields.io/nuget/v/EFrt.Core.svg) | ![EFrt.Core](https://img.shields.io/nuget/dt/EFrt.Core.svg) |
| [EFrt.Libs.Core](https://www.nuget.org/packages/EFrt.Libs.Core) | ![EFrt.Libs.Core](https://img.shields.io/nuget/v/EFrt.Libs.Core.svg) | ![EFrt.Libs.Core](https://img.shields.io/nuget/dt/EFrt.Libs.Core.svg) |
| [EFrt.Libs.CoreExt](https://www.nuget.org/packages/EFrt.Libs.CoreExt) | ![EFrt.Libs.CoreExt](https://img.shields.io/nuget/v/EFrt.Libs.CoreExt.svg) | ![EFrt.Libs.CoreExt](https://img.shields.io/nuget/dt/EFrt.Libs.CoreExt.svg) |
| [EFrt.Libs.Double](https://www.nuget.org/packages/EFrt.Libs.Double) | ![EFrt.Libs.Double](https://img.shields.io/nuget/v/EFrt.Libs.Double.svg) | ![EFrt.Libs.Double](https://img.shields.io/nuget/dt/EFrt.Libs.Double.svg) |
| [EFrt.Libs.DoubleExt](https://www.nuget.org/packages/EFrt.Libs.DoubleExt) | ![EFrt.Libs.DoubleExt](https://img.shields.io/nuget/v/EFrt.Libs.DoubleExt.svg) | ![EFrt.Libs.DoubleExt](https://img.shields.io/nuget/dt/EFrt.Libs.DoubleExt.svg) |
| [EFrt.Libs.Exception](https://www.nuget.org/packages/EFrt.Libs.Exception) | ![EFrt.Libs.Exception](https://img.shields.io/nuget/v/EFrt.Libs.Exception.svg) | ![EFrt.Libs.Exception](https://img.shields.io/nuget/dt/EFrt.Libs.Exception.svg) |
| [EFrt.Libs.Floating](https://www.nuget.org/packages/EFrt.Libs.Floating) | ![EFrt.Libs.Floating](https://img.shields.io/nuget/v/EFrt.Libs.Floating.svg) | ![EFrt.Libs.Floating](https://img.shields.io/nuget/dt/EFrt.Libs.Floating.svg) |
| [EFrt.Libs.FloatingExt](https://www.nuget.org/packages/EFrt.Libs.FloatingExt) | ![EFrt.Libs.FloatingExt](https://img.shields.io/nuget/v/EFrt.Libs.FloatingExt.svg) | ![EFrt.Libs.FloatingExt](https://img.shields.io/nuget/dt/EFrt.Libs.FloatingExt.svg) |
| [EFrt.Libs.Object](https://www.nuget.org/packages/EFrt.Libs.Object) | ![EFrt.Libs.Object](https://img.shields.io/nuget/v/EFrt.Libs.Object.svg) | ![EFrt.Libs.Object](https://img.shields.io/nuget/dt/EFrt.Libs.Object.svg) |
| [EFrt.Libs.String](https://www.nuget.org/packages/EFrt.Libs.String) | ![EFrt.Libs.String](https://img.shields.io/nuget/v/EFrt.Libs.String.svg) | ![EFrt.Libs.String](https://img.shields.io/nuget/dt/EFrt.Libs.String.svg) |
| [EFrt.Libs.Tools](https://www.nuget.org/packages/EFrt.Libs.Tools) | ![EFrt.Libs.Tools](https://img.shields.io/nuget/v/EFrt.Libs.Tools.svg) | ![EFrt.Libs.Tools](https://img.shields.io/nuget/dt/EFrt.Libs.Tools.svg) |
| [EFrt.Libs.ToolsExt](https://www.nuget.org/packages/EFrt.Libs.ToolsExt) | ![EFrt.Libs.ToolsExt](https://img.shields.io/nuget/v/EFrt.Libs.ToolsExt.svg) | ![EFrt.Libs.ToolsExt](https://img.shields.io/nuget/dt/EFrt.Libs.ToolsExt.svg) |

## Links

### Forth related links

- https://www.root.cz/serialy/programovaci-jazyk-forth/
- https://www.forth.com/starting-forth/
- https://en.wikipedia.org/wiki/Forth_(programming_language)
- https://www.fourmilab.ch/atlast/atlast.html
- http://users.ece.cmu.edu/~koopman/stack_computers/
- https://www.root.cz/serialy/programovaci-jazyk-forth/
- https://www.forth.com/starting-forth/
- https://en.wikipedia.org/wiki/Forth_(programming_language)
- https://www.fourmilab.ch/atlast/atlast.html
- http://users.ece.cmu.edu/~koopman/stack_computers/
  
### General links

- https://csharppedia.com/en/tutorial/5626/how-to-use-csharp-structs-to-create-a-union-type-similar-to-c-unions-

#### CREATE or BUILDS?

- http://amforth.sourceforge.net/TG/recipes/Builds.html
- http://www.forth.org/svfig/Len/definwds.htm

## Data types

  - cell: A 32 bit data unit (int).
  - single cell integer: 32 bit signed integer number (int, 1 cell). Ex.: 123
  - double cell integer: 64 bit signed integer number (long, 2 cells). Ex.: 123L
  - char: 16 bit integer (char).
  - byte: 8 bit integer (byte).   
  - floating point: 64 bit float number (double, 1 cell). Ex.: 123.0 123D
  - string: A double quote terminated strings, stored on the objects stack. Ex.: S" Hello!"
  - object: Any user data reference (object).


## Stacks

  - Data stack: Main stack for user data. Holds all 32bit and 64bit integers.
  - Floating point stack: Stack for 64 bit floats.
  - Return stack: Stack for interpreter internal use. Holds 32 bit signed integers.
  - Object stack: Can hold any object and strings.
  - Exception stack: Not accessible for users. Its used internally by THROW and CATCH words.
  - Input source stack: Stack for keeping the inputs. Used by the EVALUATE word.


## Heaps

  - Data heap: Main heap for user data. It is an array of bytes. Words working with it are ensuring the cell/char etc. alignment, when needed.
  - Object heap: Holds all objects and strings.


## Libraries

 * [CORE](EFrt.Libs.Core/README.md)
 * [CORE-EXT](EFrt.Libs.CoreExt/README.md)
 * [DOUBLE](EFrt.Libs.Double/README.md)
 * [DOUBLE-EXT](EFrt.Libs.DoubleExt/README.md)
 * [EXCEPTION](EFrt.Libs.Exception/README.md)
 * [FLOATING](EFrt.Libs.Floating/README.md)
 * [FLOATING-EXT](EFrt.Libs.FloatingExt/README.md)
 * [OBJECT](EFrt.Libs.Object/README.md)
 * [STRING](EFrt.Libs.String/README.md)
 * [TOOLS](EFrt.Libs.Tools/README.md)
 * [TOOLS-EXT](EFrt.Libs.ToolsExt/README.md)


## Numbers

Unknown words are parsed as numbers, following C/C# conventions. Double cell integer must have the `L` or the `l` suffix to be recognized
as a double cell integer. Any problem with parsing a number makes the parser to return a word, which is unknown, and it leads to an error.

```
unsigned-single-cell-integer :: digit-sequence .
unsigned-double-cell-integer :: digit-sequence ( 'L' | 'l' ) .
unsigned-floating-point-number :: digit-sequence ( 'D' | 'd' ) .
unsigned-number :: unsigned-single-cell-integer | unsigned-double-cell-integer | unsigned-floating-point-number .
unsigned-floating-point-number :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
scale-factor :: [ sign ] digit-sequence .
fractional-part :: digit-sequence .
sign :: '+' | '-' .
```

Note: Forth style numbers parsing using the BASE variable will be implemented later.


## Strings

Words like `S"` or `ABORT"` expect a series of characters following in the input stream terminated by the `"` (double quote) character.

### Special characters escaping

Special chars escaping is supported by the `S\"` word. It uses the Forth 2012 standard and is extended by the C# conventions.

```
\a BEL (alert, ASCII 7)
\b BS (backspace, ASCII 8)
\e ESC (escape, ASCII 27)
\f FF (form feed, ASCII 12)
\l LF (line feed, ASCII 10)
\m CR/LF pair (ASCII 13, 10)
\n newline (implementation dependent, e.g., CR/LF, CR, LF, LF/CR)
\q double-quote(ASCII 34)
\" double-quote (ASCII 34)
\r CR (carriage return, ASCII 13)
\t HT (horizontal tab, ASCII 9)
\v VT (vertical tab, ASCII 11)
\z NUL (no character, ASCII 0)
\0 NUL (no character, ASCII 0)
\\ backslash itself (ASCII 92)
\uxxxx UTF16 (A sequence of 4 hex characters.)
\x or \X UTF16 (A sequence of 1 to 4 hex characters.)
```

NOTE: The `\U` escape sequence will be added later.


## Debugging

Simple debugging is possible - word `TRACE` can turn listing of executed words on/off.

```

1 TRACE  \ Turns executed words listing on.
0 TRACE  \ Turns executed words listing on.

: a 123 . CR ;
1 TRACE
a

Trace: A
Trace: LITERAL
Trace: .
123 Trace: CR

Trace: ExitControlWord

```

Interpreter has two public events - `ExecutingWord` and `WordExecuted` - to notify a caller that a word is going to be executed and
that a word was executed. 


## Building (Ubuntu 20.04)

### .NET 6.0

https://dotnet.microsoft.com/download/dotnet/6.0

Download the latest .NET 6 version and install it to the `/home/user/Devel/bin/dotnet` directory.

```
mkdir -p $HOME/Devel/bin/dotnet && tar zxf dotnet-sdk-6.0.101-linux-x64.tar.gz -C $HOME/Devel/bin/dotnet
export DOTNET_ROOT=$HOME/Devel/bin/dotnet
export PATH=$PATH:$HOME/Devel/bin/dotnet
```

The `PATH` and the `DOTNET_ROOT` variables can be set permanently in the `.bashrc` file.

Run:

```
nano .bashrc
```

in your home directory and add to the end of the file:

```
export DOTNET_ROOT=$HOME/Devel/bin/dotnet
export PATH=$PATH:$HOME/Devel/bin/dotnet
```

### PowerShell

Install the latest powershell snap.

```
sudo snap install powershell --classic
```

### Mono

https://www.mono-project.com/download/stable/

Get the latest stable version.

### nuget.exe

https://www.nuget.org/downloads

Download the latest stable version and save it to the `/home/user/Devel/bin/nuget` directory.

### build.ps configuration

Set the `$toolsPath` and the `$useMonoToRunNuget` variables to match your build environment (Linux or Windows).

### Build

```
powershell -ExecutionPolicy Unrestricted ./build.ps1 -Configuration Release
```

### Nugets

Upload new `./nuget/*.nupkg` and `./nuget/*.snupkg` packages to nuget.org.


## Examples

```
( Hello world! )
: hello S" Hello, world!" S. CR ;

( A simplified hello-world! )
: greet ." Hello, I speak Forth!" CR ;

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
: doit 5 0 DO ." hello" 1 SPACES I . CR LOOP ;

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
VARIABLE arr            \ Variable for storring the "address" of the first cell of the new array.
HERE arr !              \ Getting the first cell address.
100 ALLOT               \ Allocation of the array.
HERE . CR               \ Will print out the index ("address") of the last cell of the new array.
123 arr !               \ Stores 123 to the first cell of the array arr.
456 arr 1 CELLS + !     \ Stores 456 to the second cell of the array arr.
arr @ . CR              \ Gets and prints out the contents of the first cell of the array arr (123).
arr ? CR                \ Shorter version of the previous example.
arr 1 CELLS + ? CR      \ Gets and prints out the contents of the second cell of the array arr (456).

( Storing a number on the heap and printing it out )
123 , HERE 1 CELLS - @ . CR

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

( Unloop )
: unloop-test 10 1 DO I DUP . CR 5 > IF ." Exiting..." CR UNLOOP EXIT THEN LOOP ." Never printer out..." ;

( Abort with a message )
: abort-with-message 10 1 DO I DUP . CR 5 > ABORT" Too big!" LOOP ." Never printer out..." ;

( Exceptions )
: th THROW ." Thrown" CR ;
: ca ." pre-t" CR ['] th CATCH ." post-t" CR ;
0 ca   \ No exception thrown.
-1 ca  \ Like ABORT.
-2 ca  \ Like ABORT" mesg".
1 ca   \ User exception.

( Is a word defined? )
: is-word-defined? ['] ' CATCH 0= IF ." Defined" ELSE ." Undefined" THEN  DROP ( Drop the product of the ' word.) ;
: w? is-word-defined? CR ;  \ Just a shortcut for the is-word-defined? word.
w? bla  \ Undefined
w? IF   \ Defined

( How the word . "dot" can be implemented... )
( n -- )                  \ Display n.
: . DUP ABS 0             \ Prepare.
   <# #S  ROT SIGN #>     \ Convert to string.
   TYPE SPACE ;           \ Output the created string.
   
( Prints a counted string )
( c-addr -- )
(c-addr --)
: printc    ( c-addr )
  DUP       ( c-addr c-addr )
  C@        ( c-addr count )
  1+        ( c-addr count )
  1         ( c-addr count start )
  DO        ( c-addr )
  DUP       ( c-addr c-addr )
  I         ( c-addr c-addr I )
  CHARS     ( c-addr c-addr bytes )
  +         ( c-addr c-addr+index )
  C@        ( c-addr char )
  EMIT      ( c-addr )
  LOOP ;
  
```

### Roman numerals for two bytes chars

Source: Thinking Forth, Leo Brodie

```
Create romans      ( ones) char I c, char V c,
                   ( tens) char X c, char L c,
               ( hundreds) char C c, char D c,
              ( thousands) char M c,

Variable column# ( current_offset)

: ones 0 column# ! ;
: tens 4 column# ! ;
: hundreds 8 column# ! ;
: thousands 12 column# ! ;

: column ( -- address-of-column ) romans column# @ + ;

: .symbol ( offset -- ) column + c@ emit ;

: oner 0 .symbol ;
: fiver 2 .symbol ;
: tener 4 .symbol ;

: oners ( #-of-oners -- )
    ?dup IF 0 DO oner LOOP THEN ;

: almost ( quotient-of-5/ -- )
    oner IF tener ELSE fiver THEN ;

: digit ( digit -- )
    5 /mod over 4 = IF almost drop ELSE IF fiver THEN
    oners THEN ;

: roman ( number -- ) 
    1000 /mod thousands digit
     100 /mod  hundreds digit
      10 /mod      tens digit
                   ones digit
  SPACE ;
```



## Acknowledgements

[JetBrains](https://www.jetbrains.com/?from=Efrt) kindly provides EFrt with a free open-source licence for their Resharper and Rider.
- **Resharper** makes Visual Studio a much better IDE
- **Rider** is fast & powerful cross platform .NET IDE

![image](https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/JetBrains_Logo_2016.svg/121px-JetBrains_Logo_2016.svg.png)
