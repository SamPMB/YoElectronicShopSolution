using Microsoft.EntityFrameworkCore;
using YoElectronicShop.Api.Data;
using YoElectronicShop.Api.Entities;
using YoElectronicShop.Api.Repositories.Contracts;
using YoElectronicShop.Models.DTO;

namespace YoElectronicShop.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly YoElectronicShopDBContext yoElectronicShopDBContext;

        public ShoppingCartRepository( YoElectronicShopDBContext yoElectronicShopDBContext)
        {   
            this.yoElectronicShopDBContext = yoElectronicShopDBContext;
        }

        private async Task<bool> CartItemExists(int cardId, int productId)
        {
            return await this.yoElectronicShopDBContext.CartIterms.AnyAsync(c => c.CartId == cardId &&
                                                                            c.ProductId == productId);


        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
              if( await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {


                var item = await (from product in this.yoElectronicShopDBContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty,
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var results = await this.yoElectronicShopDBContext.CartIterms.AddAsync(item);
                    await this.yoElectronicShopDBContext.SaveChangesAsync();
                    return results.Entity;

                }

            }

            return null; 
        }
        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await this.yoElectronicShopDBContext.CartIterms.FindAsync(id);
            if(item != null)
            {
                this.yoElectronicShopDBContext.CartIterms.Remove(item);
                await this.yoElectronicShopDBContext.SaveChangesAsync();

            }
            return item;
        }

     

        public async Task<CartItem> GetItem(int id)
        {
           
            return await (from cart in this.yoElectronicShopDBContext.Carts
                          join cartItem in yoElectronicShopDBContext.CartIterms
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();

        }


        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this.yoElectronicShopDBContext.CartIterms.FindAsync(id);
            if(item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this.yoElectronicShopDBContext.SaveChangesAsync();
                return item;
            }
            return null;
        }

        public async Task<IEnumerable<CartItem>> GeItems(int userId)
        {
            return await(from cart in this.yoElectronicShopDBContext.Carts
                         join cartItem in this.yoElectronicShopDBContext.CartIterms
                         on cart.Id equals cartItem.CartId
                         where cart.UserId == userId
                         select new CartItem
                         {

                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId
                         }).ToListAsync();
        }
    }
}
