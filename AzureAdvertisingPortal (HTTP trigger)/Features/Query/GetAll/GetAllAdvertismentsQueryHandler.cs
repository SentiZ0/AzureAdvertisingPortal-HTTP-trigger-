using AzureAdvertisingPortal__HTTP_trigger_.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetAll
{
    public class GetAllAdvertismentsQueryHandler : IRequestHandler<GetAllAdvertismentsQuery, GetAllAdvertismentsQueryResult>
    {
        private readonly string _storageConnectionString;

        public GetAllAdvertismentsQueryHandler(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("StorageConnectionString");
        }
        public async Task<GetAllAdvertismentsQueryResult> Handle(GetAllAdvertismentsQuery request, CancellationToken cancellationToken)
        {
            CloudStorageAccount storageAcc = CloudStorageAccount.Parse(_storageConnectionString);
            CloudTableClient tblclient = storageAcc.CreateCloudTableClient();
            CloudTable table = tblclient.GetTableReference("demo");

            var customQuery = new TableQuery<Advertisment>();

            var results = await table.ExecuteQuerySegmentedAsync<Advertisment>(customQuery, null);

            var advertisments = results.Results.Select(x => new GetAllAdvertismentsQueryResult.AdvertismentDTO
            {
                Category = x.PartitionKey,
                Title = x.RowKey,
                Description = x.Description,
                Timestamp = x.Timestamp,
            }).ToList();

            return new GetAllAdvertismentsQueryResult()
            { Advertisments = advertisments };

        }
    }
}
