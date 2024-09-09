using ResourceManager.Application.DocumentHistories.GetDocumentHistory;
using ResourceManager.Application.Documents.CreateDocument;
using ResourceManager.Application.Documents.UpdateDocument;
using ResourceManager.UI.Services.Interfaces;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ResourceManager.UI.Services;

public class DocumentService : IDocumentService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;

    public DocumentService(HttpClient http)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<Application.Documents.GetDocuments.DocumentResponse> AddDocument(CreateDocumentRequest document)
    {
        try
        {
            var documentjson = new StringContent(JsonSerializer.Serialize(document), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("documents", documentjson);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseBody = await response.Content.ReadAsStreamAsync();

            var newDocument = await JsonSerializer.DeserializeAsync<Application.Documents.GetDocuments.DocumentResponse>(responseBody, _serializerOptions);

            return newDocument;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteDocument(Guid id)
    {
        try
        {
            var response = await _http.DeleteAsync($"documents/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Application.Documents.GetDocument.DocumentResponse> GetDocument(Guid id)
    {
        try
        {
            var document = await _http.GetFromJsonAsync<Application.Documents.GetDocument.DocumentResponse>($"documents/{id}");

            return document;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Application.Documents.GetDocuments.DocumentResponse>?> GetDocuments()
    {
        try
        {
            var apiResponse = await _http.GetStreamAsync("documents");

            var documents = await JsonSerializer.DeserializeAsync<List<Application.Documents.GetDocuments.DocumentResponse>>(apiResponse, _serializerOptions);

            return documents;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<HistoryResponse>> GetHistories(Guid documentId)
    {
        try
        {
            // Define the URL for fetching document histories
            var apiResponse = await _http.GetStreamAsync($"documents/{documentId}/histories");

            // Deserialize the response into a list of HistoryResponse
            var histories = await JsonSerializer.DeserializeAsync<List<HistoryResponse>>(apiResponse, _serializerOptions);

            return histories;
        }
        catch (Exception e)
        {
            // Log the exception and rethrow or return an empty list
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<bool> UpdateDocument(Guid documentId, string title, string content)
    {
        try
        {
            var command = new UpdateDocumentCommand(documentId, title, content);

            var documentjson = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _http.PutAsync("documents", documentjson);

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateDocuments(Guid documentId, UpdateDocumentRequest updateDocumentRequest)
    {
        try
        {
            var documentJson = new StringContent(JsonSerializer.Serialize(updateDocumentRequest), Encoding.UTF8, "application/json");

            var response = await _http.PutAsync($"documents/{documentId}", documentJson);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {responseContent}");
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
