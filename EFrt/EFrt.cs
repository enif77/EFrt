/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System.Collections.Generic;
    using EFrt.Libs;


    /*

    https://www.root.cz/serialy/programovaci-jazyk-forth/
    https://www.forth.com/starting-forth/
    https://en.wikipedia.org/wiki/Forth_(programming_language)
    https://www.fourmilab.ch/atlast/atlast.html
    http://users.ece.cmu.edu/~koopman/stack_computers/

    https://csharppedia.com/en/tutorial/5626/how-to-use-csharp-structs-to-create-a-union-type-similar-to-c-unions-


    TODO

        - kompilace
        - řetězce jako v C/C#
        - double (separátní zásobník?)

    ?DO (a b --, -- r b)

    10 10 Do I . CR LOOP   \ Vypíše 10 - smyčka proběhne jednou.
    10 10 ?DO I . CR LOOP  \ Nevypíše nic - smyčka vůbec neproběhne.

    */



    public class EFrtExecutor
    {
        public EFrtExecutor(int stackCapacity = 32, int returnStackCapacity = 32)
        {
            _interpreter = new Interpreter(32, 32);
            _interpreter.DefineWords(new List<IWordsLIbrary>()
            {
                new BaseLib(_interpreter),
                new IoLib(_interpreter, new ConsoleWriter()),
                new IntegerLib(_interpreter),
                new FloatLib(_interpreter)
            });
        }


        private IInterpreter _interpreter;


        public void Reset()
        {
            _interpreter.Reset();
        }


        public void Execute(string src)
        {
            _interpreter.Execute(src);
        }


        public void TerminateExecution()
        {
            _interpreter.TerminateExecution();
        }
    }
}
