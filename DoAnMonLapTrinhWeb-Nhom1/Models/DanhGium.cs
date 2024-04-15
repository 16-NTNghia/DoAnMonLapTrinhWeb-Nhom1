using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class DanhGium
{
    public int? SoSao { get; set; }

    public string? NhanXet { get; set; }

    public string BienSoXe { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Xe BienSoXeNavigation { get; set; } = null!;

    public virtual TaiKhoan EmailNavigation { get; set; } = null!;
}
