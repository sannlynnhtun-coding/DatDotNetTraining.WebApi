using Dapper;
using DatDotNetTraining.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DatDotNetTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly SqlConnectionStringBuilder _sb;

    public ProductController()
    {
        _sb = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DAT_Dev",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        var lst = db.Query<ProductDto>("select * from tbl_product where IsDelete = 0;").ToList();
        return Ok(lst);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductCreateRequestDto request)
    {
        ProductDto productDto = new ProductDto
        {
            ProductName = request.ProductName,
            Price = request.Price,
            Quantity = request.Quantity,
            CreatedBy = "System",
            CreatedDate = DateTime.Now,
            IsDelete = false
        };
        string query = @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[Price]
           ,[Quantity]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[IsDelete])
     VALUES
           (@ProductName
           ,@Price
           ,@Quantity
           ,@CreatedBy
           ,@CreatedDate
           ,@IsDelete)";
        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        int result = db.Execute(query, productDto);
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
        var model = new ProductCreateResponseDto();
        ProductDto productDto = new ProductDto
        {
            ProductId = id,
            ProductName = request.ProductName,
            Price = request.Price ?? 0,
            Quantity = request.Quantity ?? 0,
            ModifiedBy = "System",
            ModifiedDate = DateTime.Now,
        };
        string conditions = "";

        if (!string.IsNullOrEmpty(request.ProductName))
        {
            conditions += "[ProductName] = @ProductName, ";
        }
        if (request.Price > 0)
        {
            conditions += "[Price] = @Price, ";
        }
        if (request.Quantity > 0)
        {
            conditions += "[Quantity] = @Quantity, ";
        }

        if (conditions.Length == 0)
        {
            model = new ProductCreateResponseDto
            {
                IsSuccess = false,
                Message = "No fields to update."
            };
            return Ok(model);
        }

        //conditions = conditions.Substring(0, conditions.Length - 2);

        string query = $@"UPDATE [dbo].[Tbl_Product]
   SET {conditions}
[ModifiedBy] = @ModifiedBy,
[ModifiedDate] = @ModifiedDate
 WHERE ProductId = @ProductId";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        int result = db.Execute(query, productDto);
        model = new ProductCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Product updated successfully." : "Failed to update product."
        };
        return Ok(model);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var model = new ProductCreateResponseDto();
        string query = @"UPDATE [dbo].[Tbl_Product] SET IsDelete = 1 WHERE ProductId = @ProductId";
        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        int result = db.Execute(query, new { ProductId = id });
        model = new ProductCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Product deleted successfully." : "Failed to delete product."
        };
        return Ok(model);
    }
}
