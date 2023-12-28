using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Compiler_Project
{

    public class Lexer
    {
        private readonly string _input;// int x = 5 ;
        private int _position;

        public Lexer(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _input = input;
            _position = 0;
        }

        private char Peek()
        {
            if (_position < _input.Length)
                return _input[_position];
            return '\0';
        }

        private char Next()
        {
            var currentChar = Peek();
            _position++;
            return currentChar;
        }

        private void SkipWhitespace()
        {
            while (char.IsWhiteSpace(Peek()))
                Next();
        }

        private bool Match(char expected)
        {
            if (Peek() == expected)
            {
                Next();
                return true;
            }
            return false;
        }

        private Token ReadInteger()
        {
            var value = "";
            while (char.IsDigit(Peek()))
                value += Next();
            return new Token(TokenType.Integer, value);
        }

        public Token GetNextToken()
        {
            while (_position < _input.Length)
            {
                var currentChar = Peek();

                if (char.IsDigit(currentChar))
                    return ReadInteger();

                switch (currentChar)
                {
                    case '+':
                        Next();
                        return new Token(TokenType.Plus, "+");
                    case '-':
                        Next();
                        return new Token(TokenType.Minus, "-");
                    case '*':
                        Next();
                        return new Token(TokenType.Multiply, "*");
                    case '/':
                        Next();
                        return new Token(TokenType.Divide, "/");
                    case '(':
                        Next();
                        return new Token(TokenType.LParen, "(");
                    case ')':
                        Next();
                        return new Token(TokenType.RParen, ")");
                    default:
                        throw new Exception($"Invalid character: {currentChar}");
                }
            }

            return new Token(TokenType.EndOfInput, "");
        }
    }


}
