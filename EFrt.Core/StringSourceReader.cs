/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// Reads from a string.
    /// </summary>
    public class StringSourceReader : ISourceReader
    {
        public char CurrentChar { get; private set; }

        public int SourcePos { get; private set; }


        public StringSourceReader(string src)
        {
            _src = src;
            SourcePos = -1;
        }


        public char NextChar()
        {
            SourcePos++;
            if (SourcePos >= _src.Length)
            {
                SourcePos = _src.Length;

                return CurrentChar = (char)0;
            }

            return CurrentChar = _src[SourcePos];
        }


        private string _src;
    }
}
