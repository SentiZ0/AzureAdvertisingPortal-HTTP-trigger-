using MediatR;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Create
{
    public class CreateAdvertismentCommand : IRequest<CreateAdvertismentCommandResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }

        public CreateAdvertismentCommand(string title, string description, string category)
        {
            Title = title;
            Description = description;
            Category = category;
        }

    }
}
