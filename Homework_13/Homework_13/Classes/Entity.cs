using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Homework_13
{
    public class Entity : Client
    {
        public Entity(JObject client)
            : base(client)
        {

        }
        public Entity(int number)
            :base (number)
        {

        }
        public override int Deposite_percent
        {
            get
            {
                return 5;
            }
        }

        public override int Credit_percent
        {
            get
            {
                return 7;
            }
        }

    }
}
