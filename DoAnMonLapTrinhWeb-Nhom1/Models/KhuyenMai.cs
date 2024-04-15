using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class KhuyenMai
{
    public int MaiKhuyenMai { get; set; }

    public string TenKhuyenMai { get; set; } = null!;

    public string NoiDungKhuyenMai { get; set; } = null!;

    public DateTime NgayKhuyenMai { get; set; }

    public double PhanTramKhhuyenMai { get; set; }
}
