using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler_Project
{
    public class Parser
    {
        private readonly Token[] _tokens;
        private int _currentTokenIndex;

        public Parser(Token[] tokens)
        {
            _tokens = tokens;
            _currentTokenIndex = 0;
        }

        private Token CurrentToken => _tokens[_currentTokenIndex];

        private void Match(TokenType expectedType)
        {
            if (CurrentToken.Type != expectedType)
                throw new Exception($"Expected token type {expectedType}, but found {CurrentToken.Type}");
            _currentTokenIndex++;
        }

        public int ParseExpression()
        {
            return ParseAddition();
        }

        private int ParseAddition()
        {
            var result = ParseMultiplication();

            while (CurrentToken.Type == TokenType.Plus || CurrentToken.Type == TokenType.Minus)
            {
                if (CurrentToken.Type == TokenType.Plus)
                {
                    Match(TokenType.Plus);
                    result += ParseMultiplication();
                }
                else if (CurrentToken.Type == TokenType.Minus)
                {
                    Match(TokenType.Minus);
                    result -= ParseMultiplication();
                }
            }

            return result;
        }

        private int ParseMultiplication()
        {
            var result = ParsePrimary();

            while (CurrentToken.Type == TokenType.Multiply || CurrentToken.Type == TokenType.Divide)
            {
                if (CurrentToken.Type == TokenType.Multiply)
                {
                    Match(TokenType.Multiply);
                    result *= ParsePrimary();
                }
                else if (CurrentToken.Type == TokenType.Divide)
                {
                    Match(TokenType.Divide);
                    var divisor = ParsePrimary();
                    if (divisor == 0)
                        throw new DivideByZeroException("Division by zero");
                    result /= divisor;
                }
            }

            return result;
        }

        private int ParsePrimary()
        {
            if (CurrentToken.Type == TokenType.Integer)
            {
                var value = int.Parse(CurrentToken.Value);
                Match(TokenType.Integer);
                return value;
            }
            else if (CurrentToken.Type == TokenType.LParen)
            {
                Match(TokenType.LParen);
                var result = ParseAddition();
                Match(TokenType.RParen);
                return result;
            }
            else
            {
                throw new Exception($"Unexpected token: {CurrentToken.Value}");
            }
        }
    }

}
