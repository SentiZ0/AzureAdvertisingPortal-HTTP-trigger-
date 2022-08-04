namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetAll
{
    public class GetAllAdvertismentsQueryResult
    {
        public List<AdvertismentDTO> Advertisments { get; set; }

        public class AdvertismentDTO
        {
            public string Category { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public DateTimeOffset Timestamp { get; set; }
        }
    }
}
