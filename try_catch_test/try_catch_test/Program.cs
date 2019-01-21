using System;

namespace try_catch_test
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("out:");
            try
            {
                string str = null;
                ProcessString(str);
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine("{0} First exception," ,e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("{0}Second exception.", e.Message);
            }
        }
        static void ProcessString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
