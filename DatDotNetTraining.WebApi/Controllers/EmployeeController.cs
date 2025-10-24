using Dapper;
using DatDotNetTraining.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DatDotNetTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly SqlConnectionStringBuilder _sb;

    public EmployeeController()
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
    public IActionResult GetEmployees()
    {
        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        var employees = db.Query<EmployeeDto>("SELECT * FROM Tbl_Employee WHERE IsDelete = 0").ToList();
        return Ok(employees);
    }

    [HttpPost]
    public IActionResult CreateEmployee(EmployeeCreateRequestDto request)
    {
        var employee = new EmployeeDto
        {
            FullName = request.FullName,
            Position = request.Position,
            Department = request.Department,
            Salary = request.Salary,
            CreateBy = "System",
            CreateDate = DateTime.Now,
            IsDelete = false
        };

        string query = @"INSERT INTO Tbl_Employee
            (FullName, Position, Department, Salary, CreateBy, CreateDate, IsDelete)
            VALUES (@FullName, @Position, @Department, @Salary, @CreateBy, @CreateDate, @IsDelete)";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        int result = db.Execute(query, employee);

        return Ok(new EmployeeCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Employee created successfully." : "Failed to create employee."
        });
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeUpdateRequestDto request)
    {
        var employee = new EmployeeDto
        {
            Id = id,
            FullName = request.FullName ?? "",
            Position = request.Position ?? "",
            Department = request.Department ?? "",
            Salary = request.Salary ?? 0,
            ModifyBy = "System",
            ModifyDate = DateTime.Now
        };

        string conditions = "";
        if (!string.IsNullOrEmpty(request.FullName)) conditions += "[FullName] = @FullName, ";
        if (!string.IsNullOrEmpty(request.Position)) conditions += "[Position] = @Position, ";
        if (!string.IsNullOrEmpty(request.Department)) conditions += "[Department] = @Department, ";
        if (request.Salary > 0) conditions += "[Salary] = @Salary, ";

        if (string.IsNullOrEmpty(conditions))
        {
            return Ok(new EmployeeCreateResponseDto
            {
                IsSuccess = false,
                Message = "No fields to update."
            });
        }

        conditions = conditions.TrimEnd(',', ' ');

        string query = $@"UPDATE Tbl_Employee SET {conditions},
            ModifyBy = @ModifyBy, ModifyDate = @ModifyDate
            WHERE Id = @Id";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        int result = db.Execute(query, employee);

        return Ok(new EmployeeCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Employee updated successfully." : "Failed to update employee."
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        string query = @"UPDATE Tbl_Employee SET IsDelete = 1 WHERE Id = @Id";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        int result = db.Execute(query, new { Id = id });

        return Ok(new EmployeeCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Employee deleted successfully." : "Failed to delete employee."
        });
    }
}
