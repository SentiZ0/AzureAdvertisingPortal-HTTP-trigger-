namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetSingle
{
    public class GetSingleAdvertismentQueryResult
    {
        public AdvertismentDTO Advertisment { get; set; }

        public class AdvertismentDTO
        {
            public string Category { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public DateTimeOffset Timestamp { get; set; }
        }
    }
}
