using System;
using System.Collections.Generic;
using System.Text;

namespace WebAdvert.SearchWorker
{
    // this should have used from the NuGet package, but that targets .NET Core 3.1, not 2.1
    public class AdvertConfirmedMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
