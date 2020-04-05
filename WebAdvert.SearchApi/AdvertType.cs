using System;
using Nest;

namespace WebAdvert.SearchWorker
{
    public class AdvertType
    {
        public string Id  { get; set; }
        public string Title { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
