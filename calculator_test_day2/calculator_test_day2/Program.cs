using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator_test_day2
{
    class Program
    {
        public static double Calculate_subtracting(double x, double y, string i)
        {
            switch (i)
            {
                case "+":
                    return (x + y);
                case "-":
                    return (x - y);
                case "*":
                    return (x * y);
                case "/":
                    return (x / y);
                default:
                    return 0;

            }
        }
        public static double Calculate_sqrt(double x)
        {
            return Math.Sqrt(x);
        }
        public static double Calculate_power(double x, int number)
        {
            double sum = 1;
            for (int i = 0; i < number; i++)
            {
                sum = sum * x;
            }
            return sum;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter the calculation method you wish to select");
                Console.WriteLine("Enter 1 for addition, subtraction, multiplication, and division");
                Console.WriteLine("Enter 2 as the square root");
                Console.WriteLine("Enter 3 to the NTH power");
                string i = Console.ReadLine();
                if (i == "1")
                {
                    Console.WriteLine(" Please enter the desired operation(+, -, *, /).");
                    string operation = Console.ReadLine();
                    Console.WriteLine("Please enter {0}", (operation == "/" ? "dividend" : "the first operand"));
                    double op1 = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter {0}", (operation == "/" ? "divisor" : "the second operand"));
                    double op2 = double.Parse(Console.ReadLine());
                    Console.WriteLine("The result is :{0} {1} {2} = {3}", op1, operation, op2, Calculate_subtracting(op1, op2, operation));
                }else if (i == "2")
                {
                    Console.WriteLine("Please enter the operand");
                    double op = double.Parse(Console.ReadLine());
                    Console.WriteLine("{0} sqrt is {1}", op, Calculate_sqrt(op));
                }else if (i == "3")
                {
                    Console.WriteLine("Please enter the operand");
                    double op = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the Nth power");
                    int op2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("The result is {0} ^ {1} = {2}", op, op2, Calculate_power(op, op2));
                }else
                {
                    Console.WriteLine("Input error, please re - enter");
                }
                Console.WriteLine();
            }
        }
    }
}
