using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace explicit_implicit_text
{
    struct RomanNumeral
    {
        private int value;
        public RomanNumeral(int value)
        {
            this.value = value;
        }
        static public implicit operator RomanNumeral(int value)
        {
            return new RomanNumeral(value);
        }
        static public implicit operator RomanNumeral(BinaryNumeral binary)
        {
            return new RomanNumeral((int)binary);
        }
        static public explicit operator int(RomanNumeral roman)
        {
            return roman.value;
        }
        static public implicit operator string(RomanNumeral roman)
        {
            return ("Conversion to string is not implemented");
        }
    }
    struct BinaryNumeral
    {
        private int value;
        public BinaryNumeral(int value)
        {
            this.value = value;
        }
        static public implicit operator BinaryNumeral(int value)
        {
            return new BinaryNumeral(value);
        }
        static public explicit operator int(BinaryNumeral roman)
        {
            return roman.value;
        }
        static public implicit operator string(BinaryNumeral roman)
        {
            return ("Conversion to string is not implemented");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RomanNumeral roman;
            BinaryNumeral binary;
            roman = 10;
            binary = (BinaryNumeral)(int)roman;
            //从RomanNumeral搭配BinaryNumeral的转换。
            //由于没有从R到B的直接转化，所以使用强制转换将R转换为int,再将int转换为B
            roman = binary;
            //R中定义了从b到R的转换，所以不需要强制转换。
            System.Console.WriteLine((int)binary);
            System.Console.WriteLine(binary);
        }
    }
}
