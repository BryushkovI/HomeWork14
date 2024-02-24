using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Homework_13
{
    public class Individual_regular : Client
    {
        public Individual_regular(JObject client)
            : base(client)
        {

        }
        public Individual_regular(int number)
            :base(number)
        {

        }
        public override int Deposite_percent
        {
            get
            {
                return 8;
            }
        }

        public override int Credit_percent
        {
            get
            {
                return 12;
            }
        }
    }
}
