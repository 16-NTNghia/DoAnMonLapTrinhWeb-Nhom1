using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class ChucVu
{
    public int IdchucVu { get; set; }

    public string? TenChucVu { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();

    public virtual ICollection<Quyen> Idquyens { get; set; } = new List<Quyen>();
}
