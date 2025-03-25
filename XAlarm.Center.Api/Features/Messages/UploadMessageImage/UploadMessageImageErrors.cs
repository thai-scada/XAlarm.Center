using XAlarm.Center.Domain.Abstractions;

namespace XAlarm.Center.Api.Features.Messages.UploadMessageImage;

public static class UploadMessageImageErrors
{
    public static readonly Error Error = new("UploadMessageImage.Error", "An error occurred while uploading image");

    public static readonly Error NotFound = new("UploadMessageImage.NotFound", "The image was not found");
}