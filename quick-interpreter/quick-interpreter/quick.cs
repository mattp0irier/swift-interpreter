﻿using System;
using System.IO;
using System.Collections.Generic;

// quick.cs: part of Quick by Matt Poirier and Trevor Russell

namespace quick_interpreter
{
    class quick
    {

        // Main: takes file or prompts for input
        static void Main(string[] args)
        {
            if (args.Length > 1) // invalid args
            {
                Console.WriteLine("Usage: quick [filename]");
                return;
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        // RunPrompt: prompt user for input until blank entered
        static void RunPrompt()
        {
            Interpreter interpreter = new Interpreter();
            string curLine = "initial value";
            while (true)
            {
                Console.Write("> ");
                curLine = Console.ReadLine();
                if (curLine.Length == 0) break; // break if no input
                Run(curLine, interpreter); // run input
            }
        }

        // RunFile: pass input file to run function
        static void RunFile(string filename)
        {
            string input = File.ReadAllText(filename);
            Run(input, null);
            
        }

        // Run: execute string input
        static void Run(string line, Interpreter? interpreter)
        {
            // Scan
            Scanner scanner = new(line);
            List<Token> tokens = scanner.scanTokens();

            // Parse
            Parser parser = new(tokens);
            List<Statement> stmts = parser.Parse();

            if (interpreter == null) interpreter = new Interpreter();


            // Interpret
            interpreter.interpret(stmts);
        }

        // Error: take line number and error message and report error
        public static void Error(int line, string msg)
        {
            Report(line, "", msg);
        }

        // Error: take token and error message and report error
        public static void Error(Token token, string msg)
        {
            if (token.type == TokenType.EOF)
                Report(token.line, " at end", msg);
            else
            {
                Report(token.line, "at '" + token.lexeme + "'", msg);
            }
        }


        // Report: write error and line number to error console
        public static void Report(int line, string where, string msg)
        {
            Console.Error.WriteLine("[line " + line + "] error" + where + ": " + msg);
            //hadError = true;
        }

    }

}