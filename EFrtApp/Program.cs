/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrtApp
{
    using System;

    using EFrt;
    using EFrt.Core;


    static class Program
    {
        static void Main(string[] args)
        {
            var interpreter = InterpreterFactory.CreateWithDefaults();

            interpreter.Output = new ConsoleWriter();
            interpreter
                .AddCoreLibrary()
                .AddCoreExtLibrary()
                .AddExceptionLibrary()
                .AddDoubleLibrary()
                .AddDoubleExtLibrary()
                .AddFloatingLibrary()
                .AddFloatingExtLibrary()
                .AddObjectLibrary()
                .AddStringLibrary()
                .AddToolsLibrary()
                .AddToolsExtLibrary();

            interpreter.AddPrimitiveWord("TRACE", () => 
            {
                interpreter.StackExpect(1);

                _tracing = interpreter.Pop() != 0;

                return 1;
            });

            interpreter.ExecutingWord += Interpreter_ExecutingWord;

            while (true)
            {
                try
                {
                    // Show a prompt.
                    Console.Write(interpreter.IsCompiling ? ":> " : "-> ");

                    // Interpret.
                    interpreter.Evaluate(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                }

                // TODO: Breaking state.

                if (interpreter.InterpreterState == InterpreterStateCode.Terminating)
                {
                    Console.WriteLine();
                    Console.WriteLine("Bye!");

                    break;
                }
            }

            //WordsListTest();


            //Console.WriteLine(Marshal.SizeOf(typeof(EfrtValue)));

            //var ds = new DataStack();

            //ds.Push(123);
            //Console.WriteLine(ds.Pop().Int);

            //ds.Push((short)11, 12);
            //var s = ds.Pop();
            //Console.WriteLine(s.Short + " " + s.Short2);
            //s.Short2 = 33;
            //Console.WriteLine(s.Short + " " + s.Short2);
        }
        
        private static bool _tracing = false;


        private static void Interpreter_ExecutingWord(object sender, InterpreterEventArgs e)
        {
            if (_tracing)
            {
                Console.WriteLine("Trace: {0} ", e.Word.Name);
            }
        }



        //        static void TestEfrt()
        //        {
        //            var interpreter = InterpreterFactory.CreateWithDefaults();

        //            interpreter.Output = new ConsoleWriter();
        //            interpreter
        //                .AddCoreLibrary()
        //                .AddCoreExtLibrary()
        //                .AddExceptionLibrary()
        //                .AddDoubleLibrary()
        //                .AddDoubleExtLibrary()
        //                .AddFloatingLibrary()
        //                .AddObjectLibrary()
        //                .AddStringLibrary()
        //                .AddToolsLibrary()
        //                .AddToolsExtLibrary();

        //            //efrt.Evaluate(_src);
        //            //efrt.Evaluate("CR .( ---)");
        //            //efrt.Evaluate("123 456 * CR .");
        //            //efrt.Evaluate("CR .( ---)");
        //            //efrt.Evaluate("10 1 DO I CR . LOOP");
        //            //efrt.Evaluate("CR .( ---)");
        //            //efrt.Evaluate("10 1 DO I CR . 5 1 DO .( -) I . LOOP .( *) LOOP");

        //            interpreter.Evaluate(": what IF 123 CR . ELSE 456 CR . THEN ; 0 what 1 what");
        //            interpreter.Evaluate("1 FLOAT CR F.");
        //            interpreter.Evaluate("2 FLOAT 3 FLOAT F/ CR F.");

        //            //efrt.Evaluate(": rep BEGIN 3 CR . 1 BEGIN 1+ DUP CR . REPEAT  REPEAT ; rep");

        //            interpreter.Evaluate(": rep2 DO 3 CR . LOOP ; 10 1 rep2");
        //            interpreter.Evaluate(": rep3 DO DUP CR . 1+ LOOP ; CR 1 11 1 rep3");
        //            interpreter.Evaluate("CR 10 11 1 rep3");

        //            interpreter.Evaluate("CR .( ---) CR");

        //            interpreter.Evaluate("WORDS CR CR FORGET rep2 WORDS CR");

        //            interpreter.Evaluate("CR .( ---) CR");

        //            interpreter.Evaluate(": rep4 ?DO DUP CR . 1+ LOOP ; CR 1 11 11 rep4");

        //            interpreter.Evaluate("CR .( ---) CR");

        //            interpreter.Evaluate("\"Hello, world!\" S. ");
        //            interpreter.Evaluate(": hello \"Hello, world!\" ; 1 SPACES hello S.");

        //            interpreter.Evaluate("CR .( ---) CR");

        //            interpreter.Evaluate("\"abcd\" \"efgh\" S+ S.");

        //            interpreter.Evaluate("CR .( ---) CR");
        //        }


        //        static void WordsListTest()
        //        {
        //            var wl = new WordsList();

        //            var i = new Interpreter(new InterpreterState(
        //                new Stack(32),
        //                new ObjectStack(32),
        //                new ReturnStack(32),
        //                new ExceptionStack(32),
        //                new Heap(1024),
        //                new ObjectHeap(1024),
        //                new WordsList()));

        //            wl.AddWord(new PrimitiveWord(i, "w1", () => 1));
        //            wl.AddWord(new PrimitiveWord(i, "w1", () => 1));
        //            wl.AddWord(new PrimitiveWord(i, "w2", () => 1));
        //            wl.AddWord(new PrimitiveWord(i, "w3", () => 1));
        //            wl.AddWord(new PrimitiveWord(i, "w1", () => 1));

        //            Console.WriteLine(WordsListToString(wl.DefinedWords));
        //            Console.WriteLine(WordsListToString(wl.WordsHistory));
        //            Console.WriteLine("---");

        //            wl.RemoveWord("w1");

        //            Console.WriteLine(WordsListToString(wl.DefinedWords));
        //            Console.WriteLine(WordsListToString(wl.WordsHistory));
        //            Console.WriteLine("---");

        //            wl.RemoveWord("w1");

        //            Console.WriteLine(WordsListToString(wl.DefinedWords));
        //            Console.WriteLine(WordsListToString(wl.WordsHistory));
        //            Console.WriteLine("---");

        //            wl.Forget("w2");

        //            Console.WriteLine(WordsListToString(wl.DefinedWords));
        //            Console.WriteLine(WordsListToString(wl.WordsHistory));
        //        }


        //        static string WordsListToString(IEnumerable<IWord> wordsList)
        //        {
        //            var nextWord = false;
        //            var sb = new StringBuilder();
        //            foreach (var w in wordsList)
        //            {
        //                if (nextWord)
        //                {
        //                    sb.Append(" ");
        //                }
        //                else
        //                {
        //                    nextWord = true;
        //                }

        //                sb.Append(w.Name);
        //            }

        //            return sb.ToString();
        //        }


        //        static string _src = @"
        //: INVERT 0= ;                   \ Negates a comparison result.
        //: TRUE -1 ;                     \ Defines the TRUE constant.
        //: FALSE 0 ;                     \ Defines the FALSE constant.

        //: add50 ( n -- n )  50 + ;      \ Adds 50 to the current stack-top value.
        //: hello ( -- ) .( Hello!) CR ;  \ Prints out 'Hello!'.
        //: deffn : fn CR .( cosi) ; ;    \ A word defining an other word.
        //: deffn2                        \ A word defining two words.
        //    : fn1 CR .( cosi1) ;
        //    : fn2 CR .( cosi2) ;
        //;

        //: smycka 10 1 DO I CR . LOOP ;  \ Prints out 1 .. 10.

        //25 ( comment) 10 * add50 CR . CR hello

        //-100 CR .

        //1 0= CR .
        //1 0= INVERT CR .
        //0 INVERT CR .
        //FALSE INVERT CR .

        //deffn fn
        //deffn2 fn1 fn2

        //0 IF CR .( cond1) THEN                     \ Prints out nothing.
        //1 IF CR .( cond2) THEN                     \ Prints out 'cond2'.
        //0 IF 0 IF CR .( cond3) THEN CR 123 . THEN  \ Prints out nothing.
        //1 IF 0 IF CR .( cond4) THEN CR 456 . THEN  \ Prints out '456'.
        //1 IF 1 IF CR .( cond5) THEN CR 789 . THEN  \ Prints out 'cond5 789'.

        //0 IF CR .( cond6a) ELSE CR .( cond6b) THEN
        //1 IF CR .( cond7a) ELSE CR .( cond7b) THEN

        //\ Prints out 'cond8a cond8a2'.
        //1 IF 
        //    CR .( cond8a) 
        //    0 IF CR .( cond8a1) ELSE CR .( cond8a2) THEN
        //    ELSE 
        //    CR .( cond8b) 
        //    1 IF CR .( cond8b1) ELSE CR .( cond8b2) THEN
        //    THEN

        //\ Prints out 'cond9a cond9a1'.
        //1 IF 
        //    CR .( cond9a) 
        //    1 IF CR .( cond9a1) ELSE CR .( cond9a2) THEN
        //    ELSE 
        //    CR .( cond9b) 
        //    1 IF CR .( cond9b1) ELSE CR .( cond9b2) THEN
        //    THEN

        //\ Prints out 'cond10b cond10b1'.
        //0 IF 
        //    CR .( cond10a) 
        //    0 IF CR .( cond10a1) ELSE CR .( cond10a2) THEN
        //    ELSE 
        //    CR .( cond10b) 
        //    1 IF CR .( cond10b1) ELSE CR .( cond10b2) THEN
        //    THEN

        //\ Prints out 'cond11b cond11b2'.
        //0 IF 
        //    CR .( cond11a) 
        //    0 IF CR .( cond11a1) ELSE CR .( cond11a2) THEN
        //    ELSE 
        //    CR .( cond11b) 
        //    0 IF CR .( cond11b1) ELSE CR .( cond11b2) THEN
        //    THEN

        //CR .( --- begin smycka --) smycka CR .( --- end smmycka ---)

        //CR .( --- begin greet --)

        //: defcond IF : greet CR .( hello) ; ELSE : greet CR .( hi) ; THEN ;

        //1 defcond greet
        //0 defcond greet

        //CR .( --- end greet --)

        //";
    }
}
