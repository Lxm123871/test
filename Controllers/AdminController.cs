using FAS202024131135.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FAS202024131135.Data;

namespace AMS202024131135.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly FAS202024131135Context _context;
        private IList<Asset> assets; 
        private string _path; //图片路径变项

        public AdminController(FAS202024131135Context context, IHostEnvironment environment)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //设置相片的文件夹路径,透过构造方法取得
            _path = environment.ContentRootPath + "//wwwroot//images";
        }

        //管理员的资产分类管理
        public IActionResult Index(string cat)
        {
            if (cat != null) //找特定类别相片,最新提交的8张相片,作为首页显示用
            {
                assets = _context.Assets.OrderBy(p => p.AssetID)
                .Where(p => p.Category.CategoryName.Equals(cat))
                .Include(p => p.Category).AsNoTracking()
                .OrderByDescending(p => p.Data)
                .Take(8)
                .ToList();
            }
            else //找最新提交的8张相片,作为首页显示用
            {
                assets = _context.Assets.OrderBy(p => p.AssetID)
                .Include(p => p.Category).AsNoTracking()
                .OrderByDescending(p => p.Data)
                .Take(8)
                .ToList();
            }
            var catNames = _context.Categories.Select(c => c.CategoryName).ToList();
            ViewBag.catNames = catNames;
            return View(assets);
        }

    }
}
