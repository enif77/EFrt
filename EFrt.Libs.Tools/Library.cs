/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Tools
{
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Words;
   

    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "TOOLS";

        private IInterpreter _interpreter;
       

        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?", PrintIndirectAction));
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
                switch (stackItems[i].ValueType)
                {
                    case StackCellValueType.FloatingPoint:
                        _interpreter.Output.Write(stackItems[i].FloatingPointValue.ToString(CultureInfo.InvariantCulture));
                        break;

                    case StackCellValueType.Object:
                        _interpreter.Output.Write(stackItems[i].StringValue);
                        break;

                    default:
                        _interpreter.Output.Write(stackItems[i].IntegerValue.ToString(CultureInfo.InvariantCulture));
                        break;
                }

                _interpreter.Output.Write(" ");
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
