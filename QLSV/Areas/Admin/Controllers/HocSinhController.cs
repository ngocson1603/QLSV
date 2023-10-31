using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV.Extension;
using QLSV.Helpper;
using QLSV.Models;
using QLSV.ModelViews;
using QLSV.OtpModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HocSinhController : Controller
    {
        public INotyfService _notyfService { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _service;

        public HocSinhController(INotyfService notyfService, IUnitOfWork unitOfWork, IService service)
        {
            _notyfService = notyfService;
            _unitOfWork = unitOfWork;
            _service = service;
        }

        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.HocSinhRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    //var lsDonHang = _context.Orders
                    //    .Include(x => x.TransactStatus)
                    //    .AsNoTracking()
                    //    .Where(x => x.CustomerId == khachhang.CustomerId)
                    //    .OrderByDescending(x => x.OrderDate)
                    //    .ToList();
                    //ViewBag.DonHang = lsDonHang;
                    //ViewBag.NumberOfGames = _unitOfWork.DiemHocSinhRepository.getDiemHocSinh(khachhang.Id).Count();
                    //ViewBag.NumberOfGamesRf = _unitOfWork.RefundRepository.listgameRefund(int.Parse(taikhoanID)).Count();
                    return View(khachhang);
                }
            }

            return RedirectToAction("Login", "HocSinh", new { Area = "Admin" });
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KhoaHoc = _unitOfWork.KhoaHocRepository.GetById((int)id);
            if (KhoaHoc == null)
            {
                return NotFound();
            }

            return View(KhoaHoc);
        }
        public ActionResult Index()
        {
            var sale = _unitOfWork.KhoaHocRepository.GetAll();
            return View(sale);
        }
        [Route("diem.html", Name = "OrderHistory")]
        public IActionResult OrderHistory()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var lsDonHang = _unitOfWork.DiemHocSinhRepository.GetAll()
                    .Where(x => x.IdHocSinh == int.Parse(taikhoanID))
                    .ToList();
                return View(lsDonHang);
            }
            return RedirectToAction("Login", "HocSinh", new { Area = "Admin" });
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public IActionResult DangkyTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> DangkyTaiKhoanAsync(RegisterViewModel taikhoan)
        {
            if (taikhoan.ConfirmPassword != taikhoan.Password)
            {
                _notyfService.Warning("The password confirmation does not match");
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                _notyfService.Warning("Please check your information and try again");
                return RedirectToAction("Index", "Home");
            }

            int kq = _service.HocSinhService.SignUp(taikhoan);

            if (kq == -1)
            {
                _notyfService.Warning("This email is already in use");
            }
            else if (kq == 0)
            {
                _notyfService.Error("Registration failed");
            }
            else
            {
                _unitOfWork.SaveChange();
                // Lưu Session KH
                var user = _unitOfWork.HocSinhRepository.FindByEmail(taikhoan.Email);
                SendVerifyEmail(user.Id);
                return View("VerifyAccount");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize, HttpPost]
        public IActionResult Refund(int productID)
        {
            if (ModelState.IsValid)
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                int userid = int.Parse(taikhoanID);
                try
                {
                    Refund refundrequest = _service.RefundService.refund(userid, productID);
                    _unitOfWork.RefundRepository.Create(refundrequest);
                    _unitOfWork.SaveChange();   
                    _notyfService.Success("Successfully");
                    return RedirectToAction(nameof(DiemHocSinh));
                }
                catch (Exception ex)
                {
                    //log
                    return RedirectToRoute("DiemHocSinh");
                }

            }
            return RedirectToRoute("DiemHocSinh");
        }

        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                return RedirectToAction("Dashboard", "HocSinh", new { Area = "Admin" });
            }
            return View();
        }


        public void SendVerifyEmail(int userId)
        {
            string verifyCode = Utilities.GetRandomKey(8);
            HttpContext.Session.SetString("SS_VerifyCode", verifyCode);
            HttpContext.Session.SetString("CustomerId", userId.ToString());

            if (_service.HocSinhService.SendVerification(userId, verifyCode))
                _notyfService.Success("A verification code has been sent to your email account");
            else
                _notyfService.Error("Failed to send the verification code");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyAccount()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyAccountAsync(string verifyCode)
        {
            string SS_verifyCode = HttpContext.Session.GetString("SS_VerifyCode");
            if (string.IsNullOrEmpty(SS_verifyCode))
            {
                _notyfService.Error("Couldn't find the verification code");
                return RedirectToAction("Index", "Home");
            }

            if (!SS_verifyCode.Equals(verifyCode))
            {
                _notyfService.Error("Verification code does not match");
                return View("VerifyAccount");
            }

            HttpContext.Session.Remove("SS_VerifyCode");
            HttpContext.Session.SetString("Role", "User");
            var user = _unitOfWork.HocSinhRepository.GetById(int.Parse(HttpContext.Session.GetString("CustomerId")));
            user.IsActive = true;
            _unitOfWork.HocSinhRepository.Update(user);
            _unitOfWork.SaveChange();

            // Identity
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.HoTen),
                        new Claim("CustomerId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "User")
                    };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customer)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Dev"))
            {
                _notyfService.Warning("Please logout your user account first");
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            if (!ModelState.IsValid)
            {
                _notyfService.Warning("Please check your information and try again");
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }

            int kq = _service.HocSinhService.SignIn(customer);

            if (kq == -1)
            {
                _notyfService.Error("Couldn't find your account");
            }
            else if (kq == 2)
            {
                _notyfService.Error("Incorrect email or password");
            }
            else
            {
                // Lưu Session KH
                var user = _unitOfWork.HocSinhRepository.FindByEmail(customer.Gmail);
                if (!user.IsActive)
                {
                    SendVerifyEmail(user.Id);
                    return View("VerifyAccount");
                }

                HttpContext.Session.SetString("CustomerId", user.Id.ToString());
                HttpContext.Session.SetString("Role", "User");

                // Identity
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.HoTen),
                        new Claim("CustomerId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "User")
                    };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                List<Cart> carts = HttpContext.Session.Get<List<Cart>>("_GioHang");
                if (carts != null)
                {
                    HttpContext.Session.Remove("_GioHang");
                    HttpContext.Session.Set("_GioHang", carts);
                }

                _notyfService.Success($"Welcome back, {user.HoTen}!");

                return RedirectToAction("Dashboard");
            }

            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Login", "HocSinh", new { Area = "Admin" });
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "HocSinh", new { Area = "Admin" });
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _unitOfWork.HocSinhRepository.GetById(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "HocSinh", new { Area = "Admin" });
                    var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    {
                        string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
                        taikhoan.Password = passnew;
                        _unitOfWork.HocSinhRepository.Update(taikhoan);
                        _unitOfWork.SaveChange();
                        _notyfService.Success("Change password successfully");
                        return RedirectToAction("Dashboard", "HocSinh", new { Area = "Admin" });
                    }
                }
            }
            catch
            {
                _notyfService.Success("Password change failed");
                return RedirectToAction("Dashboard", "HocSinh", new { Area = "Admin" });
            }
            _notyfService.Success("Password change failed");
            return RedirectToAction("Dashboard", "HocSinh", new { Area = "Admin" });
        }

        [Route("DiemHocSinh.html", Name = "DiemHocSinh")]
        public IActionResult DiemHocSinh()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.HocSinhRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var proLib = _unitOfWork.DiemHocSinhRepository.getDiemHocSinh(khachhang.Id);
                    ViewBag.DonHang = proLib;
                    return View(khachhang);
                }
            }
            return RedirectToAction("Login");
        }
        [Route("refund.html", Name = "Refund")]
        public IActionResult Refund()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var proLib = _unitOfWork.RefundRepository.listgameRefund(int.Parse(taikhoanID));
                return View(proLib);
            }
            return RedirectToAction("Login");
        }

    }
}
