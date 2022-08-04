using MediatR;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetSingle
{
    public class GetSingleAdvertismentQuery : IRequest<GetSingleAdvertismentQueryResult>
    {
        public string Category { get; set; }

        public string Title { get; set; }

        public GetSingleAdvertismentQuery(string category, string title)
        {
            Category = category;
            Title = title;
        }
    }
}
