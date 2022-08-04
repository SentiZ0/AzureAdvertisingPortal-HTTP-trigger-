using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetSingle;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Create;
using System.Net.Http;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Delete;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetAll;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Update;

namespace StorageTable
{
    public class CreateAdvertisment
    {
        private readonly IMediator _mediator;
        private readonly HttpClient _client;

        public CreateAdvertisment(IHttpClientFactory httpClientFactory, IMediator mediator)
        {
            _client = httpClientFactory.CreateClient();
            _mediator = mediator;
        }

        [FunctionName("CreateAdvertisment")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Jest dobrze w chuj");

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var query = JsonConvert.DeserializeObject<CreateAdvertismentCommand>(content);

            var response = await _mediator.Send(query);

            if (response == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(response);
        }

        [FunctionName("DeleteAdvertisment")]
        public async Task<IActionResult> Delete(
    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
    ILogger log)
        {
            log.LogInformation("Jest dobrze w chuj");

            var Title = req.Query["title"];
            var Category = req.Query["category"];

            var query = new DeleteAdvertismentCommand(Category,  Title);

            var response = await _mediator.Send(query);

            if (response == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(response);
        }

        [FunctionName("GetSingleAdvertisment")]
        public async Task<IActionResult> GetSingle(
[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
ILogger log)
        {
            log.LogInformation("Jest wykurwiœcie w chuj");

            var Title = req.Query["title"];
            var Category = req.Query["category"];

            var query = new GetSingleAdvertismentQuery(Category, Title);

            var response = await _mediator.Send(query);

            if (response == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(response);
        }

        [FunctionName("GetAllAdvertisments")]
        public async Task<IActionResult> GetAll(
[HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
ILogger log)
        {
            log.LogInformation("Jest zajebiœcie w chuj");

            var response = await _mediator.Send(new GetAllAdvertismentsQuery());

            if (response == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(response);
        }

        [FunctionName("ModifyAdvertisment")]
        public async Task<IActionResult> Modify(
[HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
ILogger log)
        {
            log.LogInformation("Jest zajebiœcie w chuj");

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var query = JsonConvert.DeserializeObject<UpdateAdvertismentCommand>(content);

            var response = await _mediator.Send(query);

            if (response == null)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(response);
        }
    }
}
