/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;
    

    public class InputSource : IInputSource
    {
        public char CurrentChar => _sourceReader.CurrentChar;
        public int SourcePos => _sourceReader.SourcePos;
        
        
        public InputSource(ISourceReader sourceReader)
        {
            _sourceReader = sourceReader ?? throw new ArgumentNullException(nameof(sourceReader));
            _tokenizer = new Tokenizer(_sourceReader);
        }
        
        
        public char NextChar() => _sourceReader.NextChar();

        public Token NextTok() => _tokenizer.NextTok();
        
        
        public string ParseWord(bool toUpperCase = true)
        {
            // Get the name of the new word.
            var tok = _tokenizer.NextTok();
            switch (tok.Code)
            {
                case Token.TokenType.Eof:
                    throw new Exception("A name of a word expected.");

                case Token.TokenType.Word:
                    return toUpperCase ? tok.SValue.ToUpperInvariant() : tok.SValue;

                default:
                    throw new Exception($"Unexpected token type ({tok}) instead of a word name.");
            }
        }


        public string ParseTerminatedString(char terminator, bool allowSpecialChars = false, bool skipLeadingTerminators = false)
        {
            return _tokenizer.ParseTerminatedString(terminator, allowSpecialChars, skipLeadingTerminators);
        }


        public Token ParseNumber(string word, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false) => 
            _tokenizer.ParseNumber(word, allowLeadingWhite, allowTrailingWhite, allowTrailingChars);
        

        public long ParseIntegerNumber(string s, out bool success)
        {
            success = true;

            // An unknown word can be a number.
            var t = _tokenizer.ParseNumber(s, true, true, true);
            switch (t.Code)
            {
                case Token.TokenType.SingleCellInteger: return t.IValue;
                case Token.TokenType.DoubleCellInteger: return t.LValue;
                case Token.TokenType.Float: return (long)t.FValue;

                // No, it is something else.
                default:
                    success = false;
                    return 0;
            }
        }


        public double ParseFloatingPointNumber(string s, out bool success)
        {
            success = true;

            // An unknown word can be a number.
            var t = _tokenizer.ParseNumber(s, true, true, true);
            switch (t.Code)
            {
                case Token.TokenType.SingleCellInteger: return t.IValue;
                case Token.TokenType.DoubleCellInteger: return t.LValue;
                case Token.TokenType.Float: return t.FValue;

                // No, it is something else.
                default:
                    success = false;
                    return 0.0;
            }
        }
        
        
        private readonly ISourceReader _sourceReader;
        private readonly Tokenizer _tokenizer;
    }
}