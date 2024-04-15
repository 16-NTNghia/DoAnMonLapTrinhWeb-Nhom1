using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class HoaDon
{
    public int MaHoaDon { get; set; }

    public DateTime NgayLap { get; set; }

    public decimal TongDonGia { get; set; }

    public int MaPhuongThuc { get; set; }

    public string Email { get; set; } = null!;

    public string BienSoXe { get; set; } = null!;

    public virtual PhuongThucThanhToan MaPhuongThucNavigation { get; set; } = null!;

    public virtual YeuCauDatXe YeuCauDatXe { get; set; } = null!;
}
