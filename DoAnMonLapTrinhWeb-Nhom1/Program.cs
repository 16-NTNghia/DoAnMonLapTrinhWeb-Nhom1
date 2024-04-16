using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(options =>
{
    options.Cookie.Name = "N1BikerentCookie";
    options.LoginPath = "/Home/Index";

    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.Cookie.MaxAge = options.ExpireTimeSpan; // optional
    options.SlidingExpiration = true;
});

var connectionString = builder.Configuration.GetConnectionString("WebsiteBanHangConnection");
builder.Services.AddDbContext<QuanLyThueXeMayTuLaiContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "trang-chu", // Đặt tên cho tuyến đường
        pattern: "trang-chu", // Đường dẫn mới
        defaults: new { controller = "Home", action = "Index" }); // Controller và action tương ứng
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly",
        pattern: "admin-quan-ly",
        defaults: new { controller = "Manager", action = "Index" });

    endpoints.MapControllerRoute(
        name: "nhan-vien-quan-ly",
        pattern: "nhan-vien-quan-ly",
        defaults: new { controller = "Manager", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "khach-hang",
        pattern: "khach-hang",
        defaults: new { controller = "CustomerBike", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-tai-khoan",
        pattern: "admin-quan-ly-tai-khoan",
        defaults: new { controller = "User", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-hang-xe",
        pattern: "admin-quan-ly-hang-xe",
        defaults: new { controller = "HangXe", action = "Index" }); 
    
    endpoints.MapControllerRoute(
        name: "nhan-vien-quan-ly-hang-xe",
        pattern: "nhan-vien-quan-ly-hang-xe",
        defaults: new { controller = "HangXe", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-loai-xe",
        pattern: "admin-quan-ly-loai-xe",
        defaults: new { controller = "LoaiXe", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "nhan-vien-quan-ly-loai-xe",
        pattern: "nhan-vien-quan-ly-loai-xe",
        defaults: new { controller = "LoaiXe", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-xe",
        pattern: "admin-quan-ly-xe",
        defaults: new { controller = "Bike", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "nhan-vien-quan-ly-xe",
        pattern: "nhan-vien-quan-ly-xe",
        defaults: new { controller = "Bike", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-hoa-don",
        pattern: "admin-quan-ly-hoa-don",
        defaults: new { controller = "HoaDon", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "nhan-vien-quan-ly-hoa-don",
        pattern: "nhan-vien-quan-ly-hoa-don",
        defaults: new { controller = "HoaDon", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-khuyen-mai",
        pattern: "admin-quan-ly-khuyen-mai",
        defaults: new { controller = "Promo", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "nhan-vien-quan-ly-khuyen-mai",
        pattern: "nhan-vien-quan-ly-khuyen-mai",
        defaults: new { controller = "Promo", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-dia-diem",
        pattern: "admin-quan-ly-dia-diem",
        defaults: new { controller = "DiaDiem", action = "Index" });
    
    endpoints.MapControllerRoute(
        name: "admin-quan-ly-thanh-toan",
        pattern: "admin-quan-ly-thanh-toan",
        defaults: new { controller = "ThanhToan", action = "Index" });
    
    endpoints.MapControllerRoute(
       name: "admin-quan-ly-thanh-toan",
       pattern: "admin-quan-ly-thanh-toan",
       defaults: new { controller = "ThanhToan", action = "Index" });
    /* endpoints.MapControllerRoute(
   name: "sua-thong-tin",
   pattern: "sua-thong-tin",
   defaults: new { controller = "User", action = "Update" });*/
    endpoints.MapControllerRoute(
      name: "tao-moi-tai-khoan",
      pattern: "tao-moi-tai-khoan",
      defaults: new { controller = "User", action = "Add" });
    
    endpoints.MapControllerRoute(
      name: "thong-tin-chi-tiet",
      pattern: "thong-tin-chi-tiet",
      defaults: new { controller = "User", action = "Display" });
    /*endpoints.MapControllerRoute(
 name: "xoa-tai-khoan",
 pattern: "xoa-tai-khoan",
 defaults: new { controller = "User", action = "Delete" });*/



    endpoints.MapControllerRoute(
        name: "them-hang-xe",
        pattern: "them-hang-xe",
        defaults: new { controller = "HangXe", action = "Create" });
    
    endpoints.MapControllerRoute(
        name: "sua-hang-xe",
        pattern: "sua-hang-xe",
        defaults: new { controller = "HangXe", action = "Edit" });
    
    endpoints.MapControllerRoute(
        name: "xoa-hang-xe",
        pattern: "xoa-hang-xe",
        defaults: new { controller = "HangXe", action = "Delete" });



    endpoints.MapControllerRoute(
        name: "sua-loai-xe",
        pattern: "sua-loai-xe",
        defaults: new { controller = "LoaiXe", action = "Edit" });
    
    endpoints.MapControllerRoute(
        name: "xoa-loai-xe",
        pattern: "xoa-loai-xe",
        defaults: new { controller = "LoaiXe", action = "Delete" });
    
    endpoints.MapControllerRoute(
        name: "them-loai-xe",
        pattern: "them-loai-xe",
        defaults: new { controller = "LoaiXe", action = "Create" });



    /*endpoints.MapControllerRoute(
name: "sua-thong-tin-xe",
pattern: "sua-thong-tin-xe",
defaults: new { controller = "Bike", action = "Update" });
    endpoints.MapControllerRoute(
name: "xoa-xe",
pattern: "xoa-xe",
defaults: new { controller = "Bike", action = "Delete" });*/
    endpoints.MapControllerRoute(
        name: "chi-tiet-xe",
        pattern: "chi-tiet-xe",
        defaults: new { controller = "Bike", action = "Display" });


    endpoints.MapControllerRoute(
        name: "sua-khuyen-mai",
        pattern: "sua-khuyen-mai",
        defaults: new { controller = "Promo", action = "Update" });
    
    endpoints.MapControllerRoute(
        name: "xoa-khuyen-mai",
        pattern: "xoa-khuyen-mai",
        defaults: new { controller = "Promo", action = "Delete" });
    
    endpoints.MapControllerRoute(
        name: "chi-tiet-khuyen-mai",
        pattern: "chi-tiet-khuyen-mai",
        defaults: new { controller = "Promo", action = "Display" });
    
    endpoints.MapControllerRoute(
        name: "tao-khuyen-mai",
        pattern: "tao-khuyen-mai",
        defaults: new { controller = "Promo", action = "Add" });


    endpoints.MapControllerRoute(
        name: "sua-dia-diem",
        pattern: "sua-dia-diem",
        defaults: new { controller = "DiaDiem", action = "Edit" });
    
    endpoints.MapControllerRoute(
        name: "xoa-dia-diem",
        pattern: "xoa-dia-diem",
        defaults: new { controller = "DiaDiem", action = "Delete" });
    
    endpoints.MapControllerRoute(
        name: "tao-dia-diem",
        pattern: "tao-dia-diem",
        defaults: new { controller = "DiaDiem", action = "Create" });


    endpoints.MapControllerRoute(
        name: "them-phuong-thuc-thanh-toan",
        pattern: "them-phuong-thuc-thanh-toan",
        defaults: new { controller = "ThanhToan", action = "Create" });
    
    endpoints.MapControllerRoute(
        name: "Sua-phuong-thuc-thanh-toan",
        pattern: "Sua-phuong-thuc-thanh-toan",
        defaults: new { controller = "ThanhToan", action = "Edit" });
    
    endpoints.MapControllerRoute(
        name: "xoa-phuong-thuc-thanh-toan",
        pattern: "xoa-phuong-thuc-thanh-toan",
        defaults: new { controller = "ThanhToan", action = "Delete" });



    endpoints.MapControllerRoute(
        name: "xe",
        pattern: "xe",
        defaults: new { controller = "RentBike", action = "Index" });




    endpoints.MapControllerRoute(
        name: "kiem-tra-dat-xe",
        pattern: "kiem-tra-dat-xe",
        defaults: new { controller = "CustomerBike", action = "CheckOrder" });



    endpoints.MapControllerRoute(
        name: "them-xe",
        pattern: "them-xe",
        defaults: new { controller = "CustomerBike", action = "Add" });
    
    endpoints.MapControllerRoute(
        name: "cap-nhat-",
        pattern: "cap-nhat",
        defaults: new { controller = "CustomerBike", action = "Update" });
    
    endpoints.MapControllerRoute(
        name: "chi-tiet",
        pattern: "chi-tiet",
        defaults: new { controller = "CustomerBike", action = "Display" });


    endpoints.MapControllerRoute(
        name: "sua-thong-tin",
        pattern: "sua-thong-tin",
        defaults: new { controller = "UserInfor", action = "Update" });
    
    endpoints.MapControllerRoute(
        name: "doi-mat-khau",
        pattern: "doi-mat-khau",
        defaults: new { controller = "UserInfor", action = "ChangePassword" });


    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
