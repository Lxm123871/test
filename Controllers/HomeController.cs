using FAS202024131135.Data;
using FAS202024131135.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Diagnostics.Metrics;

namespace FAS202024131135.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FAS202024131135Context _context;
        private IList<Asset> assets;

        public HomeController(ILogger<HomeController> logger, FAS202024131135Context context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
           
        }
 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

        // 处理登录请求
        public IActionResult Login(string uid, string pwd)
        {
            //取得会员对象
            //var member = new MemberList().GetMember(uid, pwd); 用邮箱来登录
            Employee employee = _context.Employees.FirstOrDefault(e => e.Email == uid && e.Password == pwd);
            if (employee != null)
            {
                //建立身份声明
                IList<Claim> claims = new List<Claim> {
                 new Claim(ClaimTypes.Name, employee.EmployeeName),
                 new Claim(ClaimTypes.Role, employee.Role.Trim())
                 };
                //建立身份识别对象,并指定账号与角色
                var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };
                //进行登录动作,并带入身份识别对象
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity), authProperties);
                //重定向至会员页

                // 用户已通过身份验证
                // 执行相关操作...
                TempData["Message"] = "账号或密码错误!";
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Message = "账号或密码错误!";
            return View("Login");
        }

        // 注销功能
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        // GET: Employee/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("EmployeeID,EmployeeName,Password,Phone,Role,Email,Sector")] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                //member.Role = "NotAssign"; //默认注册会员尚未指派角色,由管理指派
                employee.Role = "Employee"; //或直接指派为普通员工 由管理员管理员工的身份
                /*employee.RegisterDate = DateTime.Now;*/

                _context.Add(employee);
                await _context.SaveChangesAsync();
                //重定向至登录页
                return RedirectToAction("Login", "Home");
            }
            return View(employee);
        }

    }
}