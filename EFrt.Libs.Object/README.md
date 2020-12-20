# OBJECT

Object stack and heap manipulating words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Stack op.: What happens on the stack - () = data stack, [] = return stack, {} = object stack.
- Description: A description of the word.

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
