using YoElectronicShop.Api.Entities;
using YoElectronicShop.Models.DTO;

namespace YoElectronicShop.Api.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {

        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GeItems(int userId);
    }
}
