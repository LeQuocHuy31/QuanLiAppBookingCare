using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLiAppBookingCare.Models
{
    public class LichKham
    {
        public int id { get; set; }
        public string chuyen_khoa { get; set; }
        public string bac_si { get; set; }
        public string ngay { get; set; }
        public string thoi_gian { get; set; }
        public string phong_kham { get; set; }
        public string loai_hinh { get; set; }
        public double gia { get; set; }
    }
}
