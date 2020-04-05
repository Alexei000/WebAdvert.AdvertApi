using System;

namespace WebAdvert.SearchWorker
{
    public class MappingHelper
    {
        public static AdvertType Map(AdvertConfirmedMessage message)
        {
            return new AdvertType
            {
                Id = message.Id,
                Title = message.Title,
                CreationDateTime = DateTime.UtcNow
            };
        }
    }
}
