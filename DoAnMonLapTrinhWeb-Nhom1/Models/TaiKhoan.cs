using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class TaiKhoan
{
    public string Email { get; set; } = null!;

    public string TenNguoiDung { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public DateTime NgaySinh { get; set; }

    public string? Gplx { get; set; }

    public string? Avarta { get; set; }

    public int IdchucVu { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ChucVu IdchucVuNavigation { get; set; } = null!;

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();

    public virtual ICollection<YeuCauDatXe> YeuCauDatXes { get; set; } = new List<YeuCauDatXe>();
}
