using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CreditDocumentFrontend;
using System.Net.Http;
using SharedModels;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CreditDocumentFrontend.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CreditDocumentService>();


builder.Services.AddBlazorise(options =>
{
    options.Immediate = true;

})
     .AddBootstrapProviders()
    .AddFontAwesomeIcons();


await builder.Build().RunAsync();

