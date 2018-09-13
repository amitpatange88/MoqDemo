using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApps
{
    public class Customer
    {
        public bool AddCustomer(Mail m)
        {
            m.Send("smtp.gmail.com", "amitpatange88@gmail.com", "jasdjasd93003endd=", "amitpatange88@gmail.com", "subject", "body");

            //add ado.net code here to inser customer.

            return true;
        }
    }
}
