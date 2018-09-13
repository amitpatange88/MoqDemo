using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalApps;

namespace MOQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c = new Customer();
            c.AddCustomer();

            Console.ReadLine();
        }
    }
}
