using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using YoElectronicShop.Models.DTO;
using YoElectronicShop.Services.Contracts;

namespace YoElectronicShop.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IShoppingCartService? ShoppingCartService { get; set; }
        public List<CartItemDto>? ShoppingCartItems { get; set; }
        public string? ErrorMessage { get; set; }
        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CartChanged();
            }
            catch (Exception  ex)
            {

               ErrorMessage = ex.Message;
            }
        }
        protected async Task UpdateQty_Input(int id)
        {


            await MakeUpdateQtyButtonVisible(id, true);
        }
        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
          
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

     

        protected async Task UpdateQtyCartItem_click(int id, int qty)
        {

            try
            {
                if(qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                         CartItemId = id,
                         Qty = qty

                    };
                    var returnedUpdateDto = await this.ShoppingCartService.UpdateQty(updateItemDto);
                    UpdateTotalPrice(returnedUpdateDto);
                    CartChanged();
                    await MakeUpdateQtyButtonVisible(id, false); 

                } 
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i  => i.Id == id);
                    if (item != null)
                    {
                        item.Qyt = 1;
                        item.TotalPrice = item.Price;
                    }
                }
                
                
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected async Task DeleteCartItem_click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            RemoveCartItem(id);
            CartChanged();
        }

        private void UpdateTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qyt;
            }
        }

        private void CalculateCartSummaryTotals()
        {

            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice()
        {
             TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");

        }
        private void SetTotalQuantity() { 
          TotalQuantity  = this.ShoppingCartItems.Sum (p => p.Qyt);
        
        
        }

        private CartItemDto? GetCartItem(int id) {
        
        return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }
        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            ShoppingCartItems.Remove(cartItemDto);
        }

        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);

        }


    }
}
