using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class DiaDiem
{
    public int MaDiaDiem { get; set; }

    public string TenDiaDiem { get; set; } = null!;

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
