using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class PhuongThucThanhToan
{
    public int MaPhuongThuc { get; set; }

    public string TenPhuongThuc { get; set; } = null!;

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
