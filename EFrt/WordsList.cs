/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System;
    using System.Collections.Generic;

    using EFrt.Words;


    public class WordsList
    {
        public WordsList()
        {
            _wordsDic = new Dictionary<string, IWord>();
        }


        public bool IsWordDefined(string wordName)
        {
            return _wordsDic.ContainsKey(wordName);
        }


        public IWord GetWord(string wordName)
        {
            return _wordsDic[wordName];
        }


        public void RegisterWord(IWord word)
        {
            if (IsWordDefined(word.Name)) throw new Exception($"The '{word.Name}' word is already defined.");

            _wordsDic.Add(word.Name, word);
        }


        public void RemoveWord(string wordName)
        {
            _wordsDic.Remove(wordName);
        }


        public void Clear()
        {
            _wordsDic.Clear();
        }


        private Dictionary<string, IWord> _wordsDic;
    }
}
