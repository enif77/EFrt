/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.String
{
    using System;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Words;


    public class Library : IWordsLibrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "STRING";

        private readonly IInterpreter _interpreter;


        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S.", PrintStringAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S+", AddAction));
        }


        // {o --}
        private int PrintStringAction()
        {
            _interpreter.ObjectStackExpect(1);
            
            _interpreter.Output.Write(_interpreter.OPop().ToString());

            return 1;
        }

        // {a b -- result}
        private int AddAction()
        {
            _interpreter.ObjectStackExpect(2);
            
            _interpreter.SFunction((a, b) => a.ToString() + b.ToString());

            return 1;
        }
    }
}
