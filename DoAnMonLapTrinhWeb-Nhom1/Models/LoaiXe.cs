using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class LoaiXe
{
    public int MaLoai { get; set; }

    public string? TenLoai { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
