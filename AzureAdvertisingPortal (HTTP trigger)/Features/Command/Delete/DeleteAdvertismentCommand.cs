using MediatR;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Delete
{
    public class DeleteAdvertismentCommand : IRequest<DeleteAdvertismentCommandResult>
    {
        public string Category { get; set; }

        public string Title { get; set; }

        public DeleteAdvertismentCommand(string category, string title)
        {
            Category = category;
            Title = title;
        }
    }
}
