using FAS202024131135.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using FAS202024131135.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//注入MovieSkyContext，访问数据库
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FAS202024131135Context>(options =>
        options.UseSqlServer(conn));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//添加认证方法,使用cookie认证
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //只能透过http(s)来访问
        options.Cookie.HttpOnly = true;
        //未登录时重定向至index
        options.LoginPath = new PathString("/Home/Login");
        //拒绝访问时重定向至index
        options.AccessDeniedPath = new PathString("/Home/Login");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//创建初始数据库
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FAS202024131135Context>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//启用cookie策略
app.UseCookiePolicy();
//启用身份认证
app.UseAuthentication();
//启用授权功能
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();