using System;
using System.Collections.Generic;

namespace DatDotNetTraining.WebApi.Database.CLAppDbContextModels;

public partial class TblEmployee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Position { get; set; }

    public string? Department { get; set; }

    public decimal? Salary { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsDelete { get; set; }
}
