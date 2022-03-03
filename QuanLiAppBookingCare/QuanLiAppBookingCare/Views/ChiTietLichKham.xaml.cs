using QuanLiAppBookingCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuanLiAppBookingCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChiTietLichKham : ContentPage
    {
        LichKham GlobalLichKham;
        public ChiTietLichKham()
        {
            InitializeComponent();
        }
        public ChiTietLichKham(LichKham lichKham)
        {
            InitializeComponent();
            initHienThi(lichKham);
            GlobalLichKham = lichKham;
        }

        private void initHienThi(LichKham lichKham)
        {
            txtGia.Text = lichKham.gia.ToString();
            txtPhong.Text = lichKham.phong_kham;
            string[] buoi = new string[] { "Buổi sáng", "Buổi chiều", "Buổi tối" };
            string[] loaihinh = new string[] { "Trực tiếp", "Video Call" };
            prkThoiGian.ItemsSource = buoi;
            prkType.ItemsSource = loaihinh;
            string[] ngay = lichKham.ngay.Split('/');
            string ngayconverted = ngay.ElementAt(1) + "/" + ngay.ElementAt(0) + "/" + ngay.ElementAt(2);
            prkNgay.Date = DateTime.Parse(ngayconverted);
            prkThoiGian.SelectedIndex = Array.IndexOf(buoi, lichKham.thoi_gian);
            prkType.SelectedIndex = Array.IndexOf(loaihinh, lichKham.loai_hinh);
            txtBacSi.Text = lichKham.bac_si;
            txtChuyenKhoa.Text = lichKham.chuyen_khoa;
        }

        private async void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string insertString = "http://www.webapibookingcare.somee.com/api/DataController/UpdateLichKham?id=" + GlobalLichKham.id+"&ngay="+prkNgay.Date.ToString("dd/MM/yyyy")+ "&thoi_gian=" + prkThoiGian.SelectedItem.ToString() + "&phong_kham=" + txtPhong.Text + "&loai_hinh=" + prkType.SelectedItem.ToString() + "&gia=" + Double.Parse(txtGia.Text);
            var subjectList = await httpClient.GetStringAsync(insertString);
            await DisplayAlert("Thông báo", "Cập nhật  lịch khám thành công", "Ok");
            App.Current.MainPage = new MainPage();
        }

        private  async void cmdDelete_Clicked(object sender, EventArgs e)
        {
            var hoi = await DisplayAlert("Xác nhận", "Bạn có muốn xóa lịch khám này?", "Yes", "No");
            if (hoi)
            {
                HttpClient httpClient = new HttpClient();
                var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/DeleteLichKham?id=" + GlobalLichKham.id);
                await DisplayAlert("Thông báo", "Xóa lịch khám thành công.", "Ok");
                App.Current.MainPage = new MainPage();
            }
        }
    }
}