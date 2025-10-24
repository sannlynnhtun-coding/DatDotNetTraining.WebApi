using DatDotNetTraining.WebApi.Database;
using DatDotNetTraining.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DatDotNetTraining.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductV2Controller()
        {
            _db = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var lst = _db.Products
                .Where(x => x.IsDelete == false)
                .ToList();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateRequestDto request)
        {
            Tbl_Product item = new Tbl_Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
            };
            _db.Products.Add(item);
            int result = _db.SaveChanges();
            var model = new ProductCreateResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product created successfully." : "Failed to create product."
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateRequestDto request)
        {
            //foreach (var x in _db.Products)
            //{
            //    if (x.Id == id) return x;
            //}
            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(request.ProductName))
            {
                product.ProductName = request.ProductName;
            }
            if(request.Price > 0)
            {
                product.Price = request.Price ?? 0;
            }
            if (request.Quantity > 0)
            {
                product.Quantity = request.Quantity ?? 0;
            }
            int result = _db.SaveChanges();
            var model = new ProductCreateResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product updated successfully." : "Failed to update product."
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product is null)
            {
                return NotFound();
            }

            product.IsDelete = true;
            int result = _db.SaveChanges();
            var model = new ProductCreateResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Product deleted successfully." : "Failed to delete product."
            };
            return Ok(model);
        }
    }
}
