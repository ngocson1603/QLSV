using AspNetCoreHero.ToastNotification.Abstractions;
using QLSV.Models;
using QLSV.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDiemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public AdminDiemController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }
        // GET: SaleController
        public ActionResult Index()
        {
            var sale = _unitOfWork.KhoaHocRepository.GetAll();
            return View(sale);
        }
        // GET: SaleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult ViewProductSale(int id)
        {
            try
            {
                HttpContext.Session.SetInt32("SaleId", id);
                var ls = _unitOfWork.HocSinhRepository.listhocsinh(id).ToList();
                return View(ls);
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult ViewProductSaleInDate()
        {
            try
            {
                var ls = _unitOfWork.HocSinhRepository.listhocsinhdamua().ToList();
                return View(ls);
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }

        }
        // POST: AdminProductsController/Edit/5
        public ActionResult Edits(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleid = HttpContext.Session.GetInt32("SaleId");
            var product = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdHocSinh == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edits(int id, DiemHocSinh saleModelView)
        {
            try
            {
                var solan = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdHocSinh == saleModelView.IdHocSinh && t.IdKhoaHoc == saleModelView.IdKhoaHoc);
                DiemHocSinh diem = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdHocSinh == id && t.IdKhoaHoc == saleModelView.IdKhoaHoc).FirstOrDefault();
                diem.SoDiem = saleModelView.SoDiem;
                diem.NhanXet = saleModelView.NhanXet;
                diem.SoLan = solan.Count() + 1;
                _unitOfWork.DiemHocSinhRepository.Update(diem);
                _unitOfWork.SaveChange();
                _notyfService.Success("Update successful");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }

            return View(saleModelView);
        }
        public ActionResult DeleteSeleProduct(int id)
        {
            try
            {
                var product = _unitOfWork.DiemHocSinhRepository.GetById(id);
                _unitOfWork.DiemHocSinhRepository.Delete(product);
                _unitOfWork.SaveChange();
                _notyfService.Success("Delete successful");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: SaleController/Edit/5
        public ActionResult Edit(int id)
        {
            var ls = _unitOfWork.HocSinhRepository.listhocsinh(id).ToList();
            var tenkhoa = _unitOfWork.KhoaHocRepository.GetById(id);
            ViewBag.SaleId = id.ToString();
            ViewBag.TenKhoa = tenkhoa.course_name;
            return View(ls);
        }

        [HttpPost]
        [Route("/Sale/AjaxMethod", Name = "AjaxMethod")]
        public JsonResult AjaxMethod(string saleId, string productId, string discount, string nhanxet)
        {
            if (_unitOfWork.KhoaHocRepository.GetById(int.Parse(productId)) == null)
                return null;
            try
            {
                //var hocsinh = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdHocSinh == int.Parse(saleId) && t.IdKhoaHoc == int.Parse(productId));
                _unitOfWork.DiemHocSinhRepository.Create(new DiemHocSinh()
                {
                    IdHocSinh = int.Parse(saleId),
                    IdKhoaHoc = int.Parse(productId),
                    SoDiem = int.Parse(discount),
                    NhanXet = nhanxet,
                });
                _unitOfWork.SaveChange();
            }
            catch (Exception)
            {
                throw;
            }

            return Json(1);
        }

        [HttpPost]
        public void SaveSaleProduct(string saleId, string productId, string discount)
        {

        }

        // POST: SaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private void deletesale(int id)
        {
            var sale = _unitOfWork.KhoaHocRepository.GetById(id);
            _unitOfWork.KhoaHocRepository.Delete(sale);
            _unitOfWork.SaveChange();
        }
        // POST: SaleController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _unitOfWork.DiemHocSinhRepository.GetAll().Where(t => t.IdKhoaHoc == id).ToList();
                foreach (var item in product)
                {
                    _unitOfWork.DiemHocSinhRepository.Delete(item);
                    _unitOfWork.SaveChange();
                }

                deletesale(id);
                _notyfService.Success("Delete successful");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                _notyfService.Error("Error");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
