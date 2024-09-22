using ResourceManager.Application.DocumentHistories.GetDocumentHistory;
using ResourceManager.Application.Documents.CreateDocument;
using ResourceManager.Application.Documents.Reject;
using ResourceManager.Application.Documents.UpdateDocument;
using ResourceManager.Application.Workflows.Create;
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
    //public async Task<Application.Documents.GetDocuments.DocumentResponse> AddDocument(Guid userId, CreateDocumentRequest document)
    //{
    //    try
    //    {
    //        var documentjson = new StringContent(JsonSerializer.Serialize(document), Encoding.UTF8, "application/json");

    //        var response = await _http.PostAsync($"documents/users/{userId}", documentjson);

    //        if (!response.IsSuccessStatusCode)
    //        {
    //            return null;
    //        }
    //        var responseBody = await response.Content.ReadAsStreamAsync();

    //        var newDocument = await JsonSerializer.DeserializeAsync<Application.Documents.GetDocuments.DocumentResponse>(responseBody, _serializerOptions);

    //        return newDocument;
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }
    //}
    public async Task<bool> AddDocument(Guid userId, CreateDocumentRequest document)
    {
        try
        {
            var documentJson = new StringContent(JsonSerializer.Serialize(document), Encoding.UTF8, "application/json");

            // Make the POST request to add the document
            var response = await _http.PostAsync($"documents/users/{userId}", documentJson);

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                // Log the failure and return false
                Console.WriteLine($"Failed to add document. StatusCode: {response.StatusCode}");
                return false;
            }

            // If the response is successful, return true
            return true;
        }
        catch (Exception e)
        {
            // Log any exceptions that occur and rethrow the exception if needed
            Console.WriteLine($"An error occurred: {e.Message}");
            return false;
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
            var apiResponse = await _http.GetStreamAsync($"documents/{documentId}/histories");

            var histories = await JsonSerializer.DeserializeAsync<List<HistoryResponse>>(apiResponse, _serializerOptions);

            return histories;
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

    public async Task<bool> SubmitForApproval(Guid documentId, Guid userId)
    {
        try
        {
            var response = await _http.PutAsync($"documents/{documentId}/submit/{userId}", null);

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

    public async Task<bool> ApproveDocument(Guid documentId, Guid userId)
    {
        try
        {
            var response = await _http.PutAsync($"documents/{documentId}/approve/{userId}", null);

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

    public async Task<bool> RejectDocuemt(Guid documentId, Guid userId, RejectDocumentRequest reason)
    {
        try
        {
            var rejectDocumentJson = new StringContent(JsonSerializer.Serialize(reason), Encoding.UTF8, "application/json");

            var response = await _http.PutAsync($"documents/{documentId}/reject/{userId}", rejectDocumentJson);

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

    public async Task<bool> AddWorkflow(Guid documentId, WorkflowRequest workflow)
    {
        try
        {
            // Serialize the workflow request to JSON format
            var workflowJson = new StringContent(JsonSerializer.Serialize(workflow), Encoding.UTF8, "application/json");

            // Make a POST request to add a new workflow to the document
            var response = await _http.PostAsync($"documents/{documentId}/workflows", workflowJson);

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                // Log error and return false if the API call fails
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error adding workflow: {responseContent}");
                return false;
            }

            // If successful, return true
            return true;
        }
        catch (Exception e)
        {
            // Handle any exceptions and log the error message
            Console.WriteLine($"An error occurred while adding workflow: {e.Message}");
            return false;
        }
    }

    public async Task<bool> RemoveWorkflow(Guid documentId, Guid workflowId)
    {
        try
        {
            // Make a DELETE request to remove a workflow by its ID
            var response = await _http.DeleteAsync($"documents/{documentId}/workflows/{workflowId}");

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                // Log error and return false if the API call fails
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error removing workflow: {responseContent}");
                return false;
            }

            // If successful, return true
            return true;
        }
        catch (Exception e)
        {
            // Handle any exceptions and log the error message
            Console.WriteLine($"An error occurred while removing workflow: {e.Message}");
            return false;
        }
    }
}
