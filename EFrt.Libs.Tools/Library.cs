/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Tools
{
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Words;
   

    public class Library : IWordsLibrary
    {
        public string Name => "TOOLS";

        private readonly IInterpreter _interpreter;
       

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void Initialize()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?", PrintIndirectAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".F", PrintFloatingPointStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".O", PrintObjectStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".S", PrintStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "WORDS", WordsAction));
        }
               

        // (addr --)
        private int PrintIndirectAction()
        {
            _interpreter.Output.Write("{0} ", _interpreter.State.Heap.Items[_interpreter.Pop()]);

            return 1;
        }

        // ( -- )
        private int PrintStackAction()
        {
            _interpreter.Output.Write("Stack: ");
            var stackItems = _interpreter.State.Stack.Items;
            for (var i = 0; i <= _interpreter.State.Stack.Top; i++)
            {
                _interpreter.Output.Write(stackItems[i].ToString(CultureInfo.InvariantCulture));
                _interpreter.Output.Write(" ");
            }
            
            return 1;
        }

        // ( -- )
        private int PrintFloatingPointStackAction()
        {
            _interpreter.Output.Write("Floating point stack: ");
            var stackItems = _interpreter.State.FloatingPointStack.Items;
            for (var i = 0; i <= _interpreter.State.FloatingPointStack.Top; i++)
            {
                _interpreter.Output.Write(stackItems[i].ToString(CultureInfo.InvariantCulture));
                _interpreter.Output.Write(" ");
            }

            return 1;
        }

        // { -- }
        private int PrintObjectStackAction()
        {
            _interpreter.Output.WriteLine("Object stack: ");
            var stackItems = _interpreter.State.ObjectStack.Items;
            for (var i = _interpreter.State.ObjectStack.Top; i >= 0; i--)
            {
                _interpreter.Output.WriteLine($"  { stackItems[i] }");
            }

            return 1;
        }

        // ( -- )
        private int WordsAction()
        {
            _interpreter.Output.Write("{0} ", _interpreter.State.WordsList.ToString());

            return 1;
        }
    }
}
