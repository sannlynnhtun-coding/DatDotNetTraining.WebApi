using System;
using System.Collections.Generic;

namespace DatDotNetTraining.WebApi.Database.CLAppDbContextModels;

public partial class TblStudent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? FatherName { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsDelete { get; set; }
}
