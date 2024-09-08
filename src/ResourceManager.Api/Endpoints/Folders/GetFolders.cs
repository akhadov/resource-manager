//using MediatR;
//using ResourceManager.Api.Extensions;
//using ResourceManager.Api.Infrastructure;
//using ResourceManager.Application.Folders.GetFolder;
//using ResourceManager.Application.Folders.GetFolders;
//using ResourceManager.SharedKernel;

//namespace ResourceManager.Api.Endpoints.Folders;

//public class GetFolders : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapGet("folders", async (
//            ISender sender,
//            CancellationToken cancellationToken) =>
//        {
//            Result<List<FolderResponse>> result = await sender.Send(new GetFoldersQuery(), cancellationToken);

//            return result.Match(Results.Ok, CustomResults.Problem);
//        })
//        .WithTags(Tags.Folders);
//    }
//}
