using System;
using System.Reflection;
using IocLibrary;
using IocLibrary.Attributes;
using IocLibrary.Container;

namespace IoC
{
    public class Program
    {

        interface IInterface
        {
            void DoSomething();
        }
        
        class AnotherClass
        {
            [Inject]
            public IInterface A { get; set; }

            public AnotherClass()
            {
                Console.WriteLine("Constructor of AnotherClass is being invoked");
            }
            public void Print()
            {
                A.DoSomething();
                Console.WriteLine("I dont know, I am just printing!");
            }
        }
        [Register(typeof(IInterface))]
        class A : IInterface
        {
            public A()
            {
                Console.WriteLine("Constructor of A is being invoked");
            }
            public void DoSomething()
            {
                Console.WriteLine("I am doing something from A");
            }
        }

        class B
        {
            private A a;
            private AnotherClass _anotherClass;
            public B(A a, AnotherClass anotherClass)
            {
                Console.WriteLine("Constructor of B is being invoked");
                this.a = a;
                _anotherClass = anotherClass;
            }
            public void DoSomething()
            {
                a.DoSomething();
                Console.WriteLine("I am doing something from B");
                _anotherClass.Print();
            }
        }

        class C
        {
            private B b;
            public C(B b)
            {
                Console.WriteLine("Constructor of C is being invoked");
                this.b = b;
            }

            public void DoSomething()
            {
                b.DoSomething();
                Console.WriteLine("I am doing something from C");
            }
        }

        static void Main(string[] args)
        {
            //IocContainer.Instance.Register<IInterface, A>();

            IocContainer.Instance.AddAssembly(Assembly.GetExecutingAssembly());
            var instance = IocContainer.Instance.GetInstance<AnotherClass>();
            if (instance is AnotherClass c)
            {
                c.Print();
            }
        }
    }
}
