//using MediatR;
//using ResourceManager.Api.Extensions;
//using ResourceManager.Api.Infrastructure;
//using ResourceManager.Application.Folders.CreateFolder;
//using ResourceManager.SharedKernel;

//namespace ResourceManager.Api.Endpoints.Folders;

//public class CreateFolder : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapPost("folders", async (
//            CreateFolderRequest request,
//            ISender sender,
//            CancellationToken cancellationToken) =>
//        {
//            var command = new CreateFolderCommand(
//                request.Name,
//                request.ParentFolderId
//            );

//            Result<Guid> result = await sender.Send(command, cancellationToken);

//            return result.Match(Results.Ok, CustomResults.Problem);
//        })
//        .WithTags(Tags.Folders);
//    }
//}
