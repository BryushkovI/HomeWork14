using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Unateus_bot
{
    class DailyEvent
    {
        /// <summary>
        /// Event time (minutes)
        /// </summary>
        public int Minute { get; }
        /// <summary>
        /// Event time Hours
        /// </summary>
        public int Hour { get; }
        /// <summary>
        /// Event massage
        /// </summary>
        public string Message { get; }

        public DailyEvent(JObject Event)
        {
            Minute = int.Parse(Event["Minute"].ToString());
            Hour = int.Parse(Event["Hour"].ToString());
            Message = Event["Message"].ToString();
        }
    }
}
