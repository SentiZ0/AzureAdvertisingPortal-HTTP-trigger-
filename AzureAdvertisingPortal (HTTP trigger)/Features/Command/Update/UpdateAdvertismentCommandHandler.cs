using AzureAdvertisingPortal__HTTP_trigger_.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Update
{
    public class UpdateAdvertismentCommandHandler : IRequestHandler<UpdateAdvertismentCommand, UpdateAdvertismentCommandResult>
    {
        private readonly string _storageConnectionString;
        public UpdateAdvertismentCommandHandler(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("StorageConnectionString");
        }
        public async Task<UpdateAdvertismentCommandResult> Handle(UpdateAdvertismentCommand request, CancellationToken cancellationToken)
        {
            CloudStorageAccount storageAcc = CloudStorageAccount.Parse(_storageConnectionString);
            CloudTableClient tblclient = storageAcc.CreateCloudTableClient();
            CloudTable table = tblclient.GetTableReference("demo");

            var customQuery = new TableQuery<Advertisment>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, request.Category));
            var singleResults = await table.ExecuteQuerySegmentedAsync<Advertisment>(customQuery, null);
            var singleResult = singleResults.Results.FirstOrDefault(x => x.RowKey == request.Title);

            if(singleResult != null)
            {
                var advertisment = new Advertisment();
                advertisment.PartitionKey = request.Category;
                advertisment.RowKey = request.Title;
                advertisment.Timestamp = DateTime.UtcNow;
                advertisment.Description = request.Description;

                var result = TableOperation.InsertOrMerge(advertisment);
                await table.ExecuteAsync(result);

                return new UpdateAdvertismentCommandResult();
            }
            else
            {
                return null;
            }
        }
    }
}
