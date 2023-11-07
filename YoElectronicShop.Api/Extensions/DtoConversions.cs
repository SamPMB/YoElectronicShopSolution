using YoElectronicShop.Api.Entities;
using YoElectronicShop.Models.DTO;

namespace YoElectronicShop.Api.Extensions
{
    public static class DtoConversions
    {
        
          public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
        {
            return (from productCategory in productCategories select new ProductCategoryDto
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                IconCss = productCategory.IconCss,


            }).ToList();

        }
        

        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> Products,
            IEnumerable<ProductCategory> productCategories)
        {

            return(from  product in Products join ProductCategory in productCategories
                   on product.CategoryId equals ProductCategory.Id select new ProductDto
                   {
                       Id = product.Id,
                       Name = product.Name,
                       Description = product.Description,
                       ImageUrl = product.ImageUrl,
                       Price = product.Price,
                       Qty = product.Qty,
                       CategoryId = product.CategoryId,
                       CategoryName = ProductCategory.Name
                   }).ToList();

        }
        public static ProductDto ConvertToDto(this Product Product,
           ProductCategory productCategory)
        {

            return new ProductDto
            {  
                   
                        Id = Product.Id,
                        Name = Product.Name,
                        Description = Product.Description,
                        ImageUrl = Product.ImageUrl,
                        Price = Product.Price,
                        Qty = Product.Qty,
                        CategoryId = Product.CategoryId,
                        CategoryName = productCategory.Name
                    };

        }
        
        public static IEnumerable<CartItemDto> ConvertToDto( this IEnumerable<CartItem> cartItems,
                                                            IEnumerable<Product> products)
        {
            return (from cartItem in cartItems join product in products
                     on cartItem.ProductId equals product.Id
                     select new CartItemDto {
                     
                     Id = cartItem.Id,
                     ProductId = cartItem.ProductId,
                     ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageUrl = product.ImageUrl,
                        Price= product.Price,
                        CartId = cartItem.CartId,
                        Qyt = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty,
                     }).ToList();



        }

        public static CartItemDto ConvertToDto(this CartItem cartItem,
                                                       Product product)
        {
            return new CartItemDto 
                  
                    {

                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageUrl = product.ImageUrl,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Qyt = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty,
                    };



        }









    }
}
