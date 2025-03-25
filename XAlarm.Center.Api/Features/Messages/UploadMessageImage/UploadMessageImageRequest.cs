namespace XAlarm.Center.Api.Features.Messages.UploadMessageImage;

public record UploadMessageImageRequest(Guid ProjectId, IFormFile MessageImageFile);