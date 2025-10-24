using System;
using System.Collections.Generic;

namespace DatDotNetTraining.WebApi.Database.CLAppDbContextModels;

public partial class TblPo
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsDelete { get; set; }
}
