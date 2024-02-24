using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Unateus_bot
{
    class Methods
    {
        /// <summary>
        /// Deserialize List of Events
        /// </summary>
        /// <param name="Path">Linq of JSON</param>
        /// <returns></returns>
        public Commands Events(string Path)
        {
            string JSON = File.ReadAllText(Path);
            var data = JToken.Parse(JSON);
            Commands commands = new Commands(data);
            return commands;
        }
        /// <summary>
        /// Deserialize List of chats
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Chats ChatList(string Path)
        {
            string JSON = File.ReadAllText(Path);
            var data = JToken.Parse(JSON);
            Chats chats = new Chats(data);
            return chats;
        }
    }
}
