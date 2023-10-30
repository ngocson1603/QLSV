using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QLSV.Helpper;
using QLSV.Models;
using QLSV;
using Microsoft.AspNetCore.Http;
using PayPalCheckoutSdk.Orders;
using QLSV.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Dev,Admin")]
    public class AdminGiaoViensController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GameStoreDbContext _context;
        public INotyfService _notyfService { get; }
        public AdminGiaoViensController(IUnitOfWork unitOfWork, INotyfService notyfService,GameStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
        }

        // GET: Admin/AdminGiaoViens
        public IActionResult Index()
        {
            var collection = _unitOfWork.GiaoVienRepository.GetAll().ToList();
            return View(collection);
        }

        // GET: Admin/AdminGiaoViens/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GiaoVien = _unitOfWork.GiaoVienRepository.GetById((int)id);
            if (GiaoVien == null)
            {
                return NotFound();
            }

            return View(GiaoVien);
        }

        // GET: Admin/AdminGiaoViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminGiaoViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,full_name,email,password,phone_number,address,IdKhoaHoc,date_of_birth")] GiaoVien giaovien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GiaoVienRepository.Create(giaovien);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Update successful");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    _notyfService.Error("Error");
                    return RedirectToAction(nameof(Index));
                }
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            ViewData["khoahoc"] = new SelectList(_unitOfWork.KhoaHocRepository.GetAll(), "Id", "course_name");
            return View(giaovien);
        }

        // GET: Admin/AdminGiaoViens/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GiaoVien = _context.GiaoViens.Find(id);
            if (GiaoVien == null)
            {
                return NotFound();
            }
            return View(GiaoVien);
        }

        // POST: Admin/AdminGiaoViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,full_name,email,password,phone_number,address,IdKhoaHoc,date_of_birth")] GiaoVien giaovien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.GiaoViens.Update(giaovien);
                    _context.SaveChanges();
                    _notyfService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                ViewData["khoahoc"] = new SelectList(_unitOfWork.KhoaHocRepository.GetAll(), "Id", "course_name");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/AdminGiaoViens/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GiaoVien = _context.GiaoViens.Find(id);
            if (GiaoVien == null)
            {
                return NotFound();
            }

            return View(GiaoVien);
        }

        // POST: Admin/AdminGiaoViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var GiaoVien = _context.GiaoViens.Find(id);
                _context.GiaoViens.Remove(GiaoVien);
                _context.SaveChanges();
                _notyfService.Success("Xóa thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        [Route("tai-khoan-dev.html", Name = "InfoDev")]
        public IActionResult InfoDev()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.GiaoVienRepository.GetById(int.Parse(taikhoanID));
                if (khachhang != null)
                {
                    return View(khachhang);
                }
            }
            return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
        }

        [AllowAnonymous]
        [Route("logindev.html", Name = "Logindev")]
        public IActionResult LoginDev(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("logindev.html", Name = "Logindev")]
        public async Task<IActionResult> LoginDev(LoginDevViewModel model, string returnUrl = null)
        {
            try
            {
                    if (User.IsInRole("User"))
                    {
                        _notyfService.Warning("Please log out at User");
                        return RedirectToAction("Dashboard", "Users");
                    }
                    var kh = _unitOfWork.GiaoVienRepository.getDev(model.UserName);

                    if (kh == null)
                    {
                        ViewBag.Eror = "Login information is incorrect";
                        return View(model);
                    }
                    string pass = (model.Password.Trim());
                    // + kh.Salt.Trim()
                    if (kh.password.Trim() != pass)
                    {
                        ViewBag.Eror = "Login information is incorrect";
                        return View(model);
                    }
                    //đăng nhập thành công


                    var taikhoanID = HttpContext.Session.GetString("AccountId");
                    //identity
                    //luuw seccion Makh
                    HttpContext.Session.SetString("AccountId", kh.Id.ToString());
                    HttpContext.Session.SetString("Role", "Dev");
                    var Roles = HttpContext.Session.GetString("Role");
                    //identity
                    var userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, kh.full_name),
                            new Claim(ClaimTypes.Email, kh.email),
                            new Claim("AccountId", kh.Id.ToString()),
                            new Claim(ClaimTypes.Role, Roles)
                        };
                    var grandmaIdentity = new ClaimsIdentity(userClaims, "Dev Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    await HttpContext.SignInAsync(userPrincipal);



                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                
            }
            catch
            {
                return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
            }
            return RedirectToAction("LoginDev", "Dev", new { Area = "Admin" });
        }

    }
}
