using Dapper;
using DatDotNetTraining.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DatDotNetTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PosController : ControllerBase
{
    private readonly SqlConnectionStringBuilder _sb;

    public PosController()
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
    public IActionResult GetItems()
    {
        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        var items = db.Query<PosDto>("SELECT * FROM Tbl_Pos WHERE IsDelete = 0").ToList();
        return Ok(items);
    }

    [HttpPost]
    public IActionResult CreateItem(PosCreateRequestDto request)
    {
        var item = new PosDto
        {
            ProductName = request.ProductName,
            Quantity = request.Quantity,
            Price = request.Price,
            CreateBy = "System",
            CreateDate = DateTime.Now,
            IsDelete = false
        };

        string query = @"INSERT INTO Tbl_Pos
            (ProductName, Quantity, Price, CreateBy, CreateDate, IsDelete)
            VALUES (@ProductName, @Quantity, @Price, @CreateBy, @CreateDate, @IsDelete)";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        int result = db.Execute(query, item);

        return Ok(new PosCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "POS item created successfully." : "Failed to create POS item."
        });
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateItem(int id, PosUpdateRequestDto request)
    {
        var item = new PosDto
        {
            Id = id,
            ProductName = request.ProductName ?? "",
            Quantity = request.Quantity ?? 0,
            Price = request.Price ?? 0,
            ModifyBy = "System",
            ModifyDate = DateTime.Now
        };

        string conditions = "";
        if (!string.IsNullOrEmpty(request.ProductName)) conditions += "[ProductName] = @ProductName, ";
        if (request.Quantity > 0) conditions += "[Quantity] = @Quantity, ";
        if (request.Price > 0) conditions += "[Price] = @Price, ";

        if (string.IsNullOrEmpty(conditions))
        {
            return Ok(new PosCreateResponseDto
            {
                IsSuccess = false,
                Message = "No fields to update."
            });
        }

        conditions = conditions.TrimEnd(',', ' ');

        string query = $@"UPDATE Tbl_Pos SET {conditions},
            ModifyBy = @ModifyBy, ModifyDate = @ModifyDate
            WHERE Id = @Id";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        int result = db.Execute(query, item);

        return Ok(new PosCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "POS item updated successfully." : "Failed to update POS item."
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteItem(int id)
    {
        string query = @"UPDATE Tbl_Pos SET IsDelete = 1 WHERE Id = @Id";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        int result = db.Execute(query, new { Id = id });

        return Ok(new PosCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "POS item deleted successfully." : "Failed to delete POS item."
        });
    }
}
