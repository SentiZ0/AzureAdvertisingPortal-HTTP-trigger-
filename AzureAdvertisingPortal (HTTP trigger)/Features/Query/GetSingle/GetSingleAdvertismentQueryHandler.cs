using AzureAdvertisingPortal__HTTP_trigger_.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetSingle
{
    public class GetSingleAdvertismentQueryHandler : IRequestHandler<GetSingleAdvertismentQuery, GetSingleAdvertismentQueryResult>
    {
        private readonly string _storageConnectionString;

        public GetSingleAdvertismentQueryHandler(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("StorageConnectionString");
        }
        public async Task<GetSingleAdvertismentQueryResult> Handle(GetSingleAdvertismentQuery request, CancellationToken cancellationToken)
        {
            CloudStorageAccount storageAcc = CloudStorageAccount.Parse(_storageConnectionString);
            CloudTableClient tblclient = storageAcc.CreateCloudTableClient();
            CloudTable table = tblclient.GetTableReference("demo");

            var customQuery = new TableQuery<Advertisment>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, request.Category));

            var results = await table.ExecuteQuerySegmentedAsync<Advertisment>(customQuery, null);

            var result = results.Results.Where(x => x.RowKey == request.Title).FirstOrDefault(x => x.RowKey == request.Title);

            if (result == null)
            {
                return null;
            }

            var advertisment = new GetSingleAdvertismentQueryResult.AdvertismentDTO
            {
                Category = result.PartitionKey,
                Title = result.RowKey,
                Description = result.Description,
                Timestamp = result.Timestamp,
            };

            return new GetSingleAdvertismentQueryResult
            {
                Advertisment = advertisment,
            };
        }
    }
}