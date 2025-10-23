using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatDotNetTraining.WebApi.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDelete { get; set; }
}

public class ProductCreateRequestDto
{
    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}

public class ProductCreateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}

public class ProductUpdateRequestDto
{
    public string? ProductName { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}