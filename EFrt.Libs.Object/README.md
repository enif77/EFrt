# OBJECT

Object stack and heap manipulating words.

## Words

Words definition table columns:

- Name: A name of a word with optional parameters.
- Imm.: Immediate - if a word is executed even if we are in the compilation mode.
- Mode: I = interpretation mode only (not available during compilation), C = compilation mode only
  (not available during implementation), IC = vailable in both modes.
- Description: A word name, followed by the stack diagram - () = data stack, [] = return stack, {} = object stack - and description of the word itself.

| Name  | Imm. | Mode | Description |
| ---   | ---  | ---  | --- |
| O!     | no   | IC   | **Store into address**<br>(addr -- ) {o -- }<br>Stores the objct o into the address addr (a object variables stack index). |
| O@     | no   | IC   | **Fetch**<br>(addr -- ) { -- o}<br>Loads the object at addr (a object variables stack index) and leaves it at the top of the object stack. |
| OALLOT | no   | IC   | **Allocate object heap**<br>(n -- addr)<br>Allocates n cells of object heap space. |
| OCLEAR | no   | IC   | **Clear object point stack**<br>All items on the object stack are discarded. |
| ODEPTH | no   | IC   | **Object stack depth**<br>( -- n)<br>Returns the number of items on the object stack. |
| ODROP  | no   | IC   | **Discard top of the object stack**<br>{o -- }<br>Discards the value at the top of the object stack. |
| ODUP   | no   | IC   | **Duplicate object**<br>{o -- o o}<br>Duplicates the value at the top of the object stack. |
| OHERE  | no   | IC   | **Object heap address**<br>( -- addr)<br>The current object heap allocation address is placed on the top of the stack. addr + 1 is the first free object heap cell. |
| OOVER  | no   | IC   | **Duplicate the second object stack item**<br>{o1 o2 -- o1 o2 o1}<br>The second item on the object stack is copied to the top. |
| OPICK  | no   | IC   | **Pick item from the object stack**<br>(n -- ) { -- o}<br>The index is removed from the stack and then the indexth object stack item is copied to the top of the object stack. The top of object stack has index 0, the second item of the object stack index 1, and so on. |
| OROLL  | no   | IC   | **Rotate indexth item to top**<br>(n -- )<br>The index is removed from the stack and then the object stack item selected by the index, with 0 designating the top of the object stack, 1 the second item, and so on, is moved to the top of the objects stack. The intervening objects stack items are moved down one item. |
| OROT   | no   | IC   | **Rotate 3 object stack items**<br>{o1 o2 o3 -- o2 o3 o1}<br>The third item on the object stack is placed on the top of the object stack and the second and first items are moved down. |
| -OROT  | no   | IC   | **Reverse object stack rotate**<br>{o1 o2 o3 -- o2 o3 o1}<br>Moves the top of the object stack to the third item, moving the third and second items up. |
| OSWAP  | no   | IC   | **Swap top two object stack items**<br>{o1 o2 -- o2 o1}<br>The top two object stack items are interchanged. |
| OVARIABLE x | no   | I    | **Declare object variable**<br>An object type variable named x is declared and its value is set to null. When x is executed, its address will be placed on the stack. An object reference is reserved on the object variables stack for the variable's value. |
