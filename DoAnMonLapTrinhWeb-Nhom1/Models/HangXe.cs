using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class HangXe
{
    public int MaHangXe { get; set; }

    public string TenHang { get; set; } = null!;

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
