using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtual_test
{
    class A
    {
        public virtual void Func()
        {
            Console.WriteLine("Func In A");
        }
    }
        class B : A
        {
            public  override void Func()
            {
                Console.WriteLine("Func In B");
            }
        }
        class C : B
        {

        }
        class D : A
        {
            public  new void Func()  //new表示覆盖父类里的同名类，而不是重新实现
            {
                Console.WriteLine("Func In D");
            }
        }

    class Program
    {
        static void Main(string[] args)
        {
            A a;
            A b;
            A c;
            A d;     //定义一个d这个A类的对象，A为d的声明类
            a = new A();
            b = new B();
            c = new C();
            d = new D();
            a.Func();
            b.Func();
            c.Func();
            d.Func();
            //检查声明类A，为虚函数，转去执行实例类D，无重写，转去检查类D的父类A，为其本身，
            //执行父类A的Func方法，输出Func In A
            D d1 = new D();
            d1.Func();
        }
    }
}
