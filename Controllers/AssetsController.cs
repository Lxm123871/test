using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FAS202024131135.Data;
using FAS202024131135.Models;
using Microsoft.Extensions.Hosting;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FAS202024131135.Controllers
{
    public class AssetsController : Controller
    {
        private readonly FAS202024131135Context _context;
        private string _path;  //图片路径变项

        public AssetsController(FAS202024131135Context context, IHostEnvironment environment)
        {
            _context = context;
            //设置相片的文件夹路径,透过构造方法取得
            _path = environment.ContentRootPath + "//wwwroot//images";
        }

        // GET: Assets
        public async Task<IActionResult> Index(string searchString)
        {
            /*var fAS202024131135Context = _context.Assets.Include(a => a.Categories).Include(a => a.Employees);
            return View(await fAS202024131135Context.ToListAsync());*/
            ViewData["CurrentFilter"] = searchString;
            var fAS202024131135Context = _context.Assets.Include(m => m.Category).Include(m => m.Employee);
            if (!string.IsNullOrEmpty(searchString))
            {
                //Equals为精准查询  Contans为范查询 若查询关键字为含模糊查询的字段也会返回该条数据
                fAS202024131135Context = _context.Assets
                .Where(m => m.AssetName.Contains(searchString)
                                      || m.AssetID.ToString().Contains(searchString)
                                     || m.Category.CategoryName.Contains(searchString)
                                        || m.StorageLocation.Contains(searchString)
                                     || m.Employee.EmployeeName.Contains(searchString)
                                     || m.Data.ToString().Contains(searchString))
                    .Include(m => m.Category)
                    .Include(m => m.Employee);
            }
            return View(await fAS202024131135Context.ToListAsync());
        }



        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        /*https://juejin.cn/post/7226974035464192055*/

        // GET: Assets/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName");
            return View();
        }


        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Asset asset, IFormFile imgFile)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }*/
            if (ModelState.IsValid)
            {
                if (imgFile != null)
                {
                    if (imgFile.Length > 0)
                    {
                        //相片提交
                        string fileName = $"{Guid.NewGuid().ToString()}.{Path.GetExtension(imgFile.FileName).Substring(1)}";
                        string savePath = $"{_path}\\{fileName}";
                        using (var steam = new FileStream(savePath, FileMode.Create))
                        {
                            await imgFile.CopyToAsync(steam);
                        }

                        //相片信息写入记录
                        asset.CategoryPhoto = fileName;
                        _context.Add(asset);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName");
            return View(asset);
        }


        // GET: Assets/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName");
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("AssetID,AssetName,AssetSize,Price,Data,StorageLocation,CategoryID,EmployeeID")] Asset asset, IFormFile imgFile, string OldPicture)
        {
            if (id != asset.AssetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imgFile != null)
                    {
                        if (imgFile.Length > 0)  //有更新相片
                        {
                            //先删原相片
                            System.IO.File.Delete($"{_path}\\{OldPicture}");

                            //相片提交
                            string fileName = $"{Guid.NewGuid().ToString()}.{Path.GetExtension(imgFile.FileName).Substring(1)}";
                            string savePath = $"{_path}\\{fileName}";
                            using (var steam = new FileStream(savePath, FileMode.Create))
                            {
                                await imgFile.CopyToAsync(steam);
                            }

                            //相片信息写入记录
                            asset.CategoryPhoto = fileName;
                            //_context.Add(movie);
                            _context.Update(asset);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName");
            return View(asset);
        }


        // GET: Assets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assets == null)
            {
                return Problem("Entity set 'FAS202024131135Context.Assets'  is null.");
            }
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                //删除相片档
                System.IO.File.Delete($"{_path}\\{asset.CategoryPhoto}");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
          return (_context.Assets?.Any(e => e.AssetID == id)).GetValueOrDefault();
        }
    }
}
