using Microsoft.AspNetCore.Components;
using YoElectronicShop.Models.DTO;

namespace YoElectronicShop.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]  
        public IEnumerable<ProductDto>? Products { get; set; }  
    }
}
