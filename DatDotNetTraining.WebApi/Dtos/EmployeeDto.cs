using System.ComponentModel.DataAnnotations;

namespace DatDotNetTraining.WebApi.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string? ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
    public bool IsDelete { get; set; }
}

public class EmployeeCreateRequestDto
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

public class EmployeeUpdateRequestDto
{
    public string? FullName { get; set; }
    public string? Position { get; set; }
    public string? Department { get; set; }
    public decimal? Salary { get; set; }
}

public class EmployeeCreateResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
