//Implicit and global usings
using System;

namespace HowMinimalApiHappened
{
    class Program
    {
        //This is replaced by top level statements
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            //Cannot Infer lambda type
            Func<string, int> ConvertToInt = (string input) => int.Parse(input);
            
            //Lambdas cannot have attributes
            //[Obsolete] 
            Func<string, string> GreetingFunction = (string name) => $"Hello {name}";

        }
    }
}