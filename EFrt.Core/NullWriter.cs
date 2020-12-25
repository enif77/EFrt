/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// An output writer, that writes nowhere.
    /// </summary>
    public class NullWriter : IOutputWriter
    {
        public void Write(string format, params object[] arg)
        {
        }


        public void WriteLine()
        {
        }


        public void WriteLine(string format, params object[] arg)
        {
        }
    }
}
