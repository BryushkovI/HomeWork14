using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Homework_13
{
    public class Individual_VIP : Client
    {
        public Individual_VIP(JObject client)
            :base(client)
        {

        }
        public Individual_VIP(int number)
            : base(number)
        {

        }
        public override int Deposite_percent 
        { 
            get
            {
                return 10;
            }
        }

        public override int Credit_percent
        {
            get
            {
                return 11;
            }
        }
    }
}
