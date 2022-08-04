using AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Create;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Delete;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Update;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetAll;
using AzureAdvertisingPortal__HTTP_trigger_.Features.Query.GetSingle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AzureAdvertisingPortal__HTTP_trigger_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertismentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdvertismentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdvertisment(CreateAdvertismentCommand command)
        {
            var response = await _mediator.Send(command);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("/single")]
        public async Task<ActionResult> GetSingleAdvertisment(string category, string title)
        {
            var query = new GetSingleAdvertismentQuery(category, title);

            var response = await _mediator.Send(query);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAdvertisments()
        {
            var response = await _mediator.Send(new GetAllAdvertismentsQuery());

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAdvertisment(string category, string title)
        {
            var query = new DeleteAdvertismentCommand(category, title);

            var response = await _mediator.Send(query);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> ModifyAdvertisment([FromForm] UpdateAdvertismentCommand command)
        {
            var repsonse = await _mediator.Send(command);

            if (repsonse == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
