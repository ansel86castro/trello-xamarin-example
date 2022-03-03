using System;

namespace TestXamarin.Models
{
    public class ListItemAttachementModel
    {        
        public string Id { get; set; }

        public string Name { get; set; }

        public string Bytes { get; set; }

        public DateTime? Date { get; set; }

        public bool IsUpload { get; set; }

        public string MimeType { get; set; }    

        public string Url { get; set; }
    }
}
