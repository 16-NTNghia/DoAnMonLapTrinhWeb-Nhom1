using System;
using System.Collections.Generic;

namespace DoAnMonLapTrinhWeb_Nhom1.Models;

public partial class Xe
{
    public string BienSoXe { get; set; } = null!;

    public int MaDiaDiem { get; set; }

    public int MaHangXe { get; set; }

    public int MaLoai { get; set; }

    public string TenXe { get; set; } = null!;

    public string NamSanXuat { get; set; } = null!;

    public decimal GiaThue { get; set; }

    public string NhienLieu { get; set; } = null!;

    public string MaLucPhanKhoi { get; set; } = null!;

    public string? MoTa { get; set; }

    public bool TrangThai { get; set; }

    public bool Hide { get; set; }

    public string Email { get; set; } = null!;

    public string HinhAnh1 { get; set; }

    public string? HinhAnh2 { get; set; }
    public string? HinhAnh3 { get; set; }

    public string? HinhAnh4 { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual TaiKhoan EmailNavigation { get; set; } = null!;

    public virtual DiaDiem MaDiaDiemNavigation { get; set; } = null!;

    public virtual HangXe MaHangXeNavigation { get; set; } = null!;

    public virtual LoaiXe MaLoaiNavigation { get; set; } = null!;
    
    public virtual ICollection<YeuCauDatXe> YeuCauDatXes { get; set; } = new List<YeuCauDatXe>();
}
