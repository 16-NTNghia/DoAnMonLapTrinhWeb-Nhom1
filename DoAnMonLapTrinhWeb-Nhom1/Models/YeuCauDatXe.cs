using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class YeuCauDatXe
{
    public string Email { get; set; } = null!;

    public string BienSoXe { get; set; } = null!;

    public DateTime NgayNhan { get; set; }

    public DateTime NgayTra { get; set; }

    public int SoNgayThue { get; set; }

    public bool TrangThaiChapNhan { get; set; }

    public bool Hide { get; set; }

    public virtual Xe BienSoXeNavigation { get; set; } = null!;

    public virtual TaiKhoan EmailNavigation { get; set; } = null!;

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
