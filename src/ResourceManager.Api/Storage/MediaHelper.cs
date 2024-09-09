namespace ResourceManager.Api.Storage;

public class DocumentHelper
{
    public static string MakeDocumentName(string filename)
    {
        var fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "DOC_" + Guid.NewGuid() + extension;
        return name;
    }

    public static string[] GetDocumentExtensions()
    {
        return new string[]
        {
            // PDF files
            ".pdf",
            // Word documents
            ".doc", ".docx",
            // Excel files
            ".xls", ".xlsx",
            // Text files
            ".txt",
            // PowerPoint files
            ".ppt", ".pptx"
        };
    }
}
