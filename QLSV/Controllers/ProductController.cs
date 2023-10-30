using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV;
using QLSV.Models;
using QLSV.OtpModels;
using PagedList.Core;
using System.Drawing.Printing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using QLSV.Extension;
using QLSV.Enums;

namespace QLSV.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService _service;
        public ProductController(IUnitOfWork unitOfWork,IService service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
        }
        [Route("/academics.html", Name = "Index")]
        public IActionResult Index()
        {
            TempData.Keep("idpro");
            var x = _unitOfWork.KhoaHocRepository.GetAll();
            return View(x);
        }
        public void GetDiemHocSinh()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            ViewBag.GetId = taikhoanID;
            if (taikhoanID != null)
            {
                var khachhang = _unitOfWork.HocSinhRepository.GetAll().SingleOrDefault(x => x.Id == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var proLib = _unitOfWork.DiemHocSinhRepository.getDiemHocSinh(khachhang.Id);
                    ViewBag.DonHang = proLib;
                }
            }
        }
        public static string id1;
        public static int maxPage;
    }
}
