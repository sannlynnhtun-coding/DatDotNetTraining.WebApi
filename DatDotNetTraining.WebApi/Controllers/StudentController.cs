using Dapper;
using DatDotNetTraining.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DatDotNetTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly SqlConnectionStringBuilder _sb;

    public StudentController()
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
    public IActionResult GetStudents()
    {
        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        var lst = db.Query<StudentDto>("SELECT * FROM Tbl_Student WHERE IsDelete = 0").ToList();
        return Ok(lst);
    }

    [HttpPost]
    public IActionResult CreateStudent(StudentCreateRequestDto request)
    {
        var studentDto = new StudentDto
        {
            Name = request.Name,
            FatherName = request.FatherName,
            CreatedBy = "System",
            CreatedDate = DateTime.Now,
            IsDelete = false
        };

        string query = @"INSERT INTO Tbl_Student
            (Name, FatherName, CreateBy, CreateDate, IsDelete)
            VALUES (@Name, @FatherName, @CreatedBy, @CreatedDate, @IsDelete)";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        int result = db.Execute(query, studentDto);

        var model = new StudentCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Student created successfully." : "Failed to create student."
        };
        return Ok(model);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateStudent(int id, StudentUpdateRequestDto request)
    {
        var studentDto = new StudentDto
        {
            Id = id,
            Name = request.Name,
            FatherName = request.FatherName,
            ModifiedBy = "System",
            ModifiedDate = DateTime.Now
        };

        string conditions = "";

        if (!string.IsNullOrEmpty(request.Name))
        {
            conditions += "[Name] = @Name, ";
        }
        if (!string.IsNullOrEmpty(request.FatherName))
        {
            conditions += "[FatherName] = @FatherName, ";
        }

        if (conditions.Length == 0)
        {
            return Ok(new StudentCreateResponseDto
            {
                IsSuccess = false,
                Message = "No fields to update."
            });
        }

        conditions = conditions.TrimEnd(',', ' ');

        string query = $@"UPDATE Tbl_Student
            SET {conditions},
                ModifyBy = @ModifiedBy,
                ModifyDate = @ModifiedDate
            WHERE Id = @Id";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        int result = db.Execute(query, studentDto);

        var model = new StudentCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Student updated successfully." : "Failed to update student."
        };
        return Ok(model);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var model = new StudentCreateResponseDto();
        string query = @"UPDATE Tbl_Student SET IsDelete = 1 WHERE Id = @Id";

        using IDbConnection db = new SqlConnection(_sb.ConnectionString);
        db.Open();
        int result = db.Execute(query, new { Id = id });

        model = new StudentCreateResponseDto
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Student deleted successfully." : "Failed to delete student."
        };
        return Ok(model);
    }
}
