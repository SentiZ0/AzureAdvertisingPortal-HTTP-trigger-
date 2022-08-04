using MediatR;

namespace AzureAdvertisingPortal__HTTP_trigger_.Features.Command.Update
{
    public class UpdateAdvertismentCommand : IRequest<UpdateAdvertismentCommandResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public UpdateAdvertismentCommand(string category, string title, string description)
        {
            Category = category;
            Title = title;
            Description = description;
        }
    }
}
