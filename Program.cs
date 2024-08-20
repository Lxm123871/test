using FAS202024131135.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using FAS202024131135.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//ע��MovieSkyContext���������ݿ�
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FAS202024131135Context>(options =>
        options.UseSqlServer(conn));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//�����֤����,ʹ��cookie��֤
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //ֻ��͸��http(s)������
        options.Cookie.HttpOnly = true;
        //δ��¼ʱ�ض�����index
        options.LoginPath = new PathString("/Home/Login");
        //�ܾ�����ʱ�ض�����index
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

//������ʼ���ݿ�
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

//����cookie����
app.UseCookiePolicy();
//���������֤
app.UseAuthentication();
//������Ȩ����
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();