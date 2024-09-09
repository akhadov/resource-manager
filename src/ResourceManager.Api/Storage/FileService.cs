using ResourceManager.Application.Abstractions.Storage;

namespace ResourceManager.Api.Storage;

public class FileService : IFileService
{
    private readonly string MEDIA = "media";
    private readonly string DOCUMENTS = "documents";
    private readonly string ROOTPATH;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        ROOTPATH = webHostEnvironment.WebRootPath;
    }

    public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        string newDocumentName = DocumentHelper.MakeDocumentName(file.FileName);
        string subpath = Path.Combine(MEDIA, DOCUMENTS, newDocumentName);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);
        stream.Close();

        return subpath;
    }

    public async Task<bool> DeleteAsync(string fileId)
    {
        string path = Path.Combine(ROOTPATH, fileId);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        else
        {
            return false;
        }
    }
}