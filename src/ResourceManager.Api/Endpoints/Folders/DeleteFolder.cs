//using MediatR;
//using ResourceManager.Api.Extensions;
//using ResourceManager.Api.Infrastructure;
//using ResourceManager.Application.Folders.DeleteFolder;
//using ResourceManager.SharedKernel;

//namespace ResourceManager.Api.Endpoints.Folders;

//public class DeleteFolder : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapDelete("folders/{folderId}", async (
//            Guid folderId,
//            ISender sender,
//            CancellationToken cancellationToken) =>
//        {
//            var query = new DeleteFolderCommand(folderId);

//            Result result = await sender.Send(query, cancellationToken);

//            return result.Match(Results.NoContent, CustomResults.Problem);
//        })
//        .WithTags(Tags.Folders);
//    }
//}
