using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using YoElectronicShop;
using YoElectronicShop.Services;
using YoElectronicShop.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7261/") });
builder.Services.AddScoped<IProductService, ProductService > ();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
await builder.Build().RunAsync();
   