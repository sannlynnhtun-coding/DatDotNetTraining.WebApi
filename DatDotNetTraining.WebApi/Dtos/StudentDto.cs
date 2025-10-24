using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatDotNetTraining.WebApi.Dtos;

public class StudentDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string FatherName { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDelete { get; set; }
}

public class StudentCreateRequestDto
{
    public string Name { get; set; }

    public string FatherName { get; set; }
}

public class StudentUpdateRequestDto
{
    public string? Name { get; set; }

    public string? FatherName { get; set; }
}

public class StudentCreateResponseDto
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}
