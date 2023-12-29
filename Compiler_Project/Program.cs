using Compiler_Project;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum TokenType
{
    Integer,
    Plus,
    Minus,
    Multiply,
    Divide,
    LParen,
    RParen,
    EndOfInput
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
}


public class Program
{
    public static void Main()
    {
        //while (true)
        //{
        //    Console.Write("Enter an arithmetic expression: ");
        //    var input = Console.ReadLine();
        //    var lexer = new Lexer(input);

        //    Token token;
        //    do
        //    {
        //        token = lexer.GetNextToken();
        //        Console.WriteLine($"Token: Type={token.Type}, Value='{token.Value}'");
        //    } while (token.Type != TokenType.EndOfInput);
        //}

        #region
        while (true)
        {
            Console.Write("Enter an arithmetic expression: ");
            var input = Console.ReadLine();
            var lexer = new Lexer(input);

            var tokens = new List<Token>();
            Token token;
            do
            {
                token = lexer.GetNextToken();
                tokens.Add(token);
            } while (token.Type != TokenType.EndOfInput);

            var parser = new Parser(tokens.ToArray());
            try
            {
                var result = parser.ParseExpression();
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}

