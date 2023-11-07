using Microsoft.EntityFrameworkCore;
using YoElectronicShop.Api.Data;
using YoElectronicShop.Api.Entities;
using YoElectronicShop.Api.Repositories.Contracts;

namespace YoElectronicShop.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly YoElectronicShopDBContext yoElectronicShopDBContext;

        public ProductRepository(YoElectronicShopDBContext yoElectronicShopDBContext)
        {
            this.yoElectronicShopDBContext = yoElectronicShopDBContext;
        }
        
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var Categories = await this.yoElectronicShopDBContext.ProductCategories.ToListAsync();
            return Categories;
        }
        
        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await yoElectronicShopDBContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }
        
        public async Task<Product> GetItem(int id)
        {
            var Product = await yoElectronicShopDBContext.Products.FindAsync(id);
            return Product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var Products = await this.yoElectronicShopDBContext.Products.ToListAsync();
            return Products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await (from product in yoElectronicShopDBContext.Products
                                  where product.CategoryId == id
                                  select product).ToListAsync();
            return products;
        }
    }
}
