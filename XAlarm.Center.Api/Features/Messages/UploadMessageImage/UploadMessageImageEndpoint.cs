using FastEndpoints;
using XAlarm.Center.Domain.Users;
using XAlarm.Center.Shared.Extensions;

namespace XAlarm.Center.Api.Features.Messages.UploadMessageImage;

public class UploadMessageImageEndpoint : Endpoint<UploadMessageImageRequest, UploadMessageImageResponse>
{
    public override void Configure()
    {
        Post("api/messages/uploadMessageImage");
        AllowFileUploads();
        Policies(RoleTypes.RealmAdministrator.GetDescription());
        Description(x => x.WithTags("Message"));
    }

    public override async Task HandleAsync(UploadMessageImageRequest request, CancellationToken cancellationToken)
    {
        if (request.MessageImageFile.Length > 0)
        {
            var folderPath = Path.Combine(AppContext.BaseDirectory, "..", "assets", "images",
                request.ProjectId.ToString());
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, request.MessageImageFile.FileName);
            await using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await request.MessageImageFile.CopyToAsync(fileStream, cancellationToken);
        }

        await Send.NoContentAsync(cancellationToken);
    }
}