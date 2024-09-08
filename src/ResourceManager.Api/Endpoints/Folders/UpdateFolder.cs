
//using MediatR;
//using ResourceManager.Api.Extensions;
//using ResourceManager.Api.Infrastructure;
//using ResourceManager.Application.Folders.UpdateFolder;
//using ResourceManager.SharedKernel;

//namespace ResourceManager.Api.Endpoints.Folders;

//public class UpdateFolder : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapPut("folders/{folderId}", async (
//            Guid folderId,
//            UpdateFolderRequest request,
//            ISender sender,
//            CancellationToken cancellationToken) =>
//        {
//            var command = new UpdateFolderCommand(
//                folderId,
//                request.Name);

//            Result<Guid> result = await sender.Send(command, cancellationToken);

//            return result.Match(Results.Ok, CustomResults.Problem);
//        })
//        .WithTags(Tags.Folders);
//    }
//}
