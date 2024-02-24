using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Unateus_bot
{
    class Commands
    {
        /// <summary>
        /// List of DailyEvents
        /// </summary>
        public List<DailyEvent> dailyEvents { get; }

        public Commands(JToken token)
        {
            JObject e = (JObject)token;
            JArray Events = JArray.Parse(e["Events"].ToString());
            dailyEvents = new List<DailyEvent>();
            foreach(var i in Events)
            {
                dailyEvents.Add(new DailyEvent((JObject)i));
            }
        }
    }
}
