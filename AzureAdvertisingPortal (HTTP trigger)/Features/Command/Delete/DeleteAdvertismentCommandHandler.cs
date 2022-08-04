using AzureAdvertisingPortal__HTTP_trigger_.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Delete
{
    public class DeleteAdvertismentCommandHandler : IRequestHandler<DeleteAdvertismentCommand, DeleteAdvertismentCommandResult>
    {
        private readonly string _storageConnectionString;

        public DeleteAdvertismentCommandHandler(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("StorageConnectionString");
        }
        public async Task<DeleteAdvertismentCommandResult> Handle(DeleteAdvertismentCommand request, CancellationToken cancellationToken)
        {
            CloudStorageAccount storageAcc = CloudStorageAccount.Parse(_storageConnectionString);
            CloudTableClient tblclient = storageAcc.CreateCloudTableClient();
            CloudTable table = tblclient.GetTableReference("demo");

            var customQuery = new TableQuery<Advertisment>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, request.Category));

            var results = await table.ExecuteQuerySegmentedAsync<Advertisment>(customQuery, null);

            var result = results.Results.FirstOrDefault(x => x.RowKey == request.Title);

            if (result == null)
            {
                return null;
            }

            var advertisment = TableOperation.Delete(result);
            await table.ExecuteAsync(advertisment);

            return new DeleteAdvertismentCommandResult();
        }
    }
}
