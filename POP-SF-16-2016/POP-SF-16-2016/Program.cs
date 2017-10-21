using POP_SF_16_2016.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new A();
            var ime = new A();
            ime.SetIme("Pera");
            Console.WriteLine($"Pozdrav ja se zovem { ime.GetIme() }" );
            Console.ReadLine();
           
                  
        }
    }
}
