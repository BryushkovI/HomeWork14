using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Unateus_bot
{
    public class Chats
    {
        public List<long> IdList { get; }

        public Chats(JToken chats)
        {
            JObject data = (JObject)chats;
            JArray ListChats = JArray.Parse(data["ID"].ToString());
            IdList = new List<long>();
            foreach(var e in ListChats)
            {

                IdList.Add(long.Parse(e["Id"].ToString()));
            }
        }
    }
}
