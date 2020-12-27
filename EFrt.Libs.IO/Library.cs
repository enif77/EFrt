/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.IO
{
    using System;
    using System.Globalization;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;
   

    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "IO";

        private IInterpreter _interpreter;
       

        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?", PrintIndirectAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D.", PrintLongAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F.", PrintFloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S.", PrintStringAction));
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

        // (d --)
        private int PrintLongAction()
        {
            _interpreter.Output.Write("{0} ", new LongVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // (f --)
        private int PrintFloatAction()
        {
            _interpreter.Output.Write("{0} ", new DoubleVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // {o --}
        private int PrintStringAction()
        {
            _interpreter.Output.Write(_interpreter.OPop().ToString());

            return 1;
        }


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


        private int WordsAction()
        {
            _interpreter.Output.Write("{0} ", _interpreter.State.WordsList.ToString());

            return 1;
        }
    }
}
