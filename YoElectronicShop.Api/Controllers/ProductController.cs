using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoElectronicShop.Api.Entities;
using YoElectronicShop.Api.Extensions;
using YoElectronicShop.Api.Repositories.Contracts;
using YoElectronicShop.Models.DTO;

namespace YoElectronicShop.Api.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems() 
        {
            try
            {
                var Products = await this.productRepository.GetItems();
                var ProductCategories = await this.productRepository.GetCategories();

                if( Products == null  || ProductCategories == null ) {
                return NotFound();
                }
                else
                {
                    var productDtos = Products.ConvertToDto(ProductCategories);
                    return Ok(productDtos);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }


        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var Product = await this.productRepository.GetItem(id);
                

                if (Product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var ProductCategory =  await this.productRepository.GetCategory(Product.CategoryId);
                    var productDto = Product.ConvertToDto(ProductCategory);
                    return Ok(productDto);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }


        }
        [HttpGet]
        [Route(nameof(GetProductCategories))]
         public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await productRepository.GetCategories();
                var productCategoryDtos = productCategories.ConvertToDto();
                return Ok(productCategoryDtos);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
        {

            try
            {

                var products = await productRepository.GetItemsByCategory(categoryId);
                var productCategories = await productRepository.GetCategories();
                var productDto = products.ConvertToDto(productCategories);
                return Ok(productDto);


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
          
        }

    }



    }

