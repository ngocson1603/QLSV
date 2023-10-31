using System;
using System.Collections.Generic;

namespace QLSV.Models
{
    public class GiaoVien:BaseEntity
    {
        public string full_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public int? IdKhoaHoc { get; set; }
        public DateTime date_of_birth { get; set; }
        public KhoaHoc KhoaHoc { get; set; }
        public List<LichHoc> LichHocs { get; set; }
    }
}
