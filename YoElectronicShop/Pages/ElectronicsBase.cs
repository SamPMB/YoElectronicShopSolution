using Microsoft.AspNetCore.Components;
using YoElectronicShop.Models.DTO;
using YoElectronicShop.Services.Contracts;

namespace YoElectronicShop.Pages
{
    public class ElectronicsBase:ComponentBase
    {
        [Inject]
        IProductService ProductService {  get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
          Products = await  ProductService.GetItems();
        }

    }
}
