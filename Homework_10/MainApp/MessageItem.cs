using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp
{
    public struct MessageItem
    {
        public string Time { get; set; }
        public long Id { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string Type { get; set; }
        public string FileID { get; set; }
        public string Mime { get; set; }
        public MessageItem(string Time,long Id, string Message,string FirstName,string Type,string FileID,string Mime)
        {
            this.Time = Time;
            this.Id = Id;
            this.Message = Message;
            this.FirstName = FirstName;
            this.Type = Type;
            this.FileID = FileID;
            this.Mime = Mime;
        }

    }
}
