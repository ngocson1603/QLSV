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
using QLSV.Models;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminKhoaHocsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GameStoreDbContext _context;
        public INotyfService _notyfService { get; }
        public AdminKhoaHocsController(IUnitOfWork unitOfWork, INotyfService notyfService,GameStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
            _context = context;
        }

        // GET: Admin/AdminKhoaHocs
        public IActionResult Index()
        {
            var collection = _unitOfWork.KhoaHocRepository.GetAll().ToList();
            return View(collection);
        }

        // GET: Admin/AdminKhoaHocs/Details/5
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

        // GET: Admin/AdminKhoaHocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminKhoaHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,course_name,description,gia,NgayBatDau,NgayKetThuc")] KhoaHoc khoahoc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                //Xu ly Image
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(khoahoc.course_name) + extension;
                        khoahoc.Image = await Utilities.UploadFileBlog(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(khoahoc.Image)) khoahoc.Image = "default.jpg";
                    _context.KhoaHocs.Add(khoahoc);
                    _context.SaveChanges();
                    _notyfService.Success("Create Success");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/AdminKhoaHocs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KhoaHoc = _context.KhoaHocs.Find(id);
            if (KhoaHoc == null)
            {
                return NotFound();
            }
            return View(KhoaHoc);
        }

        // POST: Admin/AdminKhoaHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,course_name,description,gia,NgayBatDau,NgayKetThuc")] KhoaHoc khoahoc, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(khoahoc.course_name) + extension;
                        khoahoc.Image = await Utilities.UploadFileBlog(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(khoahoc.Image)) khoahoc.Image = "default.jpg";
                    _context.KhoaHocs.Update(khoahoc);
                    _context.SaveChanges();
                    _notyfService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/AdminKhoaHocs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var KhoaHoc = _context.KhoaHocs.Find(id);
            if (KhoaHoc == null)
            {
                return NotFound();
            }

            return View(KhoaHoc);
        }

        // POST: Admin/AdminKhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var KhoaHoc = _context.KhoaHocs.Find(id);
                _context.KhoaHocs.Remove(KhoaHoc);
                _context.SaveChanges();
                _notyfService.Success("Xóa thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
