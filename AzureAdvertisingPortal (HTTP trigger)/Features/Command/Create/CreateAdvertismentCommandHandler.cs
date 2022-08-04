using AzureAdvertisingPortal__HTTP_trigger_.Models;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Create
{
    public class CreateAdvertismentCommandHandler : IRequestHandler<CreateAdvertismentCommand, CreateAdvertismentCommandResult>
    {
        private readonly string _storageConnectionString;

        public CreateAdvertismentCommandHandler(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("StorageConnectionString");
        }

        public async Task<CreateAdvertismentCommandResult> Handle(CreateAdvertismentCommand request, CancellationToken cancellationToken)
        {
            CloudStorageAccount storageAcc = CloudStorageAccount.Parse(_storageConnectionString);
            CloudTableClient tblclient = storageAcc.CreateCloudTableClient();
            CloudTable table = tblclient.GetTableReference("demo");

            await table.CreateIfNotExistsAsync();

            var customQuery = new TableQuery<Advertisment>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, request.Category));
            var singleResults = await table.ExecuteQuerySegmentedAsync<Advertisment>(customQuery, null);
            var singleResult = singleResults.Results.FirstOrDefault(x => x.RowKey == request.Title);

            if (singleResult == null)
            {
                var advertisment = new Advertisment();
                advertisment.PartitionKey = request.Category;
                advertisment.RowKey = request.Title;
                advertisment.Description = request.Description;

                var insertOperation = TableOperation.Insert(advertisment);

                TableResult result = await table.ExecuteAsync(insertOperation);

                return new CreateAdvertismentCommandResult();
            }
            else
            {
                return null;
            }
        }
    }
}
