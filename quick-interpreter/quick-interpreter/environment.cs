﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Environment.cs: part of Quick by Matt Poirier and Trevor Russell

namespace quick_interpreter
{
    public class Environment
    {
        // Dictionaries holding tokens and their corresponding questions
        public readonly Dictionary<string, List<Question>> banks = new();
        public readonly Dictionary<string, List<Question>> tests = new();

        // GetBank: returns list of questions in a bank from its name
        public List<Question>? GetBank(Token name)
        {
            if (banks.ContainsKey(name.lexeme)) // checks if bank exists
            {
                return banks[name.lexeme];
            }
           //Console.WriteLine("Undefined bank: " + name.lexeme);
            return null;
        }

        // GetTest: returns list of questions in a test from its name
        public List<Question>? GetTest(Token name)
        {
            if (tests.ContainsKey(name.lexeme)) // checks if test exists
            {
                return tests[name.lexeme];
            }
            //Console.WriteLine("Undefined test: " + name.lexeme);
            return null;
        }

        // DefineBank: creates new bank with name and questions
        public void DefineBank(Token name, List<Question> questions)
        {
            banks.Add(name.lexeme, questions);

            /*debugging: output contents of question bank
            Console.WriteLine("Bank " + name.lexeme + ":");
            foreach (Question question in banks[name.lexeme])
            {
                Console.WriteLine(question.problem.lexeme);
            }
            */
        }

        // AddToBank: adds question to bank of given name
        public void AddToBank(Token name, List<Question> questions)
        {
            if (!(banks.ContainsKey(name.lexeme))) // checks if bank exists
            {
                Console.WriteLine("Bank " + name.lexeme + " does not exist.");
                return;
            }
            banks[name.lexeme].AddRange(questions); // add question

            /* debugging: output updated bank
            Console.WriteLine("Bank " + name.lexeme + ":");
            foreach (Question question in banks[name.lexeme])
            {
                Console.WriteLine(question.problem.lexeme);
            }
            */
        }

        // DefineTest: adds questions from banks to test, then adds test to the test dictionary
        public void DefineTest(Token name, List<Token> bankList)
        {
            List<Question> questions = new();
            foreach (Token bank in bankList) // for each bank provided, add questions
            {
                if (banks.ContainsKey(bank.lexeme)){
                    questions.AddRange(banks[bank.lexeme]);
                }
                else
                {
                    Console.WriteLine("Could not add bank " + bank.lexeme + "to this test.");
                }
            }
            tests.Add(name.lexeme, questions);
        }

        // DeleteQuestion: delete question at index from bank called name
        public void DeleteQuestion(Token name, int index)
        {
            List<Question>? questions = GetBank(name);

            // check if bank exists
            if (questions == null)
            {
                Console.WriteLine("Bank " + name.lexeme + " does not exist.");
                return;
            }
            if(index <= questions.Count) // checks if index is valid
            {
                banks[name.lexeme].RemoveAt(index-1);
            }
            else
            {
                Console.WriteLine("question not found in bank " + name.lexeme);
            }

        }

        // updateAnswer: update the answer for a question at position index in bank called name
        public void updateAnswer(Token name, int index, Token answer)
        {
            index = index - 1; // start numbering at 1
            List<Question>? questions = GetBank(name);

            // check if bank exists
            if (questions == null)
            {
                Console.WriteLine("Bank " + name.lexeme + " does not exist.");
                return;
            }
            else
            {
                if (index >= 0 && index < banks[name.lexeme].Count) // checks if index is valid
                {
                    banks[name.lexeme][index].solution = answer;
                }
                else
                {
                    Console.WriteLine("Test bank " + name + "has no question at index " + index);
                }
            }
        }
    }
}
