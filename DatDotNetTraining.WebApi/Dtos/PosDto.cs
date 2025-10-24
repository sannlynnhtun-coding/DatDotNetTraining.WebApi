using System.ComponentModel.DataAnnotations;

namespace DatDotNetTraining.WebApi.Dtos;

public class PosDto
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool IsDelete { get; set; }
}

public class PosCreateRequestDto
{
    [Required]
    public string ProductName { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }
}

public class PosUpdateRequestDto
{
    public string? ProductName { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}

public class PosCreateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
