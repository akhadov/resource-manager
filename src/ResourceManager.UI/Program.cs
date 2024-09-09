using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ResourceManager.UI;
using ResourceManager.UI.Services;
using ResourceManager.UI.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped<IDocumentService, DocumentService>();


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/api/v1/"),
    Timeout = Timeout.InfiniteTimeSpan
});


//builder.Services.AddScoped(
//    hp => new HttpClient
//    {
//        BaseAddress = new Uri("http://localhost:5000/api/v1/")
//    });

await builder.Build().RunAsync();
