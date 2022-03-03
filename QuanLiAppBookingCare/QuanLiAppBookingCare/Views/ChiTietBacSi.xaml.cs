using Newtonsoft.Json;
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
    public partial class ChiTietBacSi : ContentPage
    {
        int chuyenkhoa;
        BacSi BacSi;
        string tenChuyenKhoa;
        public ChiTietBacSi()
        {
            InitializeComponent();
        }
        public ChiTietBacSi(BacSi bacSi)
        {
            InitializeComponent();
            initHienThi(bacSi);
            BacSi = bacSi;
        }

        private async void initHienThi(BacSi bacSi)
        {
            txtTen.Text = bacSi.ho_ten;
            string[] ngay = bacSi.ngay_sinh.Split('/');
            string ngayconverted = ngay.ElementAt(1) + "/" + ngay.ElementAt(0) + "/" + ngay.ElementAt(2);
            prkNgaySinh.Date = DateTime.Parse(ngayconverted);
            string[] gioitinh = new string[] { "Nam", "Nữ" };
            prkGioiTinh.ItemsSource = gioitinh;
            if (bacSi.gioi_tinh == "Nam")
                prkGioiTinh.SelectedIndex = 0;
            else 
                prkGioiTinh.SelectedIndex = 1;
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetChuyenKhoa");
            var subjectListConverted = JsonConvert.DeserializeObject<List<ChuyenKhoa>>(subjectList);
            prkChuyenKhoa.ItemsSource = subjectListConverted;
            foreach (ChuyenKhoa ck in subjectListConverted)
            {
                if (ck.ten_chuyen_khoa==bacSi.chuyen_khoa)
                {
                    chuyenkhoa = ck.id - 1;
                    break;
                }    
            }
            prkChuyenKhoa.SelectedIndex = chuyenkhoa;
            txtEmail.Text = bacSi.email;
            txtID.Text = bacSi.id_zoom;
            txtPassword.Text = bacSi.pass_zoom;
        }

        private async void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/UpdateBacSi?id=" + BacSi.id+"&ho_ten=" + txtTen.Text + "&gioi_tinh=" + prkGioiTinh.SelectedItem.ToString() + "&ngay_sinh=" + prkNgaySinh.Date.ToString("dd/MM/yyyy") + "&chuyen_khoa=" + tenChuyenKhoa + "&email=" + txtEmail.Text + "&id_zoom=" + txtID.Text + "&pass_zoom=" + txtPassword.Text);
            await DisplayAlert("Thông báo", "Cập nhập bác sĩ thành công", "Ok");
            App.Current.MainPage = new MainPage();
        }

        private async void cmdDelete_Clicked(object sender, EventArgs e)
        {
            var hoi = await DisplayAlert("Xác nhận", "Bạn có muốn xóa bác sĩ này?", "Yes", "No");
            if (hoi)
            {
                HttpClient httpClient = new HttpClient();
                var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/DeleteBacSi?id=" + BacSi.id);
                await DisplayAlert("Thông báo", "Xóa bác sĩ thành công.","Ok");
                App.Current.MainPage = new MainPage();
            }
        }

        private void prkChuyenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int indexSelected = picker.SelectedIndex;
            if (indexSelected >= 0)
            {
                tenChuyenKhoa = picker.Items[indexSelected].ToString();
            }
        }
    }
}