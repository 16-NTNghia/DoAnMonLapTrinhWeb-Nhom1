using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class Quyen
{
    public int Idquyen { get; set; }

    public string? TenQuyen { get; set; }

    public virtual ICollection<ChucVu> IdchucVus { get; set; } = new List<ChucVu>();
}
