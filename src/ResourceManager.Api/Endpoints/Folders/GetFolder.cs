//using MediatR;
//using ResourceManager.Api.Extensions;
//using ResourceManager.Api.Infrastructure;
//using ResourceManager.Application.Folders.GetFolder;
//using ResourceManager.SharedKernel;

//namespace ResourceManager.Api.Endpoints.Folders;

//public class GetFolder : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapGet("folders/{folderId}", async (
//            Guid folderId,
//            ISender sender,
//            CancellationToken cancellationToken) =>
//        {
//            Result<FolderResponse> result = await sender.Send(new GetFolderQuery(folderId), cancellationToken);

//            return result.Match(Results.Ok, CustomResults.Problem);
//        })
//        .WithTags(Tags.Folders);
//    }
//}
