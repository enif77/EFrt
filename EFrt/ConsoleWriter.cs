/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System;

    using EFrt.Core;


    public class ConsoleWriter : IOutputWriter
    {
        public void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }


        public void WriteLine()
        {
            Console.WriteLine();
        }


        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}
